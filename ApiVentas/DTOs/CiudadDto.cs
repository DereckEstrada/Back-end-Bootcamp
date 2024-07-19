namespace ApiVentas.DTOs
{
    public class CiudadDto
    {
        public int CiudadID { get; set; }

        public string? CiuNombre { get; set; }

        public int? Estado { get; set; }

        public DateTime? FechaHoraReg { get; set; }

        public DateTime? FechaHoraAct { get; set; }

        public string? PaisDescrip { get; set; }
    }
}
