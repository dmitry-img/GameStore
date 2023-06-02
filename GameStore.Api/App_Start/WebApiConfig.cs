using FluentValidation.WebApi;
using GameStore.Api.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GameStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityConfig.RegisterComponents(config);

            // Global Attributes
            config.Filters.Add(new JwtAuthenticationFilterAttribute());
            config.Filters.Add(new LogIpFilterAttribute());
            config.Filters.Add(new LogPerformanceFilterAttribute());
            config.Filters.Add(new GeneralExceptionFilterAttribute());

            // Cors
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Fluent Validation
            FluentValidationModelValidatorProvider.Configure(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
