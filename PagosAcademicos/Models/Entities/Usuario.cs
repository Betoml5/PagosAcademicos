using System;
using System.Collections.Generic;

namespace PagosAcademicos.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public ulong Estatus { get; set; }

    public string Folio { get; set; } = null!;

    public int SemestreId { get; set; }

    public int CarreraId { get; set; }

    public virtual Carrera Carrera { get; set; } = null!;

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();

    public virtual Semestre Semestre { get; set; } = null!;
}
