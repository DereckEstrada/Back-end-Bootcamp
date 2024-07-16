using System;
using System.Collections.Generic;

namespace ApiVentas.Models;

public partial class Categorium
{
    public int CategoriaId { get; set; }

    public string? CategoriaDescrip { get; set; }

    public short? Estado { get; set; }

    public DateTime? FechaHoraReg { get; set; }

    public DateTime? FechaHoraAct { get; set; }

    public int? UsuIdReg { get; set; }

    public int? UsuIdAct { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
