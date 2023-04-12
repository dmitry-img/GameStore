using System.Web;

namespace GameStore.Api.Infrastructure
{
    public class ApplicationVariables
    {
        public static string AppDataPath => HttpContext.Current.Server.MapPath("~/App_Data");
    }
}
