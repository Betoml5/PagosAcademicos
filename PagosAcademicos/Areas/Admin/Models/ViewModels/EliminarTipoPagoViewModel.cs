
namespace PagosAcademicos.Areas.Admin.Models.ViewModels;
public class EliminarTipoPagoViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public int PagosRegistrados { get; set; }
}

