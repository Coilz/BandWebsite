using System;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.Common.Models.PhotoAdapterSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PhotoAdapterSettingsControllerTests
{
    [TestClass]
    public class EditTests : PhotoAdapterSettingsControllerTestBase
    {
        [TestMethod]
        public void When_Edit_is_called_with_an_Id_then_GetPhotoAdapterSettings_on_IPhotoAdapterSettingsProcess_is_called_and_the_result_is_mapped_with_PhotoAdapterSettingsMapper()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();

            PhotoProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(photoAdapterSettings)
                .Repeat.Once();
            PhotoProcess.Replay();

            var updateModel = CreateUpdatePhotoAdapterSettingsModel(Guid.NewGuid());

            PhotoAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.MapToUpdate(
                            Arg<AdapterSettings>.Matches(settings => settings.Id == photoAdapterSettings.Id)))
                .Return(updateModel)
                .Repeat.Once();
            PhotoAdapterSettingsMapper.Replay();

            var result = Controller.Edit().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as UpdatePhotoAdapterSettingsModel;
            Assert.IsNotNull(model);

            PhotoProcess.VerifyAllExpectations();
            PhotoAdapterSettingsMapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Edit_is_called_with_a_model_then_Map_on_PhotoAdapterSettingsMapper_is_called_and_the_result_is_used_to_call_UpdatePhotoAdapterSettings_on_IPhotoAdapterSettingsProcess_with()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();

            PhotoProcess
                .Expect(process =>
                        process.UpdateAdapterSettings(
                            Arg<AdapterSettings>.Matches(settings =>
                                                              settings.Id == photoAdapterSettings.Id)))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            PhotoProcess.Replay();

            var updateModel = CreateUpdatePhotoAdapterSettingsModel(photoAdapterSettings.Id);

            PhotoAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<UpdatePhotoAdapterSettingsModel>.Matches(m => m.SetName == photoAdapterSettings.SetName)))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            PhotoAdapterSettingsMapper.Replay();

            var result = Controller.Edit(updateModel).Result as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            PhotoProcess.VerifyAllExpectations();
            PhotoAdapterSettingsMapper.VerifyAllExpectations();
        }
    }
}