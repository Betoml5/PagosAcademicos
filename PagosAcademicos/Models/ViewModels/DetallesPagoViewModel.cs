

namespace PagosAcademicos.Models.ViewModels
{
    public class DetallesPagoViewModel
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Fecha { get; set; } = null!;
        public string Concepto { get; set; } = null!;
        public string MetodoDePago { get; set; } = null!;
    }
}