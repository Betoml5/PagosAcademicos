

using PagosAcademicos.Models.Entities;
using PagosAcademicos.Models.ViewModels;
namespace PagosAcademicos.Areas.Admin.Models.ViewModels;
public class IndexAdminViewModel
{
    public decimal MontoMasAlto { get; set; } = 0;
    public decimal UltimoPago { get; set; } = 0;
    public int NumeroPagos { get; set; } = 0;
    public DateTime FechaUltimoPago { get; set; }

    public IEnumerable<Pago>? Pagos { get; set; } = null!;

}