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
    public class brUbicacion : brGeneral
    {
        public beRespuesta JListadoUbicacion()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUbicacion odaUbicacion = new daUbicacion();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUbicacion.ListadoUbicacion(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta JListadoUbicacionPorUsuario(int ID_Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUbicacion odaUbicacion = new daUbicacion();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUbicacion.ListadoUbicacionPorUsuario(Conexion, ID_Usuario);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }


        public beRespuesta Editar(beUbicacion obeUbicacion)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUbicacion odaUbicacion = new daUbicacion();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUbicacion.Editar(con, obeUbicacion);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }

        public beRespuesta Eliminar(int ID_Ubicacion)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daUbicacion odaUbicacion = new daUbicacion();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaUbicacion.Eliminar(con, ID_Ubicacion);
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
