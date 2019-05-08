using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.App_Start
{
    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly HttpConfiguration _configuration;

        public ApplicationRefreshTokenProvider(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientId = context.Ticket.Properties.Dictionary["as:client_id"];
            if (string.IsNullOrEmpty(clientId))
            {
                return;
            }
            var refreshTokentId = Guid.NewGuid().ToString("n");

            using (var scope = _configuration.DependencyResolver.BeginScope())
            {
                var refreshTokenService = (IAuthService)scope.GetService(typeof(IAuthService));

                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken()
                {
                    // TODO: Hash that id
                    Id = refreshTokentId,
                    ClientId = clientId,
                    UserEmail = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToInt32(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();
                var result = await refreshTokenService.CreateTokenAsync(token, context.Request.CallCancelled);

                if (result != null)
                {
                    context.SetToken(refreshTokentId);
                }
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Set("Access-Control-Allow-Origin", allowedOrigin);

            // TODO: hash context.Token
            string hashedTokenId = context.Token;

            using (var scope = _configuration.DependencyResolver.BeginScope())
            {
                var refreshTokenService = (IAuthService)scope.GetService(typeof(IAuthService));

                var refreshToken = await refreshTokenService.FindTokenAsync(hashedTokenId, context.Request.CallCancelled);

                if (refreshToken != null && refreshToken.ExpiresUtc > DateTime.UtcNow)
                {
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = await refreshTokenService.DeleteTokenAsync(hashedTokenId, context.Request.CallCancelled);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }
    }
}