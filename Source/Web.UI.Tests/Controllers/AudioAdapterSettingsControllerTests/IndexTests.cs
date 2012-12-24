using System;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.AudioAdapterSettingsControllerTests
{
    [TestClass]
    public class IndexTests : AudioAdapterSettingsControllerTestBase
    {
        [TestMethod]
        public void When_Index_is_called_GetAudioAdapterSettings_on_IAudioAdapterSettingsProcess_is_called_and_the_result_is_mapped_with_AudioAdapterSettingsMapper()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            AudioProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(adapterSettings)
                .Repeat.Once();
            AudioProcess.Replay();

            var photoAdapterSettingsDetailsModel = CreateAudioAdapterSettingsDetailsModel(Guid.NewGuid());

            AudioAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.MapToDetail(
                            Arg<AdapterSettings>.Matches(settings => settings.Id == adapterSettings.Id)))
                .Return(photoAdapterSettingsDetailsModel)
                .Repeat.Once();
            AudioAdapterSettingsMapper.Replay();

            var result = Controller.Index().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as AudioAdapterSettingsDetailsModel;
            Assert.IsNotNull(model);

            AudioProcess.VerifyAllExpectations();
            AudioAdapterSettingsMapper.VerifyAllExpectations();
        }
    }
}