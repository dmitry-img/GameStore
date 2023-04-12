using System;
using System.Diagnostics;
using System.IO;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using GameStore.Api.Infrastructure;

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

            string appDataPath = ApplicationVariables.AppDataPath;
            string path = Path.Combine(appDataPath, "PerformanceLog.txt");

            using (var writer = File.AppendText(path))
            {
                string text = $"Date: {DateTime.UtcNow} - " +
               $"Controller: {actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName} - " +
               $"Action: {actionExecutedContext.ActionContext.ActionDescriptor.ActionName} - " +
               $"Performance: {_stopwatch.ElapsedMilliseconds} ms";
                writer.WriteLine(text);

                writer.Close();
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
