using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.Blog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.HomeControllerTests
{
    [TestClass]
    public class BlogTests : HomeControllerTestBase
    {
        [TestMethod]
        public void When_Blog_is_called_then_GetBlogArticles_on_IBlogProcess_is_called_and_the_result_is_mapped_with_BlogArticleMapper()
        {
            const int blogArticleCount = 3;

            var user = UserCreator.CreateSingle();
            var blogArticles = BlogArticleCreator.CreateCollection();
            foreach (var blogArticle in blogArticles)
            {
                blogArticle.AuthorId = user.Id;
            }

            UserProcess
                .Expect(process =>
                        process.GetUser(user.Id))
                .Return(user)
                .Repeat.Once();
            UserProcess.Replay();

            BlogProcess
                .Expect(process =>
                        process.GetBlogArticles(0, 3))
                .Return(blogArticles)
                .Repeat.Once();
            BlogProcess.Replay();

            var blogArticleDetailsModelcollection = CreateBlogArticleDetailsModelCollection();

            BlogArticleMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<IEnumerable<BlogArticle>>.Matches(articles => articles.Count() == blogArticleCount),
                            Arg<IEnumerable<User>>.Matches(users => users.ToList().Any(u => u == user))))
                .Return(blogArticleDetailsModelcollection)
                .Repeat.Once();
            BlogArticleMapper.Replay();

            var result = Controller.Blog(blogArticleCount).Result as PartialViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as ItemListModel<BlogArticleDetailsModel>;
            Assert.IsNotNull(model);

            UserProcess.VerifyAllExpectations();
            BlogProcess.VerifyAllExpectations();
            BlogArticleMapper.VerifyAllExpectations();
        }
    }
}