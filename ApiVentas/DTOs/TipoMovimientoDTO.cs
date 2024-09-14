namespace ApiVentas.DTOs
{
    public class TipoMovimientoDTO
    {
        public int TipomovId { get; set; }
        public string? TipomovDescripcion { get; set; }
        public short? TipomovIngEgr { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
