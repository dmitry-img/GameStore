using GameStore.Api.Infrastructure;
using log4net;
using System;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GameStore.Api.Filters
{
    public class LogIpFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;

            var controllerName = actionContext
               .ControllerContext
               .ControllerDescriptor
               .ControllerType
               .FullName;

            var actionName = actionContext.ActionDescriptor.ActionName;

            var message = $"{controllerName} -> {actionName} " +
                $"IP: {ip}";

            var config = actionContext.Request.GetConfiguration();
            var logger = config.DependencyResolver.GetService(typeof(ILog)) as ILog;

            logger.Debug(message);

            base.OnActionExecuting(actionContext);
        }
    }
}
