using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Models.ViewModels;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Controllers
{
    [Authorize(Roles = "Usuario")]
    
    public class PagosController : Controller
    {
        private Repository<Pago> ctx;
        private TipoPagoRepository ctxTipoPago;

        public PagosController(Repository<Pago> ctx, TipoPagoRepository ctxTipoPago)
        {
            this.ctx = ctx;
            this.ctxTipoPago = ctxTipoPago;
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



        public IActionResult Pagar()
        {

            var vm = new AgregarPagoViewModel()
            {
                MetodosPago = ctxTipoPago
                    .GetAll()
                    .Select(tipoPago => new MetodoPago
                    {
                        Id = tipoPago.Id,
                        Nombre = tipoPago.Nombre
                    })
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Pagar(AgregarPagoViewModel vm)
        {


            if (vm.MetodoDePagoId == 0)
            {
                ModelState.AddModelError("", "El metodo de pago es requerido");
            }

            if (string.IsNullOrWhiteSpace(vm.NumeroTarjeta))
            {
                ModelState.AddModelError("", "El numero de tarjeta es requerido");
            }

            if (!string.IsNullOrWhiteSpace(vm.NumeroTarjeta) && vm.NumeroTarjeta.Length != 16)
            {
                ModelState.AddModelError("", "El numero de tarjeta debe tener 16 digitos");
            }

            if (string.IsNullOrWhiteSpace(vm.CVV))
            {
                ModelState.AddModelError("", "El CVV es requerido");
            }

            if (!string.IsNullOrWhiteSpace(vm.CVV) && vm.CVV.Length != 3)
            {
                ModelState.AddModelError("", "El CVV debe tener 3 digitos");
            }

            if (string.IsNullOrWhiteSpace(vm.FechaExpiracion))
            {
                ModelState.AddModelError("", "La fecha de expiracion es requerida");
            }

            if (!string.IsNullOrWhiteSpace(vm.FechaExpiracion) && vm.FechaExpiracion.Length != 5)
            {
                ModelState.AddModelError("", "La fecha de expiracion debe tener el formato MM/YY");
            }




            if (!string.IsNullOrWhiteSpace(vm.FechaExpiracion))
            {
                var mes = vm.FechaExpiracion.Substring(0, 2);
                var anio = vm.FechaExpiracion.Substring(3, 2);

                if (int.Parse(mes) < 1 || int.Parse(mes) > 12)
                {
                    ModelState.AddModelError("", "El mes de expiracion debe ser un numero entre 1 y 12");
                }

                if (int.Parse(anio) < 22 || int.Parse(anio) > 30)
                {
                    ModelState.AddModelError("", "El año de expiracion debe ser un numero entre 21 y 30");
                }

                if (anio == "23" && int.Parse(mes) < 12)
                {
                    ModelState.AddModelError("", "La tarjeta esta vencida");
                }
            }

            if (ModelState.IsValid)
            {
                var pago = new Pago
                {
                    Concepto = "Pago de colegiatura",
                    Fecha = System.DateTime.Now,
                    TipoPagoId = vm.MetodoDePagoId,
                    UsuarioId = 3,
                    Monto = 2750
                };
                ctx.Insert(pago);
                return RedirectToAction("Index", "Usuario");
            }


            vm.MetodosPago = ctxTipoPago
                .GetAll()
                .Select(tipoPago => new MetodoPago
                {
                    Id = tipoPago.Id,
                    Nombre = tipoPago.Nombre
                });


            return View(vm);
        }


    }
}
