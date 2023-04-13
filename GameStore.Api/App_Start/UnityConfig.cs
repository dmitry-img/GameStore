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
        public static void RegisterComponents(HttpConfiguration config, UnityContainer container)
        {
            container.RegisterDALTypes();
            container.RegisterBLLTypes();
            container.RegisterApiTypes();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
