using Laboratory.WebApi.Filters;
using System.Web.Mvc;

namespace Laboratory.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalErrorFilter());
        }
    }
}
