using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public class UsuarioAutenticationDTO
    {
        public string? Username { get; set; }

        public string? Userpassword { get; set; }

        public int? UsuIdReg { get; set; }
        public string? UsuRegName{ get; set; }

    }
}
