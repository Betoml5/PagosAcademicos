namespace PagosAcademicos.Models.ViewModels
{
    public class IndexUsuarioViewModel
    {
        public string Nombre { get; set; } = null!;
        public bool Estatus { get; set; }
        public IEnumerable<PagoModel>? Pagos { get; set; }
    }

    public class PagoModel
    {
        public int Id { get; set; }
        public string Concepto { get; set; } = null!;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }


    }
}
