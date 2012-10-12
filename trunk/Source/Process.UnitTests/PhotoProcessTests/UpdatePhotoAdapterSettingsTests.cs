using System;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PhotoProcessTests
{
    [TestClass]
    public class UpdatePhotoAdapterSettingsTests : PhotoProcessTestBase
    {
        [TestMethod]
        public void When_UpdatePhotoAdapterSettings_is_called_then_UpdatePhotoAdapterSettings_on_the_BandRepository_is_called()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.UpdateAdapterSettings(photoAdapterSettings))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.UpdateAdapterSettings(photoAdapterSettings);

            Assert.AreEqual(photoAdapterSettings, result);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdatePhotoAdapterSettings_is_called_with_null_as_parameter_then_an_ArgumentNullException_is_thrown_and_UpdatePhotoAdapterSettings_on_the_BandRepository_is_never_called()
        {
            BandRepository
                .Expect(repository =>
                        repository.UpdateAdapterSettings(Arg<AdapterSettings>.Is.Anything))
                .Repeat.Never();

            Process.UpdateAdapterSettings(null);
        }
    }
}