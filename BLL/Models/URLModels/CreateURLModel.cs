using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.URLModels
{
    public class CreateURLModel
    {
        [IgnoreDataMember]
        public int CreatedById { get; set; }
        public int TestId { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTill { get; set; }
        public string UserName { get; set; }
        public int? NumberOfRuns { get; set; }
    }
}
