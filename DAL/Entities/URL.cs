using DAL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class URL : IGenericBaseEntity<int>
    {
        public int Id { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTill { get; set; }
        public string UserName { get; set; }
        public int? NumberOfRuns { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Identifier { get; set; }
        
        [ForeignKey("CreatedById")]
        public virtual Admin CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }
        public int? TestId { get; set; }
    }
}
