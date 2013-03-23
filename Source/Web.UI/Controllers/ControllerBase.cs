using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Common;
using Ewk.BandWebsite.Process;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            SetBandIdCookie(requestContext); // TODO: Find a better solution to set the BandId
            return base.BeginExecute(requestContext, callback, state);
        }

        private void SetBandIdCookie(RequestContext requestContext)
        {
            const string cookieNameBandId = "BandId";

            CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        Guid bandId;

                        var bandCookie = requestContext.HttpContext.Request.Cookies[cookieNameBandId];
                        if (bandCookie != null && !string.IsNullOrEmpty(bandCookie.Value))
                        {
                            bandId = Guid.Parse(bandCookie.Value);
                        }
                        else
                        {
                            var bandProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandProcess>(container);
                            bandId = bandProcess.EnsureBandExists().Id;

                            requestContext.HttpContext.Response.Cookies[cookieNameBandId].Value = bandId.ToString();
                        }

                        var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
                        bandIdInstaller.SetBandId(bandId);
                    });
        }
    }
}