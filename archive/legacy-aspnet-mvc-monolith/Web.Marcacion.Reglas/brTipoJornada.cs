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
    public class brTipoJornada : brGeneral
    {
        public beRespuesta JListadoTipoJornada()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daTipoJornada odaTipoJornada = new daTipoJornada();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaTipoJornada.ListadoTipoJornada(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }


        public beRespuesta Agregar(beTipoJornada obeTipoJornada)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daTipoJornada odaTipoJornada = new daTipoJornada();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaTipoJornada.Agregar(con, obeTipoJornada);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }


        public beRespuesta Editar(beTipoJornada obeTipoJornada)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daTipoJornada odaTipoJornada = new daTipoJornada();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaTipoJornada.Editar(con, obeTipoJornada);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }


        public beRespuesta Eliminar(int ID_TipoJornada)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daTipoJornada odaTipoJornada = new daTipoJornada();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaTipoJornada.Eliminar(con, ID_TipoJornada);
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
