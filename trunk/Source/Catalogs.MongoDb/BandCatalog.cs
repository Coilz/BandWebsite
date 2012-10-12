using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Catalogs.MongoDb
{
    public class BandCatalog : Catalog, IBandCatalog
    {
        #region Implementation of IBandCatalog

        public IQueryable<Musician> Musicians
        {
            get { return GetFilteredCollection<Musician>(); }
        }

        public IQueryable<User> Users
        {
            get { return GetFilteredCollection<User>(); }
        }

        public IQueryable<BlogArticle> BlogArticles
        {
            get { return GetFilteredCollection<BlogArticle>(); }
        }

        public IQueryable<Performance> Performances
        {
            get { return GetFilteredCollection<Performance>(); }
        }

        public IQueryable<AdapterSettings> AdapterSettings
        {
            get { return GetFilteredCollection<AdapterSettings>(); }
        }

        #endregion

        private IQueryable<T> GetFilteredCollection<T>() where T : BandEntity
        {
            return GetCollection<T>()
                .Where(arg =>
                       arg.BandId == BandId);
        }
    }
}