namespace ApiVentas.DTOs
{
    public class EstadoDTO
    {
        public int EstadoId { get; set; }

        public string? EstadoDescrip { get; set; }

        public int? EstadoFk { get; set; }
        public string? EstadoFkDescripcion { get; set; }

        public DateTime? FechaHoraReg { get; set; }

        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }

    }
}
