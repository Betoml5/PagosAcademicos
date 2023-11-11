using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Controllers
{
    public class PagosController : Controller
    {
        private Repository<Pago> ctx;

        public PagosController(Repository<Pago> ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("Pagar")]
        public IActionResult Pago(string id)
        {
            var pago = ctx.Get(id);

            if (pago != null)
            {
                return RedirectToAction("Index");
            }
             
            return View(pago);
        }

        public IActionResult Pagar()
        {
            return View();
        }
    }
}
