namespace ApiVentas.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        public string? CategoriaDescrip { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
