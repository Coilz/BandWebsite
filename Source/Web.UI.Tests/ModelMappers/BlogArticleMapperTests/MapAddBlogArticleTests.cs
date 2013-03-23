using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.Common.Models.Blog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BlogArticleMapperTests
{
    [TestClass]
    public class MapAddBlogArticleTests : BlogArticleMapperTestBase
    {
        [TestMethod]
        public void When_AddBlogArticle_is_mapped_to_a_BlogArticle_then_the_User_is_retrieved_from_the_IUserProcess()
        {
            var user = UserCreator.CreateSingle();

            var addBlogArticleModel = new AddBlogArticleModel
                                          {
                                              Content = "content",
                                              Title = "title"
                                          };

            var result = Mapper.Map(addBlogArticleModel, user.Id);

            Assert.AreEqual(user.Id, result.AuthorId);
            Assert.AreEqual(addBlogArticleModel.Title, result.Title);
            Assert.AreEqual(addBlogArticleModel.Content, result.Content);
        }
    }
}