using System.Web.Http;
using GameStore.Api.Infrastructure;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Infrastructure;
using Unity;
using Unity.WebApi;

namespace GameStore.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterApiTypes();
            container.RegisterDALTypes();
            container.RegisterBLLTypes();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
