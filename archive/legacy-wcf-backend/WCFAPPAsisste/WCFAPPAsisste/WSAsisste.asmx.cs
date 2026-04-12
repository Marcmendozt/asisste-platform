
using System.Web.Services;
using WCFAPPAsisste.Entidades;
using WCFAPPAsisste.Reglas;

namespace WCFAPPAsisste
{


 
    public class WSAsisste : WebService
    {

        [WebMethod]
        public beUsuario WSLogin(string Usuario, string Clave)
        {
            brUsuario obrUsuario = new brUsuario();
            beRespuesta obeRespuesta = new beRespuesta();
            beUsuario obeUsuario = new beUsuario();
            obeRespuesta = obrUsuario.TraerPerfil(Usuario, Clave);

            if (!obeRespuesta.ExisteError && obeRespuesta.MensajeError == null)
            {
                obeUsuario = (beUsuario)obeRespuesta.Data;

            }
            return obeUsuario;
        }
    }
}
