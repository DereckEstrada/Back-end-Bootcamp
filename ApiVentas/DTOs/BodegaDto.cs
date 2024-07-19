namespace ApiVentas.DTOs
{
    public class BodegaDto
    {
        public int BodegaID { get; set; }

        public string? BodNombre { get; set; }

        public string? BodDir { get; set; }

        public string? BodTel { get; set; }

        public short? Estado { get; set; }

        public DateTime? FecHoraReg { get; set; }

        public DateTime? FecHoraAct { get; set; }

        public string? SucursalDescrip { get; set; }
    }
}
