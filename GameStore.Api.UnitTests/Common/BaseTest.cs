using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Shared.Infrastructure;

namespace GameStore.Api.Tests.Common
{
    public class BaseTest
    {
        public BaseTest()
        {
            UserContext.UserObjectId = "TestUserObjectId";
        }
    }
}
