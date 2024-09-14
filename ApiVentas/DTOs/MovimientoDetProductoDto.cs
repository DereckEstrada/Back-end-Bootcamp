using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public  class MovimientoDetProductoDTO
    {
        public int MovidetProdId { get; set; }
        public int? MovicabId { get; set; }
        public string? MovicabDescripcion { get; set; }
        public int? ProductoId { get; set; }
        public string? ProductoDescripcion { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
