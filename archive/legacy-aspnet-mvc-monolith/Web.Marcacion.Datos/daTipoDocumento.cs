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
    public class daTipoDocumento
    {
        public List<beTipoDocumento> ListadoTipoDocumento(SqlConnection con)
        {
            List<beTipoDocumento> lbeTipoDocumento = new List<beTipoDocumento>();
            SqlCommand SC = new SqlCommand("sp_TipoDocumentoListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_TipoDocumento");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("Longitud");
                int item4 = SDR.GetOrdinal("ID_Estado");

                beTipoDocumento obeTipoDocumento = null;
                while (SDR.Read())
                {
                    obeTipoDocumento = new beTipoDocumento();
                    obeTipoDocumento.ID_TipoDocumento = SDR.GetInt32(item1);
                    obeTipoDocumento.Descripcion = SDR.GetString(item2);
                    obeTipoDocumento.Longitud = SDR.GetInt32(item3);
                    obeTipoDocumento.ID_Estado = SDR.GetInt32(item4);
                    lbeTipoDocumento.Add(obeTipoDocumento);
                }
            }
            SDR.Close();
            return lbeTipoDocumento;
        }
    }
}
