namespace Ewk.BandWebsite.Process.UnitTests.PerformanceProcessTests
{
    public abstract class PerformanceProcessTestBase : ProcessTestBase
    {
        protected PerformanceProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Process = new PerformanceProcess(CatalogsContainer);
        }
    }
}