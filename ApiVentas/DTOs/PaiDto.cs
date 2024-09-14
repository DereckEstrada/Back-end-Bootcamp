using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public  class PaiDTO
    {
        public int PaisId { get; set; }
        public string? PaisNombre { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
