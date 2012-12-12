using System;
using System.IO;
using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.VideoProcessTests
{
    [TestClass]
    public class AddVideoTests : VideoProcessTestBase
    {
        [TestMethod]
        public void When_AddVideo_is_called_with_a_new_Video_then_GetVideoAdapterSettings_on_the_BandRepository_is_called()
        {
            var photo = new MemoryStream();
            const string fileName = "photo.jpg";
            const string photoUrl = "http://www.photos.com/myphoto";
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.UploadItem(photo, entity.SetName, fileName, entity.OAuthAccessToken))
                .Return(photoUrl)
                .Repeat.Once();
            VideoAdapter.Replay();

            Process.AddVideo(photo, fileName);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_AddVideo_is_called_with_a_new_Video_then_UploadVideo_on_the_VideoAdapter_is_called_with_that_Video_and_the_stored_VideoAdapterSettings()
        {
            var photo = new MemoryStream();
            const string fileName = "photo.jpg";
            const string photoId = "http://www.photos.com/myphoto";
            var entity = AdapterSettingsCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.UploadItem(photo, entity.SetName, fileName, entity.OAuthAccessToken))
                .Return(photoId)
                .Repeat.Once();
            VideoAdapter.Replay();

            var result = Process.AddVideo(photo, fileName);

            Assert.AreEqual(photoId, result);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_AddVideo_is_called_and_no_VideoAdapterSettings_have_been_stored_then_an_InvalidOperationException_is_thrown_and_UploadVideo_on_the_VideoAdapter_is_never_called()
        {
            var photo = new MemoryStream();
            const string fileName = "photo.jpg";

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Throw(new InvalidOperationException())
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.UploadItem(null, null, null, null))
                .IgnoreArguments()
                .Return("")
                .Repeat.Never();
            VideoAdapter.Replay();

            Process.AddVideo(photo, fileName);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(AuthorizationException))]
        public void When_AddVideo_is_called_and_no_OAuthAccessToken_has_been_stored_then_an_InvalidOperationException_is_thrown_and_UploadVideo_on_the_VideoAdapter_is_never_called()
        {
            var photo = new MemoryStream();
            const string fileName = "photo.jpg";
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.OAuthAccessToken = null;

            BandRepository
                .Expect(repository =>
                        repository.GetAdapterSettings(Arg<string>.Is.Anything))
                .Return(entity)
                .Repeat.Once();
            BandRepository.Replay();

            VideoAdapter
                .Expect(adapter =>
                        adapter.UploadItem(null, null, null, null))
                .IgnoreArguments()
                .Return("")
                .Repeat.Never();
            VideoAdapter.Replay();

            Process.AddVideo(photo, fileName);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddVideo_is_called_with_a_null_value_for_Video_then_an_ArgumentNullException_is_thrown()
        {
            const string fileName = "photo.jpg";

            Process.AddVideo(null, fileName);
        }

        [TestMethod]
        public void When_AddVideo_is_called_with_a_new_Video_and_a_null_value_for_setName_then_UploadVideo_on_the_VideoAdapter_is_called_with_that_Video_and_the_stored_VideoAdapterSettings()
        {
            var photo = new MemoryStream();
            const string fileName = "photo.jpg";
            const string photoId = "http://www.photos.com/myphoto";
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
                        adapter.UploadItem(photo, entity.SetName, fileName, entity.OAuthAccessToken))
                .Return(photoId)
                .Repeat.Once();
            VideoAdapter.Replay();

            var result = Process.AddVideo(photo, fileName);

            Assert.AreEqual(photoId, result);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddVideo_is_called_with_a_null_value_for_fileName_then_an_ArgumentNullException_is_thrown()
        {
            var photo = new MemoryStream();

            Process.AddVideo(photo, null);
        }

        [TestMethod]
        public void When_AddVideo_is_called_with_a_new_Video_and_an_empty_string_for_setName_then_UploadVideo_on_the_VideoAdapter_is_called_with_that_Video_and_the_stored_VideoAdapterSettings()
        {
            var photo = new MemoryStream();
            const string fileName = "photo.jpg";
            const string photoId = "http://www.photos.com/myphoto";
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
                        adapter.UploadItem(photo, entity.SetName, fileName, entity.OAuthAccessToken))
                .Return(photoId)
                .Repeat.Once();
            VideoAdapter.Replay();

            var result = Process.AddVideo(photo, fileName);

            Assert.AreEqual(photoId, result);

            VideoAdapter.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddVideo_is_called_with_an_Empty_string_value_for_fileName_then_an_ArgumentNullException_is_thrown()
        {
            var photo = new MemoryStream();

            Process.AddVideo(photo, string.Empty);
        }
    }
}