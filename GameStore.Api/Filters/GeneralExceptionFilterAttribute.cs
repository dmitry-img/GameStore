using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using GameStore.Api.Infrastructure;
using GameStore.BLL.Exceptions;
using log4net;

namespace GameStore.Api.Filters
{
    public class GeneralExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                base.OnException(actionExecutedContext);
            }

            var controllerName = actionExecutedContext.ActionContext
                .ControllerContext
                .ControllerDescriptor
                .ControllerType
                .FullName;

            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            var message = $"{controllerName} -> {actionName} " +
                $"message: {actionExecutedContext.Exception.Message}";

            var config = actionExecutedContext.Request.GetConfiguration();

            var logger = config.DependencyResolver.GetService(typeof(ILog)) as ILog;

            logger.Error(message);

            HttpResponseMessage response;

            if (actionExecutedContext.Exception is NotFoundException)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "NotFoundException"
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(message),
                };
            }

            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}
