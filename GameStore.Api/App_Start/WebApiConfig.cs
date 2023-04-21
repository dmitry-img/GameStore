using GameStore.Api.Filters;
using log4net;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace GameStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityConfig.RegisterComponents(config);

            // Global Attributes
            config.Filters.Add(new LogIpFilterAttribute());
            config.Filters.Add(new LogPerformanceFilterAttribute());
            config.Filters.Add(new GeneralExceptionFilterAttribute());

            // Cors
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
