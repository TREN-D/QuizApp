using BLL.Models.AnswerModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnswerService
    {
        /// <summary>
        /// Check if answer exist and return it, or create a new answer with 'zero' identifier and return it.
        /// </summary>
        Task<Answer> UpsertAnswer(UpsertAnswerModel createAnswerModel, CancellationToken cancellationToken);
        Task ClearUncheckedAnswersAsync(ICollection<Answer> answers, CancellationToken cancellationToken);
    }
}
