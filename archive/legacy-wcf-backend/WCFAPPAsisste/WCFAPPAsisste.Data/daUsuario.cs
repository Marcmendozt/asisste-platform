using System.Data;
using System.Data.SqlClient;
using WCFAPPAsisste.Entidades;

namespace WCFAPPAsisste.Data
{
    public class daUsuario
    {
        public beUsuario TraerPerfil(SqlConnection con, string Correo, string Clave)
        {
            beUsuario obeUsuario = new beUsuario();
            SqlCommand SC = new SqlCommand("sp_ListarUsuarioMovil", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@Usuario", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = Correo;
            SqlParameter Parametro02 = SC.Parameters.Add("@Clave", SqlDbType.VarChar, 255);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = Clave;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("ID_Usuario");
                int item01 = SDR.GetOrdinal("Nombre");
                int item02 = SDR.GetOrdinal("HoraEntrada");
                int item03 = SDR.GetOrdinal("Tolerancia");
                int item04 = SDR.GetOrdinal("ID_Movil");
                int item05 = SDR.GetOrdinal("SessionMovil");

                while (SDR.Read())
                {
                    obeUsuario = new beUsuario();
                    obeUsuario.ID_Usuario = SDR.GetInt32(item00);
                    obeUsuario.Nombre = SDR.GetString(item01);
                    obeUsuario.HoraEntrada = SDR.GetString(item02);
                    obeUsuario.Tolerancia = SDR.GetInt32(item03);
                    obeUsuario.ID_Movil = SDR.GetInt32(item04);
                    obeUsuario.SessionMovil = SDR.GetBoolean(item05);
                }
            }
            SDR.Close();
            return obeUsuario;
        }
    }
}
