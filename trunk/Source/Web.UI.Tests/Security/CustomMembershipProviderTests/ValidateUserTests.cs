using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Security.CustomMembershipProviderTests
{
    [TestClass]
    public class ValidateUserTests : CustomMembershipProviderTestBase
    {
        [TestMethod]
        public void When_ValidateUser_is_called_with_an_invalid_UserName_then_false_is_returned()
        {
            UserProcess
                .Expect(process => process.GetUserByLoginName(Arg<string>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            UserProcess.Replay();

            var result = CustomMembershipProvider.ValidateUser("userName", "password");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_ValidateUser_is_called_with_a_valid_UserName_and_an_invalid_Password_then_false_is_returned()
        {
            const string decryptedPassword = "Decrypted password";
            var user = UserCreator.CreateSingle();

            UserProcess
                .Expect(process =>
                        process.GetUserByLoginName(user.Login.LoginName))
                .Return(user)
                .Repeat.Once();
            UserProcess.Replay();

            CryptographyProcess
                .Expect(process => process.Decrypt(user.Login.Password))
                .Return(decryptedPassword)
                .Repeat.Once();
            CryptographyProcess.Replay();

            var result = CustomMembershipProvider.ValidateUser(user.Login.LoginName, user.Login.Password);

            UserProcess.VerifyAllExpectations();
            CryptographyProcess.VerifyAllExpectations();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_ValidateUser_is_called_with_a_valid_UserName_and_a_valid_Password_then_true_is_returned()
        {
            const string decryptedPassword = "Decrypted password";
            var user = UserCreator.CreateSingle();

            UserProcess
                .Expect(process =>
                        process.GetUserByLoginName(user.Login.LoginName))
                .Return(user)
                .Repeat.Once();
            UserProcess.Replay();

            CryptographyProcess
                .Expect(process => process.Decrypt(user.Login.Password))
                .Return(decryptedPassword)
                .Repeat.Once();
            CryptographyProcess.Replay();

            var result = CustomMembershipProvider.ValidateUser(user.Login.LoginName, decryptedPassword);

            UserProcess.VerifyAllExpectations();
            CryptographyProcess.VerifyAllExpectations();

            Assert.IsTrue(result);
        }
    }
}