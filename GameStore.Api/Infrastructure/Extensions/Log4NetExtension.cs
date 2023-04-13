

using log4net;
using System.ComponentModel;
using Unity;
using Unity.Extension;
using Unity.Injection;

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
