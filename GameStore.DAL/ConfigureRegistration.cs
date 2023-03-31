using GameStore.DAL.Data;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace GameStore.DAL
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterDALTypes(this IUnityContainer container)
        {
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), new HierarchicalLifetimeManager());
            container.RegisterType<IGameRepository, GameRepository>();
            container.RegisterType<ICommentRepository, CommentRepository>();
            container.RegisterType<IGameGenreRepository, GameGenreRepository>();
            container.RegisterType<IGamePlatformTypeRepository, GamePlatformTypeRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<GameStoreDbContext>(new PerResolveLifetimeManager());

            return container;
        }
    }
}
