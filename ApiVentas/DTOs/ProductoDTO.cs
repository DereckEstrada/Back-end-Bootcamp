using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class ProductoDTO
    {
        public int ProdId { get; set; }
        public string? ProdDescripcion { get; set; }
        public decimal? ProdUltPrecio { get; set; }
        public int? CategoriaId { get; set; }
        public string? CategoriaDesripcion { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescripcion { get; set; }
        public int? ProveedorId { get; set; }
        public string? ProveedorDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }       
    }
}
