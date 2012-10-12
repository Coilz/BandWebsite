using System;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Common;
using Rhino.Mocks;

namespace Ewk.BandWebsite.UnitTests
{
    public abstract class UnitTestBase : Ewk.UnitTests.UnitTestBase
    {
        private BandIdContainer _container;

        protected IAppCatalog AppCatalog { get; private set; }
        protected IBandCatalog BandCatalog { get; private set; }
        protected ICatalogsContainer CatalogsContainer { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            _container = new BandIdContainer(Guid.NewGuid());
            MockHelper.RegisterInstance<IBandIdResolver>(_container);
            MockHelper.RegisterInstance<IBandIdInstaller>(_container);

            AppCatalog = MockHelper.CreateAndRegisterMock<IAppCatalog>();
            BandCatalog = MockHelper.CreateAndRegisterMock<IBandCatalog>();
            CatalogsContainer = MockHelper.CreateAndRegisterMock<ICatalogsContainer>();

            CatalogsContainer
                .Expect(container => container.AppCatalog)
                .Return(AppCatalog)
                .Repeat.Any();
            CatalogsContainer
                .Expect(container => container.BandCatalog)
                .Return(BandCatalog)
                .Repeat.Any();
            CatalogsContainer.Replay();
        }

        protected void SetBandId(Guid id)
        {
            _container.SetBandId(id);
        }
    }
}