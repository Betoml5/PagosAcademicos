namespace PagosAcademicos.Models.ViewModels

{
    public class AgregarPagoViewModel
    {
        public int MetodoDePagoId { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } = null!;
        public string NumeroTarjeta { get; set; } = null!;
        public string FechaExpiracion { get; set; } = null!;
        public string CVV { get; set; } = null!;

        public IEnumerable<MetodoPago>? MetodosPago { get; set; }

    }

    public class MetodoPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }


}

