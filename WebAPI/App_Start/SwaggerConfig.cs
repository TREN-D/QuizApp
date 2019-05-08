using System.Web.Http;
using WebActivatorEx;
using WebAPI;
using Swashbuckle.Application;
using WebAPI.Filters;

namespace WebAPI
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Quiz API");
                    c.PrettyPrint();
                    c.OperationFilter<AddHeaderParameterFilter>();
                })
                .EnableSwaggerUi();
       
        }
    }
}
