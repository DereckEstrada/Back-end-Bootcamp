namespace ApiVentas.DTOs
{
    public class TarjetaCreditoDTO
    {
        public int TarjetacredId { get; set; }
        public string? TarjetacredDescripcion { get; set; }
        public int? IndustriaId { get; set; }
        public string? IndustriaDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
