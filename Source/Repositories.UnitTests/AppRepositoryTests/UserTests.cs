using System;
using System.Linq;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.AppRepositoryTests
{
    [TestClass]
    public class UserTests : AppRepositoryTestBase
    {
        [TestMethod]
        public void When_GetAllUsers_is_called_then_all_Users_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(users.Count(), result.Count());
            Assert.AreEqual(users, result);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetUsersByLoginName_is_called_with_a_loginName_then_all_Users_with_that_loginName_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            var loginName = users.ElementAt(2).Login.LoginName;

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetUsersByLoginName(loginName);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(loginName, result.First().Login.LoginName);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetUsersByLoginName_is_called_with_an_invalid_loginName_then_no_Users_with_that_loginName_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            const string loginName = "Invalid loginName";

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetUsersByLoginName(loginName);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetUsersByEmailAddress_is_called_with_a_emailAddress_then_all_Users_with_that_emailAddress_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            var emailAddress = users.ElementAt(2).Login.EmailAddress;

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetUsersByEmailAddress(emailAddress);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(emailAddress, result.First().Login.EmailAddress);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetUsersByEmailAddress_is_called_with_an_invalid_emailAddress_then_no_Users_with_that_emailAddress_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            const string emailAddress = "Invalid@emailAddress.com";

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetUsersByEmailAddress(emailAddress);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetUsersByLoginAccount_is_called_with_a_valid_id_then_all_Users_with_that_LoginAccount_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            var id = users.ElementAt(2).Login.Id;

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetUsersByLoginAccount(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(id, result.First().Login.Id);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetUsersByLoginAccount_is_called_with_an_invalid_id_then_no_Users_are_retrieved_from_the_collection()
        {
            var users = UserCreator.CreateCollection();
            var id = Guid.NewGuid();

            AppCatalog
                .Expect(catalog => catalog.Users)
                .Return(users)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetUsersByLoginAccount(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            AppCatalog.VerifyAllExpectations();
        }
    }
}