using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class RolDTO
    {
        public int RolId { get; set; }
        public string? RolDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }      
    }
}
