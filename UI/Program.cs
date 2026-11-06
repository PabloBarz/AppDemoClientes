using BOL;
using DAL;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //FLUJO UI -> BOL -> DAL (Entidad) -> API REST -> BD
            var http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7292/"),
                Timeout = TimeSpan.FromSeconds(15)
            };

            // Clientes
            var clienteDal = new ClienteDal(http);
            var clienteBol = new ClienteBol(clienteDal);

            // Proveedores
            var proveedorDal = new ProveedorDal(http);
            var proveedorBol = new ProveedorBol(proveedorDal);

            // Iniciar menú principal
            Application.Run(new FrmMain(clienteBol, proveedorBol));


        }
    }
}