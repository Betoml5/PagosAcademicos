


using Microsoft.EntityFrameworkCore;
using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Repositories
{

    public class UsuarioRepository : Repository<Usuario>
    {
        public UsuarioRepository(FreedbClienteContext ctx) : base(ctx)
        {
        }

        override
        public IEnumerable<Usuario> GetAll()
        {
            return ctx.Usuario
            .OrderBy(x => x.Nombre)
            .Include(x => x.Carrera)
            .Include(x => x.Semestre)
            .Include(x=>x.Rol);
        }

        public Usuario? GetByNombre(string nombre)
        {
            return ctx.Usuario.FirstOrDefault(x => x.Nombre == nombre);
        }

    }
}