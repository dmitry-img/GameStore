using System.Web;

namespace GameStore.Api.Models
{
    public class ApplicationVariables
    {
        public static string AppDataPath => HttpContext.Current.Server.MapPath("~/App_Data");
    }
}