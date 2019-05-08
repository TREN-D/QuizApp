using BLL.Models.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        Task<bool> CreateAsync(CreateAdminModel createAdminModel, CancellationToken cancellationToken);
        Task<AdminModel> GetByCredentials(string email, string password, CancellationToken cancellationToken);
    }
}
