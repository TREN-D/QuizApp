using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.URLModels
{
    public class URLModel
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public int? TestId { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTill { get; set; }
        public string UserName { get; set; }
        public int? NumberOfRuns { get; set; }
        public string Identifier { get; set; }
    }
}
