namespace ApiVentas.DTOs
{
    public class FormaPagoDTO
    {
        public int FpagoId { get; set; }
        public string? FpagoDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
