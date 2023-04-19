using GameStore.Api.Filters;
using log4net;
using System.Web.Http;
using Unity;

namespace GameStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityConfig.RegisterComponents(config);
            config.Filters.Add(new LogIpFilterAttribute());
            config.Filters.Add(new LogPerformanceFilterAttribute());
            config.Filters.Add(new GeneralExceptionFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
