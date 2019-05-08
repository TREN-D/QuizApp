using BLL.Interfaces;
using BLL.Models.Shared;
using BLL.Models.TestModels;
using BLL.Models.URLModels;
using DAL.Entities;
using JsonPatch;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private readonly ITestService _testService;
        private readonly IAuthService _authService;

        public TestController(ITestService testService,
                                IAuthService authService)
        {
            _testService = testService;
            _authService = authService;
        }

        [ResponseType(typeof(ICollection<TestModel>))]
        public async Task<IHttpActionResult> Get(CancellationToken cancellationToken)
        {
            var tests = await _testService.GetAsync(cancellationToken);
            return Ok(tests);
        }

        [Route("admin/{adminId}")]
        [ResponseType(typeof(ICollection<TestModel>))]
        public async Task<IHttpActionResult> GetAdminTests(int adminId, CancellationToken cancellationToken)
        {
            var tests = await _testService.GetAdminTestsAsync(adminId, cancellationToken);
            return Ok(tests);
        }

        [Route("{id}")]
        [AllowAnonymous]
        [ResponseType(typeof(TestModel))]
        public async Task<IHttpActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var test = await _testService.GetAsync(id, cancellationToken);
            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        [Route("clean/{id}")]
        [AllowAnonymous]
        [ResponseType(typeof(TestModel))]
        public async Task<IHttpActionResult> GetClean(int id, CancellationToken cancellationToken)
        {
            var test = await _testService.GetCleanAsync(id, cancellationToken);
            if (test == null)
            {
                return NotFound();
            }
            
            return Ok(test);
        }

        [Route("admin/own")]
        [ResponseType(typeof(TestModel))]
        public async Task<IHttpActionResult> GetOwnTests([FromUri] PaginationModel paginationModel, CancellationToken cancellationToken)
        {
            var adminId = _authService.GetPrincipalId(User);

            var tests = await _testService.GetAdminTestsAsync(adminId, cancellationToken, paginationModel);
            return Ok(tests);
        }

        [HttpGet]
        [Route("admin/{adminId}/count")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetAdminTestsCount(int adminId, CancellationToken cancellationToken)
        {
            var amount = await _testService.GetTestsAmountAsync(adminId, cancellationToken);
            return Ok(amount);
        }

        [HttpGet]
        [Route("admin/own/count")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetOwnTestsCount(CancellationToken cancellationToken)
        {
            var adminId = _authService.GetPrincipalId(User);

            var amount = await _testService.GetTestsAmountAsync(adminId, cancellationToken);
            return Ok(amount);
        }

        [HttpPost]
        [ResponseType(typeof(TestModel))]
        public async Task<IHttpActionResult> CreateTest(CreateTestModel createdTestModel, CancellationToken cancellationToken)
        {
            if (createdTestModel == null)
            {
                return BadRequest("Invalid model");
            }
            createdTestModel.CreatedById = _authService.GetPrincipalId(User);

            var testModel = await _testService.CreateAsync(createdTestModel, cancellationToken);
            if (testModel != null)
            {
                return Ok(testModel);
            }
            return BadRequest();
        }

        // Content-Type for request: application/json-patch+json
        // Request Body example: [{"op": "replace", "path": "/PropertyName", "value": "PropertyReplaceValue"}]
        [HttpPatch]
        [Route("{id}")]
        [ResponseType(typeof(TestModel))]
        public async Task<IHttpActionResult> UpdateTest(int id, JsonPatchDocument<PatchTestModel> patchDocument, CancellationToken cancellationToken)
        {
            var testModel = await _testService.UpdateAsync(id, patchDocument, cancellationToken);

            if (testModel != null)
            {
                return Ok(testModel);
            }
            return BadRequest();
        }

        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _testService.DeleteAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}