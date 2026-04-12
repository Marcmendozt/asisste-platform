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
    public class daModulos
    {
        public List<beModulos> ListadoModulosPerfil(SqlConnection con, int ID_Perfil)
        {
            List<beModulos> lbeModulos = new List<beModulos>();
            SqlCommand SC = new SqlCommand("sp_ModuloPerfilListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@ID_Perfil", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Perfil;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Modulo");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("class");

                beModulos obeModulos = null;
                while (SDR.Read())
                {
                    obeModulos = new beModulos();
                    obeModulos.ID_Modulo = SDR.GetInt32(item1);
                    obeModulos.DescripcionModulo = SDR.GetString(item2);
                    obeModulos.DescripcionImage = SDR.GetString(item3);
                    lbeModulos.Add(obeModulos);
                }
            }
            SDR.Close();
            return lbeModulos;
        }
    }
}
