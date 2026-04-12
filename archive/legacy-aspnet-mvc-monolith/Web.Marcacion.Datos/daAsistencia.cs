using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Marcacion.Entidades;


namespace Web.Marcacion.Datos
{
    public class daAsistencia
    {
        public List<beAsistencia> ListadoAsistenciaPorPerfil(SqlConnection con)
        {
            List<beAsistencia> lbeAsistencia = new List<beAsistencia>();
            SqlCommand SC = new SqlCommand("sp_AsistenciaListarPorPerfil", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("Apellidos");
                int item2 = SDR.GetOrdinal("Nombres");
                int item3 = SDR.GetOrdinal("NumeroDocumento");
                int item4 = SDR.GetOrdinal("Fecha");
                //Hora ingreso movil
                int item5 = SDR.GetOrdinal("HIM");
                //Hora salida movil
                int item6 = SDR.GetOrdinal("HSM");
                //Hora ingreso real
                int item7 = SDR.GetOrdinal("HIR");
                //Hora salida real
                int item8 = SDR.GetOrdinal("HSR");
                int item9 = SDR.GetOrdinal("RetrasoEntrada");
         
                int item11 = SDR.GetOrdinal("UbicacionIngreso");
                int item12 = SDR.GetOrdinal("UbicacionSalida");
                int item13 = SDR.GetOrdinal("ID_LugarTrabajo");
                int item14 = SDR.GetOrdinal("LugarTrabajo");
                int item15 = SDR.GetOrdinal("Falta");
                int item16 = SDR.GetOrdinal("Tolerancia");
                int item17 = SDR.GetOrdinal("Geovalla");
                beAsistencia obeAsistencia = null;
                while (SDR.Read())
                {
                    obeAsistencia = new beAsistencia();
                    obeAsistencia.Apellidos = SDR.GetString(item1);
                    obeAsistencia.Nombres = SDR.GetString(item2);
                    obeAsistencia.NumeroDocumento = SDR.GetString(item3);
                    obeAsistencia.Fecha = SDR.GetDateTime(item4);
                    obeAsistencia.HoraIngresoMovil = SDR.GetTimeSpan(item5);
                    obeAsistencia.HoraSalidaMovil = SDR.GetTimeSpan(item6);
                    obeAsistencia.HoraIngresoReal = SDR.GetTimeSpan(item7);
                    obeAsistencia.HoraSalidaReal = SDR.GetTimeSpan(item8);
                    obeAsistencia.RetrasoEntrada = SDR.GetTimeSpan(item9);
            
                    obeAsistencia.UbicacionIngreso = SDR.GetString(item11);
                    obeAsistencia.UbicacionSalida = SDR.GetString(item12);
                    obeAsistencia.ID_LugarTrabajo = SDR.GetInt32(item13);
                    obeAsistencia.LugarTrabajo = SDR.GetString(item14);
                    obeAsistencia.Falta = SDR.GetBoolean(item15);
                    obeAsistencia.Tolerancia = SDR.GetInt32(item16);
                    obeAsistencia.Geovalla = SDR.GetString(item17);
                    lbeAsistencia.Add(obeAsistencia);
                }
            }
            SDR.Close();
            return lbeAsistencia;
        }


