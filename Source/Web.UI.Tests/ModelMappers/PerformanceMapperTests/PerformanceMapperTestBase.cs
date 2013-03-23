using Ewk.BandWebsite.Web.Common.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PerformanceMapperTests
{
    public abstract class PerformanceMapperTestBase : ModelMappersTestBase
    {
        protected PerformanceMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new PerformanceMapper(CatalogsContainer);
        }
    }
}