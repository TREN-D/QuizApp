using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Shared;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.App_Start
{
    public class ApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {
        private readonly HttpConfiguration _configuration;

        private const string INVALID_CLIENT_ID = "invalid_clientId";

        public ApplicationOAuthProvider(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            Client client = null;

            if (!context.TryGetBasicCredentials(out string _, out string _))
            {
                context.TryGetFormCredentials(out _, out _);
            }

            if (context.ClientId == null)
            {
                // Uncomment if client_id is optional
                // context.Validated();
                context.SetError(INVALID_CLIENT_ID, "ClientId should be sent.");
                return;
            }

            using (var scope = _configuration.DependencyResolver.BeginScope())
            {
                var refreshTokenService = (IAuthService)scope.GetService(typeof(IAuthService));
                client = await refreshTokenService.FindClientAsync(context.ClientId, context.Request.CallCancelled);
            }

            if (client == null)
            {
                context.SetError(INVALID_CLIENT_ID, $"Client {context.ClientId} is not registered in the system.");
                return;
            }

            if (!client.Active)
            {
                context.SetError(INVALID_CLIENT_ID, "Client is inactive.");
                return;
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", "*");
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";

            context.OwinContext.Response.Headers.Set("Access-Control-Allow-Origin", allowedOrigin);

            using (var scope = _configuration.DependencyResolver.BeginScope())
            {
                var adminService = (IAdminService)scope.GetService(typeof(IAdminService));
                var admin = await adminService.GetByCredentials(context.UserName, context.Password, context.Request.CallCancelled);
                if (admin != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, admin.Email));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Email, admin.Email));

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {
                            "as:client_id", context.ClientId ?? string.Empty
                        }
                    });
                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                }
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if(originalClient != currentClient)
            {
                context.SetError(INVALID_CLIENT_ID, "Refresh tokent is issued to a different clientId");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token request
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            // Add claims if needed
            // newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}