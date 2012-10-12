using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BandMapperTests
{
    [TestClass]
    public class MapToBandDetailsModelTests : BandMapperTestBase
    {
        [TestMethod]
        public void When_Band_is_mapped_to_a_DetailsModel_then_no_data_is_retrieved_from_process_classes()
        {
            BandProcess
                .Expect(process =>
                        process.GetBand())
                .Repeat.Never();
            BandProcess.Replay();

            var entity = BandCreator.CreateSingle();

            var result = Mapper.MapToAboutModel(entity);

            Assert.AreEqual(entity.Founded.ToLocalTime(), result.DateFounded);
            Assert.AreEqual(entity.Description, result.Info);
        }

        [TestMethod]
        public void When_Band_is_mapped_to_a_DetailsModel_and_the_Description_is_null_then_the_Info_is_stringEmpty()
        {
            BandProcess
                .Expect(process =>
                        process.GetBand())
                .Repeat.Never();
            BandProcess.Replay();

            var entity = BandCreator.CreateSingle();
            entity.Description = null;

            var result = Mapper.MapToAboutModel(entity);

            Assert.AreEqual(entity.Founded.ToLocalTime(), result.DateFounded);
            Assert.AreEqual(string.Empty, result.Info);
        }
    }
}