using System.Linq;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Catalogs
{
    /// <summary>
    /// Provides access to the datastore
    /// </summary>
    public interface IAppCatalog : ICatalog
    {
        /// <summary>
        /// Gets access to the current <see cref="Band"/> instance.
        /// </summary>
        Band CurrentBand { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="Band"/> instances.
        /// </summary>
        IQueryable<Band> Bands { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="User"/> instances.
        /// </summary>
        IQueryable<User> Users { get; }

        /// <summary>
        /// Gets access to the collection of <see cref="LoginAccount"/> instances.
        /// </summary>
        IQueryable<LoginAccount> LoginAccounts { get; }
    }
}