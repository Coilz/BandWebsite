using System.Web.Mvc;

namespace Ewk.BandWebsite.Web.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
