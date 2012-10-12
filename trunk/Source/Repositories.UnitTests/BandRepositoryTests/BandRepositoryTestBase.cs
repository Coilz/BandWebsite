namespace Ewk.BandWebsite.Repositories.UnitTests.BandRepositoryTests
{
    public abstract class BandRepositoryTestBase : RepositoriesTestBase
    {
        protected BandRepository Repository { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Repository = new BandRepository(CatalogsContainer);
        }
    }
}