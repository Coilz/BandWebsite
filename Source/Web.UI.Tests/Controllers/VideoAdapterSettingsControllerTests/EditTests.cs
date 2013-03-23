using System;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.Common.Models.VideoAdapterSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.VideoAdapterSettingsControllerTests
{
    [TestClass]
    public class EditTests : VideoAdapterSettingsControllerTestBase
    {
        [TestMethod]
        public void When_Edit_is_called_with_an_Id_then_GetVideoAdapterSettings_on_IVideoAdapterSettingsProcess_is_called_and_the_result_is_mapped_with_VideoAdapterSettingsMapper()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            VideoProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(adapterSettings)
                .Repeat.Once();
            VideoProcess.Replay();

            var updateModel = CreateUpdateVideoAdapterSettingsModel(Guid.NewGuid());

            VideoAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.MapToUpdate(
                            Arg<AdapterSettings>.Matches(settings => settings.Id == adapterSettings.Id)))
                .Return(updateModel)
                .Repeat.Once();
            VideoAdapterSettingsMapper.Replay();

            var result = Controller.Edit().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as UpdateVideoAdapterSettingsModel;
            Assert.IsNotNull(model);

            VideoProcess.VerifyAllExpectations();
            VideoAdapterSettingsMapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Edit_is_called_with_a_model_then_Map_on_VideoAdapterSettingsMapper_is_called_and_the_result_is_used_to_call_UpdateVideoAdapterSettings_on_IVideoAdapterSettingsProcess_with()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            VideoProcess
                .Expect(process =>
                        process.UpdateAdapterSettings(
                            Arg<AdapterSettings>.Matches(settings =>
                                                              settings.Id == adapterSettings.Id)))
                .Return(adapterSettings)
                .Repeat.Once();
            VideoProcess.Replay();

            var updateModel = CreateUpdateVideoAdapterSettingsModel(adapterSettings.Id);

            VideoAdapterSettingsMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<UpdateVideoAdapterSettingsModel>.Matches(m => m.SetName == adapterSettings.SetName)))
                .Return(adapterSettings)
                .Repeat.Once();
            VideoAdapterSettingsMapper.Replay();

            var result = Controller.Edit(updateModel).Result as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            VideoProcess.VerifyAllExpectations();
            VideoAdapterSettingsMapper.VerifyAllExpectations();
        }
    }
}