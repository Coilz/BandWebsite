using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.AudioAdapterSettingsMapperTests
{
    [TestClass]
    public class MapUpdateAudioAdapterSettingsTests : AudioAdapterSettingsMapperTestBase
    {
        [TestMethod]
        public void When_UpdateAudioAdapterSettings_is_mapped_to_a_AudioAdapterSettings_then_all_fields_are_mapped_correctly()
        {
            const string setName = "SetName";
            const string fullName = "FullName";
            const string userName = "UserName";
            const string userId = "UserId";

            var entity = AdapterSettingsCreator.CreateSingle();

            AudioProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(entity)
                .Repeat.Once();
            AudioProcess.Replay();

            var updateModel = new UpdateAudioAdapterSettingsModel
                                  {
                                      FullName = fullName,
                                      UserName = userName,
                                      UserId = userId,
                                      SetName = setName,
                                  };

            var result = Mapper.Map(updateModel);

            Assert.AreEqual(setName, result.SetName);
            Assert.AreNotEqual(fullName, result.OAuthAccessToken.FullName);
            Assert.AreNotEqual(userName, result.OAuthAccessToken.Username);
            Assert.AreNotEqual(userId, result.OAuthAccessToken.UserId);

            AudioProcess.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateAudioAdapterSettings_is_mapped_to_a_AudioAdapterSettings_and_the_SetName_is_null_then_all_fields_are_mapped_correctly_and_SetName_in_the_result_is_null()
        {
            const string fullName = "FullName";
            const string userName = "UserName";
            const string userId = "UserId";

            var entity = AdapterSettingsCreator.CreateSingle();

            AudioProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(entity)
                .Repeat.Once();
            AudioProcess.Replay();

            var updateModel = new UpdateAudioAdapterSettingsModel
                                  {
                                      FullName = fullName,
                                      UserName = userName,
                                      UserId = userId,
                                      SetName = null,
                                  };

            var result = Mapper.Map(updateModel);

            Assert.AreEqual(null, result.SetName);
            Assert.AreNotEqual(fullName, result.OAuthAccessToken.FullName);
            Assert.AreNotEqual(userName, result.OAuthAccessToken.Username);
            Assert.AreNotEqual(userId, result.OAuthAccessToken.UserId);

            AudioProcess.VerifyAllExpectations();
        }
    }
}