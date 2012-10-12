namespace Ewk.BandWebsite.Repositories.UnitTests.AppRepositoryTests
{
    public abstract class AppRepositoryTestBase : RepositoriesTestBase
    {
        protected AppRepository Repository { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Repository = new AppRepository(CatalogsContainer);
        }
    }
}