using System;
using System.Collections.Generic;

namespace ApiVentas.Models;

public partial class Modulo
{
    public int ModuloId { get; set; }

    public string? ModuloDescripcion { get; set; }

    public short? Estado { get; set; }

    public DateTime? FechaHoraReg { get; set; }

    public DateTime? FechaHoraAct { get; set; }

    public int? UsuIdReg { get; set; }

    public int? UsuIdAct { get; set; }

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
}
