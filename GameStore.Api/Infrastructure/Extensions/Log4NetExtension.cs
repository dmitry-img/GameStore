using log4net;
using Unity;
using Unity.Extension;


namespace GameStore.Api.Infrastructure.Extensions
{
    public class Log4NetExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterFactory<ILog>(c => LogManager.GetLogger(c.GetType()));
        }
    }
}
