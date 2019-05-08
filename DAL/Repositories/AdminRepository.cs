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
    public class AdminRepository : GenericRepository<Admin, int>, IAdminRepository
    {
        public AdminRepository(IApplicationDbContext context)
            : base(context)
        {

        }

        public async Task<Admin> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins
                                            .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);
            return admin;
        }
    }
}
