using System;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.BandProcessTests
{
    [TestClass]
    public class UpdateBandTests : BandProcessTestBase
    {
        [TestMethod]
        public void When_UpdateBand_is_called_then_UpdateBand_on_the_AppRepository_is_called()
        {
            var entity = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.UpdateBand(entity))
                .Return(entity)
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.UpdateBand(entity);

            Assert.AreEqual(entity, result);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdateBand_is_called_with_null_as_parameter_then_an_ArgumentNullException_is_thrown_and_UpdateBand_on_the_AppRepository_is_never_called()
        {
            AppRepository
                .Expect(repository =>
                        repository.UpdateBand(Arg<Band>.Is.Anything))
                .Repeat.Never();
            AppRepository.Replay();

            Process.UpdateBand(null);
        }
    }
}