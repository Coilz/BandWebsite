using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.PhotoAdapterSettingsMapperTests
{
    [TestClass]
    public class MapToPhotoAdapterSettingsDetailsModelTests : PhotoAdapterSettingsMapperTestBase
    {
        [TestMethod]
        public void When_PhotoAdapterSettings_is_mapped_to_a_PhotoAdapterSettingsDetailsModel_then_all_data_is_mapped_correct()
        {
            var entity = AdapterSettingsCreator.CreateSingle();

            var result = Mapper.MapToDetail(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.CreationDate, result.CreationDate);
            Assert.AreEqual(entity.ModificationDate, result.ModificationDate);
            Assert.AreEqual(entity.SetName, result.SetName);

            Assert.AreEqual(entity.OAuthAccessToken.FullName, result.FullName);
            Assert.AreEqual(entity.OAuthAccessToken.UserId, result.UserId);
            Assert.AreEqual(entity.OAuthAccessToken.Username, result.UserName);
        }

        [TestMethod]
        public void When_PhotoAdapterSettings_is_mapped_to_a_PhotoAdapterSettingsDetailsModel_and_the_ApiKey_is_null_then_the_result_ApiKey_is_null()
        {
            var entity = AdapterSettingsCreator.CreateSingle();
            entity.SetName = null;

            var result = Mapper.MapToDetail(entity);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(entity.CreationDate, result.CreationDate);
            Assert.AreEqual(entity.ModificationDate, result.ModificationDate);

            Assert.AreEqual(entity.SetName, result.SetName);
            Assert.AreEqual(entity.OAuthAccessToken.UserId, result.UserId);
        }
    }
}