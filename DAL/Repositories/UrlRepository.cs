using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using DAL.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UrlRepository : GenericRepository<URL, int>, IUrlRepository
    {
        public UrlRepository(IApplicationDbContext context)
            : base(context)
        {

        }

        public async Task<URL> GetAsync(string identifier, CancellationToken cancellationToken)
        {
            try
            {
                return await Get(track: true).SingleAsync(u => u.Identifier.Equals(identifier), cancellationToken);
            }
            catch
            {
                throw new NotFoundException($"Can't find an url with identifier {identifier}");
            }
        }
    }
}
