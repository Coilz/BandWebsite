using System;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.BandRepositoryTests
{
    [TestClass]
    public class BlogArticleTests : BandRepositoryTestBase
    {
        [TestMethod]
        public void When_GetBlogArticle_is_called_with_an_Id_then_the_BlogArticle_with_the_corresponding_Id_is_retrieved_from_the_collection()
        {
            var blogArticles = BlogArticleCreator.CreateCollection();
            var blogArticleId = blogArticles.ElementAt(2).Id;

            BandCatalog
                .Expect(catalog => catalog.BlogArticles)
                .Return(blogArticles)
                .Repeat.Once();
            BandCatalog.Replay();

            var blogArticle = Repository.GetBlogArticle(blogArticleId);

            Assert.IsNotNull(blogArticle);
            Assert.AreEqual(blogArticleId, blogArticle.Id);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetBlogArticle_is_called_with_an_Id_and_there_is_no_BlogArticle_in_the_collection_with_that_Id_then_an_InvalidOperationException_is_thrown()
        {
            var blogArticles = BlogArticleCreator.CreateCollection();
            var blogArticleId = Guid.NewGuid();

            BandCatalog
                .Expect(catalog => catalog.BlogArticles)
                .Return(blogArticles)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.GetBlogArticle(blogArticleId);
        }

        [TestMethod]
        public void When_GetAllBlogArticles_is_called_then_all_BlogArticles_are_retrieved_from_the_collection()
        {
            var blogArticles = BlogArticleCreator.CreateCollection();

            BandCatalog
                .Expect(catalog => catalog.BlogArticles)
                .Return(blogArticles)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAllBlogArticles();

            Assert.IsNotNull(result);
            Assert.AreEqual(blogArticles.Count(), result.Count());
            Assert.AreEqual(blogArticles, result);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_AddBlogArticle_is_called_with_a_BlogArticle_then_the_BlogArticle_is_added_to_the_collection()
        {
            var blogArticle = BlogArticleCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Add(blogArticle))
                .Return(blogArticle)
                .Repeat.Once();
            BandCatalog.Replay();

            blogArticle = Repository.AddBlogArticle(blogArticle);

            Assert.IsNotNull(blogArticle);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddBlogArticle_is_called_with_null_as_BlogArticle_then_an_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog => catalog.Add(Arg<BlogArticle>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.AddBlogArticle(null);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateBlogArticle_is_called_with_a_BlogArticle_then_the_BlogArticle_is_updated_in_the_collection()
        {
            var blogArticle = BlogArticleCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Update(blogArticle))
                .Return(blogArticle)
                .Repeat.Once();
            BandCatalog.Replay();

            blogArticle = Repository.UpdateBlogArticle(blogArticle);

            Assert.IsNotNull(blogArticle);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdateBlogArticle_is_called_with_null_as_BlogArticle_then_an_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog => catalog.Update(Arg<BlogArticle>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.UpdateBlogArticle(null);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_RemoveBlogArticle_is_called_with_a_BlogArticle_then_the_BlogArticle_is_removed_from_the_collection()
        {
            var blogArticle = BlogArticleCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Remove(blogArticle))
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.RemoveBlogArticle(blogArticle);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_RemoveBlogArticle_is_called_with_null_as_BlogArticle_then_an_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog => catalog.Remove(Arg<BlogArticle>.Is.Anything))
                .Repeat.Never();
            BandCatalog.Replay();

            Repository.RemoveBlogArticle(null);

            BandCatalog.VerifyAllExpectations();
        }
    }
}