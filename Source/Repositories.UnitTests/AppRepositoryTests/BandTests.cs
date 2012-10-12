using System;
using System.Linq;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.AppRepositoryTests
{
    [TestClass]
    public class BandTests : AppRepositoryTestBase
    {
        [TestMethod]
        public void When_GetBand_is_called_without_an_Id_then_the_current_Band_is_retrieved_from_the_collection()
        {
            var band = BandCreator.CreateSingle();

            AppCatalog
                .Expect(catalog => catalog.CurrentBand)
                .Return(band)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetBand();

            Assert.IsNotNull(result);
            Assert.AreEqual(band, result);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetBand_is_called_without_an_Id_and_the_current_Band_is_not_Known_then_an_InvalidOperationException_is_thrown()
        {
            AppCatalog
                .Expect(catalog => catalog.CurrentBand)
                .Return(null)
                .Repeat.Once();
            AppCatalog.Replay();

            Repository.GetBand();
        }

        [TestMethod]
        public void When_GetBand_is_called_with_an_Id_then_the_Band_with_the_corresponding_Id_is_retrieved_from_the_collection()
        {
            var bands = BandCreator.CreateCollection();
            var bandId = bands.ElementAt(2).Id;

            AppCatalog
                .Expect(catalog => catalog.Bands)
                .Return(bands)
                .Repeat.Once();
            AppCatalog.Replay();

            var band = Repository.GetBand(bandId);

            Assert.IsNotNull(band);
            Assert.AreEqual(bandId, band.Id);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetBand_is_called_with_an_Id_and_there_is_no_Band_in_the_collection_with_that_Id_then_an_InvalidOperationException_is_thrown()
        {
            var bands = BandCreator.CreateCollection();
            var bandId = Guid.NewGuid();

            AppCatalog
                .Expect(catalog => catalog.Bands)
                .Return(bands)
                .Repeat.Once();
            AppCatalog.Replay();

            Repository.GetBand(bandId);
        }

        [TestMethod]
        public void When_GetAllBands_is_called_then_all_Bands_are_retrieved_from_the_collection()
        {
            var bands = BandCreator.CreateCollection();

            AppCatalog
                .Expect(catalog => catalog.Bands)
                .Return(bands)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetAllBands();

            Assert.IsNotNull(result);
            Assert.AreEqual(bands.Count(), result.Count());
            Assert.AreEqual(bands, result);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_AddBand_is_called_with_a_valid_Band_then_the_Band_is_added_to_the_collection()
        {
            var band = BandCreator.CreateSingle();

            AppCatalog
                .Expect(catalog => catalog.Add(band))
                .Return(band)
                .Repeat.Once();
            AppCatalog.Replay();

            band = Repository.AddBand(band);

            Assert.IsNotNull(band);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddBand_is_called_with_a_null_value_for_Band_then_an_ArgumentNullException_is_thrown()
        {
            AppCatalog
                .Expect(catalog => 
                    catalog.Add(Arg<Band>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            AppCatalog.Replay();

            Repository.AddBand(null);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateBand_is_called_with_a_valid_Band_then_the_Band_is_updated_in_the_collection()
        {
            var entity = BandCreator.CreateSingle();

            AppCatalog
                .Expect(catalog => catalog.Update(entity))
                .Return(entity)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.UpdateBand(entity);

            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Id, result.Id);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdateBand_is_called_with_a_null_value_for_Band_then_an_ArgumentNullException_is_thrown()
        {
            AppCatalog
                .Expect(catalog => 
                    catalog.Update(Arg<Band>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            AppCatalog.Replay();

            Repository.UpdateBand(null);

            AppCatalog.VerifyAllExpectations();
        }
    }
}