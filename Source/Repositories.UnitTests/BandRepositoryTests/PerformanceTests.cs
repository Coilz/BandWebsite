using System;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.BandRepositoryTests
{
    [TestClass]
    public class PerformanceTests : BandRepositoryTestBase
    {
        [TestMethod]
        public void When_GetPerformance_is_called_with_an_Id_then_the_Performance_with_the_corresponding_Id_is_retrieved_from_the_collection()
        {
            var performances = PerformanceCreator.CreateFutureCollection();
            var performanceId = performances.ElementAt(2).Id;

            BandCatalog
                .Expect(catalog => catalog.Performances)
                .Return(performances)
                .Repeat.Once();
            BandCatalog.Replay();

            var performance = Repository.GetPerformance(performanceId);

            Assert.IsNotNull(performance);
            Assert.AreEqual(performanceId, performance.Id);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetPerformance_is_called_with_an_Id_and_there_is_no_Performance_in_the_collection_with_that_Id_then_an_InvalidOperationException_is_thrown()
        {
            var performances = PerformanceCreator.CreateFutureCollection();
            var performanceId = Guid.NewGuid();

            BandCatalog
                .Expect(catalog => catalog.Performances)
                .Return(performances)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.GetPerformance(performanceId);
        }

        [TestMethod]
        public void When_GetAllPerformances_is_called_then_all_future_Performances_are_retrieved_from_the_collection()
        {
            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandCatalog
                .Expect(catalog => catalog.Performances)
                .Return(performances)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAllFuturePerformances();

            Assert.IsNotNull(result);
            Assert.AreEqual(futurePerformances.Count(), result.Count());
            Assert.AreEqual(futurePerformances.First(), result.First());

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPerformances_is_called_then_the_specified_number_of_future_Performances_are_retrieved_from_the_collection()
        {
            const int page = 0;
            const int pageSize = 3;

            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandCatalog
                .Expect(catalog => catalog.Performances)
                .Return(performances)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetFuturePerformances(page, pageSize);

            Assert.IsNotNull(result);
            Assert.AreEqual(pageSize, result.Count());
            Assert.AreEqual(futurePerformances.First(), result.First());

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetAllPastPerformances_is_called_then_all_past_Performances_are_retrieved_from_the_collection()
        {
            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandCatalog
                .Expect(catalog => catalog.Performances)
                .Return(performances)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAllPastPerformances();

            Assert.IsNotNull(result);
            Assert.AreEqual(pastPerformances.Count(), result.Count());
            Assert.AreEqual(pastPerformances.First(), result.First());

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPastPerformances_is_called_then_the_specified_number_of_past_Performances_are_retrieved_from_the_collection()
        {
            const int page = 0;
            const int pageSize = 3;

            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandCatalog
                .Expect(catalog => catalog.Performances)
                .Return(performances)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetPastPerformances(page, pageSize);

            Assert.IsNotNull(result);
            Assert.AreEqual(pageSize, result.Count());
            Assert.AreEqual(pastPerformances.First(), result.First());

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_AddPerformance_is_called_with_a_Performance_then_the_Performance_is_added_to_the_collection()
        {
            var performance = PerformanceCreator.CreateSingleFuture();

            BandCatalog
                .Expect(catalog => catalog.Add(performance))
                .Return(performance)
                .Repeat.Once();
            BandCatalog.Replay();

            performance = Repository.AddPerformance(performance);

            Assert.IsNotNull(performance);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddPerformance_is_called_with_null_as_Performance_then_an_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog => catalog.Add(Arg<Performance>.Is.Anything))
                .Return(null)
                .Repeat.Never();
            BandCatalog.Replay();

            Repository.AddPerformance(null);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdatePerformance_is_called_with_a_Performance_then_the_Performance_is_updated_in_the_collection()
        {
            var performance = PerformanceCreator.CreateSingleFuture();

            BandCatalog
                .Expect(catalog => catalog.Update(performance))
                .Return(performance)
                .Repeat.Once();
            BandCatalog.Replay();

            performance = Repository.UpdatePerformance(performance);

            Assert.IsNotNull(performance);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdatePerformance_is_called_with_null_as_Performance_then_an_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog => catalog.Update(Arg<Performance>.Is.Anything))
                .Return(null)
                .Repeat.Never();
            BandCatalog.Replay();

            Repository.UpdatePerformance(null);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_RemovePerformance_is_called_with_a_Performance_then_the_Performance_is_removed_from_the_collection()
        {
            var performance = PerformanceCreator.CreateSingleFuture();

            BandCatalog
                .Expect(catalog => catalog.Remove(performance))
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.RemovePerformance(performance);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_RemovePerformance_is_called_with_null_as_Performance_then_an_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog => catalog.Remove(Arg<Performance>.Is.Anything))
                .Repeat.Never();
            BandCatalog.Replay();

            Repository.RemovePerformance(null);

            BandCatalog.VerifyAllExpectations();
        }
    }
}