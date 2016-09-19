using System.Web.Mvc;

namespace TidalExplorer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}