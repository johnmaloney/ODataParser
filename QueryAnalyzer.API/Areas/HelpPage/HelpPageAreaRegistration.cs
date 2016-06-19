using System.Web.Http;
using System.Web.Mvc;

namespace QueryAnalyzer.API.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });


            // By default route the user to the Help area if accessing the base URI.
            context.MapRoute(
                "Help Area",
                "",
                new { controller = "Help", action = "Index" });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}