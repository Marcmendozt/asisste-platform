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
    public class daMovil
    {
        public List<beMovil> ListadoMoviles(SqlConnection con, int ID_Usuario)
        {
            List<beMovil> lbeMovil = new List<beMovil>();
            SqlCommand SC = new SqlCommand("sp_MovilListarUsuario", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Movil");
                int item2 = SDR.GetOrdinal("ID_Usuario");
                int item3 = SDR.GetOrdinal("Model");
                int item4 = SDR.GetOrdinal("Manufacturer");
                int item5 = SDR.GetOrdinal("Name");
                int item6 = SDR.GetOrdinal("Version");
                int item7 = SDR.GetOrdinal("Platform");
                int item8 = SDR.GetOrdinal("Idiom");
                int item9 = SDR.GetOrdinal("DeviceType");
                int item10 = SDR.GetOrdinal("IMEI");

                beMovil obeMovil = null;
                while (SDR.Read())
                {
                    obeMovil = new beMovil();
                    obeMovil.ID_Movil = SDR.GetInt32(item1);
                    obeMovil.ID_Usuario = SDR.GetInt32(item2);
                    obeMovil.Model = SDR.GetString(item3);
                    obeMovil.Manufacturer = SDR.GetString(item4);
                    obeMovil.Name = SDR.GetString(item5);
                    obeMovil.Version = SDR.GetString(item6);
                    obeMovil.Platform = SDR.GetString(item7);
                    obeMovil.Idiom = SDR.GetString(item8);
                    obeMovil.DeviceType = SDR.GetString(item9);
                    obeMovil.IMEI = SDR.GetString(item10);
                    lbeMovil.Add(obeMovil);
                }
            }
            SDR.Close();
            return lbeMovil;
        }
    }
}
