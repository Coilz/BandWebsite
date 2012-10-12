using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.UserProcessTests
{
    [TestClass]
    public class GetUserByEmailAddressTests : UserProcessTestBase
    {
        [TestMethod]
        public void When_GetUserByEmailAddress_is_called_with_a_valid_loginName_then_GetUsersByEmailAddress_on_the_AppRepository_is_called_with_that_EmailAddress()
        {
            var users = UserCreator.CreateCollection();
            var user = users.ElementAt(2);
            var loginName = user.Login.EmailAddress;

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByEmailAddress(loginName))
                .Return(new List<User> {user})
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.GetUserByEmailAddress(loginName);

            Assert.AreEqual(loginName, result.Login.EmailAddress);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetUserByEmailAddress_is_called_with_a_valid_loginName_and_GetUsersByEmailAddress_on_the_AppRepository_returns_multiple_Users_then_an_InvalidOperationException_is_thrown()
        {
            var users = UserCreator.CreateCollection();
            var user = users.ElementAt(2);
            var loginName = user.Login.EmailAddress;

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByEmailAddress(loginName))
                .Return(new List<User> {user, user})
                .Repeat.Once();
            AppRepository.Replay();

            Process.GetUserByEmailAddress(loginName);
        }

        [TestMethod]
        public void When_GetUserByEmailAddress_on_the_AppRepository_returns_no_Users_then_GetUserByEmailAddress_returns_null()
        {
            const string emailAddress = "Some@emailAddress.com";

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByEmailAddress(emailAddress))
                .Return(new List<User>())
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.GetUserByEmailAddress(emailAddress);

            Assert.IsNull(result);

            AppRepository.VerifyAllExpectations();
        }
    }
}