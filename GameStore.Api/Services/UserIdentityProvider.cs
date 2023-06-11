using System.Security.Principal;
using System.Web;
using GameStore.Api.Interfaces;

namespace GameStore.Api.Services
{
    public class UserIdentityProvider : IUserIdentityProvider
    {
        public IIdentity GetIdentity()
        {
            return HttpContext.Current.User.Identity;
        }
    }
}
