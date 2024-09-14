using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public  class MovimientoCabDTO
    {
        public int MovicabId { get; set; }
        public int? TipomovId { get; set; }
        public string? TipomovDescripcion { get; set; }
        public int? TipomovIngEgr { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescripcion { get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescripcion { get; set; }
        public int? BodegaId { get; set; }
        public string? BodegaDescripcion { get; set; }
        public string? SecuenciaFactura { get; set; }
        public string? AutorizacionSri { get; set; }
        public string? ClaveAcceso { get; set; }
        public int? ClienteId { get; set; }
        public string? ClienteName { get; set; }
        public int? PuntovtaId { get; set; }
        public string? PuntovtaDescripcion { get; set; }
        public int? ProveedorId { get; set; }
        public string? ProveedorDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
