using Ewk.BandWebsite.Web.UI.Controllers;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.HomeControllerTests
{
    public abstract class HomeControllerTestBase : ControllerTestBase
    {
        protected HomeController Controller { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Controller = new HomeController();
        }
    }
}