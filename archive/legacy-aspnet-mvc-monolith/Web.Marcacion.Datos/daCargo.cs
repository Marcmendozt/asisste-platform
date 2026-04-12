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
    public class daCargo
    {
        public List<beCargo> ListadoCargos(SqlConnection con)
        {
            List<beCargo> lbeCargo = new List<beCargo>();
            SqlCommand SC = new SqlCommand("sp_CargoListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Cargo");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("ID_Estado");

                beCargo obeCargo = null;
                while (SDR.Read())
                {
                    obeCargo = new beCargo();
                    obeCargo.ID_Cargo = SDR.GetInt32(item1);
                    obeCargo.Descripcion = SDR.GetString(item2);
                    obeCargo.ID_Estado = SDR.GetInt32(item3);
                    lbeCargo.Add(obeCargo);
                }
            }
            SDR.Close();
            return lbeCargo;
        }
    }
}
