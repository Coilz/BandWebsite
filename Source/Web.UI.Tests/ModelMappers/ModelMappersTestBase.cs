using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.UnitTests;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers
{
    public abstract class ModelMappersTestBase : UnitTestBase
    {
        protected IAudioProcess AudioProcess { get; private set; }
        protected IBandProcess BandProcess { get; private set; }
        protected IBlogProcess BlogProcess { get; private set; }
        protected IUserProcess UserProcess { get; private set; }
        protected IPerformanceProcess PerformanceProcess { get; private set; }
        protected IPhotoProcess PhotoProcess { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            AudioProcess = MockHelper.CreateAndRegisterMock<IAudioProcess>();
            BandProcess = MockHelper.CreateAndRegisterMock<IBandProcess>();
            BlogProcess = MockHelper.CreateAndRegisterMock<IBlogProcess>();
            UserProcess = MockHelper.CreateAndRegisterMock<IUserProcess>();
            PerformanceProcess = MockHelper.CreateAndRegisterMock<IPerformanceProcess>();
            PhotoProcess = MockHelper.CreateAndRegisterMock<IPhotoProcess>();
        }
    }
}