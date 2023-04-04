using GameStore.Api.Models;
using System;
using System.IO;
using System.Web.Mvc;

namespace GameStore.Api.Filters
{
    public class LogIpFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string appDataPath = ApplicationVariables.AppDataPath;
            string path = Path.Combine(appDataPath, "RequestantIps.txt");

            var ip = filterContext.HttpContext.Request.UserHostAddress;

            using (var writer = File.AppendText(path))
            {
                string text = $"Date: {DateTime.UtcNow} - " +
                    $"Controller: {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} - " +
                    $"Action: {filterContext.ActionDescriptor.ActionName} - " +
                    $"Ip: {ip}";
    
                writer.WriteLine(text);
                writer.Close();
            }
        }
    }
}