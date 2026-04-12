using System.Data.SqlClient;
using Web.Marcacion.Datos;
using Web.Marcacion.Entidades;

namespace Web.Marcacion.Reglas
{
    public class brUsuario : brGeneral
    {
        public beRespuesta ValidarLogin(string Usuario, string Clave)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.Login(con, Usuario, Clave);
                    if (obeRespuesta.Data == null)
                    {
                        obeRespuesta.ExisteError = true;
                        obeRespuesta.MensajeError = "EL USUARIO NO EXISTE";
                    }
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta BuscarUsuario(string Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.BuscarUsuario(con, Usuario);
                    if (obeRespuesta.Data == null)
                    {
                        obeRespuesta.ExisteError = true;
                        obeRespuesta.MensajeError = "EL USUARIO NO EXISTE";
                    }
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }


        public beRespuesta Agregar(beUsuario obeUsuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.Agregar(con, obeUsuario);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }


        public beRespuesta RecuperarContraseña(string Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.RecuperarContraseña(con, Usuario);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }



        public beRespuesta CapturarIDUsuario(string Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.CapturarIDUsuario(con, Usuario);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }



        public beRespuesta Eliminar(int ID_Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.Eliminar(con, ID_Usuario);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }
    }
}
