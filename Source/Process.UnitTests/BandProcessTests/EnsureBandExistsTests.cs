using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.BandProcessTests
{
    [TestClass]
    public class EnsureBandExistsTests : BandProcessTestBase
    {
        [TestMethod]
        public void When_EnsureBandExists_is_called_and_one_Band_exists_then_only_GetAllBands_on_the_AppRepository_is_called()
        {
            var band = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.GetAllBands())
                .Return(new List<Band> {band})
                .Repeat.Once();
            AppRepository
                .Expect(repository =>
                        repository.AddBand(Arg<Band>.Is.NotNull))
                .Repeat.Never();
            AppRepository.Replay();

            var result = Process.EnsureBandExists();

            Assert.AreEqual(band.Id, result.Id);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_EnsureBandExists_is_called_and_more_than_one_Band_exists_then_an_Exception_is_thrown()
        {
            var bands = BandCreator.CreateCollection();

            AppRepository
                .Expect(repository =>
                        repository.GetAllBands())
                .Return(bands)
                .Repeat.Once();
            AppRepository
                .Expect(repository =>
                        repository.AddBand(Arg<Band>.Is.NotNull))
                .Repeat.Never();
            AppRepository.Replay();

            Process.EnsureBandExists();
        }

        [TestMethod]
        public void When_EnsureBandExists_is_called_and_no_Band_exists_then_AddBand_on_the_AppRepository_is_called_with_a_newly_created_Band()
        {
            Band band = null;

            AppRepository
                .Expect(repository =>
                        repository.GetAllBands())
                .Return(new List<Band>())
                .Repeat.Once();
            AppRepository
                .Expect(repository =>
                        repository.AddBand(Arg<Band>.Is.NotNull))
                .WhenCalled(invocation => { band = (Band) invocation.Arguments[0]; })
                .Return(band)
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.EnsureBandExists();

            Assert.IsNotNull(band);
            Assert.IsNotNull(result);
            Assert.AreEqual(band.Id, result.Id);
            Assert.IsFalse(string.IsNullOrEmpty(result.Passphrase));
            Assert.IsFalse(string.IsNullOrEmpty(result.InitVector));
            Assert.IsFalse(string.IsNullOrEmpty(result.SaltValue));

            AppRepository.VerifyAllExpectations();
        }
    }
}