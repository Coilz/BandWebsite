using System.Web.Security;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.UnitTests;
using Ewk.BandWebsite.Web.Common.ModelMappers;
using Ewk.BandWebsite.Web.UI.Security;

namespace Ewk.BandWebsite.Web.UI.Tests.Security.CustomMembershipProviderTests
{
    public abstract class CustomMembershipProviderTestBase : UnitTestBase
    {
        protected IBandProcess BandProcess { get; private set; }
        protected IUserProcess UserProcess { get; private set; }
        protected IUserMapper UserMapper { get; private set; }
        protected ICryptographyProcess CryptographyProcess { get; private set; }
        protected CustomMembershipProvider CustomMembershipProvider { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            BandProcess = MockHelper.CreateAndRegisterMock<IBandProcess>();
            UserProcess = MockHelper.CreateAndRegisterMock<IUserProcess>();
            UserMapper = MockHelper.CreateAndRegisterMock<IUserMapper>();
            CryptographyProcess = MockHelper.CreateAndRegisterMock<ICryptographyProcess>();

            CustomMembershipProvider = new CustomMembershipProvider();
        }

        protected MembershipUser CreateMembershipUser(User user)
        {
            var login = user.Login;
            return new MembershipUser("CustomMembershipProvider",
                login.LoginName,
                login.Id,
                login.EmailAddress,
                string.Empty,
                string.Empty,
                login.IsApproved,
                login.IsLockedOut,
                login.CreationDate,
                login.LastLoginDate,
                login.LastActivityDate,
                login.LastPasswordChangedDate,
                login.LastLockoutDate);
        }
    }
}