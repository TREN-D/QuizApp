using BLL.Interfaces;
using BLL.Models.OptionModels;
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
    [RoutePrefix("api/option")]
    public class OptionController : ApiController
    {
        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpPost]
        [ResponseType(typeof(OptionModel))]
        public async Task<IHttpActionResult> CreateOption(CreateOptionModel createOptionModel, CancellationToken cancellationToken)
        {
            if(createOptionModel == null)
            {
                return BadRequest("Invalid createOption model");
            }

            var optionModel = await _optionService.CreateAsync(createOptionModel, cancellationToken);
            if (optionModel != null)
            {
                return Ok(optionModel);
            }

            return BadRequest();
        }

        // Content-Type for request: application/json-patch+json
        // Request Body example: [{"op": "replace", "path": "/PropertyName", "value": "PropertyReplaceValue"}]
        [HttpPatch]
        [Route("{id}")]
        [ResponseType(typeof(OptionModel))]
        public async Task<IHttpActionResult> UpdateOption(int id, JsonPatchDocument<PatchOptionModel> patchDocument, CancellationToken cancellationToken)
        {
            var optionModel = await _optionService.UpdateAsync(id, patchDocument, cancellationToken);

            if (optionModel != null)
            {
                return Ok(optionModel);
            }

            return BadRequest();
        }

        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var optionModel = await _optionService.DeleteAsync(id, cancellationToken);
            if (optionModel != null)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}