using DAL.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.QuestionModels
{
    public class PatchQuestionModel
    {
        public string QuestionText { get; set; }
        public int ScoreForAnswer { get; set; }
        public QuestionTypes QuestionType { get; set; }
    }
}
