using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using GameStore.DAL;
using GameStore.BLL;

namespace GameStore.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            container.RegisterDALTypes();
            container.RegisterBLLTypes();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}