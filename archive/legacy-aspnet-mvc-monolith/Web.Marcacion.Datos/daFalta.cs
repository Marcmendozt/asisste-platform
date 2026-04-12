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
    public class daFalta
    {
        public List<beFalta> ListarFalta(SqlConnection SQLConexion) {
            List<beFalta> lbeFalta = new List<beFalta>();
            SqlCommand SC = new SqlCommand("sp_FaltaListar", SQLConexion);
            SC.CommandType = CommandType.StoredProcedure;
         
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Falta");
                int item2 = SDR.GetOrdinal("ID_Usuario");
                int item3 = SDR.GetOrdinal("NombreCompleto");
                int item4 = SDR.GetOrdinal("Usuario");
                int item5 = SDR.GetOrdinal("Fecha");
                int item6 = SDR.GetOrdinal("Documento");
                int item7 = SDR.GetOrdinal("Extension");
                int item8 = SDR.GetOrdinal("ID_Estado");
                int item9 = SDR.GetOrdinal("URL");
 

                beFalta obeFalta = null;
                while (SDR.Read())
                {
                    obeFalta = new beFalta();
                    obeFalta.ID_Falta = SDR.GetInt32(item1);
                    obeFalta.ID_Usuario = SDR.GetInt32(item2);
                    obeFalta.NombreCompleto = SDR.GetString(item3);
                    obeFalta.NombreUsuario = SDR.GetString(item4);
                    obeFalta.Fecha = SDR.GetDateTime(item5);
                    obeFalta.Documento = SDR.GetString(item6);
                    obeFalta.Extension = SDR.GetString(item7);
                    obeFalta.ID_Estado = SDR.GetInt32(item8);
                    obeFalta.Url = SDR.GetString(item9);
                    lbeFalta.Add(obeFalta);
                }
            }
            SDR.Close();
            return lbeFalta;
        }


        public List<beFalta> ListarFaltaPorUsuario(SqlConnection SQLConexion,int ID_Usuario)
        {
            List<beFalta> lbeFalta = new List<beFalta>();
            SqlCommand SC = new SqlCommand("sp_FaltaListarUsuario", SQLConexion);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Falta");
                int item2 = SDR.GetOrdinal("ID_Usuario");
                int item3 = SDR.GetOrdinal("NombreCompleto");
                int item4 = SDR.GetOrdinal("Usuario");
                int item5 = SDR.GetOrdinal("Fecha");
                int item6 = SDR.GetOrdinal("Documento");
                int item7 = SDR.GetOrdinal("Extension");
                int item8 = SDR.GetOrdinal("ID_Estado");
                int item9 = SDR.GetOrdinal("URL");


                beFalta obeFalta = null;
                while (SDR.Read())
                {
                    obeFalta = new beFalta();
                    obeFalta.ID_Falta = SDR.GetInt32(item1);
                    obeFalta.ID_Usuario = SDR.GetInt32(item2);
                    obeFalta.NombreCompleto = SDR.GetString(item3);
                    obeFalta.NombreUsuario = SDR.GetString(item4);
                    obeFalta.Fecha = SDR.GetDateTime(item5);
                    obeFalta.Documento = SDR.GetString(item6);
                    obeFalta.Extension = SDR.GetString(item7);
                    obeFalta.ID_Estado = SDR.GetInt32(item8);
                    obeFalta.Url = SDR.GetString(item9);
                    lbeFalta.Add(obeFalta);
                }
            }
            SDR.Close();
            return lbeFalta;
        }

        public beFalta VerificarFalta(SqlConnection con, int ID_Usuario, DateTime Fecha)
        {
            beFalta obeFalta = new beFalta();
            SqlCommand SC = new SqlCommand("sp_FaltaVerificar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlParameter Parametro02 = SC.Parameters.Add("@Fecha", SqlDbType.DateTime);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = Fecha;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("Fecha");
                while (SDR.Read())
                {
                    obeFalta = new beFalta();
                    obeFalta.Fecha = SDR.GetDateTime(item00);

                }
            }
            SDR.Close();
            return obeFalta;
        }


        public int RegistrarFalta(SqlConnection con, beFalta obeFalta)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_FaltaInsertar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeFalta.ID_Usuario;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Fecha", SqlDbType.Date);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeFalta.Fecha;
            SqlParameter Parametro03 = cmd.Parameters.Add("@Documento", SqlDbType.VarChar, 100);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obeFalta.Documento;
            SqlParameter Parametro04 = cmd.Parameters.Add("@Extension", SqlDbType.VarChar, 50);
            Parametro04.Direction = ParameterDirection.Input;
            Parametro04.Value = obeFalta.Extension;
            SqlParameter Parametro05 = cmd.Parameters.Add("@ID_Estado", SqlDbType.Int);
            Parametro05.Direction = ParameterDirection.Input;
            Parametro05.Value = obeFalta.ID_Estado;
            SqlParameter Parametro06 = cmd.Parameters.Add("@URL", SqlDbType.VarChar, 255);
            Parametro06.Direction = ParameterDirection.Input;
            Parametro06.Value = obeFalta.Url;
            i = cmd.ExecuteNonQuery();
            return i;
        }

    }
}
