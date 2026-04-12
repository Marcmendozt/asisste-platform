using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WCFAPPAsisste.Data;
using WCFAPPAsisste.Entidades;

namespace WCFAPPAsisste.Reglas
{
    public class brUsuario : brGeneral
    {
        public beRespuesta TraerPerfil(string Usuario, string Clave)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUsuario odaUsuario = new daUsuario();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUsuario.TraerPerfil(con, Usuario, Clave);
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
