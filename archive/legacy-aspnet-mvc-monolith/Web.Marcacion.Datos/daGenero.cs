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
    public class daGenero
    {
        public List<beGenero> ListadoGeneros(SqlConnection con)
        {
            List<beGenero> lbeGenero = new List<beGenero>();
            SqlCommand SC = new SqlCommand("sp_GeneroListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Genero");
                int item2 = SDR.GetOrdinal("Descripcion");
                beGenero obeGenero = null;
                while (SDR.Read())
                {
                    obeGenero = new beGenero();
                    obeGenero.ID_Genero = SDR.GetInt32(item1);
                    obeGenero.Descripcion = SDR.GetString(item2);
                    lbeGenero.Add(obeGenero);
                }
            }
            SDR.Close();
            return lbeGenero;
        }
    }
}
