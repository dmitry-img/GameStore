using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using GameStore.Shared;
using GameStore.Shared.Infrastructure;

namespace GameStore.Api.Filters
{
    public class CurrentUserFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal is ClaimsPrincipal principal)
            {
                UserContext.UserObjectId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
