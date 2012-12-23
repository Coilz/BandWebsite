using Ewk.BandWebsite.Web.UI.Controllers;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.VideoAdapterSettingsControllerTests
{
    public abstract class VideoAdapterSettingsControllerTestBase : ControllerTestBase
    {
        protected VideoAdapterSettingsController Controller { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Controller = new VideoAdapterSettingsController();
        }
    }
}