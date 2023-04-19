using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using GameStore.Api.Infrastructure;
using log4net;

namespace GameStore.Api.Filters
{
    public class LogPerformanceFilterAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch.Start();
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _stopwatch.Stop();

            var controllerName = actionExecutedContext
               .ActionContext
               .ControllerContext
               .ControllerDescriptor
               .ControllerType
               .FullName;

            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            var message = $"{controllerName} -> {actionName} " +
                $"Performance: {_stopwatch.ElapsedMilliseconds} ms";

            var config = actionExecutedContext.Request.GetConfiguration();
            var logger = config.DependencyResolver.GetService(typeof(ILog)) as ILog;

            logger.Debug(message);

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
