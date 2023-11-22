


using Microsoft.AspNetCore.Mvc;

namespace PagosAcademicos.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class MetodosPagoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Agregar()
        {
            return View();
        }

        public IActionResult Editar(int Id)
        {
            return View();
        }

        public IActionResult Eliminar(int Id)
        {
            return View();
        }

    }
}
