using BLL.Interfaces;
using BLL.Models.QuestionModels;
using DAL.Entities;
using JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/question")]
    public class QuestionController : ApiController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        [ResponseType(typeof(QuestionModel))]
        public async Task<IHttpActionResult> AddQuestion(CreateQuestionModel createQuestionModel, CancellationToken cancellationToken)
        {
            if (createQuestionModel == null)
            {
                return BadRequest("Invalid createQuestion model");
            }
            var questionModel = await _questionService.CreateAsync(createQuestionModel, cancellationToken);
            if (questionModel != null)
            {
                return Ok(questionModel);
            }
            return BadRequest();
        }

        // Content-Type for request: application/json-patch+json
        // Request Body example: [{"op": "replace", "path": "/PropertyName", "value": "PropertyReplaceValue"}]
        [HttpPatch]
        [Route("{id}")]
        [ResponseType(typeof(QuestionModel))]
        public async Task<IHttpActionResult> UpdateQuestion(int id, JsonPatchDocument<PatchQuestionModel> patchDocument, CancellationToken cancellationToken)
        {
            var questionModel = await _questionService.UpdateAsync(id, patchDocument, cancellationToken);
            if (questionModel != null)
            {
                return Ok(questionModel);
            }

            return BadRequest();
        }

        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var questionModel = await _questionService.DeleteAsync(id, cancellationToken);
            if (questionModel != null)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
