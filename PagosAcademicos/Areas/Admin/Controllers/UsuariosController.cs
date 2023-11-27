



using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Areas.Admin.Models.ViewModels;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioRepository usuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            var vm = usuarioRepository
            .GetAll()
            .OrderBy(x => x.Carrera)
            .Select(x => new IndexUsuarioAdminViewModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Carrera = x.Carrera.Nombre,
                Semestre = x.Semestre.Nombre,
                Estatus = x.Estatus == 1,


            });

            return View(vm);
        }

    }
}