using System;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Catalogs.MongoDb
{
    public class CatalogsContainer : ICatalogsContainer
    {
        private IAppCatalog _appCatalog;
        private IBandCatalog _bandCatalog;

        #region Implementation of ICatalogsContainer

        public IAppCatalog AppCatalog
        {
            get { return _appCatalog ?? (_appCatalog = ResolveAppCatalog()); }
        }

        public IBandCatalog BandCatalog
        {
            get { return _bandCatalog ?? (_bandCatalog = ResolveBandCatalog()); }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            // free managed resources
            if (_appCatalog != null)
            {
                _appCatalog.Dispose();
                _appCatalog = null;
            }

            if (_bandCatalog != null)
            {
                _bandCatalog.Dispose();
                _bandCatalog = null;
            }
        }

        #endregion

        private static IAppCatalog ResolveAppCatalog()
        {
            return DependencyConfiguration.DependencyResolver.Resolve<IAppCatalog>();
        }

        private static IBandCatalog ResolveBandCatalog()
        {
            return DependencyConfiguration.DependencyResolver.Resolve<IBandCatalog>();
        }
    }
}
