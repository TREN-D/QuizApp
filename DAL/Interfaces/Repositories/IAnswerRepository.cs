using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Repositories
{
    public interface IAnswerRepository : IGenericRepository<Answer, int>
    {
    }
}
