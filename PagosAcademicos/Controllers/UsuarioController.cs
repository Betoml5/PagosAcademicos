using Microsoft.AspNetCore.Mvc;

namespace PagosAcademicos.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
