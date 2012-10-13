using System.Linq;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PerformanceProcessTests
{
    [TestClass]
    public class GetPerformancesTests : PerformanceProcessTestBase
    {
        [TestMethod]
        public void When_GetPerformances_is_called_then_GetAllPerformance_on_the_BandRepository_is_called()
        {
            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandRepository
                .Expect(repository =>
                        repository.GetAllPerformances())
                .Return(performances)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.GetPerformances();

            Assert.AreEqual(performances.Count(), result.Count());
            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPerformances_is_called_with_a_valid_page_and_pageSize_then_GetPerformance_on_the_BandRepository_is_called()
        {
            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandRepository
                .Expect(repository =>
                        repository.GetPerformances(Arg<int>.Is.Anything, Arg<int>.Is.Anything))
                .Return(performances)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.GetPerformances(0, 32);

            Assert.AreEqual(performances.Count(), result.Count());
            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPastPerformances_is_called_then_GetAllPastPerformance_on_the_BandRepository_is_called()
        {
            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandRepository
                .Expect(repository =>
                        repository.GetAllPastPerformances())
                .Return(performances)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.GetPastPerformances();

            Assert.AreEqual(performances.Count(), result.Count());
            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPastPerformances_is_called_with_a_valid_page_and_pageSize_then_GetPastPerformance_on_the_BandRepository_is_called()
        {
            var futurePerformances = PerformanceCreator.CreateFutureCollection();
            var pastPerformances = PerformanceCreator.CreatePastCollection();
            var performances = pastPerformances.Union(futurePerformances);

            BandRepository
                .Expect(repository =>
                        repository.GetPastPerformances(Arg<int>.Is.Anything, Arg<int>.Is.Anything))
                .Return(performances)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.GetPastPerformances(0, 32);

            Assert.AreEqual(performances.Count(), result.Count());
            BandRepository.VerifyAllExpectations();
        }
    }
}