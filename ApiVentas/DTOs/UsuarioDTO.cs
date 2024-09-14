namespace ApiVentas.DTOs
{
    public class UsuarioDTO
    {
        public int UsuId { get; set; }
        public string? UsuNombre { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
