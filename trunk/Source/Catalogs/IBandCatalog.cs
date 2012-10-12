using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Catalogs
{
    /// <summary>
    /// Provides access to the bandspecific datastore
    /// </summary>
    public interface IBandCatalog : ICatalog
    {
        /// <summary>
        /// Gets access to the collection of <see cref="Musician"/> instances.
        /// </summary>
        IQueryable<Musician> Musicians { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="User"/> instances.
        /// </summary>
        IQueryable<User> Users { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="BlogArticle"/> instances.
        /// </summary>
        IQueryable<BlogArticle> BlogArticles { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="Performance"/> instances.
        /// </summary>
        IQueryable<Performance> Performances { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="AdapterSettings"/> instances.
        /// </summary>
        IQueryable<AdapterSettings> AdapterSettings { get; }
    }
}