namespace ApiVentas.DTOs
{
    public class TipoMovimientoDTO
    {
        public int TipomovId { get; set; }

        public string? TipomovDescrip { get; set; }

        public short? TipomovIngEgr { get; set; }

        public string? EstadoDescrip { get; set; }

        public DateTime? FechaHoraReg { get; set; }

        public DateTime? FechaHoraAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
