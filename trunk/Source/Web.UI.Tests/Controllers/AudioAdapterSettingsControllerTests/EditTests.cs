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
    public class EditTests : AudioAdapterSettingsControllerTestBase
    {
        [TestMethod]
        public void When_Edit_is_called_with_an_Id_then_GetAudioAdapterSettings_on_IAudioAdapterSettingsProcess_is_called_and_the_result_is_mapped_with_AudioAdapterSettingsMapper()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            AudioProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(adapterSettings)
                .Repeat.Once();
            AudioProcess.Replay();

            var updateModel = CreateUpdateAudioAdapterSettingsModel(Guid.NewGuid());

            AudioAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.MapToUpdate(
                            Arg<AdapterSettings>.Matches(settings => settings.Id == adapterSettings.Id)))
                .Return(updateModel)
                .Repeat.Once();
            AudioAdapterSettingsMapper.Replay();

            var result = Controller.Edit().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as UpdateAudioAdapterSettingsModel;
            Assert.IsNotNull(model);

            AudioProcess.VerifyAllExpectations();
            AudioAdapterSettingsMapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Edit_is_called_with_a_model_then_Map_on_AudioAdapterSettingsMapper_is_called_and_the_result_is_used_to_call_UpdateAudioAdapterSettings_on_IAudioAdapterSettingsProcess_with()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            AudioProcess
                .Expect(process =>
                        process.UpdateAdapterSettings(
                            Arg<AdapterSettings>.Matches(settings =>
                                                              settings.Id == adapterSettings.Id)))
                .Return(adapterSettings)
                .Repeat.Once();
            AudioProcess.Replay();

            var updateModel = CreateUpdateAudioAdapterSettingsModel(adapterSettings.Id);

            AudioAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<UpdateAudioAdapterSettingsModel>.Matches(m => m.SetName == adapterSettings.SetName)))
                .Return(adapterSettings)
                .Repeat.Once();
            AudioAdapterSettingsMapper.Replay();

            var result = Controller.Edit(updateModel).Result as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            AudioProcess.VerifyAllExpectations();
            AudioAdapterSettingsMapper.VerifyAllExpectations();
        }
    }
}