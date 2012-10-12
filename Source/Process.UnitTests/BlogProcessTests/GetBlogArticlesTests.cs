using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.BlogProcessTests
{
    [TestClass]
    public class GetBlogArticlesTests : BlogProcessTestBase
    {
        [TestMethod]
        public void When_GetBlogArticle_is_called_with_a_valid_Guid_then_GetBlogArticle_on_the_BandRepository_is_called_with_that_Guid()
        {
            var articles = BlogArticleCreator.CreateCollection();

            BandRepository
                .Expect(repository =>
                        repository.GetAllBlogArticles())
                .Return(articles)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetBlogArticles();

            BandRepository.VerifyAllExpectations();
        }
    }
}