using Microsoft.AspNetCore.Mvc;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

namespace PagosAcademicos.Controllers
{
    public class UsuarioController : Controller


        
    {
        private readonly Repository<Usuario> ctx;

        public UsuarioController(Repository<Usuario> ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
