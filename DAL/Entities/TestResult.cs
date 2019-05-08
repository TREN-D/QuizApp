using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class TestResult : IGenericBaseEntity<int>
    {
        [Key, ForeignKey("URL")]
        public int Id { get; set; }
        public int DiffScore { get; set; }
        public int TimeTestPassed { get; set; }
        public DateTime StartTest { get; set; }
        public DateTime? FinishTest { get; set; }
        public bool IsFinished { get; set; }
        public string UserName { get; set; }
        [Required]
        public virtual URL URL { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
