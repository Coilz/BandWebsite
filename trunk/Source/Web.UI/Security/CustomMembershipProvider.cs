using System;
using System.Web.Security;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Common;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Web.UI.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        #region Overrides of MembershipProvider

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var statusCode = MembershipCreateStatus.Success;

            var membershipUser = CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = userProcess.GetUserByLoginName(username);
                        if (user != null)
                        {
                            statusCode = MembershipCreateStatus.DuplicateUserName;
                            return null;
                        }

                        user = userProcess.GetUserByEmailAddress(email);
                        if (user != null)
                        {
                            statusCode = MembershipCreateStatus.DuplicateEmail;
                            return null;
                        }

                        var bandProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandProcess>(container);
                        var band = bandProcess.EnsureBandExists();

                        InstallBandId(band.Id);

                        var cryptographyProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<ICryptographyProcess>(container);
                        var encryptedPassword = cryptographyProcess.Encrypt(password);

                        user = new User
                                       {
                                           Login = new LoginAccount
                                                       {
                                                           LoginName = username,
                                                           Password = encryptedPassword,
                                                           EmailAddress = email,
                                                           IsApproved = isApproved,
                                                           IsOnline = true,
                                                           LastLoginDate = DateTime.UtcNow,
                                                           LastActivityDate = DateTime.UtcNow,
                                                           LastPasswordChangedDate = DateTime.UtcNow
                                                       },
                                       };

                        user = userProcess.AddUser(user);

                        var userMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserMapper>(container);
                        return userMapper.Map(user);
                    });

            status = statusCode;
            return membershipUser;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = userProcess.GetUserByLoginName(username);

                        InstallBandId(user.BandId);

                        var cryptographyProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<ICryptographyProcess>(container);

                        return cryptographyProcess.Decrypt(user.Login.Password);
                    });
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = userProcess.GetUserByLoginName(username);

                        InstallBandId(user.BandId);

                        var cryptographyProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<ICryptographyProcess>(container);
                        var currentPassword = cryptographyProcess.Decrypt(user.Login.Password);
                        if (oldPassword != currentPassword)
                        {
                            return false;
                        }

                        user.Login.Password = cryptographyProcess.Encrypt(newPassword);
                        userProcess.UpdateUser(user);

                        return true;
                    });
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser membershipUser)
        {
            CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                {
                    var userMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserMapper>(container);
                    var user = userMapper.Map(membershipUser);

                    InstallBandId(user.BandId);

                    var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                    userProcess.UpdateUser(user);

                    return true;
                });
        }

        public override bool ValidateUser(string username, string password)
        {
            return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var userProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = userProcess.GetUserByLoginName(username);
                        if (user == null) return false;

                        InstallBandId(user.BandId);

                        var cryptographyProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<ICryptographyProcess>(container);
                        var currentPassword = cryptographyProcess.Decrypt(user.Login.Password);

                        return password == currentPassword;
                    });
        }

        public override bool UnlockUser(string userName)
        {
            return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = process.GetUserByLoginName(userName);

                        InstallBandId(user.BandId);
                        
                        user.Login.IsLockedOut = false;
                        process.UpdateUser(user);

                        return true;
                    });
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = process.GetUserByLoginAccount((Guid) providerUserKey);

                        InstallBandId(user.BandId);

                        var userMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserMapper>(container);
                        return userMapper.Map(user);
                    });
        }

        public override MembershipUser GetUser(string userName, bool userIsOnline)
        {
            return CatalogsConsumerHelper.ExecuteWithCatalogScope(
                container =>
                    {
                        var process = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserProcess>(container);
                        var user = process.GetUserByLoginName(userName);

                        InstallBandId(user.BandId);

                        var userMapper = CatalogsConsumerHelper.ResolveCatalogsConsumer<IUserMapper>(container);
                        return userMapper.Map(user);
                    });
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 3; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 8; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 2; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        private static void InstallBandId(Guid id)
        {
            var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
            bandIdInstaller.SetBandId(id);
        }
    }
}