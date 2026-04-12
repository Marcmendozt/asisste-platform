using ShellLogin.Services.Routing;
using Splat;
using XAMAsisste.Servicios.Identidad;

namespace XAMAsisste.VistaModelos
{
    class CargandoVistaModelo : BaseVistaModelo
    {
        private readonly EnrutamientoServicio IEnrutamientoServicio;
        private readonly IdentidadServicio IIdentidadServicio;

        public CargandoVistaModelo(EnrutamientoServicio ERS = null, IdentidadServicio ITS = null)
        {

            this.IEnrutamientoServicio = ERS ?? Locator.Current.GetService<EnrutamientoServicio>();
            this.IIdentidadServicio = ITS ?? Locator.Current.GetService<IdentidadServicio>();
        }

        public async void Inicio()
        {
            var EstaAutenticado = await this.IIdentidadServicio.VerificarRegistracion();
            if (EstaAutenticado)
            {
                await this.IEnrutamientoServicio.Navegar("///Principal");
            }
            else
            {
                await this.IEnrutamientoServicio.Navegar("///Login");
            }
        }


    }

   
}
