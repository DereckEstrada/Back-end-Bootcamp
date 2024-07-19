namespace ApiVentas.DTOs
{
    public class IndustriaDto
    {
        public int IndustriaID { get; set; }

        public string? IndustriaDescrip { get; set; }

        public short? Estado { get; set; }

        public DateTime? FecHoraReg { get; set; }

        public DateTime? FecHoraAct { get; set; }
    }
}
