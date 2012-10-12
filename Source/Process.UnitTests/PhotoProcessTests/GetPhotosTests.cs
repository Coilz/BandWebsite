using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Domain.BandModel;
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
            const string photoUrl = "http://www.photos.com/myphoto";
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(new List<string>{photoUrl})
                .Repeat.Once();
            PhotoAdapter.Replay();

            Process.GetPhotos();

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPhotos_is_called_with_a_new_Photo_then_UploadPhoto_on_the_PhotoAdapter_is_called_with_that_Photo_and_the_stored_PhotoAdapterSettings()
        {
            const string photoUrl = "http://www.photos.com/myphoto";
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(new List<string> { photoUrl })
                .Repeat.Once();
            PhotoAdapter.Replay();

            var result = Process.GetPhotos();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(photoUrl, result.First().OriginalString);

            PhotoAdapter.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPhotos_is_called_with_a_new_Photo_and_a_null_value_for_setName_then_UploadPhoto_on_the_PhotoAdapter_is_called_with_that_Photo_and_the_stored_PhotoAdapterSettings()
        {
            const string photoUrl = "http://www.photos.com/myphoto";
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = null;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(new List<string> { photoUrl })
                .Repeat.Once();
            PhotoAdapter.Replay();

            var result = Process.GetPhotos();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(photoUrl, result.First().OriginalString);

            PhotoAdapter.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetPhotos_is_called_with_a_new_Photo_and_an_empty_string_for_setName_then_UploadPhoto_on_the_PhotoAdapter_is_called_with_that_Photo_and_the_stored_PhotoAdapterSettings()
        {
            const string photoUrl = "http://www.photos.com/myphoto";
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = string.Empty;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            PhotoAdapter
                .Expect(adapter =>
                        adapter.GetItems(entity.SetName, entity.OAuthAccessToken))
                .Return(new List<string> { photoUrl })
                .Repeat.Once();
            PhotoAdapter.Replay();

            var result = Process.GetPhotos();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(photoUrl, result.First().OriginalString);

            PhotoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetPhotos_is_called_and_no_PhotoAdapterSettings_have_been_stored_then_an_InvalidOperationException_is_thrown_and_GetPhotos_on_the_PhotoAdapter_is_never_called()
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
                .Return(new List<string>())
                .Repeat.Never();
            PhotoAdapter.Replay();

            Process.GetPhotos();

            PhotoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_GetPhotos_is_called_and_no_OAuthAccessToken_has_been_stored_then_an_InvalidOperationException_is_thrown_and_GetPhotos_on_the_PhotoAdapter_is_never_called()
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
                .Return(new List<string>())
                .Repeat.Never();
            PhotoAdapter.Replay();

            Process.GetPhotos();

            PhotoAdapter.VerifyAllExpectations();
        }
    }
}