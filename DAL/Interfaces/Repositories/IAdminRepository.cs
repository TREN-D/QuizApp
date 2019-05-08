using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin, int>
    {
        Task<Admin> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
