using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Answer : IGenericBaseEntity<int>
    {
        public int Id { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }

        [Required]
        [ForeignKey("TestResultId")]
        public virtual TestResult TestResult { get; set; }
        public int TestResultId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
        public int? QuestionId { get; set; }

        [ForeignKey("OptionId")]
        public virtual Option Option { get; set; }
        public int? OptionId { get; set; }
    }
}
