using Ewk.BandWebsite.Web.UI.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BandMapperTests
{
    public abstract class BandMapperTestBase : ModelMappersTestBase
    {
        protected BandMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new BandMapper(CatalogsContainer);
        }
    }
}