using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public  class MovimientoDetPagoDTO
    {
        public int MovidetPagoId { get; set; }
        public int? MovicabId { get; set; }
        public string? MovicabDescripcion { get; set; }
        public int? FpagoId { get; set; }
        public string? FpagoDescripcion { get; set; }
        public decimal? ValorPagado { get; set; }
        public int? IndustriaId { get; set; }
        public string? IndustriaDescripcion { get; set; }
        public string? Lote { get; set; }
        public string? Voucher { get; set; }
        public int? TarjetacredId { get; set; }
        public string? TarjetacredDescripcion { get; set; }
        public int? BancoId { get; set; }
        public int? ComprobanteId { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? ClienteId { get; set; }
        public string? ClienteDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}