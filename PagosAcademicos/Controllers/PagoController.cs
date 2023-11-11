using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Controllers
{
    public class PagoController : Controller
    {
        private Repository<Pago> ctx;

        public PagoController(Repository<Pago> ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pago(string id)
        {
            var pago = ctx.Get(id);

            if (pago != null)
            {
                return RedirectToAction("Index");
            }
             
            return View(pago);
        }
    }
}
