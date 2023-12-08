



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Areas.Admin.Models.ViewModels;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Models.ViewModels;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly Repository<Carrera> carreraRepository;
        private readonly Repository<Semestre> semestreRepository;

        public UsuariosController(UsuarioRepository usuarioRepository, Repository<Carrera> carreraRepository, Repository<Semestre> semestreRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.carreraRepository = carreraRepository;
            this.semestreRepository = semestreRepository;

        }

        public IActionResult Index()
        {
            var vm = usuarioRepository
            .GetAll()
            .Where(x => x.Rol.Nombre == "Usuario")
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





        public IActionResult Editar(int Id)
        {

            var usuario = usuarioRepository.Get(Id);
            var carreras = carreraRepository.GetAll();
            var semestres = semestreRepository.GetAll();

            if (usuario != null)
            {
                var vm = new AgregarUsuarioViewModel()
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Carrera = usuario.CarreraId,
                    Semestre = usuario.SemestreId,
                    Carreras = carreras,
                    Semestres = semestres
                };

                return View(vm);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(AgregarUsuarioViewModel vm)
        {
            var usuario = usuarioRepository.Get(vm.Id);
            var carreras = carreraRepository.GetAll();
            var semestres = semestreRepository.GetAll();
            if (usuario != null)
            {
                if (string.IsNullOrWhiteSpace(vm.Nombre))
                {
                    ModelState.AddModelError("", "El nombre es requerido");
                }

                if (string.IsNullOrWhiteSpace(vm.Apellido))
                {
                    ModelState.AddModelError("", "El apellido es requerido");
                }

                if (vm.Carrera == 0)
                {
                    ModelState.AddModelError("", "La carrera es requerida");
                }

                if (vm.Semestre == 0)
                {
                    ModelState.AddModelError("", "El semestre es requerido");
                }

                if (!ModelState.IsValid)
                {
                    vm.Carreras = carreras;
                    vm.Semestres = semestres;
                    return View(vm);
                }


                usuario.Nombre = vm.Nombre;
                usuario.Apellido = vm.Apellido;
                usuario.SemestreId = vm.Semestre;
                usuario.CarreraId = vm.Carrera;
                usuarioRepository.Update(usuario);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }




        public IActionResult Eliminar(int Id)
        {
            var user = usuarioRepository.Get(Id);
            if (user != null)
            {
                var vm = new EliminarUsuarioViewModel()
                {
                    Id = user.Id,
                    Nombre = user.Nombre
                };
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(EliminarUsuarioViewModel usuario)
        {
            var user = usuarioRepository.Get(usuario.Id);
            if (user != null)
            {
                usuarioRepository.Delete(user);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }


    }
}