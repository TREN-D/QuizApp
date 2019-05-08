using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Shared
{
    public class EntityModelException : Exception
    {
        public EntityModelException()
        {

        }

        public EntityModelException(string message)
            : base(message)
        {

        }
    }
}
