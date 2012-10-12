using System.Web.Mvc;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PerformanceControllerTests
{
    [TestClass]
    public class DeleteTests : PerformanceControllerTestBase
    {
        [TestMethod]
        public void When_Delete_is_called_with_an_Id_then_GetPerformance_on_IPerformanceProcess_is_called_and_the_result_used_to_call_RemovePerformance_on_IPerformanceProcess()
        {
            var entity = PerformanceCreator.CreateSingle();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(entity.Id))
                .Return(entity)
                .Repeat.Once();
            PerformanceProcess
                .Expect(process =>
                        process.RemovePerformance(entity))
                .Repeat.Once();
            PerformanceProcess.Replay();

            var result = Controller.Delete(entity.Id).Result as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            PerformanceProcess.VerifyAllExpectations();
        }
    }
}