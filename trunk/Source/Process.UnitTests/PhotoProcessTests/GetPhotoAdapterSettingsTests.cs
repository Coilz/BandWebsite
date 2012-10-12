using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PhotoProcessTests
{
    [TestClass]
    public class GetPhotoAdapterSettingsTests : PhotoProcessTestBase
    {
        [TestMethod]
        public void When_GetPhotoAdapterSettings_is_called_then_GetPhotoAdapterSettings_on_the_BandRepository_is_called()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.GetAdapterSettings();

            Assert.AreEqual(photoAdapterSettings, result);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPhotoAdapterSettings_is_called_and_no_PhotoAdapterSettings_have_been_stored_then_AddPhotoAdapterSettings_on_the_BandRepository_is_called()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Throw(new InvalidOperationException())
                .Repeat.Once();

            BandRepository
                .Expect(repository =>
                        repository.AddAdapterSettings(photoAdapterSettings))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetAdapterSettings();
        }
    }
}