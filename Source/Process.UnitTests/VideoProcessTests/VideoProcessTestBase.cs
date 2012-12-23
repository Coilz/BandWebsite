using Ewk.BandWebsite.Adapters;

namespace Ewk.BandWebsite.Process.UnitTests.VideoProcessTests
{
    public abstract class VideoProcessTestBase : ProcessTestBase
    {
        protected IVideoAdapter VideoAdapter { get; private set; }
        protected VideoProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            VideoAdapter = MockHelper.CreateAndRegisterMock<IVideoAdapter>();
            Process = new VideoProcess(CatalogsContainer);
        }
    }
}