
using ShellLogin.Services.Routing;
using Splat;
using System;
using Xamarin.Forms;
using XAMAsisste.Servicios.Identidad;
using XAMAsisste.Servicios.SQL.BaseDatos;
using XAMAsisste.VistaModelos;

namespace XAMAsisste
{
    public partial class App : Application
    {




        public static BDAsisste BD;

       
        public App()
        {

            if (BD == null)
            {
                string BaseDatosAsisste = "SQLLiteAsisste.db3";
                string Ruta = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), BaseDatosAsisste);
                BD = new BDAsisste(Ruta);
            }
            Inicio();
            InitializeComponent();

            MainPage = new Cascara();
           
        }

        private void Inicio() {
            // Servicios
            Locator.CurrentMutable.RegisterLazySingleton<EnrutamientoServicio>(() => new CascaraEnrutamientoServicio());
            Locator.CurrentMutable.RegisterLazySingleton<IdentidadServicio>(() => new IdentidadServicioTalon());

            // VistaModelos
            Locator.CurrentMutable.Register(() => new CargandoVistaModelo());
            Locator.CurrentMutable.Register(() => new LoginVistaModelo());
            Locator.CurrentMutable.Register(() => new FaltasVistaModelo());

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            
        }

       








    }
}
