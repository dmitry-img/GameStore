using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using GameStore.Api.Interfaces;

namespace GameStore.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IUserIdentityProvider _identityProvider;

        public CurrentUserService(IUserIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public string GetCurrentUserObjectId()
        {
            var identity = _identityProvider.GetIdentity() as ClaimsIdentity;

            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }

            return null;
        }
    }
}
