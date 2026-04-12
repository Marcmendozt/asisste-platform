using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShellLogin.Services.Routing
{
    public class CascaraEnrutamientoServicio : EnrutamientoServicio
    {
        public CascaraEnrutamientoServicio()
        {
        }
        public Task Regresar()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task RegresarModal()
        {
            return Shell.Current.Navigation.PopModalAsync();
        }

        public Task Navegar(string Ruta)
        {
            return Shell.Current.GoToAsync(Ruta);
        }

      

        public Task Mensajes(string Titulo, string Mensaje, string Salida)
        {
            return Shell.Current.DisplayAlert(Titulo,Mensaje,Salida);
        }

    

        Task<bool> EnrutamientoServicio.MensajesSalida(string Titulo, string Mensaje, string Entrada, string Salida)
        {
            return Shell.Current.DisplayAlert(Titulo, Mensaje, Entrada, Salida);
        }
    }
}
