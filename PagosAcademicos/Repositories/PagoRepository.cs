


using Microsoft.EntityFrameworkCore;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

public class PagoRepository : Repository<Pago>
{
    public PagoRepository(FreedbClienteContext ctx) : base(ctx)
    {
    }

    override
        public IEnumerable<Pago> GetAll()
    {
        return ctx.Pago
        .OrderBy(x => x.Fecha)
        .Include(x => x.TipoPago)
        .Include(x => x.Usuario);

    }
}