        public List<beAsistenciaUsuario> ListadoPorUsuario(SqlConnection con, int ID_Usuario)
        {
            List<beAsistenciaUsuario> lbeAsistenciaUsuario = new List<beAsistenciaUsuario>();
            SqlCommand SC = new SqlCommand("sp_AsistenciaListarPorUsuario", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {

                int item4 = SDR.GetOrdinal("Fecha");
                int item7 = SDR.GetOrdinal("HIR");
                int item8 = SDR.GetOrdinal("HSR");
                int item9 = SDR.GetOrdinal("RetrasoEntrada");

                int item11 = SDR.GetOrdinal("UbicacionIngreso");
                int item12 = SDR.GetOrdinal("UbicacionSalida");

                int item14 = SDR.GetOrdinal("LugarTrabajo");
                int item15 = SDR.GetOrdinal("Falta");
                int item16 = SDR.GetOrdinal("Tolerancia");
                int item17 = SDR.GetOrdinal("Geovalla");
                beAsistenciaUsuario obeAsistenciaUsuario = null;
                while (SDR.Read())
                {
                    obeAsistenciaUsuario = new beAsistenciaUsuario();
                    obeAsistenciaUsuario.Fecha = SDR.GetDateTime(item4);
                    obeAsistenciaUsuario.HoraIngresoReal = SDR.GetTimeSpan(item7);
                    obeAsistenciaUsuario.HoraSalidaReal = SDR.GetTimeSpan(item8);
                    obeAsistenciaUsuario.RetrasoEntrada = SDR.GetTimeSpan(item9);
                    obeAsistenciaUsuario.UbicacionIngreso = SDR.GetString(item11);
                    obeAsistenciaUsuario.UbicacionSalida = SDR.GetString(item12);
                    obeAsistenciaUsuario.LugarTrabajo = SDR.GetString(item14);
                    obeAsistenciaUsuario.Falta = SDR.GetBoolean(item15);
                    obeAsistenciaUsuario.Tolerancia = SDR.GetInt32(item16);
                    obeAsistenciaUsuario.Geovalla = SDR.GetString(item17);
                    lbeAsistenciaUsuario.Add(obeAsistenciaUsuario);
                }
            }
            SDR.Close();
            return lbeAsistenciaUsuario;
        }


        public List<beAsistenciaUsuario> FiltradoAsistenciaUsuario(SqlConnection con, int ID_Usuario,DateTime FechaInicio,DateTime FechaFin)
        {
            List<beAsistenciaUsuario> lbeAsistenciaUsuario = new List<beAsistenciaUsuario>();
            SqlCommand SC = new SqlCommand("sp_AsistenciaFiltrarUsuario", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlParameter Parametro02 = SC.Parameters.Add("@FechaInicio", SqlDbType.Date);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = FechaInicio;
            SqlParameter Parametro03 = SC.Parameters.Add("@FechaFin", SqlDbType.Date);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = FechaFin;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {

                int item4 = SDR.GetOrdinal("Fecha");
                int item7 = SDR.GetOrdinal("HoraIngresoReal");
                int item8 = SDR.GetOrdinal("HoraSalidaReal");
                int item9 = SDR.GetOrdinal("RetrasoEntrada");
                int item11 = SDR.GetOrdinal("UbicacionIngreso");
                int item12 = SDR.GetOrdinal("UbicacionSalida");
                int item14 = SDR.GetOrdinal("LugarTrabajo");
                int item15 = SDR.GetOrdinal("Falta");
                int item16 = SDR.GetOrdinal("Tolerancia");
                int item17 = SDR.GetOrdinal("Geovalla");
                beAsistenciaUsuario obeAsistenciaUsuario = null;
                while (SDR.Read())
                {
                    obeAsistenciaUsuario = new beAsistenciaUsuario();
                    obeAsistenciaUsuario.Fecha = SDR.GetDateTime(item4);
                    obeAsistenciaUsuario.HoraIngresoReal = SDR.GetTimeSpan(item7);
                    obeAsistenciaUsuario.HoraSalidaReal = SDR.GetTimeSpan(item8);
                    obeAsistenciaUsuario.RetrasoEntrada = SDR.GetTimeSpan(item9);
                    obeAsistenciaUsuario.UbicacionIngreso = SDR.GetString(item11);
                    obeAsistenciaUsuario.UbicacionSalida = SDR.GetString(item12);
                    obeAsistenciaUsuario.LugarTrabajo = SDR.GetString(item14);
                    obeAsistenciaUsuario.Falta = SDR.GetBoolean(item15);
                    obeAsistenciaUsuario.Tolerancia = SDR.GetInt32(item16);
                    obeAsistenciaUsuario.Geovalla = SDR.GetString(item17);
                    lbeAsistenciaUsuario.Add(obeAsistenciaUsuario);
                }
            }
            SDR.Close();
            return lbeAsistenciaUsuario;
        }
        public beAsistencia VerificarExistenciasAsistencias(SqlConnection con, int ID_Usuario, DateTime Fecha)
        {
            beAsistencia obeAsistencia = new beAsistencia();

            SqlCommand SC = new SqlCommand("sp_AsistenciaVerificarExistencias", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlParameter Parametro02 = SC.Parameters.Add("@Fecha", SqlDbType.Date);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = Fecha;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("Fecha");
                int item01 = SDR.GetOrdinal("HoraSalidaReal");

                while (SDR.Read())
                {
                    obeAsistencia = new beAsistencia();
                    obeAsistencia.Fecha = SDR.GetDateTime(item00);
                    obeAsistencia.HoraSalidaReal = SDR.GetTimeSpan(item01);

                }
            }
            SDR.Close();
            return obeAsistencia;
        }

        public List<beAsistencia> FiltradoAsistenciaPerfil(SqlConnection con, DateTime FechaInicio, DateTime FechaFin)
        {
            List<beAsistencia> lbeAsistencia = new List<beAsistencia>();
            SqlCommand SC = new SqlCommand("sp_AsistenciaFiltrarPerfil", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro02 = SC.Parameters.Add("@FechaInicio", SqlDbType.Date);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = FechaInicio;
            SqlParameter Parametro03 = SC.Parameters.Add("@FechaFin", SqlDbType.Date);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = FechaFin;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("Apellidos");
                int item2 = SDR.GetOrdinal("Nombres");
                int item3 = SDR.GetOrdinal("NumeroDocumento");
                int item4 = SDR.GetOrdinal("Fecha");
                //Hora ingreso movil
                int item5 = SDR.GetOrdinal("HIM");
                //Hora salida movil
                int item6 = SDR.GetOrdinal("HSM");
                //Hora ingreso real
                int item7 = SDR.GetOrdinal("HIR");
                //Hora salida real
                int item8 = SDR.GetOrdinal("HSR");
                int item9 = SDR.GetOrdinal("RetrasoEntrada");

                int item11 = SDR.GetOrdinal("UbicacionIngreso");
                int item12 = SDR.GetOrdinal("UbicacionSalida");
                int item13 = SDR.GetOrdinal("ID_LugarTrabajo");
                int item14 = SDR.GetOrdinal("LugarTrabajo");
                int item15 = SDR.GetOrdinal("Falta");
                int item16 = SDR.GetOrdinal("Tolerancia");
                int item17 = SDR.GetOrdinal("Geovalla");
                beAsistencia obeAsistencia = null;
                while (SDR.Read())
                {
                    obeAsistencia = new beAsistencia();
                    obeAsistencia.Apellidos = SDR.GetString(item1);
                    obeAsistencia.Nombres = SDR.GetString(item2);
                    obeAsistencia.NumeroDocumento = SDR.GetString(item3);
                    obeAsistencia.Fecha = SDR.GetDateTime(item4);
                    obeAsistencia.HoraIngresoMovil = SDR.GetTimeSpan(item5);
                    obeAsistencia.HoraSalidaMovil = SDR.GetTimeSpan(item6);
                    obeAsistencia.HoraIngresoReal = SDR.GetTimeSpan(item7);
                    obeAsistencia.HoraSalidaReal = SDR.GetTimeSpan(item8);
                    obeAsistencia.RetrasoEntrada = SDR.GetTimeSpan(item9);

                    obeAsistencia.UbicacionIngreso = SDR.GetString(item11);
                    obeAsistencia.UbicacionSalida = SDR.GetString(item12);
                    obeAsistencia.ID_LugarTrabajo = SDR.GetInt32(item13);
                    obeAsistencia.LugarTrabajo = SDR.GetString(item14);
                    obeAsistencia.Falta = SDR.GetBoolean(item15);
                    obeAsistencia.Tolerancia = SDR.GetInt32(item16);
                    obeAsistencia.Geovalla = SDR.GetString(item17);
                    lbeAsistencia.Add(obeAsistencia);
                }
            }
            SDR.Close();
            return lbeAsistencia;
        }
    }
}
