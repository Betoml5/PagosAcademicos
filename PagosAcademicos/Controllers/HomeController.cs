using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Helpers;
using PagosAcademicos.Models.ViewModels;
using PagosAcademicos.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PagosAcademicos.Controllers
{

    public class HomeController : Controller
    {
        public HomeController(UsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }

        public UsuarioRepository UsuarioRepository { get; }

        public IActionResult Index()
        {
            if (User.Identity != null)
            {
                var claimEncontrada = User.Identities
                    .SelectMany(ci => ci.Claims)
                    .FirstOrDefault(c => c.Type == "Id");
                string IdClaim = "0";
                if (claimEncontrada != null) { IdClaim = claimEncontrada.Value; }

                var queryUser = UsuarioRepository.GetAll().Where(x => x.Id == int.Parse(IdClaim)).FirstOrDefault();

                
                if (queryUser != null)
                {
                    string RolUsuario = queryUser.Rol.Nombre;

                    if (RolUsuario == "Administrador")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (RolUsuario == "Usuario")
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                }
            }

            IndexLoginViewModel vm = new IndexLoginViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(IndexLoginViewModel vm)
        {

            if (string.IsNullOrEmpty(vm.Correo))
                ModelState.AddModelError("", "Escribe tu correo electrónico");
            if (string.IsNullOrEmpty(vm.Contrasena))
                ModelState.AddModelError("", "Escribe tu contraseña");

            if (ModelState.IsValid) { 

                var user = UsuarioRepository
                    .GetAll()
                    .FirstOrDefault(x => x.Correo?.ToLower() == vm.Correo.ToLower() && x.Contrasena == Encriptacion.StringToSHA512(vm.Contrasena));

                if (user != null)
                {
                    List<Claim> claims = new()
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Nombre),
                        new Claim(ClaimTypes.Role, user.Rol.Nombre)
                    };

                    ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });



                    if (user.Rol.Nombre == "Administrador")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (user.Rol.Nombre == "Usuario")
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                }
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
            }
            return View(vm);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Denied()
        {
            // TODO: Your code here
            return View();
        }




    }
}
