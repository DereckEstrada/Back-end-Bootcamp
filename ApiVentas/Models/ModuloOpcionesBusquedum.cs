using System;
using System.Collections.Generic;

namespace ApiVentas.Models;

public partial class ModuloOpcionesBusquedum
{
    public int ModuloOpcionesBusquedaId { get; set; }

    public int? OpcionesBusquedaId { get; set; }

    public int? ModuloId { get; set; }

    public int? EstadoId { get; set; }

    public DateTime? FechaHoraReg { get; set; }

    public int? UsuIdReg { get; set; }

    public DateTime? FechaHoraAct { get; set; }

    public int? UsuIdAct { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual Usuario? UsuIdActNavigation { get; set; }

    public virtual Usuario? UsuIdRegNavigation { get; set; }
}
