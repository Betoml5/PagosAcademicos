﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Areas.Admin.Models.ViewModels;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Models.ViewModels;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Areas.Admin.Controllers
{

	[Area("Admin")]
	[Authorize(Roles = "Administrador")]
	public class HomeController : Controller
	{
		private PagoRepository pagosRepository;

		public HomeController(PagoRepository pagosRepository)
		{
			this.pagosRepository = pagosRepository;
		}

		public IActionResult Index()
		{
			var vm = new IndexAdminViewModel();

			var pagos = pagosRepository
				.GetAll()
				.OrderBy(x => x.Fecha);

			if (!pagos.Any())
			{
				return View(vm);
			}

			var pagoMasAlto = pagosRepository
				.GetAll()
				.OrderByDescending(x => x.Monto)
				.FirstOrDefault();
			var ultimoPago = pagosRepository
				.GetAll()
				.OrderByDescending(x => x.Fecha)
				.FirstOrDefault();


			vm.Pagos = pagos;
			vm.FechaUltimoPago = ultimoPago.Fecha;
			vm.UltimoPago = ultimoPago.Monto;
			vm.MontoMasAlto = pagoMasAlto.Monto;
			vm.NumeroPagos = pagos.Count();

			return View(vm);
		}
	}
}
