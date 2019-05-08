using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Test : IGenericBaseEntity<int>
    {
        public int Id { get; set; }
        public int? TestTime { get; set; }
        public string TestDescription { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Admin CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public virtual ICollection<URL> URLs { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
