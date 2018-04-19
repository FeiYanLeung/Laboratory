using System.Web.Mvc;

namespace Laboratory.WebApi.Areas.v1
{
    public class v3AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "v1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "v1_default",
                "v1/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}