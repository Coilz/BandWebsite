using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.VideoProcessTests
{
    [TestClass]
    public class GetVideoAdapterSettingsTests : VideoProcessTestBase
    {
        [TestMethod]
        public void When_GetVideoAdapterSettings_is_called_then_GetVideoAdapterSettings_on_the_BandRepository_is_called()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(adapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.GetAdapterSettings();

            Assert.AreEqual(adapterSettings, result);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetVideoAdapterSettings_is_called_and_no_VideoAdapterSettings_have_been_stored_then_AddVideoAdapterSettings_on_the_BandRepository_is_called()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Throw(new InvalidOperationException())
                .Repeat.Once();

            BandRepository
                .Expect(repository =>
                        repository.AddAdapterSettings(adapterSettings))
                .Return(adapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetAdapterSettings();
        }
    }
}