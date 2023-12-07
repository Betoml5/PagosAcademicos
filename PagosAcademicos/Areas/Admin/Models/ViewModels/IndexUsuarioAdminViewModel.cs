


namespace PagosAcademicos.Areas.Admin.Models.ViewModels
{

    public class IndexUsuarioAdminViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public bool Estatus { get; set; }
        public string Semestre { get; set; } = null!;
        public string Carrera { get; set; } = null!;


    }

}