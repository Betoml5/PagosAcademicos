


using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Areas.Admin.Models.ViewModels;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class MetodosPagoController : Controller
    {
        private readonly TipoPagoRepository tipoPagoctx;

        public MetodosPagoController(TipoPagoRepository tipoPagoctx)
        {
            this.tipoPagoctx = tipoPagoctx;
        }

        public IActionResult Index()
        {

            var vm = tipoPagoctx
            .GetAll()
            .OrderBy(x => x.Id)
            .Select(x => new IndexTipoPagoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                PagosRegistrados = x.Pago.Count
            });



            return View(vm);
        }


        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(TipoPago tipoPago)
        {


            var tipoPagoExistente = tipoPagoctx.GetByNombre(tipoPago.Nombre);

            if (tipoPagoExistente != null)
            {
                ModelState.AddModelError("", "Ese tipo de pago ya existe");
            }

            if (string.IsNullOrWhiteSpace(tipoPago.Nombre))
            {
                ModelState.AddModelError("", "El nombre es requerido");
            }

            if (ModelState.IsValid)
            {
                tipoPagoctx.Insert(tipoPago);
                return RedirectToAction("Index");
            }

            return View(tipoPago);
        }

        public IActionResult Eliminar(int Id)
        {
            var tipoPago = tipoPagoctx.Get(Id);

            if (tipoPago != null)
            {
                var vm = new EliminarTipoPagoViewModel()
                {
                    Id = tipoPago.Id,
                    Nombre = tipoPago.Nombre,
                    PagosRegistrados = tipoPago.Pago.Count
                };
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(EliminarTipoPagoViewModel vm)
        {
            var tipoPago = tipoPagoctx.Get(vm.Id);
            if (tipoPago != null)
            {
                tipoPagoctx.Delete(tipoPago);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public IActionResult Editar(int Id)
        {

            var tipoPago = tipoPagoctx.Get(Id);

            if (tipoPago != null)
            {
                var vm = new TipoPago()
                {
                    Id = tipoPago.Id,
                    Nombre = tipoPago.Nombre,
                };
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(TipoPago vm)
        {
            var tipoPago = tipoPagoctx.Get(vm.Id);

            if (tipoPago != null)
            {
                tipoPago.Nombre = vm.Nombre;
                tipoPagoctx.Update(tipoPago);
                return RedirectToAction("Index");
            }

            return View(vm);
        }


    }
}
