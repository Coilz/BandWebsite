using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.BandProcessTests
{
    [TestClass]
    public class GetBandTests : BandProcessTestBase
    {
        [TestMethod]
        public void When_GetBand_is_called_then_GetBand_on_the_AppRepository_is_called()
        {
            var entity = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.GetBand())
                .Return(entity)
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.GetBand();

            Assert.AreEqual(entity, result);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetBand_is_called_and_no_Band_has_been_stored_then_an_InvalidOperationException_is_thrown()
        {
            AppRepository
                .Expect(repository =>
                        repository.GetBand())
                .Throw(new InvalidOperationException())
                .Repeat.Once();
            AppRepository.Replay();

            Process.GetBand();
        }
    }
}