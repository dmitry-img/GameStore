using System;

namespace GameStore.Shared.Infrastructure
{
    public static class UserContext
    {
        [ThreadStatic]
        public static string UserObjectId;
    }
}
