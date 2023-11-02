using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Repositories
{
    public class SemestreRepository
    {

        private readonly PagosAcademicosContext ctx;
        public SemestreRepository(PagosAcademicosContext ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<Semestre> Get()
        {
            return ctx.Semestre.OrderBy(x => x.Nombre);
        }

        public Semestre? Get(int Id)
        {
            return ctx.Semestre.Find(Id);
        }

        public Semestre? Get(string nombre)
        {
            return ctx.Semestre.FirstOrDefault(x => x.Nombre == nombre);
        }

        public void Delete(Semestre semestre)
        {
            ctx.Remove(semestre);
            ctx.SaveChanges();
        }

        public void Update(Semestre semestre)
        {
            ctx.Update(semestre);
            ctx.SaveChanges();
        }

        public void Insert(Semestre semestre)
        {
            ctx.Add(semestre);
            ctx.SaveChanges();
        }
    }
}
