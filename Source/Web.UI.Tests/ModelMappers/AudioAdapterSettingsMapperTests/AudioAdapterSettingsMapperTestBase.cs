using Ewk.BandWebsite.Web.Common.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.AudioAdapterSettingsMapperTests
{
    public abstract class AudioAdapterSettingsMapperTestBase : ModelMappersTestBase
    {
        protected AudioAdapterSettingsMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new AudioAdapterSettingsMapper(CatalogsContainer);
        }
    }
}