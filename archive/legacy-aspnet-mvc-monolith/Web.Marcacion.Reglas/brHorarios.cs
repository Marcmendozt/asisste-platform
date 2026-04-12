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
    public class brHorarios : brGeneral
    {

        public beRespuesta JListadoHorarios()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daHorarios odaHorarios = new daHorarios();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaHorarios.ListadoHorarios(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta JListadoHorariosTipoJornada(int ID_TipoJornada)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daHorarios odaHorarios = new daHorarios();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaHorarios.ListadoHorariosTipoJornada(Conexion, ID_TipoJornada);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta Agregar(beHorarios obeHorarios)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daHorarios odaHorarios = new daHorarios();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaHorarios.AgregarHorarios(con, obeHorarios);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }

        public beRespuesta Editar(beHorarios obeHorarios)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daHorarios odaHorarios = new daHorarios();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaHorarios.EditarHorarios(con, obeHorarios);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }

        public beRespuesta Eliminar(int ID_Horario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daHorarios odaHorarios = new daHorarios();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaHorarios.Eliminar(con, ID_Horario);
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
