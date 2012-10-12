using System;
using System.Globalization;
using Ewk.BandWebsite.Web.UI.Models.Performance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PerformanceMapperTests
{
    [TestClass]
    public class MapAddPerformanceTests : PerformanceMapperTestBase
    {
        [TestMethod]
        public void When_AddPerformance_is_mapped_to_a_Performance_then_the_values_are_mapped_correctly()
        {
            var startDateTime = DateTime.Now.AddDays(37);

            var addPerformanceModel = new AddPerformanceModel
                                          {
                                              Date = startDateTime.Date,
                                              StartTime = startDateTime,
                                              EndTime = startDateTime.AddHours(4),
                                              City = "City",
                                              VenueName = "VenueName",
                                              VenueUrl = "http://google.com",
                                              Info = "Info",
                                              Price = 15,
                                          };

            var result = Mapper.Map(addPerformanceModel);

            Assert.AreEqual(addPerformanceModel.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture), result.StartDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture), result.StartDateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.EndTime.ToString("HH:mm", CultureInfo.InvariantCulture), result.EndDateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.City, result.City);
            Assert.AreEqual(addPerformanceModel.VenueName, result.VenueName);
            Assert.AreEqual(addPerformanceModel.VenueUrl, result.VenueUri.OriginalString);
            Assert.AreEqual(addPerformanceModel.Price.ToString("{0:C}", CultureInfo.InvariantCulture), result.Price.ToString("{0:C}", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.Info, result.Info);
        }

        [TestMethod]
        public void When_AddPerformance_is_mapped_to_a_Performance_and_the_url_is_null_then_the_values_except_the_Uri_are_mapped_correctly()
        {
            var startDateTime = DateTime.Now.AddDays(37);

            var addPerformanceModel = new AddPerformanceModel
                                          {
                                              Date = startDateTime.Date,
                                              StartTime = startDateTime,
                                              EndTime = startDateTime.AddHours(4),
                                              City = "City",
                                              VenueName = "VenueName",
                                              VenueUrl = null,
                                              Info = "Info",
                                              Price = 15,
                                          };

            var result = Mapper.Map(addPerformanceModel);

            Assert.AreEqual(addPerformanceModel.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture), result.StartDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture), result.StartDateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.EndTime.ToString("HH:mm", CultureInfo.InvariantCulture), result.EndDateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.City, result.City);
            Assert.AreEqual(addPerformanceModel.VenueName, result.VenueName);
            Assert.AreEqual(addPerformanceModel.Price.ToString("{0:C}", CultureInfo.InvariantCulture), result.Price.ToString("{0:C}", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.Info, result.Info);
            Assert.AreEqual(null, result.VenueUri);
        }

        [TestMethod]
        public void When_AddPerformance_is_mapped_to_a_Performance_and_the_url_is_an_empty_string_then_the_values_except_the_Uri_are_mapped_correctly()
        {
            var startDateTime = DateTime.Now.AddDays(37);

            var addPerformanceModel = new AddPerformanceModel
                                          {
                                              Date = startDateTime.Date,
                                              StartTime = startDateTime,
                                              EndTime = startDateTime.AddHours(4),
                                              City = "City",
                                              VenueName = "VenueName",
                                              VenueUrl = string.Empty,
                                              Info = "Info",
                                              Price = 15,
                                          };

            var result = Mapper.Map(addPerformanceModel);

            Assert.AreEqual(addPerformanceModel.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture), result.StartDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.StartTime.ToString("HH:mm", CultureInfo.InvariantCulture), result.StartDateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.EndTime.ToString("HH:mm", CultureInfo.InvariantCulture), result.EndDateTime.ToString("HH:mm", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.City, result.City);
            Assert.AreEqual(addPerformanceModel.VenueName, result.VenueName);
            Assert.AreEqual(addPerformanceModel.Price.ToString("{0:C}", CultureInfo.InvariantCulture), result.Price.ToString("{0:C}", CultureInfo.InvariantCulture));
            Assert.AreEqual(addPerformanceModel.Info, result.Info);
            Assert.AreEqual(null, result.VenueUri);
        }
    }
}