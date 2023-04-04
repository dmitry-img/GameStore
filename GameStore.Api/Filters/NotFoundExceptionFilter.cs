using GameStore.BLL.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace GameStore.Api.Filters
{
    public class NotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                if (filterContext.Exception is NotFoundException)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(filterContext.Exception.Message),
                        ReasonPhrase = "NotFoundException"
                    };
                }
            }
        }
    }
}