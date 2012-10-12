using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Repositories;

namespace Ewk.BandWebsite.Process
{
    public abstract class ProcessBase
    {
        private readonly ICatalogsContainer _catalogsContainer;
        private IAppRepository _appRepository;
        private IBandRepository _bandRepository;

        protected ProcessBase(ICatalogsContainer catalogsContainer)
        {
            _catalogsContainer = catalogsContainer;
        }

        protected ICatalogsContainer CatalogsContainer
        {
            get { return _catalogsContainer; }
        }

        protected IAppRepository AppRepository
        {
            get
            {
                return _appRepository ?? (_appRepository = CatalogsConsumerHelper.ResolveCatalogsConsumer<IAppRepository>(_catalogsContainer));
            }
        }

        protected IBandRepository BandRepository
        {
            get
            {
                return _bandRepository ?? (_bandRepository = CatalogsConsumerHelper.ResolveCatalogsConsumer<IBandRepository>(_catalogsContainer));
            }
        }
    }
}