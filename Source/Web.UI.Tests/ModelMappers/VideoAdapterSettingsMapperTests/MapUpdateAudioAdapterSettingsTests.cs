using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.VideoAdapterSettingsMapperTests
{
    [TestClass]
    public class MapUpdateVideoAdapterSettingsTests : VideoAdapterSettingsMapperTestBase
    {
        [TestMethod]
        public void When_UpdateVideoAdapterSettings_is_mapped_to_a_VideoAdapterSettings_then_all_fields_are_mapped_correctly()
        {
            const string setName = "SetName";
            const string fullName = "FullName";
            const string userName = "UserName";
            const string userId = "UserId";

            var entity = AdapterSettingsCreator.CreateSingle();

            VideoProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(entity)
                .Repeat.Once();
            VideoProcess.Replay();

            var updateModel = new UpdateVideoAdapterSettingsModel
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

            VideoProcess.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateVideoAdapterSettings_is_mapped_to_a_VideoAdapterSettings_and_the_SetName_is_null_then_all_fields_are_mapped_correctly_and_SetName_in_the_result_is_null()
        {
            const string fullName = "FullName";
            const string userName = "UserName";
            const string userId = "UserId";

            var entity = AdapterSettingsCreator.CreateSingle();

            VideoProcess
                .Expect(process =>
                        process.GetAdapterSettings())
                .Return(entity)
                .Repeat.Once();
            VideoProcess.Replay();

            var updateModel = new UpdateVideoAdapterSettingsModel
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

            VideoProcess.VerifyAllExpectations();
        }
    }
}