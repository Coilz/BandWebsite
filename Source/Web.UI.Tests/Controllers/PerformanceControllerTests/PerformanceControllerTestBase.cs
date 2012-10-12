using Ewk.BandWebsite.Web.UI.Controllers;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PerformanceControllerTests
{
    public abstract class PerformanceControllerTestBase : ControllerTestBase
    {
        protected PerformanceController Controller { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Controller = new PerformanceController();
        }
    }
}