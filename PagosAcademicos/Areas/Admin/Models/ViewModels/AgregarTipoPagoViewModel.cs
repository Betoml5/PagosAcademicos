

namespace PagosAcademicos.Areas.Admin.Models.ViewModels
{
    public class AgregarTipoPagoViewModel
    {
        public string Nombre { get; set; } = null!;
        public IFormFile Icono { get; set; } = null!;
    }
}