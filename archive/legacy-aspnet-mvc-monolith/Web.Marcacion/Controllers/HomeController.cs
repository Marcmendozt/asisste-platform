using System;
using System.Web.Mvc;

using Web.Marcacion.Entidades;

using Web.Marcacion.Reglas;



namespace Web.Marcacion.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Login()
        {
            return View();
        }




        public string Ingresar(string us, string pw)
        {
            string Respuesta = "";
            try
            {
                brUsuario obrUsuario = new brUsuario();
                beUsuario obeUsuario = new beUsuario();
                var PassEncrip = Encripta(pw);
                beRespuesta obeRespuesta = obrUsuario.ValidarLogin(us, PassEncrip);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    obeUsuario = (beUsuario)obeRespuesta.Data;
                    //Nombre Completo y última Conexión
                    Session["DATOSUSUARIO"] = obeUsuario;
                    Respuesta = "Correcto_" + obeUsuario.ID_Perfil + "_" + obeUsuario.ID_Persona + "_" + obeUsuario.NombreCompleto + "_" + obeUsuario.Genero + "_" + obeUsuario.Perfil + "_" + obeUsuario.ID_Usuario + "_" + obeUsuario.DocumentoIdentidad + "_" + obeUsuario.Nombres + "_" + obeUsuario.Apellidos + "_" + obeUsuario.Usuario;




                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception E)
            {
                Respuesta = "Error_" + E.Message;
            }

            return Respuesta;
        }


        public string CerrarSesion()
        {
            string Valor = "";
            try
            {
                brUsuario obrUsuario = new brUsuario();
                beUsuario oUsuario = (beUsuario)Session["DATOSUSUARIO"];
                Session.RemoveAll();
                Valor = "Correcto_" + "1";
            }
            catch (Exception EX)
            {

                Valor = "ERROR_" + EX.Message;
            }

            return Valor;

        }



        [HttpPost]
        public string Encripta(string valor)
        {
            return Seguridad.Seguridad.Encriptar(valor);
        }


        [HttpPost]
        public string Desencripta(string valor)
        {
            return Seguridad.Seguridad.Desencriptar(valor);
        }


        public ActionResult TimeOver()
        {
            return View();
        }



        public ActionResult RecuperarContraseña()
        {

            return View();
        }



    }
}