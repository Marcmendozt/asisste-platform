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
    public class daLugarTrabajo
    {
        public List<beLugarTrabajo> ListadoLugarTrabajo(SqlConnection con)
        {
            List<beLugarTrabajo> lbeLugarTrabajo = new List<beLugarTrabajo>();
            SqlCommand SC = new SqlCommand("sp_LugarTrabajoListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_LugarTrabajo");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("ID_Estado");

                beLugarTrabajo obeLugarTrabajo = null;
                while (SDR.Read())
                {
                    obeLugarTrabajo = new beLugarTrabajo();
                    obeLugarTrabajo.ID_LugarTrabajo = SDR.GetInt32(item1);
                    obeLugarTrabajo.Descripcion = SDR.GetString(item2);
                    obeLugarTrabajo.ID_Estado = SDR.GetInt32(item3);
                    lbeLugarTrabajo.Add(obeLugarTrabajo);
                }
            }
            SDR.Close();
            return lbeLugarTrabajo;
        }
    }
}
