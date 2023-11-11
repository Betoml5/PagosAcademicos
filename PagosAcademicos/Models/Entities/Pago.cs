using System;
using System.Collections.Generic;

namespace PagosAcademicos.Models.Entities;

public partial class Pago
{
    public int Id { get; set; }

    public string Monto { get; set; } = null!;

    public string Concepto { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int UsuarioId { get; set; }

    public int TipoPagoId { get; set; }

    public virtual TipoPago TipoPago { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
