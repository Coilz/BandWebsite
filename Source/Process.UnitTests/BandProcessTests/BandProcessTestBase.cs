namespace Ewk.BandWebsite.Process.UnitTests.BandProcessTests
{
    public abstract class BandProcessTestBase : ProcessTestBase
    {
        protected BandProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            MockHelper.RegisterInstance<ICryptographyProcess>(new CryptographyProcess(CatalogsContainer));

            Process = new BandProcess(CatalogsContainer);
        }
    }
}