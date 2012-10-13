using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PerformanceProcessTests
{
    [TestClass]
    public class GetPerformancesTests : PerformanceProcessTestBase
    {
        [TestMethod]
        public void When_GetPerformances_is_called_with_a_valid_Guid_then_GetPerformance_on_the_BandRepository_is_called_with_that_Guid()
        {
            var performances = PerformanceCreator.CreateFutureCollection();

            BandRepository
                .Expect(repository =>
                        repository.GetAllPerformances())
                .Return(performances)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetPerformances();

            BandRepository.VerifyAllExpectations();
        }
    }
}