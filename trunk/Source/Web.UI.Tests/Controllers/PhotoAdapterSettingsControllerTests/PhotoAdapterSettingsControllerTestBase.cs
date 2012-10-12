using Ewk.BandWebsite.Web.UI.Controllers;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PhotoAdapterSettingsControllerTests
{
    public abstract class PhotoAdapterSettingsControllerTestBase : ControllerTestBase
    {
        protected PhotoAdapterSettingsController Controller { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Controller = new PhotoAdapterSettingsController();
        }
    }
}