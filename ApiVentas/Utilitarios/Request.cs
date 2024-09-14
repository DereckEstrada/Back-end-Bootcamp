namespace ApiVentas.Utilitarios
{
    public class Request
    {
        public dynamic? Usuario { get; set; }
        public string Ip { get; set; }
        public int Modulo { get; set; }
        public string Operacion { get; set; }
        public dynamic? Data { get; set; }
    }
}
