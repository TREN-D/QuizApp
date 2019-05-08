using DAL.Entities;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AnswerRepository : GenericRepository<Answer, int>, IAnswerRepository
    {
        public AnswerRepository(IApplicationDbContext context)
            : base(context)
        {

        }
    }
}