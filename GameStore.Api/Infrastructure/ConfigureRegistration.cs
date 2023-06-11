using GameStore.Api.Infrastructure.Extensions;
using GameStore.Api.Interfaces;
using GameStore.Api.Services;
using Unity;

namespace GameStore.Api.Infrastructure
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterApiTypes(this IUnityContainer container)
        {
            container.AddNewExtension<Log4NetExtension>();
            container.RegisterType<ICurrentUserService, CurrentUserService>();
            container.RegisterType<IUserIdentityProvider, UserIdentityProvider>();

            return container;
        }
    }
}
