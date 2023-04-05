using GameStore.Api.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GameStore.Api.Filters
{
    public class LogIpFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string appDataPath = ApplicationVariables.AppDataPath;
            string path = Path.Combine(appDataPath, "RequestantIps.txt");

            var ip = HttpContext.Current.Request.UserHostAddress;

            using (var writer = File.AppendText(path))
            {
                string text = $"Date: {DateTime.UtcNow} - " +
                    $"Controller: {actionContext.ActionDescriptor.ControllerDescriptor.ControllerName} - " +
                    $"Action: {actionContext.ActionDescriptor.ActionName} - " +
                    $"Ip: {ip}";

                writer.WriteLine(text);
                writer.Close();
            }

            base.OnActionExecuting(actionContext);
        }

    }
}