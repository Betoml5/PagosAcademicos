﻿using Microsoft.AspNetCore.Authorization;
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
        private UsuarioRepository ctxUsuario;

        public PagosController(Repository<Pago> ctx, TipoPagoRepository ctxTipoPago, UsuarioRepository ctxUsuario)
        {
            this.ctx = ctx;
            this.ctxTipoPago = ctxTipoPago;
            this.ctxUsuario = ctxUsuario;
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

            // Verificar que el mes y año esten en el formato correcto

            if (vm.MetodoDePagoId == 0)
            {
                ModelState.AddModelError("", "El método de pago es requerido");
            }

            if (string.IsNullOrWhiteSpace(vm.NumeroTarjeta))
            {
                ModelState.AddModelError("", "El numero de tarjeta es requerido");
            }

            if (!string.IsNullOrWhiteSpace(vm.NumeroTarjeta) && vm.NumeroTarjeta.Length != 16)
            {
                ModelState.AddModelError("", "El numero de tarjeta debe tener 16 dígitos");
            }

            if (string.IsNullOrWhiteSpace(vm.CVV))
            {
                ModelState.AddModelError("", "El CVV es requerido");
            }

            if (!string.IsNullOrWhiteSpace(vm.CVV) && vm.CVV.Length != 3)
            {
                ModelState.AddModelError("", "El CVV debe tener 3 dígitos");
            }

            if (string.IsNullOrWhiteSpace(vm.FechaExpiracion))
            {
                ModelState.AddModelError("", "La fecha de expiración es requerida");
            }



            if (!string.IsNullOrWhiteSpace(vm.FechaExpiracion))
            {
                // verificar que tenga el formato MM/YY con 5 caracteres y que el 3er caracter sea /

                if (vm.FechaExpiracion.Length != 5 || vm.FechaExpiracion[2] != '/')
                {
                    ModelState.AddModelError("", "La fecha de expiración debe tener el formato MM/YY");

                }
                else
                {

                    var mes = vm.FechaExpiracion.Substring(0, 2);
                    var anio = vm.FechaExpiracion.Substring(3, 2);

                    if (int.Parse(mes) < 1 || int.Parse(mes) > 12)
                    {
                        ModelState.AddModelError("", "El mes de expiración debe ser un numero entre 1 y 12");
                    }

                    if (int.Parse(anio) < 23 || int.Parse(anio) > 30)
                    {
                        ModelState.AddModelError("", "El año de expiración debe ser un año entre 23 y 30");
                    }

                    if (anio == "23" && int.Parse(mes) < 12)
                    {
                        ModelState.AddModelError("", "La tarjeta esta vencida");
                    }

                }



            }

            if (!string.IsNullOrWhiteSpace(vm.NumeroTarjeta))
            {
                // Que solo sean numeros
                foreach (var c in vm.NumeroTarjeta)
                {
                    if (!char.IsDigit(c))
                    {
                        ModelState.AddModelError("", "El numero de tarjeta debe contener solo números");
                    }
                }
            }





            if (ModelState.IsValid)
            {
                var claimEncontrada = User.Identities
                        .SelectMany(ci => ci.Claims)
                        .FirstOrDefault(c => c.Type == "Id");
                string IdClaim = "";
                if (claimEncontrada != null) { IdClaim = claimEncontrada.Value; }

                var pago = new Pago
                {
                    Concepto = "Pago de colegiatura",
                    Fecha = System.DateTime.Now,
                    TipoPagoId = vm.MetodoDePagoId,
                    UsuarioId = int.Parse(IdClaim),
                    Monto = 2750
                };
                ctx.Insert(pago);
                var usr = ctxUsuario.Get(int.Parse(IdClaim));
                usr.Estatus = 0;
                ctxUsuario.Update(usr);
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
