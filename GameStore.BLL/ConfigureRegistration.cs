using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Profiles;
using GameStore.BLL.Services;
using Unity;

namespace GameStore.BLL
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterBLLTypes(this IUnityContainer container)
        {
            var mapperConfiguration = new MapperConfiguration(conf =>
            {
                conf.AddProfile<MappingProfile>();
            });

            container.RegisterInstance<IMapper>(mapperConfiguration.CreateMapper());

            container.RegisterType<IGameService, GameService>();
            container.RegisterType<ICommentService, CommentService>();

            return container;
        }
    }
}
