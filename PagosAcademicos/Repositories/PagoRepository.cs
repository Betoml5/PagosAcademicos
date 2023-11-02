using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Repositories
{

    public class PagoRepository
    {
        private readonly PagosAcademicosContext ctx;


        public PagoRepository(PagosAcademicosContext ctx)
        {
            this.ctx = ctx; 
        }

        public IEnumerable<Pago> Get()
        {
            return ctx.Pago.OrderBy(x => x.Fecha);
        }

        public Pago? Get(int Id)
        {
            return ctx.Pago.Find(Id);
        }

        public void Delete(Pago pago)
        {
            ctx.Pago.Remove(pago);
            ctx.SaveChanges();
        }

        public void Update(Pago pago) {
            ctx.Pago.Update(pago);
            ctx.SaveChanges();
        }

        public void Insert(Pago pago)
        {
            ctx.Pago.Add(pago);
            ctx.SaveChanges();
        }

    }
}
