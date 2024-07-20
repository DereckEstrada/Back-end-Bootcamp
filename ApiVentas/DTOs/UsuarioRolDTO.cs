namespace ApiVentas.DTOs
{
    public class UsuarioRolDTO
    {
        public int? UsuarioRolID { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? RolDescripcion { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdIdAct { get; set; }
    }
}
