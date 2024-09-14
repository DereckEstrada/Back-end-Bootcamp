using ApiVentas.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiVentas.DTOs
{
    public class StockDTO
    {
        public long StockId { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescripcion{ get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescripcion { get; set; }
        public int? BodegaId { get; set; }
        public string? BodegaDescripcion { get; set; }
        public int? ProdId { get; set; }
        public string? ProdDescripcion { get; set; }
        public int? CantidadStock { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
