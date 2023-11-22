﻿using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Areas.Admin.Models.ViewModels;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagosController : Controller
    {

        private readonly Repository<Pago> ctx;
        public PagosController(Repository<Pago> ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {

            var vm = ctx
                .GetAll()
                .OrderBy(x => x.Fecha)
                .Select(pago => new IndexPagosViewModel()
                {
                    Id = pago.Id,
                    Usuario = new UsuarioModel()
                    {
                        Id = pago.Usuario.Id,
                        Nombre = pago.Usuario.Nombre
                    },
                    Monto = pago.Monto,
                    Concepto = pago.Concepto,
                    Fecha = pago.Fecha

                });

            return View(vm);
        }

        public IActionResult Detalles(int Id)
        {
            return View();
        }
    }
}
