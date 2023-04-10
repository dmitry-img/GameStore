using GameStore.DAL.Data;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using Unity;
using Unity.Lifetime;

namespace GameStore.DAL.Infrastructure
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterDALTypes(this IUnityContainer container)
        {
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), new HierarchicalLifetimeManager());
            container.RegisterType<IGameRepository, GameRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<GameStoreDbContext>(new PerResolveLifetimeManager());

            return container;
        }
    }
}
