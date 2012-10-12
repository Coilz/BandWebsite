using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BlogArticleMapperTests
{
    [TestClass]
    public class MapToUpdateBlogArticleModelTests : BlogArticleMapperTestBase
    {
        [TestMethod]
        public void When_BlogArticle_is_mapped_to_an_UpdateBlogArticleModel_then_all_corresponding_fields_are_mapped()
        {
            var blogArticle = BlogArticleCreator.CreateSingle();

            var result = Mapper.MapToUpdate(blogArticle);

            Assert.AreEqual(blogArticle.Id, result.Id);
            Assert.AreEqual(blogArticle.Title, result.Title);
            Assert.AreEqual(blogArticle.Content, result.Content);
        }
    }
}