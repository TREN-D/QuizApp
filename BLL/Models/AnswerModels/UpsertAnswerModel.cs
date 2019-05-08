using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.AnswerModels
{
    public class UpsertAnswerModel
    {
        public int TestResultId { get; set; }
        public string UserAnswer { get; set; }
        public int QuestionId { get; set; }
        public int? OptionId { get; set; }
    }
}
