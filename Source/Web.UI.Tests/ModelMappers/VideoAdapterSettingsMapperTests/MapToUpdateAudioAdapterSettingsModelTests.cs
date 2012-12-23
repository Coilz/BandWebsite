using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.VideoAdapterSettingsMapperTests
{
    [TestClass]
    public class MapToUpdateVideoAdapterSettingsModelTests : VideoAdapterSettingsMapperTestBase
    {
        [TestMethod]
        public void When_VideoAdapterSettings_is_mapped_to_an_UpdateVideoAdapterSettingsModel_then_all_corresponding_fields_are_mapped()
        {
            var entity = AdapterSettingsCreator.CreateSingle();

            var result = Mapper.MapToUpdate(entity);

            Assert.AreEqual(entity.SetName, result.SetName);
            Assert.AreEqual(entity.OAuthAccessToken.FullName, result.FullName);
            Assert.AreEqual(entity.OAuthAccessToken.UserId, result.UserId);
            Assert.AreEqual(entity.OAuthAccessToken.Username, result.UserName);
        }

        [TestMethod]
        public void When_VideoAdapterSettings_is_mapped_to_an_UpdateVideoAdapterSettingsModel_and_the_SetName_is_null_then_all_corresponding_fields_are_mapped_and_the_result_SetName_is_null()
        {
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = null;

            var result = Mapper.MapToUpdate(entity);

            Assert.AreEqual(entity.SetName, result.SetName);
            Assert.AreEqual(entity.OAuthAccessToken.UserId, result.UserId);
        }
    }
}