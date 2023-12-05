﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Models.ViewModels;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : Controller

    {
        private readonly Repository<Usuario> usuarioctx;
        private readonly Repository<Pago> pagoctx;

        public UsuarioController(Repository<Usuario> usuarioctx, Repository<Pago> pagoctx)
        {
            this.usuarioctx = usuarioctx;
            this.pagoctx = pagoctx;
        }
        public IActionResult Index()
        {
            //TODO
            //Aqui tendremos que tráenos los datos del usuario logeado
            var pagos = pagoctx
                .GetAll()
                .Select(x => new PagoModel()
                {
                    Id = x.Id,
                    Concepto = x.Concepto,
                    Monto = x.Monto,
                    Fecha = x.Fecha,
                }).OrderBy(x => x.Fecha);

            var vm = new IndexUsuarioViewModel()
            {
                Nombre = "Juan",
                Estatus = false,
                Pagos = pagos


            };

            return View(vm);

        }

        [Route("Usuario/detalles-pago/{id}")]
        public IActionResult DetallesPago(int id)
        {
            var pago = pagoctx.Get(id);

            if (pago == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new DetallesPagoViewModel()
            {
                Id = pago.Id,
                Monto = pago.Monto,
                Fecha = pago.Fecha.ToString("dd/MM/yyyy"),

                Concepto = pago.Concepto
            };

            return View(vm);
        }



    }
}
