using DAL.Interfaces;
using DAL.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Question : IGenericBaseEntity<int>
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int ScoreForAnswer { get; set; }
        public QuestionTypes QuestionType { get; set; }

        [Required]
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }
        public int TestId { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
