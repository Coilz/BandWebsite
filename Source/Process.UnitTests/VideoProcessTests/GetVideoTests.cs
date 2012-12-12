using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.VideoProcessTests
{
    [TestClass]
    public class GetVideoTests : VideoProcessTestBase
    {
        [TestMethod]
        public void When_GetVideo_is_called_then_GetVideoAdapterSettings_on_the_BandRepository_is_called()
        {
            var tracks = VideoCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            VideoAdapter.Replay();

            Process.GetVideos();

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetVideos_is_called_with_a_new_Video_then_GetItems_on_the_VideoAdapter_is_called_with_that_Video_and_the_stored_VideoAdapterSettings()
        {
            var tracks = VideoCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            VideoAdapter.Replay();

            var results = Process.GetVideos();

            Assert.AreEqual(tracks.Count(), results.Count());
            Assert.AreEqual(tracks.First().ResourceUri, results.First().ResourceUri);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetVideo_is_called_with_a_new_Video_and_a_null_value_for_setName_then_UploadVideo_on_the_VideoAdapter_is_called_with_that_Video_and_the_stored_VideoAdapterSettings()
        {
            var tracks = VideoCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = null;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            VideoAdapter.Replay();

            var result = Process.GetVideos();

            Assert.AreEqual(tracks.Count(), result.Count());
            Assert.AreEqual(tracks.First().ResourceUri, result.First().ResourceUri);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetVideo_is_called_with_a_new_Video_and_an_empty_string_for_setName_then_UploadVideo_on_the_VideoAdapter_is_called_with_that_Video_and_the_stored_VideoAdapterSettings()
        {
            var tracks = VideoCreator.CreateCollection();
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = string.Empty;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(tracks)
                .Repeat.Once();
            VideoAdapter.Replay();

            var result = Process.GetVideos();

            Assert.AreEqual(tracks.Count(), result.Count());
            Assert.AreEqual(tracks.First().ResourceUri, result.First().ResourceUri);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetVideo_is_called_and_no_VideoAdapterSettings_have_been_stored_then_an_InvalidOperationException_is_thrown_and_GetVideo_on_the_VideoAdapter_is_never_called()
        {
            var adapterSettings = AdapterSettingsCreator.CreateSingle();
            adapterSettings.OAuthAccessToken = null;
            adapterSettings.OAuthRequestToken = null;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Throw(new InvalidOperationException())
                .Repeat.Once();
            BandRepository
                .Expect(repository =>
                        repository.AddAdapterSettings(Arg<AdapterSettings>.Is.Anything))
                .Return(adapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.GetItems(null, null))
                .IgnoreArguments()
                .Return(new List<Video>())
                .Repeat.Never();
            VideoAdapter.Replay();

            Process.GetVideos(0, 10);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetVideo_is_called_and_no_OAuthAccessToken_has_been_stored_then_an_InvalidOperationException_is_thrown_and_GetVideo_on_the_VideoAdapter_is_never_called()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();
            photoAdapterSettings.OAuthAccessToken = null;
            
            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.GetItems(null, null))
                .IgnoreArguments()
                .Return(new List<Video>())
                .Repeat.Never();
            VideoAdapter.Replay();

            Process.GetVideos(0, 10);

            VideoAdapter.VerifyAllExpectations();
        }
    }
}