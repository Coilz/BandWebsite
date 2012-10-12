using System;
using System.Linq;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.AppRepositoryTests
{
    [TestClass]
    public class LoginAccountTests : AppRepositoryTestBase
    {
        [TestMethod]
        public void When_GetLoginAccountByLoginName_is_called_with_a_loginName_then_the_LoginAccount_with_that_loginName_is_retrieved_from_the_collection()
        {
            var loginAccounts = LoginAccountCreator.CreateCollection();
            var loginName = loginAccounts.ElementAt(2).LoginName;

            AppCatalog
                .Expect(catalog => catalog.LoginAccounts)
                .Return(loginAccounts)
                .Repeat.Once();
            AppCatalog.Replay();

            var result = Repository.GetLoginAccountByLoginName(loginName);

            Assert.IsNotNull(result);
            Assert.AreEqual(loginName, result.LoginName);

            AppCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetLoginAccountByLoginName_is_called_with_an_invalid_loginName_then_a_InvalidOperationException_is_thrown()
        {
            var loginAccounts = LoginAccountCreator.CreateCollection();
            const string loginName = "Invalid loginName";

            AppCatalog
                .Expect(catalog => catalog.LoginAccounts)
                .Return(loginAccounts)
                .Repeat.Once();
            AppCatalog.Replay();

            Repository.GetLoginAccountByLoginName(loginName);
        }
    }
}