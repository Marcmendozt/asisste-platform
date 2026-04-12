using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Marcacion.Datos;
using Web.Marcacion.Entidades;

namespace Web.Marcacion.Reglas
{
    public class brPerfil : brGeneral
    {
        public beRespuesta JListadoPerfil()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daPerfil odaPerfil = new daPerfil();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaPerfil.ListadoPerfiles(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }


        public beRespuesta Agregar(bePerfil obePerfil)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daPerfil odaPerfil = new daPerfil();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();
                
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaPerfil.Agregar(con, obePerfil);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }

      

        public beRespuesta Editar(bePerfil obePerfil)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daPerfil odaPerfil = new daPerfil();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaPerfil.Editar(con, obePerfil);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }



        public beRespuesta Eliminar(int ID_Perfil)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daPerfil odaPerfil = new daPerfil();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaPerfil.Eliminar(con, ID_Perfil);
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
