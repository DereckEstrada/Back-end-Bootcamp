namespace ApiVentas.Utilitarios
{
    public class ControlError
    {
        public void LogErrorMetodos(string clase, string error, string metodo) 
        {
            string ruta = string.Empty;
            string archivo = string.Empty;
            DateTime fecha = DateTime.Now;
            try
            {
                ruta = "C:\\Proyecto integrador\\LogsApisGrupal";
                archivo = $"Log_{fecha.ToString("dd-MM-yyyy")}";

                if(!Directory.Exists(ruta))

                {
                    Directory.CreateDirectory(ruta);
                }

                StreamWriter writ = new StreamWriter($"{ruta}\\{archivo}");
                writ.WriteLine($"Se presento una novedad en la clase: {clase} en el metodo: {metodo}, con el siguiete error: {error}");
                writ.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
