using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.BlogProcessTests
{
    [TestClass]
    public class GetBlogArticleTests : BlogProcessTestBase
    {
        [TestMethod]
        public void When_GetBlogArticle_is_called_with_a_valid_Guid_then_GetBlogArticle_on_the_BandRepository_is_called_with_that_Guid()
        {
            var article = BlogArticleCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetBlogArticle(article.Id))
                .Return(article)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetBlogArticle(article.Id);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_GetBlogArticle_is_called_with_an_invalid_Guid_then_an_ArgumentNullException_is_thrown()
        {
            Process.GetBlogArticle(Guid.Empty);
        }
    }
}