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
    public class daPerfil
    {
        public List<bePerfil> ListadoPerfiles(SqlConnection con)
        {
            List<bePerfil> lbePerfil = new List<bePerfil>();
            SqlCommand SC = new SqlCommand("sp_PerfilListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_Perfil");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("ID_Estado");

                bePerfil obePerfil = null;
                while (SDR.Read())
                {
                    obePerfil = new bePerfil();
                    obePerfil.ID_Perfil = SDR.GetInt32(item1);
                    obePerfil.Descripcion = SDR.GetString(item2);
                    obePerfil.ID_Estado = SDR.GetInt32(item3);
                    lbePerfil.Add(obePerfil);
                }
            }
            SDR.Close();
            return lbePerfil;
        }

       

        public int Agregar(SqlConnection con, bePerfil obePerfil)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_PerfilAgregar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar,100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obePerfil.Descripcion;
            SqlParameter Parametro02 = cmd.Parameters.Add("@ID_Estado", SqlDbType.Int);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obePerfil.ID_Estado;
            SqlParameter Parametro03 = cmd.Parameters.Add("@EXISTE", SqlDbType.Int);
            Parametro03.Direction = ParameterDirection.Output;
            i = cmd.ExecuteNonQuery();
            obePerfil.EXISTE = (int)Parametro03.Value;
            return i;
        }


        public int Eliminar(SqlConnection con, int ID_Perfil)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_PerfilEliminar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Perfil", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Perfil;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int Editar(SqlConnection con, bePerfil obePerfil)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_PerfilEditar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Perfil", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obePerfil.ID_Perfil;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obePerfil.Descripcion;
            i = cmd.ExecuteNonQuery();
            return i;
        }

    }
}
