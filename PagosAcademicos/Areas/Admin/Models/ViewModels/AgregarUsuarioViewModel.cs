

using PagosAcademicos.Models.Entities;

namespace PagosAcademicos.Areas.Admin.Models.ViewModels
{
    public class AgregarUsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Carrera { get; set; }
        public int Semestre { get; set; }

        public IEnumerable<Carrera>? Carreras { get; set; }
        public IEnumerable<Semestre>? Semestres { get; set; }
    }
}