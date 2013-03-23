using System;
using System.Globalization;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.Common.Models.Performance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PerformanceMapperTests
{
    [TestClass]
    public class MapUpdatePerformanceTests : PerformanceMapperTestBase
    {
        [TestMethod]
        public void When_UpdatePerformance_is_mapped_to_a_Performance_then_all_fields_are_mapped_correctly()
        {
            var startDateTime = DateTime.Now.AddDays(37);
            var endDateTime = startDateTime.AddHours(4);

            var entity = PerformanceCreator.CreateSingleFuture();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(entity.Id))
                .Return(entity)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var updateModel = new UpdatePerformanceModel
                                  {
                                      Id = entity.Id,
                                      Date = startDateTime.Date,
                                      StartTime = startDateTime,
                                      EndTime = endDateTime,
                                      City = "City",
                                      Info = "Info",
                                      Price = DateTime.UtcNow.Ticks,
                                      VenueName = "VenueName",
                                  };

            var result = Mapper.Map(updateModel, entity.Id);

            Assert.AreEqual(entity.Id, result.Id, "Id not correct");
            Assert.AreEqual(updateModel.Date.ToString("ddMMyyyy", CultureInfo.InvariantCulture), result.StartDateTime.ToString("ddMMyyyy", CultureInfo.InvariantCulture), "StartDateTime not correct");
            Assert.AreEqual(updateModel.StartTime.ToString("HHmm", CultureInfo.InvariantCulture), result.StartDateTime.ToString("HHmm", CultureInfo.InvariantCulture), "StartDateTime not correct");
            Assert.AreEqual(updateModel.EndTime.ToString("HHmm", CultureInfo.InvariantCulture), result.EndDateTime.ToString("HHmm", CultureInfo.InvariantCulture), "EndDateTime not correct");
            Assert.AreEqual(updateModel.City, result.City, "City not correct");
            Assert.AreEqual(updateModel.Info, result.Info, "Info not correct");
            Assert.AreEqual(updateModel.Price, result.Price, "Proce not correct");
            Assert.AreEqual(updateModel.VenueName, result.VenueName, "VenueName not correct");

            PerformanceProcess.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdatePerformance_is_mapped_to_a_Performance_and_the_VenueUrl_is_stringEmpty_then_all_fields_are_mapped_correctly_and_VenueUri_in_the_result_is_null()
        {
            var startDateTime = DateTime.Now.AddDays(37);
            var endDateTime = startDateTime.AddHours(4);

            var entity = PerformanceCreator.CreateSingleFuture();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(entity.Id))
                .Return(entity)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var updateModel = new UpdatePerformanceModel
                                  {
                                      Id = entity.Id,
                                      Date = startDateTime.Date,
                                      StartTime = startDateTime,
                                      EndTime = endDateTime,
                                      City = "City",
                                      Info = "Info",
                                      Price = DateTime.UtcNow.Ticks,
                                      VenueUrl = string.Empty,
                                  };

            var result = Mapper.Map(updateModel, entity.Id);

            Assert.AreEqual(entity.Id, result.Id, "Id not correct");
            Assert.AreEqual(updateModel.Date.ToString("ddMMyyyy", CultureInfo.InvariantCulture), result.StartDateTime.ToString("ddMMyyyy", CultureInfo.InvariantCulture), "StartDateTime not correct");
            Assert.AreEqual(updateModel.StartTime.ToString("HHmm", CultureInfo.InvariantCulture), result.StartDateTime.ToString("HHmm", CultureInfo.InvariantCulture), "StartDateTime not correct");
            Assert.AreEqual(updateModel.EndTime.ToString("HHmm", CultureInfo.InvariantCulture), result.EndDateTime.ToString("HHmm", CultureInfo.InvariantCulture), "EndDateTime not correct");
            Assert.AreEqual(updateModel.City, result.City, "City not correct");
            Assert.AreEqual(updateModel.Info, result.Info, "Info not correct");
            Assert.AreEqual(updateModel.Price, result.Price, "Proce not correct");
            Assert.AreEqual(updateModel.VenueName, result.VenueName, "VenueName not correct");
            Assert.AreEqual(null, result.VenueUri, "VenueUri not correct");

            PerformanceProcess.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdatePerformance_is_mapped_to_a_Performance_and_the_VenueUrl_is_null_then_all_fields_are_mapped_correctly_and_VenueUri_in_the_result_is_null()
        {
            var startDateTime = DateTime.Now.AddDays(37);
            var endDateTime = startDateTime.AddHours(4);

            var entity = PerformanceCreator.CreateSingleFuture();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(entity.Id))
                .Return(entity)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var updateModel = new UpdatePerformanceModel
                                  {
                                      Id = entity.Id,
                                      Date = startDateTime.Date,
                                      StartTime = startDateTime,
                                      EndTime = endDateTime,
                                      City = "City",
                                      Info = "Info",
                                      Price = DateTime.UtcNow.Ticks,
                                      VenueUrl = null,
                                  };

            var result = Mapper.Map(updateModel, entity.Id);

            Assert.AreEqual(entity.Id, result.Id, "Id not correct");
            Assert.AreEqual(updateModel.Date.ToString("ddMMyyyy", CultureInfo.InvariantCulture), result.StartDateTime.ToString("ddMMyyyy", CultureInfo.InvariantCulture), "StartDateTime not correct");
            Assert.AreEqual(updateModel.StartTime.ToString("HHmm", CultureInfo.InvariantCulture), result.StartDateTime.ToString("HHmm", CultureInfo.InvariantCulture), "StartDateTime not correct");
            Assert.AreEqual(updateModel.EndTime.ToString("HHmm", CultureInfo.InvariantCulture), result.EndDateTime.ToString("HHmm", CultureInfo.InvariantCulture), "EndDateTime not correct");
            Assert.AreEqual(updateModel.City, result.City, "City not correct");
            Assert.AreEqual(updateModel.Info, result.Info, "Info not correct");
            Assert.AreEqual(updateModel.Price, result.Price, "Proce not correct");
            Assert.AreEqual(updateModel.VenueName, result.VenueName, "VenueName not correct");
            Assert.AreEqual(null, result.VenueUri, "VenueUri not correct");

            PerformanceProcess.VerifyAllExpectations();
        }
    }
}