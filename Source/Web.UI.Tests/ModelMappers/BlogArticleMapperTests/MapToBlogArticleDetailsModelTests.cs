using System;
using System.Linq;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BlogArticleMapperTests
{
    [TestClass]
    public class MapToBlogArticleDetailsModelTests : BlogArticleMapperTestBase
    {
        [TestMethod]
        public void When_BlogArticle_is_mapped_to_a_BlogArticleDetailsModel_then_no_data_is_retrieved_from_process_classes()
        {
            BlogProcess
                .Expect(process =>
                        process.GetBlogArticle(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            BlogProcess.Replay();

            UserProcess
                .Expect(process =>
                        process.GetUser(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            UserProcess.Replay();

            var blogArticle = BlogArticleCreator.CreateSingle();
            var user = UserCreator.CreateSingle();
            blogArticle.AuthorId = user.Id;

            var result = Mapper.MapToDetail(blogArticle, user);

            Assert.AreEqual(blogArticle.Id, result.Id);
            Assert.AreEqual(blogArticle.ModificationDate, result.ModificationDate);
            Assert.AreEqual(blogArticle.PublishDate, result.PublishDate);
            Assert.AreEqual(blogArticle.Title, result.Title);
            Assert.AreEqual(blogArticle.Content, result.Content);
            Assert.AreEqual(blogArticle.CreationDate, result.CreationDate);
            Assert.AreEqual(user.Login.LoginName, result.AuthorName);
        }

        [TestMethod]
        public void When_a_list_of_BlogArticles_is_mapped_to_a_BlogArticleDetailsModels_then_no_data_is_retrieved_from_process_classes()
        {
            BlogProcess
                .Expect(process =>
                        process.GetBlogArticle(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            BlogProcess.Replay();

            UserProcess
                .Expect(process =>
                        process.GetUser(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            UserProcess.Replay();

            var blogArticles = BlogArticleCreator.CreateCollection();
            var user = UserCreator.CreateSingle();
            foreach (var blogArticle in blogArticles)
            {
                blogArticle.AuthorId = user.Id;
            }

            var result = Mapper.Map(blogArticles, new[]{user});

            foreach (var blogarticleDetailsModel in result.Items)
            {
                Assert.AreEqual(user.Login.LoginName, blogarticleDetailsModel.AuthorName);
            }

            foreach (var blogArticle in blogArticles)
            {
                Assert.IsTrue(result.Items.Single(model => 
                    model.Id == blogArticle.Id &&
                    model.ModificationDate == blogArticle.ModificationDate &&
                    model.PublishDate == blogArticle.PublishDate &&
                    model.Title == blogArticle.Title &&
                    model.Content == blogArticle.Content &&
                    model.CreationDate == blogArticle.CreationDate)!= null);
            }
        }
    }
}