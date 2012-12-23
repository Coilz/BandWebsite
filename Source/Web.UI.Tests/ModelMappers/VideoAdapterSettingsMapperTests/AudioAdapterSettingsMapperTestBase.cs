using Ewk.BandWebsite.Web.UI.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.VideoAdapterSettingsMapperTests
{
    public abstract class VideoAdapterSettingsMapperTestBase : ModelMappersTestBase
    {
        protected VideoAdapterSettingsMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new VideoAdapterSettingsMapper(CatalogsContainer);
        }
    }
}