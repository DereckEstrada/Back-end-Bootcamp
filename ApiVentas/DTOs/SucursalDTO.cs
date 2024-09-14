namespace ApiVentas.DTOs
{
    public class SucursalDTO
    {
        public int SucursalId { get; set; }
        public string? SucursalRuc { get; set; }
        public string? SucursalNombre { get; set; }
        public string? SucursalDireccion { get; set; }
        public string? SucursalTelefono { get; set; }
        public string? CodEstablecimientoSri { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
