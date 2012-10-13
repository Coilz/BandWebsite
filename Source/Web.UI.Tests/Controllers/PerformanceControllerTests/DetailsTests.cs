using System.Web.Mvc;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PerformanceControllerTests
{
    [TestClass]
    public class DetailsTests : PerformanceControllerTestBase
    {
        [TestMethod]
        public void When_Details_is_called_GetPerformance_on_IPerformanceProcess_is_called_with_the_correct_parameter_and_the_result_is_mapped_with_PerformanceMapper()
        {
            var performance = PerformanceCreator.CreateSingleFuture();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(performance.Id))
                .Return(performance)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var performanceDetailsModel = CreatePerformanceDetailsModel(performance.Id);

            PerformanceMapper
                .Expect(mapper =>
                        mapper.MapToDetail(performance))
                .Return(performanceDetailsModel)
                .Repeat.Once();
            PerformanceMapper.Replay();

            var result = Controller.Details(performance.Id).Result as ViewResult;

            Assert.IsNotNull(result);
            
            PerformanceProcess.VerifyAllExpectations();
            PerformanceMapper.VerifyAllExpectations();
        }
    }
}