using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Api.Filters
{
    public class LogIpFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //TODO finish the LogIp filter
            string appDataPath = HttpContext.Current.Server.MapPath("~/App_Data");
            string path = Path.Combine(appDataPath, "RequestantIps.txt");

            var ip = filterContext.HttpContext.Request;
            File.WriteAllText(path, $"Date: {DateTime.UtcNow} - Ip: {ip}");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}