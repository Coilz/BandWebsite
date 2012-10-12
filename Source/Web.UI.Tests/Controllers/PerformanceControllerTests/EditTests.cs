using System;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.Performance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PerformanceControllerTests
{
    [TestClass]
    public class EditTests : PerformanceControllerTestBase
    {
        [TestMethod]
        public void When_Edit_is_called_with_an_Id_then_GetPerformance_on_IPerformanceProcess_is_called_and_the_result_is_mapped_with_PerformanceMapper()
        {
            var entity = PerformanceCreator.CreateSingle();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(entity.Id))
                .Return(entity)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var updateModel = CreateUpdatePerformanceModel(Guid.NewGuid());

            PerformanceMapper
                .Expect(mapper =>
                        mapper.MapToUpdate(
                            Arg<Performance>.Matches(settings =>
                                                     settings.Id == entity.Id)))
                .Return(updateModel)
                .Repeat.Once();
            PerformanceMapper.Replay();

            var result = Controller.Edit(entity.Id).Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as UpdatePerformanceModel;
            Assert.IsNotNull(model);

            PerformanceProcess.VerifyAllExpectations();
            PerformanceMapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Edit_is_called_with_a_model_then_Map_on_PerformanceMapper_is_called_and_the_result_is_used_to_call_UpdatePerformance_on_IPerformanceProcess_with()
        {
            var entity = PerformanceCreator.CreateSingle();

            PerformanceProcess
                .Expect(process =>
                        process.UpdatePerformance(
                            Arg<Performance>.Matches(settings =>
                                                     settings.Id == entity.Id)))
                .Return(entity)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var updateModel = CreateUpdatePerformanceModel(entity.Id);

            PerformanceMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<UpdatePerformanceModel>.Matches(m =>
                                                                m.City == entity.City &&
                                                                m.VenueName == entity.VenueName),
                            Arg<Guid>.Matches(guid => guid == entity.Id)))
                .Return(entity)
                .Repeat.Once();
            PerformanceMapper.Replay();

            var result = Controller.Edit(entity.Id, updateModel).Result as RedirectToRouteResult;
            Assert.IsNotNull(result);

            var routeValues = result.RouteValues;
            Assert.AreEqual(1, routeValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.AreEqual("action", routeValue.Key);
                Assert.AreEqual("Index", routeValue.Value);
            }

            PerformanceProcess.VerifyAllExpectations();
            PerformanceMapper.VerifyAllExpectations();
        }
    }
}