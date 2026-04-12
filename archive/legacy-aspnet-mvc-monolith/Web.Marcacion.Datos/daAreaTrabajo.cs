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
    public class daAreaTrabajo
    {
        public List<beAreaTrabajo> ListadoAreaTrabajo(SqlConnection con)
        {
            List<beAreaTrabajo> lbeAreaTrabajo = new List<beAreaTrabajo>();
            SqlCommand SC = new SqlCommand("sp_AreaTrabajo", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_AreaTrabajo");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("ID_Estado");

                beAreaTrabajo obeAreaTrabajo = null;
                while (SDR.Read())
                {
                    obeAreaTrabajo = new beAreaTrabajo();
                    obeAreaTrabajo.ID_LugarTrabajo = SDR.GetInt32(item1);
                    obeAreaTrabajo.Descripcion = SDR.GetString(item2);
                    obeAreaTrabajo.ID_Estado = SDR.GetInt32(item3);
                    lbeAreaTrabajo.Add(obeAreaTrabajo);
                }
            }
            SDR.Close();
            return lbeAreaTrabajo;
        }
    }
}
