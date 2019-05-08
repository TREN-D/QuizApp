using BLL.Interfaces;
using BLL.Models.URLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/url")]
    public class URLController : ApiController
    {
        private readonly IUrlService _urlService;
        private readonly IAuthService _authService;

        public URLController(IUrlService urlService,
                                        IAuthService authService)
        {
            _urlService = urlService;
            _authService = authService;
        }

        [HttpPost]
        [ResponseType(typeof(URLModel))]
        public async Task<IHttpActionResult> CreateUrl(CreateURLModel createUrlModel, CancellationToken cancellationToken)
        {
            if(createUrlModel == null)
            {
                return BadRequest("Invalid model");
            }

            createUrlModel.CreatedById = _authService.GetPrincipalId(User);

            var urlModel = await _urlService.CreateAsync(createUrlModel, cancellationToken);

            if (urlModel!= null)
            {
                return Ok(urlModel);
            }
            return BadRequest();
        }

        [Route("{identifier}")]
        [AllowAnonymous]
        [ResponseType(typeof(URLModel))]
        public async Task<IHttpActionResult> GetByIdentifier(string identifier, CancellationToken cancellationToken)
        {
            var urlModel = await _urlService.GetAsync(identifier, cancellationToken);
            if (urlModel != null)
            {
                return Ok(urlModel);
            }
            return NotFound();
        }
    }
}
