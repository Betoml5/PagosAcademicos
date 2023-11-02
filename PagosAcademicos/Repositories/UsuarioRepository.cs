using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Repositories
{
    public class UsuarioRepository
    {
        private readonly PagosAcademicosContext ctx;
        public UsuarioRepository(PagosAcademicosContext ctx)
        {
                this.ctx = ctx;
        }
        public IEnumerable<Usuario> Get() {
            return ctx.Usuario.OrderBy(x => x.Nombre);
        }

        public Usuario? Get(int Id)
        {
            return ctx.Usuario.Find(Id);
        }

        public Usuario? Get(string nombre)
        {
            return ctx.Usuario.FirstOrDefault(x => x.Nombre == nombre);
        }

        public void Delete(Usuario usuario)
        {
            ctx.Remove(usuario);
            ctx.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            ctx.Update(usuario);
            ctx.SaveChanges();
        }

        public void Insert(Usuario usuario)
        {
            ctx.Add(usuario);
            ctx.SaveChanges();
        }
    }
}
