using GameStore.Api.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

namespace GameStore.Api.Filters
{
    public class LogPerformanceFilter : IActionFilter
    {
        readonly Stopwatch _stopwatch = new Stopwatch();

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopwatch.Stop();

            string appDataPath = ApplicationVariables.AppDataPath;
            string path = Path.Combine(appDataPath, "PerformanceLog.txt");

            using (var writer = File.AppendText(path))
            {
                string text = $"Date: {DateTime.UtcNow} - " +
               $"Controller: {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} - " +
               $"Action: {filterContext.ActionDescriptor.ActionName} - " +
               $"Performance: {_stopwatch.ElapsedMilliseconds} ms";
                writer.WriteLine(text);

                writer.Close();
            }
        }
    }
}