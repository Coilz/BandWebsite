using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BandMapperTests
{
    [TestClass]
    public class MapToAboutUpdateModelTests : BandMapperTestBase
    {
        [TestMethod]
        public void When_Band_is_mapped_to_an_UpdateModel_then_all_corresponding_fields_are_mapped()
        {
            var entity = BandCreator.CreateSingle();

            var result = Mapper.MapToAboutUpdateModel(entity);

            Assert.AreEqual(entity.Founded.ToLocalTime(), result.DateFounded);
            Assert.AreEqual(entity.Description, result.Info);
        }

        [TestMethod]
        public void When_Band_is_mapped_to_an_UpdateModel_and_the_Description_is_null_then_all_corresponding_fields_are_mapped_and_the_result_Description_is_stringEmpty()
        {
            var entity = BandCreator.CreateSingle();
            entity.Description = null;

            var result = Mapper.MapToAboutUpdateModel(entity);

            Assert.AreEqual(entity.Founded.ToLocalTime(), result.DateFounded);
            Assert.AreEqual(string.Empty, result.Info);
        }
    }
}