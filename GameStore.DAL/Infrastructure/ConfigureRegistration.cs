using System.Configuration;
using GameStore.DAL.Data;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using StackExchange.Redis;
using Unity;
using Unity.Lifetime;

namespace GameStore.DAL.Infrastructure
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterDALTypes(this IUnityContainer container)
        {
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            container.RegisterType<IGameRepository, GameRepository>();
            container.RegisterType(typeof(IDistributedCache<>), typeof(RedisCacheRepository<>));
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<GameStoreDbContext>(new HierarchicalLifetimeManager());

            var redisConnectionString = ConfigurationManager.AppSettings["RedisConnectionString"];
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            container.RegisterInstance(redis);

            return container;
        }
    }
}
