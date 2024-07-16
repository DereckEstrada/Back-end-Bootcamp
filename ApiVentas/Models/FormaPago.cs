using System;
using System.Collections.Generic;

namespace ApiVentas.Models;

public partial class FormaPago
{
    public int FpagoId { get; set; }

    public string? FpagoDescripcion { get; set; }

    public short? Estado { get; set; }

    public DateTime? FechaHoraReg { get; set; }

    public DateTime? FechaHoraAct { get; set; }

    public int? UsuIdReg { get; set; }

    public int? UsuIdAct { get; set; }

    public virtual ICollection<MovimientoDetPago> MovimientoDetPagos { get; set; } = new List<MovimientoDetPago>();
}
