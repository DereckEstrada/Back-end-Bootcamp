namespace ApiVentas.Utilitarios
{
    public class EnumOptions
    {
        public enum Options
        {
            id, nombre, producto, login, marca, empresa,all, direccion, telefono, estado, usuario, precio
        }
        public bool ValidOptions(string option)
        {
            return Enum.IsDefined(typeof(Options), option);
        }
    }
}
