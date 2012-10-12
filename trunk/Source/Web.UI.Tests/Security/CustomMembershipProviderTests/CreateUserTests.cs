using System;
using System.Web.Security;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Security.CustomMembershipProviderTests
{
    [TestClass]
    public class CreateUserTests : CustomMembershipProviderTestBase
    {
        [TestMethod]
        public void When_CreatUser_is_called_then_the_User_is_created_and_registered_under_the_Band_that_is_provided_by_the_BandProcess()
        {
            var band = BandCreator.CreateSingle();
            var user = UserCreator.CreateSingle();
            var login = user.Login;
            const string encryptedPassword = "encyptedPassword";

            BandProcess
                .Expect(process => process.EnsureBandExists())
                .Return(band)
                .Repeat.Once();
            BandProcess.Replay();

            CryptographyProcess
                .Expect(process => process.Encrypt(login.Password))
                .Return(encryptedPassword)
                .Repeat.Once();
            CryptographyProcess.Replay();

            UserProcess
                .Expect(process => process.GetUserByLoginName(Arg<string>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            UserProcess
                .Expect(process => process.GetUserByEmailAddress(Arg<string>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            UserProcess
                .Expect(process =>
                        process.AddUser(Arg<User>.Matches(u =>
                                                          u.BandId == band.Id &&
                                                          u.Login.Password == encryptedPassword &&
                                                          u.Login.IsOnline &&
                                                          u.Login.LastLoginDate > DateTime.UtcNow.AddSeconds(-2) &&
                                                          u.Login.LastActivityDate > DateTime.UtcNow.AddSeconds(-2) &&
                                                          u.Login.LastPasswordChangedDate >
                                                          DateTime.UtcNow.AddSeconds(-2))))
                .Return(user)
                .Repeat.Once();
            UserProcess.Replay();

            var membershipUser = CreateMembershipUser(user);
            UserMapper
                .Expect(mapper => mapper.Map(user))
                .Return(membershipUser)
                .Repeat.Once();
            UserMapper.Replay();

            MembershipCreateStatus createStatus;
            var result = CustomMembershipProvider.CreateUser(
                login.LoginName,
                login.Password,
                login.EmailAddress,
                string.Empty,
                string.Empty,
                login.IsApproved,
                login.Id,
                out createStatus);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ProviderUserKey);
            Assert.AreEqual(user.Login.LoginName, result.UserName);
            Assert.AreEqual(MembershipCreateStatus.Success, createStatus);

            BandProcess.VerifyAllExpectations();
            CryptographyProcess.VerifyAllExpectations();
            UserProcess.VerifyAllExpectations();
            UserMapper.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_CreatUser_is_called_with_a_userName_that_already_exists_then_the_MembershipCreateStatus_is_DuplicateUserName()
        {
            UserProcess
                .Expect(process => process.GetUserByLoginName(Arg<string>.Is.Anything))
                .Return(UserCreator.CreateSingle())
                .Repeat.Once();
            UserProcess.Replay();

            MembershipCreateStatus createStatus;
            var result = CustomMembershipProvider.CreateUser(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                false,
                Guid.Empty,
                out createStatus);

            Assert.IsNull(result);
            Assert.AreEqual(MembershipCreateStatus.DuplicateUserName, createStatus);
        }

        [TestMethod]
        public void When_CreatUser_is_called_with_an_emailAddress_that_already_exists_then_the_MembershipCreateStatus_is_DuplicateEmail()
        {
            UserProcess
                .Expect(process => process.GetUserByLoginName(Arg<string>.Is.Anything))
                .Return(null)
                .Repeat.Once();
            UserProcess
                .Expect(process => process.GetUserByEmailAddress(Arg<string>.Is.Anything))
                .Return(UserCreator.CreateSingle())
                .Repeat.Once();
            UserProcess.Replay();

            MembershipCreateStatus createStatus;
            var result = CustomMembershipProvider.CreateUser(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                false,
                Guid.Empty,
                out createStatus);

            Assert.IsNull(result);
            Assert.AreEqual(MembershipCreateStatus.DuplicateEmail, createStatus);
        }
    }
}
