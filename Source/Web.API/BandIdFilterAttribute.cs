using System;
using Ewk.BandWebsite.Common;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Web.API
{
    public class BandIdFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var bandId = (Guid)actionContext.ActionArguments["bandId"];

            var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
            bandIdInstaller.SetBandId(bandId);

            base.OnActionExecuting(actionContext);
        }
    }
}