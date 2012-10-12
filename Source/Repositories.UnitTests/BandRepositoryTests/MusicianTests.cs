using System;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.BandRepositoryTests
{
    [TestClass]
    public class MusicianTests : BandRepositoryTestBase
    {
        [TestMethod]
        public void When_GetMusician_is_called_with_an_Id_then_the_Musician_with_the_corresponding_Id_is_retrieved_from_the_collection()
        {
            var entities = MusicianCreator.CreateCollection();
            var entityId = entities.ElementAt(2).Id;

            BandCatalog
                .Expect(catalog => catalog.Musicians)
                .Return(entities)
                .Repeat.Once();
            BandCatalog.Replay();

            var entity = Repository.GetMusician(entityId);

            Assert.IsNotNull(entity);
            Assert.AreEqual(entityId, entity.Id);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetMusician_is_called_with_an_Id_and_there_is_no_Musician_in_the_collection_with_that_Id_then_an_InvalidOperationException_is_thrown()
        {
            var entities = MusicianCreator.CreateCollection();
            var entityId = Guid.NewGuid();

            BandCatalog
                .Expect(catalog => catalog.Musicians)
                .Return(entities)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.GetMusician(entityId);
        }

        [TestMethod]
        public void When_GetAllMusicians_is_called_then_all_Musicians_are_retrieved_from_the_collection()
        {
            var entities = MusicianCreator.CreateCollection();

            BandCatalog
                .Expect(catalog => catalog.Musicians)
                .Return(entities)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAllMusicians();

            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count(), result.Count());
            Assert.AreEqual(entities, result);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_AddMusician_is_called_with_then_the_Musician_is_added_to_the_collection()
        {
            var entity = MusicianCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Add(entity))
                .Return(entity)
                .Repeat.Once();
            BandCatalog.Replay();

            entity = Repository.AddMusician(entity);

            Assert.IsNotNull(entity);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_AddMusician_is_called_with_null_as_Musician_then_a_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog =>
                        catalog.Add(Arg<Musician>.Is.Anything))
                .Return(null)
                .Repeat.Never();
            BandCatalog.Replay();

            Repository.AddMusician(null);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateMusician_is_called_with_a_valid_Musician_then_the_Musician_is_updated_in_the_collection()
        {
            var entity = MusicianCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Update(entity))
                .Return(entity)
                .Repeat.Once();
            BandCatalog.Replay();

            entity = Repository.UpdateMusician(entity);

            Assert.IsNotNull(entity);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_UpdateMusician_is_called_with_null_as_Musician_then_a_ArgumentNullException_is_thrown()
        {
            BandCatalog
                .Expect(catalog =>
                        catalog.Update(Arg<Musician>.Is.Anything))
                .Return(null)
                .Repeat.Never();
            BandCatalog.Replay();

            Repository.UpdateMusician(null);

            BandCatalog.VerifyAllExpectations();
        }
    }
}