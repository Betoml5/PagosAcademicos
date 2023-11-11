using System;
using System.Collections.Generic;

namespace PagosAcademicos.Models.Entities;

public partial class TipoPago
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
