namespace ApiVentas.DTOs
{
    public class TarjetaCreditoDTO
    {
        public int TarjetaCreditoID { get; set; }
        public string? TarjetaDescripcion { get; set; }
        public string? IndustriaDescripcion { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaReg { get; set; }
        public DateTime? FechaAct { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdAct { get; set; }
    }
}
