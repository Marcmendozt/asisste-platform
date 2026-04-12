using System.Threading.Tasks;

namespace ShellLogin.Services.Routing
{
    public interface EnrutamientoServicio
    {
        Task Regresar();
        Task RegresarModal();
        Task Navegar(string Ruta);

        Task Mensajes(string Titulo,string Mensaje,string Salida);

        Task<bool> MensajesSalida(string Titulo, string Mensaje, string Entrada, string Salida);
    }
}
