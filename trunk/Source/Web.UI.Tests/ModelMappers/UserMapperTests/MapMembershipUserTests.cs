using System;
using System.Web.Security;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.UserMapperTests
{
    [TestClass]
    public class MapMembershipUserTests : UserMapperTestBase
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_MembershipUser_is_mapped_to_a_User_and_the_parameter_is_null_then_an_ArgumentNullException_is_thrown()
        {
            Mapper.Map((MembershipUser)null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_MembershipUser_is_mapped_to_a_User_and_id_of_the_MembershipUser_is_null_then_an_ArgumentNullException_is_thrown()
        {
            var user = UserCreator.CreateSingle();

            var membershipUser = new MembershipUser(
                "CustomMembershipProvider",
                user.Login.LoginName,
                null,
                user.Login.EmailAddress,
                "",
                "",
                true,
                false,
                user.Login.CreationDate,
                user.Login.LastLoginDate,
                user.Login.LastActivityDate,
                user.Login.LastPasswordChangedDate,
                user.Login.LastLockoutDate);

            Mapper.Map(membershipUser);
        }

        [TestMethod]
        public void When_MembershipUser_is_mapped_to_a_User_then_the_User_is_retrieved_from_the_IUserProcess()
        {
            var lastActivityDate = DateTime.Now.AddMilliseconds(30);
            var user = UserCreator.CreateSingle();

            UserProcess
                .Expect(process => process.GetUser(user.Id))
                .Return(user)
                .Repeat.Once();
            UserProcess.Replay();

            var membershipUser = new MembershipUser(
                "CustomMembershipProvider",
                user.Login.LoginName,
                user.Id,
                user.Login.EmailAddress,
                "",
                "",
                true,
                false,
                user.Login.CreationDate,
                user.Login.LastLoginDate,
                lastActivityDate,
                user.Login.LastPasswordChangedDate,
                user.Login.LastLockoutDate);

            var result = Mapper.Map(membershipUser);

            Assert.IsNotNull(membershipUser.ProviderUserKey);
            Assert.AreEqual((Guid)membershipUser.ProviderUserKey, result.Id);
            //Assert.AreEqual(user.BandId, result.BandId);
            Assert.AreEqual(user.Login.CreationDate, result.Login.CreationDate);
            Assert.AreEqual(user.Login.EmailAddress, result.Login.EmailAddress);
            Assert.AreEqual(user.Login.IsApproved, result.Login.IsApproved);
            Assert.AreEqual(user.Login.IsLockedOut, result.Login.IsLockedOut);
            Assert.AreEqual(user.Login.IsOnline, result.Login.IsOnline);

            Assert.AreEqual(lastActivityDate, result.Login.LastActivityDate);

            Assert.AreEqual(user.Login.LastLockoutDate, result.Login.LastLockoutDate);
            Assert.AreEqual(user.Login.LastLoginDate, result.Login.LastLoginDate);
            Assert.AreEqual(user.Login.LastPasswordChangedDate, result.Login.LastPasswordChangedDate);
            //Assert.AreEqual(user.Login.ModificationDate, result.Login.ModificationDate);
            //Assert.AreEqual(user.Name, result.Name);

            UserProcess.VerifyAllExpectations();
        }
    }
}