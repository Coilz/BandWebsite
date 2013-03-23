using System;
using System.Globalization;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.Common.Models.Home;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.BandMapperTests
{
    [TestClass]
    public class MapUpdateAboutModelTests : BandMapperTestBase
    {
        [TestMethod]
        public void When_UpdateModel_is_mapped_to_an_Entity_then_all_fields_are_mapped_correctly()
        {
            var entity = BandCreator.CreateSingle();

            BandProcess
                .Expect(process =>
                        process.GetBand())
                .Return(entity)
                .Repeat.Once();
            BandProcess.Replay();

            var updateModel = new AboutUpdateModel
                                  {
                                      DateFounded = DateTime.Now.AddMonths(-4).Date,
                                      Info = "Info",
                                  };

            var result = Mapper.Map(updateModel);

            Assert.AreEqual(entity.Id, result.Id, "Id not correct");
            Assert.AreEqual(updateModel.DateFounded.ToUniversalTime(), result.Founded, "Date founded not correct");
            Assert.AreEqual(updateModel.Info, result.Description, "Info not correct");

            BandProcess.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateModel_is_mapped_to_an_Entity_and_the_Info_is_stringEmpty_then_all_fields_are_mapped_correctly_and_Info_in_the_result_is_null()
        {
            var entity = BandCreator.CreateSingle();

            BandProcess
                .Expect(process =>
                        process.GetBand())
                .Return(entity)
                .Repeat.Once();
            BandProcess.Replay();

            var updateModel = new AboutUpdateModel
                                  {
                                      DateFounded = DateTime.Now.AddMonths(-4).Date,
                                      Info = string.Empty,
                                  };

            var result = Mapper.Map(updateModel);

            Assert.AreEqual(entity.Id, result.Id, "Id not correct");
            Assert.AreEqual(updateModel.DateFounded.ToUniversalTime(), result.Founded, "Date founded not correct");
            Assert.AreEqual(null, result.Description, "Description not correct");

            BandProcess.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateModel_is_mapped_to_an_Entity_and_the_Info_is_null_then_all_fields_are_mapped_correctly_and_Info_in_the_result_is_null()
        {
            var entity = BandCreator.CreateSingle();

            BandProcess
                .Expect(process =>
                        process.GetBand())
                .Return(entity)
                .Repeat.Once();
            BandProcess.Replay();

            var updateModel = new AboutUpdateModel
                                  {
                                      DateFounded = DateTime.Now.AddMonths(-4).Date,
                                      Info = null,
                                  };

            var result = Mapper.Map(updateModel);

            Assert.AreEqual(entity.Id, result.Id, "Id not correct");
            Assert.AreEqual(updateModel.DateFounded.ToUniversalTime(), result.Founded, "Date founded not correct");
            Assert.AreEqual(null, result.Description, "Description not correct");

            BandProcess.VerifyAllExpectations();
        }
    }
}