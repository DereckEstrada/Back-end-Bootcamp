using ApiVentas.Models;

namespace ApiVentas.DTOs
{
    public class RequestDTO
    {
        public int? moduloId { get; set; }
        public string? moduloDescripcion { get; set; }
        public int? opcionId { get; set; }
        public string? opcionDescripcion { get; set; }
    }
}
