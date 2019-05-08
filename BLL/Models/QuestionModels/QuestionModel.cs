using BLL.Models.OptionModels;
using DAL.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.QuestionModels
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public int ScoreForAnswer { get; set; }
        public QuestionTypes QuestionType { get; set; }

        public ICollection<OptionModel> Options { get; set; }
    }
}
