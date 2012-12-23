using System;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.VideoAdapterSettingsControllerTests
{
    [TestClass]
    public class IndexTests : VideoAdapterSettingsControllerTestBase
    {
        [TestMethod]
        public void When_Index_is_called_GetVideoAdapterSettings_on_IVideoAdapterSettingsProcess_is_called_and_the_result_is_mapped_with_VideoAdapterSettingsMapper()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            VideoProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(adapterSettings)
                .Repeat.Once();
            VideoProcess.Replay();

            var photoAdapterSettingsDetailsModel = CreateVideoAdapterSettingsDetailsModel(Guid.NewGuid());

            VideoAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.MapToDetail(
                            Arg<AdapterSettings>.Matches(settings => settings.Id == adapterSettings.Id)))
                .Return(photoAdapterSettingsDetailsModel)
                .Repeat.Once();
            VideoAdapterSettingsMapper.Replay();

            var result = Controller.Index().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as VideoAdapterSettingsDetailsModel;
            Assert.IsNotNull(model);

            VideoProcess.VerifyAllExpectations();
            VideoAdapterSettingsMapper.VerifyAllExpectations();
        }
    }
}