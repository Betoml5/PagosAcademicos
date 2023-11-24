

namespace PagosAcademicos.Areas.Admin.Models.ViewModels;

public class IndexTipoPagoViewModel
{


    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public int PagosRegistrados { get; set; }

}