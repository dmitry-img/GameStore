using GameStore.Api.Infrastructure.Extensions;

using Unity;
using Unity.Lifetime;

namespace GameStore.Api.Infrastructure
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterApiTypes(this IUnityContainer container)
        {
            container.AddNewExtension<Log4NetExtension>();
            return container;
        }
    }
}
