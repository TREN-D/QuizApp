using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interfaces;
using BLL.Models.Shared;
using BLL.Models.TestModels;
using BLL.Services.Shared;
using DAL.Entities;
using DAL.Interfaces.Repositories;
using JsonPatch;
using NLog;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestService : ITestService
    {
        private readonly ILogger _logger;
        private readonly ITestRepository _testRepository;
        private readonly IAdminRepository _adminRepository;

        public TestService(ITestRepository testRepository,
                            ILogger logger,
                            IAdminRepository adminRepository)
        {
            _logger = logger;
            _testRepository = testRepository;
            _adminRepository = adminRepository;
        }

        public async Task<TestModel> CreateAsync(CreateTestModel createTestModel, CancellationToken cancellationToken)
        {
            await _adminRepository.GetAsync(createTestModel.CreatedById, cancellationToken);

            var test = Mapper.Map<CreateTestModel, Test>(createTestModel);

            test = await _testRepository.AddAsync(test, cancellationToken);

            var testModel = Mapper.Map<Test, TestModel>(test);
            return testModel;
        }

        public async Task<ICollection<TestModel>> GetAsync(CancellationToken cancellationToken)
        {
            var testModels = await _testRepository.Get()
                .ProjectTo<TestModel>()
                .ToListAsync(cancellationToken);

            return testModels;
        }

        public async Task<ICollection<TestModel>> GetAdminTestsAsync(int adminId, CancellationToken cancellationToken, PaginationModel paginationModel = null)
        {
            var tests = _testRepository.Get()
                .Where(t => t.CreatedById == adminId);

            if(paginationModel != null)
            {
                tests = tests.GetPaginatedTestsById(paginationModel);
            }
            var testModels = await tests.ProjectTo<TestModel>()
                                            .ToListAsync(cancellationToken);

            return testModels;
        }

        public async Task<int> GetTestsAmountAsync(int adminId, CancellationToken cancellationToken)
        {
            var amount = await _testRepository.Get()
               .Where(t => t.CreatedById == adminId).CountAsync(cancellationToken);

            return amount;
        }

        public async Task<TestModel> GetAsync(int id, CancellationToken cancellationToken)
        {
            var test = await _testRepository.GetAsync(id, cancellationToken);
            var testModel = Mapper.Map<Test, TestModel>(test);
            return testModel;
        }

        public async Task<TestModel> GetCleanAsync(int id, CancellationToken cancellationToken)
        {
            var test = await GetAsync(id, cancellationToken);
            if(test != null)
            {
                foreach (var question in test.Questions)
                {
                    foreach (var option in question.Options)
                    {
                        option.IsCorrect = false;
                    }
                }
            }
            return test;
        }

        public async Task<TestModel> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var test = await _testRepository.DeleteAsync(id, cancellationToken);
            var testModel = Mapper.Map<Test, TestModel>(test);
            return testModel;
        }

        public async Task<TestModel> UpdateAsync(int id, JsonPatchDocument<PatchTestModel> patchDocument, CancellationToken cancellationToken)
        {
            var test = await _testRepository.GetAsync(id, cancellationToken);

            var patchTestModel = Mapper.Map<Test, PatchTestModel>(test);

            patchDocument.ApplyUpdatesTo(patchTestModel);

            Mapper.Map(patchTestModel, test);

            try
            {
                ClearTestUrls(test);
                test = await _testRepository.UpdateAsync(test, cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(ex.Message);
                throw ex;
            }
            var testModel = Mapper.Map<Test, TestModel>(test);

            return testModel;
        }

        public void ClearTestUrls(Test test)
        {
            test.URLs?.Clear();
        }
    }
}
