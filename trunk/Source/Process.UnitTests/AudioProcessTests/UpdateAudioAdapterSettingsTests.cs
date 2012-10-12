using System;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.AudioProcessTests
{
    [TestClass]
    public class UpdateAudioAdapterSettingsTests : AudioProcessTestBase
    {
        [TestMethod]
        public void When_UpdateAudioAdapterSettings_is_called_then_UpdateAudioAdapterSettings_on_the_BandRepository_is_called()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.UpdateAdapterSettings(adapterSettings))
                .Return(adapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.UpdateAdapterSettings(adapterSettings);

            Assert.AreEqual(adapterSettings, result);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdateAudioAdapterSettings_is_called_with_null_as_parameter_then_an_ArgumentNullException_is_thrown_and_UpdateAudioAdapterSettings_on_the_BandRepository_is_never_called()
        {
            BandRepository
                .Expect(repository =>
                        repository.UpdateAdapterSettings(Arg<AdapterSettings>.Is.Anything))
                .Repeat.Never();

            Process.UpdateAdapterSettings(null);
        }
    }
}