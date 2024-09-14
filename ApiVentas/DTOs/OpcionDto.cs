using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public  class OpcionDTO
    {
        public int OpcionId { get; set; }
        public string? OpcionDescripcion { get; set; }
        public int? ModuloId { get; set; }
        public string? ModuloDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
