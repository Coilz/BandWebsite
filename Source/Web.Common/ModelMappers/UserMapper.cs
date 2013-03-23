using System;
using System.Web.Security;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Process;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public class UserMapper : IUserMapper
    {
        private readonly ICatalogsContainer _catalogsContainer;

        private IUserProcess _userProcess;

        public UserMapper(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IUserMapper

        public MembershipUser Map(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            return new MembershipUser(
                "CustomMembershipProvider",
                user.Login.LoginName,
                user.Id,
                user.Login.EmailAddress,
                "",
                "",
                user.Login.IsApproved,
                user.Login.IsLockedOut,
                user.Login.CreationDate,
                user.Login.LastLoginDate,
                user.Login.LastActivityDate,
                user.Login.LastPasswordChangedDate,
                user.Login.LastLockoutDate);
        }

        public User Map(MembershipUser membershipUser)
        {
            if (membershipUser == null) throw new ArgumentNullException("membershipUser");
            if (membershipUser.ProviderUserKey == null) throw new ArgumentNullException("membershipUser");

            var id = (Guid)membershipUser.ProviderUserKey;

            var userFromStore = UserProcess.GetUser(id);
            userFromStore.Login.EmailAddress = membershipUser.Email;
            userFromStore.Login.IsApproved = membershipUser.IsApproved;
            userFromStore.Login.IsLockedOut = membershipUser.IsLockedOut;
            userFromStore.Login.IsOnline = membershipUser.IsOnline;
            userFromStore.Login.LastActivityDate = membershipUser.LastActivityDate;
            userFromStore.Login.LastLockoutDate = membershipUser.LastLockoutDate;
            userFromStore.Login.LastLoginDate = membershipUser.LastLoginDate;
            userFromStore.Login.LastPasswordChangedDate = membershipUser.LastPasswordChangedDate;

            return userFromStore;
        }

        #endregion

        private IUserProcess UserProcess
        {
            get
            {
                return _userProcess ?? (_userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(_catalogsContainer));
            }
        }
    }
}