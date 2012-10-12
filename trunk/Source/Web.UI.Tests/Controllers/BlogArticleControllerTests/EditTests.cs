using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.Blog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.BlogArticleControllerTests
{
    [TestClass]
    public class EditTests : BlogArticleControllerTestBase
    {
        [TestMethod]
        public void When_Edit_is_called_with_an_Id_then_GetBlogArticle_on_IBlogArticleProcess_is_called_and_the_result_is_mapped_with_BlogArticleMapper()
        {
            var entity = BlogArticleCreator.CreateSingle();

            BlogProcess
                .Expect(process =>
                        process.GetBlogArticle(entity.Id))
                .Return(entity)
                .Repeat.Once();
            BlogProcess.Replay();

            var updateModel = CreateUpdateBlogArticleModel(Guid.NewGuid());

            BlogArticleMapper
                .Expect(mapper =>
                        mapper.MapToUpdate(
                            Arg<BlogArticle>.Matches(settings => settings.Id == entity.Id)))
                .Return(updateModel)
                .Repeat.Once();
            BlogArticleMapper.Replay();

            var result = Controller.Edit(entity.Id).Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as UpdateBlogArticleModel;
            Assert.IsNotNull(model);

            BlogProcess.VerifyAllExpectations();
            BlogArticleMapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Edit_is_called_with_a_model_then_Map_on_BlogArticleMapper_is_called_and_the_result_is_used_to_call_UpdateBlogArticle_on_IBlogArticleProcess_with()
        {
            var entity = BlogArticleCreator.CreateSingle();

            BlogProcess
                .Expect(process =>
                        process.UpdateBlogArticle(
                            Arg<BlogArticle>.Matches(settings =>
                                                     settings.Id == entity.Id)))
                .Return(entity)
                .Repeat.Once();
            BlogProcess.Replay();

            var updateModel = CreateUpdateBlogArticleModel(entity.Id);

            BlogArticleMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<UpdateBlogArticleModel>.Matches(m =>
                                                                m.Content == entity.Content &&
                                                                m.Title == entity.Title),
                            Arg<Guid>.Matches(guid => guid == entity.Id)))
                .Return(entity)
                .Repeat.Once();
            BlogArticleMapper.Replay();

            var result = Controller.Edit(entity.Id, updateModel) as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            BlogProcess.VerifyAllExpectations();
            BlogArticleMapper.VerifyAllExpectations();
        }
    }
}