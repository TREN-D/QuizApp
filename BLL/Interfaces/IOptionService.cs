using BLL.Models.OptionModels;
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
    public interface IOptionService
    {
        Task<OptionModel> CreateAsync(CreateOptionModel createOptionModel, CancellationToken cancellationToken);
        Task<OptionModel> UpdateAsync(int id, JsonPatchDocument<PatchOptionModel> patchDocument, CancellationToken cancellationToken);
        Task<OptionModel> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
