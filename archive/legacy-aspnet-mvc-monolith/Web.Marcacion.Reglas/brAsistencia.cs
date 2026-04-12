using System;

using System.Data.SqlClient;

using Web.Marcacion.Datos;
using Web.Marcacion.Entidades;


namespace Web.Marcacion.Reglas
{
    public class brAsistencia : brGeneral
    {



        public beRespuesta VerificarExistenciasAsistenciasAPI(int ID_Usuario, DateTime Fecha)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daAsistencia odaAsistencia = new daAsistencia();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaAsistencia.VerificarExistenciasAsistencias(Conexion, ID_Usuario, Fecha);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta JListadoPorPerfil()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daAsistencia odaAsistencia = new daAsistencia();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaAsistencia.ListadoAsistenciaPorPerfil(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }


        public beRespuesta JListadoPorUsuario(int ID_Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daAsistencia odaAsistencia = new daAsistencia();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaAsistencia.ListadoPorUsuario(Conexion, ID_Usuario);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }
        public beRespuesta FiltradoPorUsuario(int ID_Usuario,DateTime FechaInicio,DateTime FechaFin)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daAsistencia odaAsistencia = new daAsistencia();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaAsistencia.FiltradoAsistenciaUsuario(Conexion, ID_Usuario, FechaInicio, FechaFin);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta FiltradoPorPerfil(DateTime FechaInicio, DateTime FechaFin)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daAsistencia odaAsistencia = new daAsistencia();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaAsistencia.FiltradoAsistenciaPerfil(Conexion,FechaInicio, FechaFin);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }
    }
}
