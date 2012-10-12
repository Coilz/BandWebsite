using Ewk.BandWebsite.Adapters;

namespace Ewk.BandWebsite.Process.UnitTests.PhotoProcessTests
{
    public abstract class PhotoProcessTestBase : ProcessTestBase
    {
        protected IPhotoAdapter PhotoAdapter { get; private set; }
        protected PhotoProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            PhotoAdapter = MockHelper.CreateAndRegisterMock<IPhotoAdapter>();
            Process = new PhotoProcess(CatalogsContainer);
        }
    }
}