using Ewk.BandWebsite.Web.UI.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PhotoAdapterSettingsMapperTests
{
    public abstract class PhotoAdapterSettingsMapperTestBase : ModelMappersTestBase
    {
        protected PhotoAdapterSettingsMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new PhotoAdapterSettingsMapper(CatalogsContainer);
        }
    }
}