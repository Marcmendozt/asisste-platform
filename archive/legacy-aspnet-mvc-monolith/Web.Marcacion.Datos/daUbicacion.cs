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
    public class daUbicacion
    {

        public List<beUbicacion> ListadoUbicacion(SqlConnection con)
        {
            List<beUbicacion> lbeUbicacion = new List<beUbicacion>();
            SqlCommand SC = new SqlCommand("sp_UbicacionListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Ubicacion");
                int item2 = SDR.GetOrdinal("ID_Usuario");
                int item3 = SDR.GetOrdinal("Usuario");
                int item4 = SDR.GetOrdinal("Ubicacion");

                beUbicacion obeUbicacion = null;
                while (SDR.Read())
                {
                    obeUbicacion = new beUbicacion();
                    obeUbicacion.ID_Ubicacion = SDR.GetInt32(item1);
                    obeUbicacion.ID_Usuario = SDR.GetInt32(item2);
                    obeUbicacion.NombreUsuario = SDR.GetString(item3);
                    obeUbicacion.Ubicacion = SDR.GetString(item4);
                    lbeUbicacion.Add(obeUbicacion);
                }
            }
            SDR.Close();
            return lbeUbicacion;
        }


        public List<beUbicacion> ListadoUbicacionPorUsuario(SqlConnection con,int ID_Usuario)
        {
            List<beUbicacion> lbeUbicacion = new List<beUbicacion>();
            SqlCommand SC = new SqlCommand("sp_UbicacionListarUsuario", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Ubicacion");
                int item2 = SDR.GetOrdinal("ID_Usuario");
                int item3 = SDR.GetOrdinal("Usuario");
                int item4 = SDR.GetOrdinal("Ubicacion");

                beUbicacion obeUbicacion = null;
                while (SDR.Read())
                {
                    obeUbicacion = new beUbicacion();
                    obeUbicacion.ID_Ubicacion = SDR.GetInt32(item1);
                    obeUbicacion.ID_Usuario = SDR.GetInt32(item2);
                    obeUbicacion.NombreUsuario = SDR.GetString(item3);
                    obeUbicacion.Ubicacion = SDR.GetString(item4);
                    lbeUbicacion.Add(obeUbicacion);
                }
            }
            SDR.Close();
            return lbeUbicacion;
        }

        public int Editar(SqlConnection con, beUbicacion obeUbicacion)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_UbicacionEditar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeUbicacion.ID_Usuario;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar,255);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeUbicacion.Ubicacion;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int Eliminar(SqlConnection con, int ID_Ubicacion)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_UbicacionEliminar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Ubicacion", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Ubicacion;
            i = cmd.ExecuteNonQuery();
            return i;
        }

    }
}
