using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PerformanceProcessTests
{
    [TestClass]
    public class GetPerformanceTests : PerformanceProcessTestBase
    {
        [TestMethod]
        public void When_GetPerformance_is_called_with_a_valid_Guid_then_GetPerformance_on_the_BandRepository_is_called_with_that_Guid()
        {
            var performance = PerformanceCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetPerformance(performance.Id))
                .Return(performance)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetPerformance(performance.Id);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_GetPerformance_is_called_with_an_invalid_Guid_then_an_ArgumentNullException_is_thrown()
        {
            Process.GetPerformance(Guid.Empty);
        }
    }
}