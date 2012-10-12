using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly ICatalogsContainer _catalogsContainer;

        public AppRepository(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        #region Implementation of IAppRepository

        #region Band

        public Band AddBand(Band band)
        {
            if (band == null) throw new ArgumentNullException("band");

            return _catalogsContainer.AppCatalog.Add(band);
        }

        public Band UpdateBand(Band band)
        {
            if (band == null) throw new ArgumentNullException("band");

            return _catalogsContainer.AppCatalog.Update(band);
        }

        public Band GetBand()
        {
            var result = _catalogsContainer.AppCatalog.CurrentBand;

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public Band GetBand(Guid id)
        {
            var result = _catalogsContainer.AppCatalog.Bands
                .SingleOrDefault(band =>
                                 band.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<Band> GetAllBands()
        {
            return _catalogsContainer.AppCatalog.Bands;
        }

        #endregion

        #region LoginAccount

        public LoginAccount GetLoginAccountByLoginName(string loginName)
        {
            var result = _catalogsContainer.AppCatalog.LoginAccounts
                .SingleOrDefault(account =>
                                 account.LoginName == loginName);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        #endregion

        #region User

        public IEnumerable<User> GetUsersByLoginName(string loginName)
        {
            return _catalogsContainer.AppCatalog.Users
                .Where(user =>
                       user.Login.LoginName == loginName);
        }

        public IEnumerable<User> GetUsersByEmailAddress(string emailAddress)
        {
            return _catalogsContainer.AppCatalog.Users
                .Where(user =>
                       user.Login.EmailAddress == emailAddress);
        }

        public IEnumerable<User> GetUsersByLoginAccount(Guid id)
        {
            return _catalogsContainer.AppCatalog.Users
                .Where(user =>
                       user.Login.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _catalogsContainer.AppCatalog.Users;
        }

        #endregion

        #endregion
    }
}