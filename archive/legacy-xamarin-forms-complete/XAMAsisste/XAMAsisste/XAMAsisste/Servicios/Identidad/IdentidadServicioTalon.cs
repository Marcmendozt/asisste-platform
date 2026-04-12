using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XAMAsisste.Servicios.Identidad
{
    class IdentidadServicioTalon : IdentidadServicio
    {
        public Task Autenticar()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarRegistracion()
        {
            await Task.Delay(1337);
            return false;
        }
    }
}
