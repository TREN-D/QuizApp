using BLL.Interfaces;
using BLL.Models.AdminModels;
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
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAdminService _adminService;
        public AuthController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Register(CreateAdminModel createAdminModel, CancellationToken cancellationToken)
        {
            var result = await _adminService.CreateAsync(createAdminModel, cancellationToken);
            if(result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("This email is already exist");
            }
        }
    }
}
