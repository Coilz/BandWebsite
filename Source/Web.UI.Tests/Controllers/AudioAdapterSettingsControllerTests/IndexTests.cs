using System;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.PhotoAdapterSettings;
using Ewk.BandWebsite.Web.UI.Tests.Controllers.PhotoAdapterSettingsControllerTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.AudioAdapterSettingsControllerTests
{
    [TestClass]
    public class IndexTests : PhotoAdapterSettingsControllerTestBase
    {
        [TestMethod]
        public void When_Index_is_called_GetPhotoAdapterSettings_on_IPhotoAdapterSettingsProcess_is_called_and_the_result_is_mapped_with_PhotoAdapterSettingsMapper()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            PhotoProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(adapterSettings)
                .Repeat.Once();
            PhotoProcess.Replay();

            var photoAdapterSettingsDetailsModel = CreatePhotoAdapterSettingsDetailsModel(Guid.NewGuid());

            PhotoAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.MapToDetail(
                            Arg<AdapterSettings>.Matches(settings => settings.Id == adapterSettings.Id)))
                .Return(photoAdapterSettingsDetailsModel)
                .Repeat.Once();
            PhotoAdapterSettingsMapper.Replay();

            var result = Controller.Index().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as PhotoAdapterSettingsDetailsModel;
            Assert.IsNotNull(model);

            PhotoProcess.VerifyAllExpectations();
            PhotoAdapterSettingsMapper.VerifyAllExpectations();
        }
    }
}