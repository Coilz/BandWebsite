using System.Linq;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Catalogs.MongoDb
{
    public class AppCatalog : Catalog, IAppCatalog
    {
        #region Implementation of IBandCatalog

        public Band CurrentBand
        {
            get
            {
                return GetCollection<Band>()
                    .SingleOrDefault(band =>
                                     band.Id == BandId);
            }
        }

        public IQueryable<Band> Bands
        {
            get { return GetCollection<Band>(); }
        }

        public IQueryable<User> Users
        {
            get { return GetCollection<User>(); }
        }

        public IQueryable<LoginAccount> LoginAccounts
        {
            get { return GetCollection<LoginAccount>(); }
        }

        #endregion
    }
}