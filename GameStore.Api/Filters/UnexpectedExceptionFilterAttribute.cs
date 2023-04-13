using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using GameStore.BLL.Exceptions;
using log4net;

namespace GameStore.Api.Filters
{
    public class UnexpectedExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILog _logger;

        public UnexpectedExceptionFilterAttribute(ILog logger)
        {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                return;
            }

            var controllerName = actionExecutedContext.ActionContext
                .ControllerContext
                .ControllerDescriptor
                .ControllerType
                .Name;

            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            var message = $"{controllerName} -> {actionName}: " +
                $"exception message: {actionExecutedContext.Exception.Message}";

            _logger.Error(message);
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(message),
            };
            actionExecutedContext.Response = response;

            base.OnException(actionExecutedContext);
        }
    }
}
