using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces.Repositories
{
    public interface IUrlRepository : IGenericRepository<URL, int>
    {
        Task<URL> GetAsync(string identifier, CancellationToken cancellationToken);
    }
}
