namespace PagosAcademicos.Areas.Admin.Models.ViewModels
{
    public class IndexPagosViewModel
    {
        public int Id { get; set; }
        public UsuarioModel Usuario { get; set; } = null!;

        public decimal Monto { get; set; }

        public string Concepto { get; set; } = null!;
        public DateTime Fecha { get; set; } 

    }

    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
