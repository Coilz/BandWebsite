using System;
using System.Threading.Tasks;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.ModelMappers.UserMapperTests
{
    [TestClass]
    public class MapToMembershipUserTests : UserMapperTestBase
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_User_is_mapped_to_a_MembershipUser_and_the_parameter_is_null_then_an_ArgumentNullException_is_thrown()
        {
            Mapper.Map((User)null);
        }

        [TestMethod]
        public void When_User_is_mapped_to_a_MembershipUser_then_the_User_is_not_retrieved_from_the_IUserProcess()
        {
            var user = UserCreator.CreateSingle();

            UserProcess
                .Expect(process => process.GetUser(user.Id))
                .Return(user)
                .Repeat.Never();
            UserProcess.Replay();

            var result = Mapper.Map(user);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ProviderUserKey);
            Assert.AreEqual(user.Id, (Guid)result.ProviderUserKey);
            //Assert.AreEqual(user.BandId, result.BandId);
            Assert.AreEqual(user.Login.CreationDate, result.CreationDate.ToUniversalTime());
            Assert.AreEqual(user.Login.EmailAddress, result.Email);
            Assert.AreEqual(user.Login.IsApproved, result.IsApproved);
            Assert.AreEqual(user.Login.IsLockedOut, result.IsLockedOut);
            Assert.AreEqual(user.Login.IsOnline, result.IsOnline);
            Assert.AreEqual(user.Login.LastActivityDate, result.LastActivityDate.ToUniversalTime());
            Assert.AreEqual(user.Login.LastLockoutDate, result.LastLockoutDate.ToUniversalTime());
            Assert.AreEqual(user.Login.LastLoginDate, result.LastLoginDate.ToUniversalTime());
            Assert.AreEqual(user.Login.LastPasswordChangedDate, result.LastPasswordChangedDate.ToUniversalTime());
            //Assert.AreEqual(user.ModificationDate, result.ModificationDate);
            //Assert.AreEqual(user.Name, result.Name);

            UserProcess.VerifyAllExpectations();
        }
    }
}