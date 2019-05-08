using BLL.Models.AnswerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.TestResultModels
{
    public class TestResultModel
    {
        public int Id { get; set; }
        public int DiffScore { get; set; }
        public int TimeTestPassed { get; set; }
        public string UserName { get; set; }

        public int UrlId { get; set; }

        public ICollection<AnswerModel> Answers { get; set; }
    }
}
