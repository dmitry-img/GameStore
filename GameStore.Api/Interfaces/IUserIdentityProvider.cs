using System.Security.Principal;
using System.Web;

namespace GameStore.Api.Interfaces
{
    public interface IUserIdentityProvider
    {
        IIdentity GetIdentity();
    }
}
