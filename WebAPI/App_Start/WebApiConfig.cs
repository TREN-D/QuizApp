using Autofac.Integration.WebApi;
using JsonPatch.Formatting;
using Newtonsoft.Json.Serialization;
using NLog;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using WebAPI.App_Start;
using WebAPI.ExceptionHandlers;
using WebAPI.Filters;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            // Web API configuration and services

            // Add filter for model error handling
            config.Filters.Add(new ValidateModelFilter());
            config.Filters.Add(new AuthorizeAttribute());

            // Enable CORS for the Angular App
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Replace GlobalExceptionHandler for catching unhandled exceptions from BLL and DAL
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            
            // Set JSON formatter as default one and remove XmlFormatter
            var jsonFormatter = config.Formatters.JsonFormatter;
            // Comment for angular model binding with PascalCase properties.
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            // Add JsonPatchFormatte to bind request
            config.Formatters.Add(new JsonPatchFormatter());

            // Create Dependency Injection Container with autofac and config it 
            var container = AutofacConfig.CreateContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
