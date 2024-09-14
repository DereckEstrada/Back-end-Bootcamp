namespace ApiVentas.DTOs
{
    public class CiudadDTO
    {
        public int CiudadId { get; set; }
        public string? CiudadNombre { get; set; }
        public int? PaisId { get; set; }
        public string? PaisDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
