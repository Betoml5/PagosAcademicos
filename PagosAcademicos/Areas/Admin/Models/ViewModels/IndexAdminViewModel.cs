

using PagosAcademicos.Models.Entities;
using PagosAcademicos.Models.ViewModels;
namespace PagosAcademicos.Areas.Admin.Models.ViewModels;
public class IndexAdminViewModel
{
    public decimal MontoMasAlto { get; set; }
    public decimal UltimoPago { get; set; }
    public int NumeroPagos { get; set; }
    public DateTime FechaUltimoPago { get; set; }

    public IEnumerable<Pago>? Pagos { get; set; } = null!;

}