using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class PuntoEmisionSriDTO
    {
        public int PuntoEmisionId { get; set; }
        public string? PuntoEmision { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescripcion{ get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescripcion{ get; set; }
        public string? CodEstablecimientoSri { get; set; }
        public int? UltSecuencia { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }      
    }
}
