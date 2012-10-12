namespace Ewk.BandWebsite.Process.UnitTests.BlogProcessTests
{
    public abstract class BlogProcessTestBase : ProcessTestBase
    {
        protected BlogProcess Process { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Process = new BlogProcess(CatalogsContainer);
        }
    }
}