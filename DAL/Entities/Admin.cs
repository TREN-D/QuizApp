using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Admin : IGenericBaseEntity<int>
    {
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string Password { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Phone { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<URL> URLs { get; set; }
    }
}
