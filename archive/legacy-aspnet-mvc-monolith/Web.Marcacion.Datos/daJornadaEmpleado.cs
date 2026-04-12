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
    public class daJornadaEmpleado
    {
        public List<beJornadaEmpleado> ListadoJornadaEmpleado(SqlConnection con)
        {
            List<beJornadaEmpleado> lbeJornadaEmpleado = new List<beJornadaEmpleado>();
            SqlCommand SC = new SqlCommand("sp_EmpleadoJornadaListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_EmpleadoJornada");
                int item2 = SDR.GetOrdinal("ID_Horario");
                int item3 = SDR.GetOrdinal("ID_TipoJornada");
                int item4 = SDR.GetOrdinal("TipoJornada");
                int item5 = SDR.GetOrdinal("NombreCompleto");
                int item6 = SDR.GetOrdinal("AreaTrabajo");
                int item7 = SDR.GetOrdinal("Cargo");
                int item8 = SDR.GetOrdinal("Entrada");
                int item9 = SDR.GetOrdinal("Salida");
                int item10 = SDR.GetOrdinal("Tolerancia");

                beJornadaEmpleado obeJornadaEmpleado = null;
                while (SDR.Read())
                {
                    obeJornadaEmpleado = new beJornadaEmpleado();
                    obeJornadaEmpleado.ID_EmpleadoJornada = SDR.GetInt32(item1);
                    obeJornadaEmpleado.ID_Horario = SDR.GetInt32(item2);
                    obeJornadaEmpleado.ID_TipoJornada = SDR.GetInt32(item3);
                    obeJornadaEmpleado.TipoJornada = SDR.GetString(item4);
                    obeJornadaEmpleado.NombreCompleto = SDR.GetString(item5);
                    obeJornadaEmpleado.AreaTrabajo = SDR.GetString(item6);
                    obeJornadaEmpleado.Cargo = SDR.GetString(item7);
                    obeJornadaEmpleado.Entrada = SDR.GetTimeSpan(item8);
                    obeJornadaEmpleado.Salida = SDR.GetTimeSpan(item9);
                    obeJornadaEmpleado.Tolerancia = SDR.GetInt32(item10);
                    lbeJornadaEmpleado.Add(obeJornadaEmpleado);
                }
            }
            SDR.Close();
            return lbeJornadaEmpleado;
        }


        public int Agregar(SqlConnection con, int ID_Usuario,int ID_TipoJornada)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_EmpleadoJornadaInsertar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value =ID_Usuario;
            SqlParameter Parametro02 = cmd.Parameters.Add("@ID_Horario", SqlDbType.Int);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = ID_TipoJornada;
            i = cmd.ExecuteNonQuery();
            return i;
        }




        public int Editar(SqlConnection con, beJornadaEmpleado obeJornadaEmpleado)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_EmpleadoJornadaEditar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_EmpleadoJornada", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeJornadaEmpleado.ID_EmpleadoJornada;
            SqlParameter Parametro02 = cmd.Parameters.Add("@ID_Horario", SqlDbType.Int);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeJornadaEmpleado.ID_Horario;
            i = cmd.ExecuteNonQuery();
            return i;
        }
    }
}
