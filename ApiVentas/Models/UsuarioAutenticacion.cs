using System;
using System.Collections.Generic;

namespace ApiVentas.Models;

public partial class UsuarioAutenticacion
{
    public string? Username { get; set; }

    public string? Userpassword { get; set; }

    public int? UsuId { get; set; }

    public virtual Usuario? Usu { get; set; }
}
