using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Option : IGenericBaseEntity<int>
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
        public int? QuestionId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
