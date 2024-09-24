using System;
using System.Collections.Generic;

namespace ApiVentas.Models;

public partial class Dashboard
{
    public decimal? TotalDia { get; set; }

    public int? TotalMovimientos { get; set; }

    public int? CantidadStock { get; set; }
}
