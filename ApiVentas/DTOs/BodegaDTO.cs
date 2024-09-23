namespace ApiVentas.DTOs
{
    public class BodegaDTO
    {
        public int BodegaId { get; set; }
        public string? BodegaNombre { get; set; }
        public string? BodegaDireccion { get; set; }
        public string? BodegaTelefono { get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
