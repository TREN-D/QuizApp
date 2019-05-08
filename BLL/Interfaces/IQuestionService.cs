using BLL.Models.QuestionModels;
using DAL.Entities;
using JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IQuestionService
    {
        Task<QuestionModel> CreateAsync(CreateQuestionModel createQuestionModel, CancellationToken cancellationToken);
        Task<QuestionModel> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<QuestionModel> UpdateAsync(int id, JsonPatchDocument<PatchQuestionModel> patchDocument, CancellationToken cancellationToken);
    }
}
