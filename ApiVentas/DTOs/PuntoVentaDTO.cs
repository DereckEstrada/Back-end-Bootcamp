using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class PuntoVentaDTO
    {
        public int PuntovtaId { get; set; }
        public string? PuntovtaNombre { get; set; }
        public int? PuntoEmisionId { get; set; }
        public string? PuntoEmisionDescripcion{ get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
