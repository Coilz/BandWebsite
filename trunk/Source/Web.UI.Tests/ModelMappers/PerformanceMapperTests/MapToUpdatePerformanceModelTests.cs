using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PerformanceMapperTests
{
    [TestClass]
    public class MapToUpdatePerformanceModelTests : PerformanceMapperTestBase
    {
        [TestMethod]
        public void When_Performance_is_mapped_to_an_UpdatePerformanceModel_then_all_corresponding_fields_are_mapped()
        {
            var entity = PerformanceCreator.CreateSingle();

            var result = Mapper.MapToUpdate(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.StartDateTime.Date.ToLocalTime(), result.Date);
            Assert.AreEqual(entity.StartDateTime.ToLocalTime(), result.StartTime);
            Assert.AreEqual(entity.EndDateTime.ToLocalTime(), result.EndTime);
            Assert.AreEqual(entity.City, result.City);
            Assert.AreEqual(entity.Info, result.Info);
            Assert.AreEqual(entity.Price, result.Price);
            Assert.AreEqual(entity.VenueName, result.VenueName);
            Assert.AreEqual(entity.VenueUri.OriginalString, result.VenueUrl);
        }

        [TestMethod]
        public void When_Performance_is_mapped_to_an_UpdatePerformanceModel_and_the_VenueUri_is_null_then_all_corresponding_fields_are_mapped_and_the_result_VenueUrl_is_stringEmpty()
        {
            var entity = PerformanceCreator.CreateSingle();
            entity.VenueUri = null;

            var result = Mapper.MapToUpdate(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.StartDateTime.Date.ToLocalTime(), result.Date);
            Assert.AreEqual(entity.StartDateTime.ToLocalTime(), result.StartTime);
            Assert.AreEqual(entity.EndDateTime.ToLocalTime(), result.EndTime);
            Assert.AreEqual(entity.City, result.City);
            Assert.AreEqual(entity.Info, result.Info);
            Assert.AreEqual(entity.Price, result.Price);
            Assert.AreEqual(entity.VenueName, result.VenueName);
            Assert.AreEqual(string.Empty, result.VenueUrl);
        }
    }
}