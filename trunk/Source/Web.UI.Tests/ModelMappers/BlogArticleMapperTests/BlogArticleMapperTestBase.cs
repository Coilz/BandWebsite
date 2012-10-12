using Ewk.BandWebsite.Web.UI.ModelMappers;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BlogArticleMapperTests
{
    public abstract class BlogArticleMapperTestBase : ModelMappersTestBase
    {
        protected BlogArticleMapper Mapper { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Mapper = new BlogArticleMapper(CatalogsContainer);
        }
    }
}