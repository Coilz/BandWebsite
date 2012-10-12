using System.Web.Mvc;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.HomeControllerTests
{
    [TestClass]
    public class AboutTests : HomeControllerTestBase
    {
        [TestMethod]
        public void When_About_is_called_GetBand_on_IBandProcess_is_called_with_the_correct_parameter_and_the_result_is_mapped_with_BandMapper()
        {
            var entity = BandCreator.CreateSingle();

            BandProcess
                .Expect(process =>
                        process.GetBand())
                .Return(entity)
                .Repeat.Once();
            BandProcess.Replay();

            var detailModel = CreateAboutModel();

            BandMapper
                .Expect(mapper =>
                        mapper.MapToAboutModel(entity))
                .Return(detailModel)
                .Repeat.Once();
            BandMapper.Replay();

            var result = Controller.About().Result as ViewResult;

            Assert.IsNotNull(result);
            
            BandProcess.VerifyAllExpectations();
            BandMapper.VerifyAllExpectations();
        }
    }
}