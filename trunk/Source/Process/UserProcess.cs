using System;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Process
{
    /// <summary>
    /// Provides methods to process blog users.
    /// </summary>
    public class UserProcess : ProcessBase, IUserProcess
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="catalogsContainer">A container with all catalogs to access the store.</param>
        public UserProcess(ICatalogsContainer catalogsContainer)
            : base(catalogsContainer)
        {
        }

        #region Implementation of IUserProcess

        public User GetUser(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException("id");

            return BandRepository.GetUser(id);
        }

        public User GetUserByLoginName(string loginName)
        {
            return AppRepository.GetUsersByLoginName(loginName)
                .SingleOrDefault();
        }

        public User GetUserByEmailAddress(string emailAddress)
        {
            return AppRepository.GetUsersByEmailAddress(emailAddress)
                .SingleOrDefault();
        }

        public User GetUserByLoginAccount(Guid id)
        {
            var users = AppRepository.GetUsersByLoginAccount(id);
            var user = users.SingleOrDefault();

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            return user;
        }

        public User AddUser(User user)
        {
            return BandRepository.AddUser(user);
        }

        public User UpdateUser(User user)
        {
            return BandRepository.UpdateUser(user);
        }

        #endregion
    }
}