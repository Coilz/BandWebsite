using Ewk.BandWebsite.Repositories;
using Ewk.BandWebsite.UnitTests;

namespace Ewk.BandWebsite.Process.UnitTests
{
    public abstract class ProcessTestBase : UnitTestBase
    {
        protected IAppRepository AppRepository { get; private set; }
        protected IBandRepository BandRepository { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            AppRepository = MockHelper.CreateAndRegisterMock<IAppRepository>();
            BandRepository = MockHelper.CreateAndRegisterMock<IBandRepository>();
        }
    }
}