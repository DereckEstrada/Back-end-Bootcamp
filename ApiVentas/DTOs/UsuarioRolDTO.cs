namespace ApiVentas.DTOs
{
    public class UsuarioRolDTO
    {
        public int UsuRolId { get; set; }
        public int? UsuId { get; set; }
        public string? UsuDescripcion { get; set; }
        public int? RolId { get; set; }
        public string? RolDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
