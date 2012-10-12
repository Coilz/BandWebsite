using System;
using System.Linq;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PerformanceMapperTests
{
    [TestClass]
    public class MapToPerformanceDetailsModelTests : PerformanceMapperTestBase
    {
        [TestMethod]
        public void When_Performance_is_mapped_to_a_PerformanceDetailsModel_then_no_data_is_retrieved_from_process_classes()
        {
            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            PerformanceProcess.Replay();

            var entity = PerformanceCreator.CreateSingle();

            var result = Mapper.MapToDetail(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.CreationDate, result.CreationDate);
            Assert.AreEqual(entity.ModificationDate, result.ModificationDate);

            Assert.AreEqual(entity.StartDateTime.Date.ToLocalTime(), result.Date);
            Assert.AreEqual(entity.StartDateTime.ToLocalTime(), result.StartTime);
            Assert.AreEqual(entity.EndDateTime.ToLocalTime(), result.EndTime);
            Assert.AreEqual(entity.City, result.City);
            Assert.AreEqual(entity.VenueName, result.VenueName);
            Assert.AreEqual(entity.VenueUri.OriginalString, result.VenueUrl);
            Assert.AreEqual(entity.Info, result.Info);
            Assert.AreEqual(entity.Price, result.Price);
        }

        [TestMethod]
        public void When_a_list_of_Performances_is_mapped_to_a_PerformanceDetailsModels_then_no_data_is_retrieved_from_process_classes()
        {
            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            PerformanceProcess.Replay();

            var entities = PerformanceCreator.CreateCollection();

            var result = Mapper.Map(entities);

            foreach (var entity in entities)
            {
                Assert.IsTrue(result.Items
                                  .Single(model =>
                                          model.Id == entity.Id &&
                                          model.ModificationDate == entity.ModificationDate &&
                                          model.CreationDate == entity.CreationDate &&

                                          model.Date == entity.StartDateTime.Date.ToLocalTime() &&
                                          model.StartTime == entity.StartDateTime.ToLocalTime() &&
                                          model.EndTime == entity.EndDateTime.ToLocalTime() &&
                                          model.City == entity.City &&
                                          model.Info == entity.Info &&
                                          model.Price == entity.Price &&
                                          model.VenueName == entity.VenueName &&
                                          model.VenueUrl == entity.VenueUri.OriginalString) != null);
            }
        }

        [TestMethod]
        public void When_Performance_is_mapped_to_a_PerformanceDetailsModel_and_the_venueUri_is_null_then_the_resultUrl_is_stringEmpty()
        {
            PerformanceProcess
                .Expect(process =>
                        process.GetPerformance(Arg<Guid>.Is.Anything))
                .Repeat.Never();
            PerformanceProcess.Replay();

            var entity = PerformanceCreator.CreateSingle();
            entity.VenueUri = null;

            var result = Mapper.MapToDetail(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.CreationDate, result.CreationDate);
            Assert.AreEqual(entity.ModificationDate, result.ModificationDate);

            Assert.AreEqual(entity.StartDateTime.Date.ToLocalTime(), result.Date);
            Assert.AreEqual(entity.StartDateTime.ToLocalTime(), result.StartTime);
            Assert.AreEqual(entity.EndDateTime.ToLocalTime(), result.EndTime);
            Assert.AreEqual(entity.City, result.City);
            Assert.AreEqual(entity.VenueName, result.VenueName);
            Assert.AreEqual(string.Empty, result.VenueUrl);
            Assert.AreEqual(entity.Info, result.Info);
            Assert.AreEqual(entity.Price, result.Price);
        }
    }
}