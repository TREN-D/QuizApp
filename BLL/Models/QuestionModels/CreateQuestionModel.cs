using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.QuestionModels
{
    public class CreateQuestionModel
    {
        public int TestId { get; set; }
        public string QuestionText { get; set; }
    }
}
