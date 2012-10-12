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
    public class GetUserByLoginNameTests : UserProcessTestBase
    {
        [TestMethod]
        public void When_GetUserByLoginName_is_called_with_a_valid_loginName_then_GetUsersByLoginName_on_the_AppRepository_is_called_with_that_LoginName()
        {
            var users = UserCreator.CreateCollection();
            var user = users.ElementAt(2);
            var loginName = user.Login.LoginName;

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByLoginName(loginName))
                .Return(new List<User> {user})
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.GetUserByLoginName(loginName);

            Assert.AreEqual(loginName, result.Login.LoginName);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetUserByLoginName_is_called_with_a_valid_loginName_and_GetUsersByLoginName_on_the_AppRepository_returns_multiple_Users_then_an_InvalidOperationException_is_thrown()
        {
            var users = UserCreator.CreateCollection();
            var user = users.ElementAt(2);
            var loginName = user.Login.LoginName;

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByLoginName(loginName))
                .Return(new List<User> {user, user})
                .Repeat.Once();
            AppRepository.Replay();

            Process.GetUserByLoginName(loginName);
        }

        [TestMethod]
        public void When_GetUserByLoginName_on_the_AppRepository_returns_no_Users_then_GetUserByLoginName_returns_null()
        {
            const string loginName = "Some loginName";

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByLoginName(loginName))
                .Return(new List<User>())
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.GetUserByLoginName(loginName);

            Assert.IsNull(result);

            AppRepository.VerifyAllExpectations();
        }
    }
}