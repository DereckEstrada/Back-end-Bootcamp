namespace ApiVentas.DTOs
{
    public class UsuarioPermisoDTO
    {
        public int PermisoId { get; set; }
        public string? Modulodescripcion { get; set; }
        public string? OpcionDescripcion { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? Estado {  get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? UsuIdReg {  get; set; }
        public int? UsuIdAct {  get; set; }
    }
}
