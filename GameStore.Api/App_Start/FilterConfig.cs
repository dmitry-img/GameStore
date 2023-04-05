﻿using GameStore.Api.Filters;
using System.Web.Mvc;

namespace GameStore.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NotFoundExceptionFilter());
        }
    }
}
