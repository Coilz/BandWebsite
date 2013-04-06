using System.Linq;
using System.Web.Http;

namespace Ewk.BandWebsite.Web.UI.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "AudioApi",
                routeTemplate: "api/audio/{bandId}/{id}",
                defaults: new
                    {
                        controller = "AudioApi",
                        id = RouteParameter.Optional,
                    }
                );

            config.Routes.MapHttpRoute(
                name: "AudioPagingApi",
                routeTemplate: "api/audio/{bandId}/{page}/{pageSize}",
                defaults: new
                    {
                        controller = "AudioApi",
                        page = RouteParameter.Optional,
                        pageSize = RouteParameter.Optional
                    }
                );

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
                name: "DefaultPagingApi",
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