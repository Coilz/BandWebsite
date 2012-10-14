using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Domain.Dto;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.PhotoProcessTests
{
    [TestClass]
    public class GetPhotosTests : PhotoProcessTestBase
    {
        [TestMethod]
        public void When_GetPhotos_is_called_then_GetPhotoAdapterSettings_on_the_BandRepository_is_called()
        {
            var photos = PhotoCreator.CreateCollection();
            var settings = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(settings)
                .Repeat.Once();
            BandRepository.Replay();

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(settings.SetName, settings.OAuthAccessToken))
                .Return(photos)
                .Repeat.Once();
            PhotoAdapter.Replay();

            var result = Process.GetPhotos();

            Assert.AreEqual(photos.Count(), result.Count());
            Assert.AreEqual(photos.First(), result.First());
            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetPhotos_is_called_and_no_PhotoAdapterSettings_have_been_stored_then_an_AuthorizationException_is_thrown_and_GetPhotos_on_the_PhotoAdapter_is_never_called()
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

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(null, null))
                .IgnoreArguments()
                .Repeat.Never();
            PhotoAdapter.Replay();

            Process.GetPhotos();

            PhotoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetPhotos_is_called_and_no_OAuthAccessToken_has_been_stored_then_an_AuthorizationException_is_thrown_and_GetPhotos_on_the_PhotoAdapter_is_never_called()
        {
            var photoAdapterSettings = AdapterSettingsCreator.CreateSingle();
            photoAdapterSettings.OAuthAccessToken = null;
            
            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(photoAdapterSettings)
                .Repeat.Once();
            BandRepository.Replay();

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(null, null))
                .IgnoreArguments()
                .Repeat.Never();
            PhotoAdapter.Replay();

            Process.GetPhotos();

            PhotoAdapter.VerifyAllExpectations();
        }
    }
}