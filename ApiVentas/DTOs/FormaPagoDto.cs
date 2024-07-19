namespace ApiVentas.DTOs
{
    public class FormaPagoDto
    {
        public int FpagoID { get; set; }

        public string? FpagoDescrip { get; set; }

        public short? Estado { get; set; }

        public DateTime? FecHoraReg { get; set; }

        public DateTime? FecHoraAct { get; set; }
    }
}
