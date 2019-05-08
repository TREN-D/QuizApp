using BLL.Models.Shared;
using BLL.Models.TestModels;
using BLL.Models.URLModels;
using DAL.Entities;
using JsonPatch;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestService
    {
        Task<TestModel> CreateAsync(CreateTestModel createTestModel, CancellationToken cancellationToken);
        Task<TestModel> UpdateAsync(int id, JsonPatchDocument<PatchTestModel> patchDocument, CancellationToken cancellationToken);
        Task<TestModel> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<TestModel> GetAsync(int id, CancellationToken cancellationToken);
        Task<TestModel> GetCleanAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<TestModel>> GetAdminTestsAsync(int adminId, CancellationToken cancellationToken, PaginationModel paginationModel = null);
        Task<ICollection<TestModel>> GetAsync(CancellationToken cancellationToken);
        Task<int> GetTestsAmountAsync(int adminId, CancellationToken cancellationToken);
        void ClearTestUrls(Test test);
    }
}
