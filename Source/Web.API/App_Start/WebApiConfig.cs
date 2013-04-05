using System.Web.Http;

namespace Ewk.BandWebsite.Web.API.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{bandId}/{id}",
                defaults:
                    new
                        {
                            id = RouteParameter.Optional,
                        }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultPagedApi",
                routeTemplate: "api/{controller}/{bandId}/{page}/{pageSize}",
                defaults:
                    new
                        {
                            page = RouteParameter.Optional,
                            pageSize = RouteParameter.Optional,
                        }
                );

            // config.MessageHandlers.Add(new TokenValidationHandler());
        }
    }
}