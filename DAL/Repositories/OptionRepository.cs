using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OptionRepository : GenericRepository<Option, int>, IOptionRepository
    {
        public OptionRepository(IApplicationDbContext context)
            : base(context)
        {

        }
    }
}
