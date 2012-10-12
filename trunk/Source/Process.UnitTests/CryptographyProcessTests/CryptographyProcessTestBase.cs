namespace Ewk.BandWebsite.Process.UnitTests.CryptographyProcessTests
{
    public abstract class CryptographyProcessTestBase : ProcessTestBase
    {
        protected CryptographyProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Process = new CryptographyProcess(CatalogsContainer);
        }
    }
}