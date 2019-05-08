using BLL.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.TestModels
{
    public class TestModel
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public int? TestTime { get; set; }
        public string TestDescription { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
