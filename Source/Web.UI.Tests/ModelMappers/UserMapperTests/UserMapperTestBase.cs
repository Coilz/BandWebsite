using Ewk.BandWebsite.Web.Common.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.UserMapperTests
{
    public abstract class UserMapperTestBase : ModelMappersTestBase
    {
        protected UserMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new UserMapper(CatalogsContainer);
        }
    }
}