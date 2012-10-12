using Ewk.BandWebsite.Web.UI.Controllers;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.BlogArticleControllerTests
{
    public abstract class BlogArticleControllerTestBase : ControllerTestBase
    {
        protected BlogArticleController Controller { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            Controller = new BlogArticleController();
        }
    }
}