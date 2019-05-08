using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.TestModels
{
    public class CreateTestModel
    {
        [IgnoreDataMember]
        public int CreatedById { get; set; }
        public string TestDescription { get; set; }
    }
}
