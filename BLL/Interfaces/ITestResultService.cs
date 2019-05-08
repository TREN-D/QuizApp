using BLL.Models.AnswerModels;
using BLL.Models.TestResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestResultService
    {
        Task<bool> CreateAsync(CreateTestResultModel createTestResultModel, CancellationToken cancellationToken);
        Task<TestResultModel> FinishTestAsync(int id, CancellationToken cancellationToken);
        Task AddAnswersAsync(UpsertAnswerModel[] upsertAnswerModels, CancellationToken cancellationToken);
    }
}
