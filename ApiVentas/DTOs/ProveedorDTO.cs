using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class ProveedorDTO
    {
        public int ProvId { get; set; }
        public string? ProvRuc { get; set; }
        public string? ProvNomComercial { get; set; }
        public string? ProvRazon { get; set; }
        public string? ProvDireccion { get; set; }
        public string? ProvTelefono { get; set; }
        public int? CiudadId { get; set; }
        public string? CiudadDescripcion{ get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
