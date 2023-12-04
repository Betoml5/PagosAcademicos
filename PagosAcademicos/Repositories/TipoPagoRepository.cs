


using Microsoft.EntityFrameworkCore;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

public class TipoPagoRepository : Repository<TipoPago>
{
    public TipoPagoRepository(FreedbClienteContext ctx) : base(ctx)
    {
    }

    override
        public IEnumerable<TipoPago> GetAll()
    {
        return ctx.TipoPago
        .Include(x => x.Pago)
        .OrderBy(x => x.Nombre);
    }

    public TipoPago? GetByNombre(string nombre)
    {
        return ctx.TipoPago
        .Where(x => x.Nombre == nombre)
        .FirstOrDefault();
    }
}