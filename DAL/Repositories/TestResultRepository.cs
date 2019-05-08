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
    public class TestResultRepository : GenericRepository<TestResult, int>, ITestResultRepository
    {
        public TestResultRepository(IApplicationDbContext context)
            : base(context)
        {

        }
    }
}
