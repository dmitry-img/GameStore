using GameStore.DAL.Data;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using System.Data.Entity;
using Unity;

namespace GameStore.DAL
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterDALTypes(this IUnityContainer container)
        {
            container.RegisterType<IGameRepository, GameRepository>();
            container.RegisterType<ICommentRepository, CommentRepository>();
            container.RegisterType<IGameGenreRepository, GameGenreRepository>();
            container.RegisterType<IGamePlatformTypeRepository, GamePlatformTypeRepository>();
            container.RegisterType<DbContext, GameStoreDbContext>();

            return container;
        }
    }
}
