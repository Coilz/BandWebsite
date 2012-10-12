using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.BlogProcessTests
{
    [TestClass]
    public class RemoveBlogArticleTests : BlogProcessTestBase
    {
        [TestMethod]
        public void When_RemoveBlogArticle_is_called_with_a_new_BlogArticle_then_RemoveBlogArticle_on_the_BandRepository_is_called_with_that_BlogArticle()
        {
            var article = BlogArticleCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.RemoveBlogArticle(article))
                .Repeat.Once();
            BandRepository.Replay();

            Process.RemoveBlogArticle(article);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_RemoveBlogArticle_is_called_with_a_null_value_for_BlogArticle_then_an_ArgumentNullException_is_thrown()
        {
            BandRepository
                .Expect(repository =>
                        repository.RemoveBlogArticle(null))
                .Repeat.Never();
            BandRepository.Replay();

            Process.RemoveBlogArticle(null);

            BandRepository.VerifyAllExpectations();
        }
    }
}