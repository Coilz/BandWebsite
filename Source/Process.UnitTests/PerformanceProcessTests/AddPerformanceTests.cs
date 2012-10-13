using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PerformanceProcessTests
{
    [TestClass]
    public class AddPerformanceTests : PerformanceProcessTestBase
    {
        [TestMethod]
        public void When_AddPerformance_is_called_with_a_new_Performance_then_AddPerformance_on_the_BandRepository_is_called_with_that_Performance()
        {
            var performance = PerformanceCreator.CreateSingleFuture();

            BandRepository
                .Expect(repository =>
                        repository.AddPerformance(performance))
                .Return(performance)
                .Repeat.Once();
            BandRepository.Replay();

            Process.AddPerformance(performance);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddPerformance_is_called_with_a_null_value_for_Performance_then_an_ArgumentNullException_is_thrown()
        {
            Process.AddPerformance(null);
        }
    }
}