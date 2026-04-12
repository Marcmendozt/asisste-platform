using System.Threading.Tasks;

namespace XAMAsisste.Servicios.Identidad
{
    interface IdentidadServicio
    {
        Task<bool> VerificarRegistracion();
        Task Autenticar();
    }
}
