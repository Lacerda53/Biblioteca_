using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}