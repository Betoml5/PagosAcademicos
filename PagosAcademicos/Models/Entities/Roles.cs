using System;
using System.Collections.Generic;

namespace PagosAcademicos.Models.Entities;

public partial class Roles
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
