using AutoMapper;
using BLL.Interfaces;
using BLL.Models.QuestionModels;
using DAL.Entities;
using DAL.Interfaces.Repositories;
using DAL.Shared;
using JsonPatch;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityModelException = DAL.Shared.EntityModelException;

namespace BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestRepository _testRepository;
        private readonly ITestService _testService;
        private readonly ILogger _logger;

        public QuestionService(IQuestionRepository questionRepository,
                                ILogger logger,
                                ITestRepository testRepository,
                                ITestService testService)
        {
            _questionRepository = questionRepository;
            _logger = logger;
            _testRepository = testRepository;
            _testService = testService;
        }

        public async Task<QuestionModel> CreateAsync(CreateQuestionModel createQuestionModel, CancellationToken cancellationToken)
        {
            var test = await _testRepository.GetAsync(createQuestionModel.TestId, cancellationToken);

            _testService.ClearTestUrls(test);

            var question = Mapper.Map<CreateQuestionModel, Question>(createQuestionModel);

            question = await _questionRepository.AddAsync(question, cancellationToken);
            var questionModel = Mapper.Map<Question, QuestionModel>(question);
            return questionModel;
        }

        public async Task<QuestionModel> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            QuestionModel questionModel = null;
            var question = await _questionRepository.GetAsync(id, cancellationToken);
            var test = question.Test;

            var removedQuestion = await _questionRepository.DeleteAsync(id, cancellationToken);
            if (removedQuestion == null)
            {
                return questionModel;
            }

            _testService.ClearTestUrls(test);
            await _testRepository.SaveChangesAsync(cancellationToken);

            questionModel = Mapper.Map<Question, QuestionModel>(removedQuestion);
            return questionModel;
        }

        public async Task<QuestionModel> UpdateAsync(int id, JsonPatchDocument<PatchQuestionModel> patchDocument, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetAsync(id, cancellationToken);

            var patchQuestionModel = Mapper.Map<Question, PatchQuestionModel>(question);

            patchDocument.ApplyUpdatesTo(patchQuestionModel);

            var isQuestionTypeValid = Enum.IsDefined(typeof(QuestionTypes), patchQuestionModel.QuestionType);
            if(!isQuestionTypeValid)
            {
                throw new EntityModelException("Gained not correct quetion type in update method");
            }

            Mapper.Map(patchQuestionModel, question);

            try
            {
                _testService.ClearTestUrls(question.Test);
                question = await _questionRepository.UpdateAsync(question, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(ex);
                throw ex;
            }
            var questionModel = Mapper.Map<Question, QuestionModel>(question);

            return questionModel;
        }
    }
}
