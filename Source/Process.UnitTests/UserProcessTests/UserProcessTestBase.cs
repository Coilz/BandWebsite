namespace Ewk.BandWebsite.Process.UnitTests.UserProcessTests
{
    public abstract class UserProcessTestBase : ProcessTestBase
    {
        protected UserProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Process = new UserProcess(CatalogsContainer);
        }
    }
}