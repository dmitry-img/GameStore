using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using GameStore.DAL.Infrastructure;
using GameStore.BLL.Infrastructure;

namespace GameStore.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            container.RegisterDALTypes();
            container.RegisterBLLTypes();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}