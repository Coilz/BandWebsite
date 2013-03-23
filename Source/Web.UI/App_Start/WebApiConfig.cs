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
                routeTemplate: "api/audio/{bandId}/{id}/{page}/{pageSize}",
                defaults: new { controller = "AudioApi", id = RouteParameter.Optional, page = RouteParameter.Optional, pageSize = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{bandId}/{id}/{page}/{pageSize}",
                defaults: new { id = RouteParameter.Optional, page = RouteParameter.Optional, pageSize = RouteParameter.Optional }
                );

/*
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
*/
        }
    }
}