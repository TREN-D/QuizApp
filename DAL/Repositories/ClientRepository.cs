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
    public class ClientRepository : GenericRepository<Client, string>, IClientRepository
    {
        public ClientRepository(IApplicationDbContext context)
            : base(context)
        {

        }
    }
}
