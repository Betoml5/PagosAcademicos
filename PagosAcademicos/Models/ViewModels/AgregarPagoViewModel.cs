namespace PagosAcademicos.Models.ViewModels

{
    public class AgregarPagoViewModel
    {
        public int MetodoDePagoId { get; set; }
        public decimal Monto { get; set; }
        public string? Concepto { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? FechaExpiracion { get; set; }
        public string? CVV { get; set; }

        public IEnumerable<MetodoPago>? MetodosPago { get; set; }

    }

    public class MetodoPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }


}

