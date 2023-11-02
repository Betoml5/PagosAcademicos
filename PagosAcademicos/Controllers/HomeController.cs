using Microsoft.AspNetCore.Mvc;

namespace PagosAcademicos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
