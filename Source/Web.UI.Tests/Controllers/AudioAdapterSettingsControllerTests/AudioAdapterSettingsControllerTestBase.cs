using Ewk.BandWebsite.Web.UI.Controllers;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.AudioAdapterSettingsControllerTests
{
    public abstract class AudioAdapterSettingsControllerTestBase : ControllerTestBase
    {
        protected AudioAdapterSettingsController Controller { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Controller = new AudioAdapterSettingsController();
        }
    }
}