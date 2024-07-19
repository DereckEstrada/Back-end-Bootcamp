namespace ApiVentas.DTOs
{
    public class CategoriaDto
    {
        public int CategoriaID { get; set; }

        public string? CategDescrip { get; set; }

        public short? Estado { get; set; }

        public DateTime? FecHoraReg { get; set; }

        public DateTime? FecHoraAct { get; set; }
    }
}
