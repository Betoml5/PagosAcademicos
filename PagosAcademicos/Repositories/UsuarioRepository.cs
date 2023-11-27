


using Microsoft.EntityFrameworkCore;
using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Repositories
{

    public class UsuarioRepository : Repository<Usuario>
    {
        public UsuarioRepository(PagosacademicosContext ctx) : base(ctx)
        {
        }

        override
        public IEnumerable<Usuario> GetAll()
        {
            return ctx.Usuario
            .OrderBy(x => x.Nombre)
            .Include(x => x.Carrera)
            .Include(x => x.Semestre);
        }

    }
}