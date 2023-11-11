using Microsoft.AspNetCore.Mvc;

namespace PagosAcademicos.Areas.Admin.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
