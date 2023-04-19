using System.Web.Http;
using GameStore.Api;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace GameStore.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    c.SingleApiVersion("v1", "GameStore.Api"))
                .EnableSwaggerUi();
        }
    }
}
