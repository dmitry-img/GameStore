using GameStore.BLL.Exceptions;
using log4net;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace GameStore.Api.Filters
{
    public class NotFoundExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILog _logger;

        public NotFoundExceptionFilterAttribute(ILog logger)
        {
            _logger = logger;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                if (actionExecutedContext.Exception is NotFoundException)
                {
                    var controllerName = actionExecutedContext.ActionContext
                        .ControllerContext
                        .ControllerDescriptor
                        .ControllerType
                        .Name;

                    var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

                    var message = $"{controllerName} -> {actionName}: " +
                        $"exception message: {actionExecutedContext.Exception.Message}";

                    _logger.Error(message);

                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(actionExecutedContext.Exception.Message),
                        ReasonPhrase = "NotFoundException"
                    };
                    actionExecutedContext.Response = response;
                }
            }

            base.OnException(actionExecutedContext);
        }
    }
}
