using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.AudioProcessTests
{
    [TestClass]
    public class GetAudioTests : AudioProcessTestBase
    {
        [TestMethod]
        public void When_GetAudio_is_called_then_GetAudioAdapterSettings_on_the_BandRepository_is_called()
        {
            var tracks = AudioTrackCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            AudioAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            AudioAdapter.Replay();

            Process.GetAudioTracks();

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetAudioTracks_is_called_with_a_new_Audio_then_GetItems_on_the_AudioAdapter_is_called_with_that_Audio_and_the_stored_AudioAdapterSettings()
        {
            var tracks = AudioTrackCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            AudioAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            AudioAdapter.Replay();

            var results = Process.GetAudioTracks();

            Assert.AreEqual(tracks.Count(), results.Count());
            Assert.AreEqual(tracks.First().ResourceUri, results.First().ResourceUri);

            AudioAdapter.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetAudio_is_called_with_a_new_Audio_and_a_null_value_for_setName_then_UploadAudio_on_the_AudioAdapter_is_called_with_that_Audio_and_the_stored_AudioAdapterSettings()
        {
            var tracks = AudioTrackCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = null;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            AudioAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            AudioAdapter.Replay();

            var result = Process.GetAudioTracks();

            Assert.AreEqual(tracks.Count(), result.Count());
            Assert.AreEqual(tracks.First().ResourceUri, result.First().ResourceUri);

            AudioAdapter.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetAudio_is_called_with_a_new_Audio_and_an_empty_string_for_setName_then_UploadAudio_on_the_AudioAdapter_is_called_with_that_Audio_and_the_stored_AudioAdapterSettings()
        {
            var tracks = AudioTrackCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = string.Empty;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            AudioAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            AudioAdapter.Replay();

            var result = Process.GetAudioTracks();

            Assert.AreEqual(tracks.Count(), result.Count());
            Assert.AreEqual(tracks.First().ResourceUri, result.First().ResourceUri);

            AudioAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetAudio_is_called_and_no_AudioAdapterSettings_have_been_stored_then_an_InvalidOperationException_is_thrown_and_GetAudio_on_the_AudioAdapter_is_never_called()
        {
            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Throw(new InvalidOperationException())
                .Repeat.Once();
            BandRepository.Replay();

            AudioAdapter
                .Expect(adapter =>
                        adapter.GetItems(null, null))
                .IgnoreArguments()
                .Return(new List<AudioTrack>())
                .Repeat.Never();
            AudioAdapter.Replay();

            Process.GetAudioTracks(0, 10);

            AudioAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetAudio_is_called_and_no_OAuthAccessToken_has_been_stored_then_an_InvalidOperationException_is_thrown_and_GetAudio_on_the_AudioAdapter_is_never_called()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();
            photoAdapterSettings.OAuthAccessToken = null;
            
            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            AudioAdapter
                .Expect(adapter =>
                        adapter.GetItems(null, null))
                .IgnoreArguments()
                .Return(new List<AudioTrack>())
                .Repeat.Never();
            AudioAdapter.Replay();

            Process.GetAudioTracks(0, 10);

            AudioAdapter.VerifyAllExpectations();
        }
    }
}