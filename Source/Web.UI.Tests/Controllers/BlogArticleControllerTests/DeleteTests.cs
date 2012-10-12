using System.Web.Mvc;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.BlogArticleControllerTests
{
    [TestClass]
    public class DeleteTests : BlogArticleControllerTestBase
    {
        [TestMethod]
        public void When_Delete_is_called_with_an_Id_then_GetBlogArticle_on_IBlogArticleProcess_is_called_and_the_result_used_to_call_RemoveBlogArticle_on_IBlogArticleProcess()
        {
            var entity = BlogArticleCreator.CreateSingle();

            BlogProcess
                .Expect(process =>
                        process.GetBlogArticle(entity.Id))
                .Return(entity)
                .Repeat.Once();
            BlogProcess
                .Expect(process =>
                        process.RemoveBlogArticle(entity))
                .Repeat.Once();
            BlogProcess.Replay();

            var result = Controller.Delete(entity.Id).Result as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            BlogProcess.VerifyAllExpectations();
        }
    }
}