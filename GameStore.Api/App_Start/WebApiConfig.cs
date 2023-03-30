using AutoMapper;
using GameStore.Api.Models;
using GameStore.BLL;
using GameStore.BLL.Profiles;
using GameStore.DAL;
using System.Web.Http;
using Unity;

namespace GameStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterDALTypes();
            container.RegisterBLLTypes();

            config.DependencyResolver = new UnityResolver(container);


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
