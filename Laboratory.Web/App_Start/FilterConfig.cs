using Laboratory.Web.Attributes;
using System.Web.Mvc;

namespace Laboratory.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InternationalizationAttribute());
            filters.Add(new CustomErrorAttribute());
        }
    }
}
