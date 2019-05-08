using System;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(WebAPI.App_Start.Startup))]

namespace WebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            AutomapperConfig.Initialize();

            HttpConfiguration configuration = new HttpConfiguration();

            SwaggerConfig.Register(configuration);

            WebApiConfig.Register(configuration);

            ConfigureOAuth(app, configuration);

            app.UseWebApi(configuration);
        }

        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration configuration)
        {
            OAuthAuthorizationServerOptions OAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                Provider = new ApplicationOAuthProvider(configuration),
                RefreshTokenProvider = new ApplicationRefreshTokenProvider(configuration)
            };

            app.UseOAuthAuthorizationServer(OAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
