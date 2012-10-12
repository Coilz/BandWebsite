using System;
using System.Linq;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.BandRepositoryTests
{
    [TestClass]
    public class UserTests : BandRepositoryTestBase
    {
        [TestMethod]
        public void When_GetUser_is_called_with_an_Id_then_the_User_with_the_corresponding_Id_is_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            var userId = users.ElementAt(2).Id;

            BandCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            BandCatalog.Replay();

            var user = Repository.GetUser(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetUser_is_called_with_an_Id_and_there_is_no_User_in_the_collection_with_that_Id_then_an_InvalidOperationException_is_thrown()
        {
            var users = UserCreator.CreateCollection();
            var userId = Guid.NewGuid();

            BandCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.GetUser(userId);
        }

        [TestMethod]
        public void When_GetAllUsers_is_called_then_all_Users_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();

            BandCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(users.Count(), result.Count());
            Assert.AreEqual(users, result);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_AddUser_is_called_with_then_the_User_with_is_added_to_the_collection()
        {
            var user = UserCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Add(user))
                .Return(user)
                .Repeat.Once();
            BandCatalog.Replay();

            user = Repository.AddUser(user);

            Assert.IsNotNull(user);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdateUser_is_called_with_a_valid_User_then_the_User_with_is_updated_in_the_collection()
        {
            var user = UserCreator.CreateSingle();

            BandCatalog
                .Expect(catalog => catalog.Update(user))
                .Return(user)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.UpdateUser(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);

            BandCatalog.VerifyAllExpectations();
        }
    }
}