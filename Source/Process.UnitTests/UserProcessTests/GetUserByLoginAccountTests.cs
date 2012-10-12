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
    public class GetUserByLoginAccountTests : UserProcessTestBase
    {
        [TestMethod]
        public void When_GetUserByLoginAccount_is_called_with_a_valid_id_then_GetUsersByLoginAccount_on_the_AppRepository_is_called_with_that_id()
        {
            var users = UserCreator.CreateCollection();
            var user = users.ElementAt(2);
            var id = user.Login.Id;

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByLoginAccount(id))
                .Return(new List<User> {user})
                .Repeat.Once();
            AppRepository.Replay();

            var result = Process.GetUserByLoginAccount(id);

            Assert.AreEqual(id, result.Login.Id);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetUserByLoginAccount_is_called_with_a_valid_id_and_GetUsersByLoginAccount_on_the_BandRepository_returns_multiple_Users_then_an_InvalidOperationException_is_thrown()
        {
            var users = UserCreator.CreateCollection();
            var user = users.ElementAt(2);
            var id = user.Login.Id;

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByLoginAccount(id))
                .Return(new List<User> {user, user})
                .Repeat.Once();
            AppRepository.Replay();

            Process.GetUserByLoginAccount(id);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetUserByLoginAccount_on_the_BandRepository_returns_no_Users_then_an_InvalidOperationException_is_thrown()
        {
            var id = Guid.NewGuid();

            AppRepository
                .Expect(repository =>
                        repository.GetUsersByLoginAccount(id))
                .Return(new List<User>())
                .Repeat.Once();
            AppRepository.Replay();

            Process.GetUserByLoginAccount(id);
        }
    }
}