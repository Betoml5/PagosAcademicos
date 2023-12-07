


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Areas.Admin.Models.ViewModels;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
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
        public IActionResult Agregar(AgregarTipoPagoViewModel tipoPago)
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

            if (tipoPago.Icono == null)
            {
                ModelState.AddModelError("", "El icono es requerido");
            }

            if (tipoPago.Icono != null && tipoPago.Icono.ContentType != "image/png")
            {
                ModelState.AddModelError("", "El icono debe ser png");
            }

            // if (tipoPago.Icono != null && tipoPago.Icono.Length > 1024 * 1024)
            // {
            //     ModelState.AddModelError("", "El icono debe pesar menos de 1MB");
            // }


            if (ModelState.IsValid)
            {
                var metodoPago = new TipoPago()
                {
                    Nombre = tipoPago.Nombre,
                };

                tipoPagoctx.Insert(metodoPago);

                if (tipoPago.Icono != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/icons/{metodoPago.Id}.png");
                    tipoPago.Icono.CopyTo(fs);
                    fs.Close();
                }

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
                var vm = new AgregarTipoPagoViewModel()
                {
                    Id = tipoPago.Id,
                    Nombre = tipoPago.Nombre,
                };
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(AgregarTipoPagoViewModel vm)
        {
            var pago = tipoPagoctx.Get(vm.Id);
            var tipoPagoExistenteNombre = tipoPagoctx.GetByNombre(vm.Nombre);

            if (pago == null)
            {
                return RedirectToAction("Index");
            }

            if (tipoPagoExistenteNombre != null && pago.Id != vm.Id)
            {
                ModelState.AddModelError("", "Ese tipo de pago ya existe");
            }

            if (string.IsNullOrWhiteSpace(vm.Nombre))
            {
                ModelState.AddModelError("", "El nombre es requerido");
            }

            if (vm.Icono == null)
            {
                ModelState.AddModelError("", "El icono es requerido");
            }

            if (vm.Icono != null && vm.Icono.ContentType != "image/png")
            {
                ModelState.AddModelError("", "El icono debe ser png");
            }


            if (ModelState.IsValid)
            {

                pago.Nombre = vm.Nombre;
                tipoPagoctx.Update(pago);
                if (vm.Icono != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/icons/{pago.Id}.png");
                    vm.Icono.CopyTo(fs);
                    fs.Close();
                }


                return RedirectToAction("Index");
            }

            return View(vm);
        }


    }
}
