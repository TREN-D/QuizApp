using AutoMapper;
using BLL.Interfaces;
using BLL.Models.AnswerModels;
using DAL.Entities;
using DAL.Interfaces.Repositories;
using DAL.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;

        public AnswerService(IAnswerRepository answerRepository,
                            IQuestionRepository questionRepository,
                            IOptionRepository optionRepository)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
        }

        public async Task<Answer> UpsertAnswer(UpsertAnswerModel createAnswerModel, CancellationToken cancellationToken)
        {
            await CheckIfPrincipalEntitiesExist(createAnswerModel, cancellationToken);
            var existingAnswer = await _answerRepository.Get(track: true)
                                                            .SingleOrDefaultAsync(a => a.OptionId == createAnswerModel.OptionId
                                                                        && a.QuestionId == createAnswerModel.QuestionId
                                                                        && a.TestResultId == createAnswerModel.TestResultId,
                                                                        cancellationToken);

            Answer answer = null;
            if (existingAnswer != null)
            {
                Mapper.Map(createAnswerModel, existingAnswer);
                answer = await _answerRepository.UpdateAsync(existingAnswer, cancellationToken);
            }
            else
            {
                answer = Mapper.Map<UpsertAnswerModel, Answer>(createAnswerModel);
            }
            return answer;
        }

        /// <summary>
        /// Check if question and option exist while user upsert answer
        /// and throw if any of them isn't
        /// </summary>
        /// <exception cref="DAL.Shared.NotFoundException"></exception>
        private async Task CheckIfPrincipalEntitiesExist(UpsertAnswerModel createAnswerModel, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetAsync(createAnswerModel.QuestionId, cancellationToken);
            if(createAnswerModel.OptionId != null)
            {
                await _optionRepository.GetAsync(createAnswerModel.OptionId.Value, cancellationToken);
            }
        }

        public async Task ClearUncheckedAnswersAsync(ICollection<Answer> answers, CancellationToken cancellationToken)
        {
            await _answerRepository.DeleteAsync(answers, cancellationToken);
        }
    }
}
