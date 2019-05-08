using BLL.Interfaces;
using BLL.Models.AnswerModels;
using BLL.Models.TestResultModels;
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
    [AllowAnonymous]
    [RoutePrefix("api/testResult")]
    public class TestResultController : ApiController
    {
        private readonly ITestResultService _testResultService;
        public TestResultController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CreateTestResult(CreateTestResultModel createTestResultModel, CancellationToken cancellationToken)
        {
            if (createTestResultModel == null)
            {
                return BadRequest("Invalid model to create test result");
            }

            var isValid = await _testResultService.CreateAsync(createTestResultModel, cancellationToken);
            if (isValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("upsertAnswers")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpsertAnswersInTestResult(UpsertAnswerModel[] upsertAnswerModels, CancellationToken cancellationToken)
        {
            if (upsertAnswerModels == null || upsertAnswerModels.Count() < 0)
            {
                return BadRequest("Invalid upsert answer model");
            }
            await _testResultService.AddAnswersAsync(upsertAnswerModels, cancellationToken);
            return Ok();
        }

        [HttpPost]
        [Route("finishTest")]
        [ResponseType(typeof(TestResultModel))]
        public async Task<IHttpActionResult> FinishTestResult([FromBody] int testResultId, CancellationToken cancellationToken)
        {
            if (testResultId <= 0)
            {
                return BadRequest("Invalid test result identifier");
            }

            var testResultModel = await _testResultService.FinishTestAsync(testResultId, cancellationToken);
            if (testResultModel != null)
            {
                return Ok(testResultModel);
            }
            return BadRequest();
        }
    }
}
