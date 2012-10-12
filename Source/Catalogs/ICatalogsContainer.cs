using System;

namespace Ewk.BandWebsite.Catalogs
{
    /// <summary>
    /// Contains <see cref="ICatalog"/> instances.
    /// </summary>
    public interface ICatalogsContainer : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="IAppCatalog"/> instance.
        /// </summary>
        IAppCatalog AppCatalog { get; }

        /// <summary>
        /// Gets the <see cref="IBandCatalog"/> instance.
        /// </summary>
        IBandCatalog BandCatalog { get; }
    }
}