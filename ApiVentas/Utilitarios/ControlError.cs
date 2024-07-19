using Microsoft.IdentityModel.Tokens;

namespace ApiVentas.Utilitarios
{
    public class ControlError
    {
        private readonly IConfiguration _configuration;

        public ControlError()
        {
        }

        public ControlError(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void LogError(string clase, string metodo, string message)
        {

            var ruta=string.Empty;
            var archivo=string.Empty;
            DateTime date=DateTime.Now; 
            try
            {
                ruta = _configuration.GetSection("Rutas:CarpetaLog").Value;
                archivo = $"Log_{date.ToString("dd-MM-yyyy")}";
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }
                StreamWriter writer=new StreamWriter($"{ruta}{archivo}", true);
                writer.WriteLine($"Se presento una novedad en la clase: '{clase}', en el metodo: '{metodo}', con el siguente error: '{message}'");
                writer.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
