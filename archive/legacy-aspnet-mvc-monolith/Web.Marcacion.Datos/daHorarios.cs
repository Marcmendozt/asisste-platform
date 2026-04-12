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
    public class daHorarios
    {
        public List<beHorarios> ListadoHorarios(SqlConnection con)
        {
            List<beHorarios> lbeHorarios = new List<beHorarios>();
            SqlCommand SC = new SqlCommand("sp_HorarioListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Horario");
                int item2 = SDR.GetOrdinal("ID_TipoJornada");
                int item3 = SDR.GetOrdinal("Jornada");
                int item4 = SDR.GetOrdinal("Entrada");
                int item5 = SDR.GetOrdinal("Salida");
                int item6 = SDR.GetOrdinal("Tolerancia");


                beHorarios obeHorarios = null;
                while (SDR.Read())
                {
                    obeHorarios = new beHorarios();
                    obeHorarios.ID_Horario = SDR.GetInt32(item1);
                    obeHorarios.ID_TipoJornada = SDR.GetInt32(item2);
                    obeHorarios.Jornada = SDR.GetString(item3);
                    obeHorarios.Entrada = SDR.GetTimeSpan(item4);
                    obeHorarios.Salida = SDR.GetTimeSpan(item5);
                    obeHorarios.Tolerancia = SDR.GetInt32(item6);
                    lbeHorarios.Add(obeHorarios);
                }
            }
            SDR.Close();
            return lbeHorarios;
        }



        public List<beHorarios> ListadoHorariosTipoJornada(SqlConnection con,int ID_TipoJornada)
        {
            List<beHorarios> lbeHorarios = new List<beHorarios>();
            SqlCommand SC = new SqlCommand("sp_HorarioTipoJornadaListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_TipoJornada", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_TipoJornada;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Horario");
                int item2 = SDR.GetOrdinal("Entrada");
                int item3 = SDR.GetOrdinal("Salida");
                int item4 = SDR.GetOrdinal("ID_TipoJornada");
                beHorarios obeHorarios = null;
                while (SDR.Read())
                {
                    obeHorarios = new beHorarios();
                    obeHorarios.ID_Horario = SDR.GetInt32(item1);
                    obeHorarios.Entrada = SDR.GetTimeSpan(item2);
                    obeHorarios.Salida = SDR.GetTimeSpan(item3);
                    obeHorarios.ID_TipoJornada = SDR.GetInt32(item4);
                    lbeHorarios.Add(obeHorarios);
                }
            }
            SDR.Close();
            return lbeHorarios;
        }

        public int AgregarHorarios(SqlConnection con, beHorarios obeHorarios)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_HorarioAgregar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_TipoJornada", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeHorarios.ID_TipoJornada;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Entrada", SqlDbType.Time,7);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeHorarios.Entrada;
            SqlParameter Parametro03 = cmd.Parameters.Add("@Salida", SqlDbType.Time,7);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obeHorarios.Salida;
            SqlParameter Parametro04 = cmd.Parameters.Add("@Tolerancia", SqlDbType.Int);
            Parametro04.Direction = ParameterDirection.Input;
            Parametro04.Value = obeHorarios.Tolerancia;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int EditarHorarios(SqlConnection con, beHorarios obeHorarios)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_HorarioEditar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro00 = cmd.Parameters.Add("@ID_Horario", SqlDbType.Int);
            Parametro00.Direction = ParameterDirection.Input;
            Parametro00.Value = obeHorarios.ID_Horario;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_TipoJornada", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeHorarios.ID_TipoJornada;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Entrada", SqlDbType.Time, 7);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeHorarios.Entrada;
            SqlParameter Parametro03 = cmd.Parameters.Add("@Salida", SqlDbType.Time,7);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obeHorarios.Salida;
            SqlParameter Parametro04 = cmd.Parameters.Add("@Tolerancia", SqlDbType.Int);
            Parametro04.Direction = ParameterDirection.Input;
            Parametro04.Value = obeHorarios.Tolerancia;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int Eliminar(SqlConnection con, int ID_Horario)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_HorarioEliminar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Horario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Horario;
            i = cmd.ExecuteNonQuery();
            return i;
        }
    }
}
