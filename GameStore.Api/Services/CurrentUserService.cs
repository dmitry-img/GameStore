using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using GameStore.Api.Interfaces;

namespace GameStore.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string GetCurrentUserObjectId()
        {
            if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
            }

            return null;
        }

    }
}
