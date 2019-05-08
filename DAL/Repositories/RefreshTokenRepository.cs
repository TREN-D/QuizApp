using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken, string>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IApplicationDbContext context)
            : base(context)
        {

        }
    }
}
