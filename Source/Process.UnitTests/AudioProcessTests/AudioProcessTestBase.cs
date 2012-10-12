using Ewk.BandWebsite.Adapters;

namespace Ewk.BandWebsite.Process.UnitTests.AudioProcessTests
{
    public abstract class AudioProcessTestBase : ProcessTestBase
    {
        protected IAudioAdapter AudioAdapter { get; private set; }
        protected AudioProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            AudioAdapter = MockHelper.CreateAndRegisterMock<IAudioAdapter>();
            Process = new AudioProcess(CatalogsContainer);
        }
    }
}