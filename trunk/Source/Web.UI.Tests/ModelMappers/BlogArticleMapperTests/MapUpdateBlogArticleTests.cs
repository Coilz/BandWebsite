using System.Threading.Tasks;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.Blog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BlogArticleMapperTests
{
    [TestClass]
    public class MapUpdateBlogArticleTests : BlogArticleMapperTestBase
    {
        [TestMethod]
        public void When_UpdateBlogArticle_is_mapped_to_a_BlogArticle_then_the_User_is_retrieved_from_the_IUserProcess_and_added_to_the_BlogArticle()
        {
            const string title = "title";
            const string content = "content";

            var entity = BlogArticleCreator.CreateSingle();

            BlogProcess
                .Expect(process =>
                        process.GetBlogArticle(entity.Id))
                .Return(entity)
                .Repeat.Once();
            BlogProcess.Replay();

            var updateBlogArticleModel = new UpdateBlogArticleModel
                                          {
                                              Title = title,
                                              Content = content,
                                          };

            var result = Mapper.Map(updateBlogArticleModel, entity.Id);

            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(content, result.Content);

            BlogProcess.VerifyAllExpectations();
        }
    }
}