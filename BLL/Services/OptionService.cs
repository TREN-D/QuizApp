using AutoMapper;
using BLL.Interfaces;
using BLL.Models.OptionModels;
using DAL.Entities;
using DAL.Interfaces.Repositories;
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

namespace BLL.Services
{
    public class OptionService : IOptionService
    {
        private readonly ILogger _logger;
        private readonly IOptionRepository _optionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestService _testService;

        public OptionService(ILogger logger,
                                IOptionRepository optionRepository,
                                IQuestionRepository questionRepository,
                                ITestService testService)
        {
            _logger = logger;
            _optionRepository = optionRepository;
            _questionRepository = questionRepository;
            _testService = testService;
        }

        public async Task<OptionModel> CreateAsync(CreateOptionModel createOptionModel, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetAsync(createOptionModel.QuestionId, cancellationToken);

            _testService.ClearTestUrls(question.Test);

            var option = Mapper.Map<CreateOptionModel, Option>(createOptionModel);

            option = await _optionRepository.AddAsync(option, cancellationToken);
            var optionModel = Mapper.Map<Option, OptionModel>(option);
            return optionModel;
        }

        public async Task<OptionModel> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            OptionModel optionModel = null;
            var test = await _optionRepository.GetAsync(id, cancellationToken)
                                                                 .ContinueWith(tq => tq.Exception == null ? tq.Result?.Question.Test : null);

            var option = await _optionRepository.DeleteAsync(id, cancellationToken);

            if (option == null)
            {
                return null;
            }
            _testService.ClearTestUrls(test);

            await _optionRepository.SaveChangesAsync(cancellationToken);

            optionModel = Mapper.Map<Option, OptionModel>(option);
            return optionModel;
        }

        public async Task<OptionModel> UpdateAsync(int id, JsonPatchDocument<PatchOptionModel> patchDocument, CancellationToken cancellationToken)
        {
            var option = await _optionRepository.GetAsync(id, cancellationToken);

            var patchOptionModel = Mapper.Map<Option, PatchOptionModel>(option);

            patchDocument.ApplyUpdatesTo(patchOptionModel);

            Mapper.Map(patchOptionModel, option);

            try
            {
                _testService.ClearTestUrls(option.Question.Test);
                option = await _optionRepository.UpdateAsync(option, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(ex);
                throw ex;
            }
            var optionModel = Mapper.Map<Option, OptionModel>(option);

            return optionModel;
        }
    }
}
