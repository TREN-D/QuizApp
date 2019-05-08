using AutoMapper;
using BLL.Interfaces;
using BLL.Models.AnswerModels;
using BLL.Models.TestResultModels;
using DAL.Entities;
using DAL.Interfaces.Repositories;
using DAL.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _testResultRepository;
        private readonly IAnswerService _answerService;
        private readonly IUrlRepository _urlRepository;
        private readonly ILogger _logger;

        public TestResultService(ITestResultRepository testResultRepository,
                                 IUrlRepository urlRepository,
                                 IAnswerService answerService,
                                 ILogger logger)
        {
            _answerService = answerService;
            _testResultRepository = testResultRepository;
            _urlRepository = urlRepository;
            _logger = logger;
        }

        protected async Task<TestResultModel> CheckAnswersAsync(TestResultModel testResultModel, CancellationToken cancellationToken)
        {
            var groupedAnswers = testResultModel
                                    .Answers
                                    .GroupBy(a => a.QuestionId,
                                            a => a,
                                            (id, answers) =>
                                            new
                                            {
                                                QuestionId = id.Value,
                                                Answers = answers
                                            });

            var testQuestions = await GetTestQuestionsAsync(testResultModel.Id, cancellationToken);

            foreach (var groupedAnswer in groupedAnswers)
            {
                var testQuestion = testQuestions.First(q => q.Id == groupedAnswer.QuestionId);
                var answerModels = groupedAnswer.Answers;
                var isCorrect = false;
                switch (testQuestion.QuestionType)
                {
                    case QuestionTypes.Text:
                        {
                            var answerModel = answerModels.Single();
                            isCorrect = VerifyTextAnswer(answerModel, testQuestion);
                            break;
                        }
                    case QuestionTypes.Check:
                    case QuestionTypes.Radio:
                        {
                            isCorrect = VerifyAnswers(answerModels, testQuestion);
                            break;
                        }

                    default:
                        {
                            throw new EntityModelException("Can't find matching question type");
                        }
                }

                if (isCorrect)
                {
                    MarkAnswersAsCorrect(answerModels);
                    testResultModel.DiffScore += testQuestion.ScoreForAnswer;
                }
            }
            return testResultModel;
        }

        private async Task<ICollection<Question>> GetTestQuestionsAsync(int testResultId, CancellationToken cancellationToken)
        {
            var url = await _urlRepository.GetAsync(testResultId, cancellationToken);
            var questions = url.Test.Questions;
            return questions;
        }

        private void MarkAnswersAsCorrect(IEnumerable<AnswerModel> answers)
        {
            foreach (var answer in answers)
            {
                answer.IsCorrect = true;
            }
        }

        private bool VerifyTextAnswer(AnswerModel answerModel, Question question)
        {
            var result = false;
            var options = question.Options;

            if (options.Any(o => o.OptionText.Equals(answerModel.UserAnswer)))
            {
                result = true;
            }
            return result;
        }

        private bool VerifyAnswers(IEnumerable<AnswerModel> answerModels, Question question)
        {
            var result = false;
            var correctOptions = question.Options.Where(o => o.IsCorrect);
            if (answerModels.Count() == correctOptions.Count())
            {
                var correctAnswersCount = answerModels
                                            .Where(a => a.OptionId.HasValue)
                                            .Select(a => a.OptionId.Value)
                                            .Intersect(correctOptions.Select(o => o.Id))
                                            .Count();

                if (correctAnswersCount == correctOptions.Count())
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>Check if url is valid for starting test.</summary>
        /// <exception cref="DAL.Shared.EntityModelException">Thrown when url is invalid</exception>
        private void CheckIfCanCreateTestResult(URL url)
        {
            if (url.NumberOfRuns.HasValue && url.NumberOfRuns.Value <= 0)
            {
                throw new EntityModelException("Can't start a test because it has run out of attempts");
            }
            else if (url?.ActiveTill < DateTime.UtcNow)
            {
                throw new EntityModelException("Can't start a test because it's expired");
            }
            else if (url.Test == null)
            {
                throw new EntityModelException("Can't start a test because URL isn't pointing to any test");
            }
        }

        /// <summary>Check if url is valid for finishing test.</summary>
        /// <exception cref="DAL.Shared.EntityModelException">Thrown when url is invalid</exception>
        private void CheckIfCanFinishTestResult(URL url)
        {
            if (url?.ActiveTill < DateTime.UtcNow)
            {
                throw new EntityModelException("Can't finish a test because it's expired");
            }
            else if (url.Test == null)
            {
                throw new EntityModelException("Can't finish a test because URL isn't pointing to any test");
            }
        }

        public async Task<bool> CreateAsync(CreateTestResultModel createTestResultModel, CancellationToken cancellationToken)
        {
            var url = await _urlRepository.GetAsync(createTestResultModel.UrlId, cancellationToken);
            CheckIfCanCreateTestResult(url);

            url.NumberOfRuns--;
            await _testResultRepository.DeleteAsync(url.Id, cancellationToken);
            var testResult = new TestResult
            {
                URL = url,
                StartTest = DateTime.UtcNow,
                UserName = createTestResultModel.UserName
            };

            testResult = await _testResultRepository.AddAsync(testResult, cancellationToken);
            return testResult != null;
        }

        public async Task AddAnswersAsync(UpsertAnswerModel[] upsertAnswerModels, CancellationToken cancellationToken)
        {
            var testResult = await _testResultRepository.GetAsync(upsertAnswerModels.First().TestResultId, cancellationToken);
            var selectedQuestionIds = upsertAnswerModels.Select(a => a.QuestionId).Distinct();
            var answersToDelete = new List<Answer>();

            if (testResult.IsFinished)
            {
                throw new EntityModelException("Can't add answers to finished test result");
            }
            try
            {
                foreach (var upsertAnswerModel in upsertAnswerModels)
                {
                    var answer = await _answerService.UpsertAnswer(upsertAnswerModel, cancellationToken);
                    if (answer.Id == 0)
                    {
                        testResult.Answers.Add(answer);
                    }
                    answersToDelete.Add(answer);
                }
                testResult = await _testResultRepository.UpdateAsync(testResult, cancellationToken);
                answersToDelete = testResult.Answers.Where(a => selectedQuestionIds.Any(id => id == a.QuestionId)).Except(answersToDelete).ToList();
                await _answerService.ClearUncheckedAnswersAsync(answersToDelete, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task<TestResultModel> FinishTestAsync(int id, CancellationToken cancellationToken)
        {
            var testResult = await _testResultRepository.GetAsync(id, cancellationToken);
            var url = await _urlRepository.GetAsync(id, cancellationToken, track: false);

            if (testResult.IsFinished)
            {
                throw new EntityModelException("Can't end test, because it's already finished");
            }

            testResult.FinishTest = DateTime.UtcNow;
            var diffSpan = testResult.FinishTest.Value - testResult.StartTest;
            testResult.TimeTestPassed = Convert.ToInt32(diffSpan.TotalMinutes);

            var testResultModel = Mapper.Map<TestResult, TestResultModel>(testResult);

            CheckIfCanFinishTestResult(url);

            testResultModel = await CheckAnswersAsync(testResultModel, cancellationToken);

            foreach (var answerModel in testResultModel.Answers)
            {
                testResult.Answers.Single(a => a.Id == answerModel.Id).IsCorrect = answerModel.IsCorrect;
            }
            testResult.IsFinished = true;
            testResult.DiffScore = testResultModel.DiffScore;

            try
            {
                testResult = await _testResultRepository.UpdateAsync(testResult, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(ex.Message);
                throw ex;
            }
            Mapper.Map(testResult, testResultModel);

            return testResultModel;
        }
    }
}
