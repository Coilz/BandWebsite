using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PerformanceProcessTests
{
    [TestClass]
    public class RemovePerformanceTests : PerformanceProcessTestBase
    {
        [TestMethod]
        public void When_RemovePerformance_is_called_with_a_Performance_then_RemovePerformance_on_the_BandRepository_is_called_with_that_Performance()
        {
            var performance = PerformanceCreator.CreateSingleFuture();

            BandRepository
                .Expect(repository =>
                        repository.RemovePerformance(performance))
                .Repeat.Once();
            BandRepository.Replay();

            Process.RemovePerformance(performance);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_RemovePerformance_is_called_with_a_null_value_for_Performance_then_an_ArgumentNullException_is_thrown()
        {
            Process.RemovePerformance(null);
        }
    }
}