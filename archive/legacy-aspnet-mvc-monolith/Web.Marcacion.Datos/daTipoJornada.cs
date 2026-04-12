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
    public class daTipoJornada
    {

        public List<beTipoJornada> ListadoTipoJornada(SqlConnection con)
        {
            List<beTipoJornada> lbeTipoJornada = new List<beTipoJornada>();
            SqlCommand SC = new SqlCommand("sp_TipoJornadaListar", con);
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item1 = SDR.GetOrdinal("ID_TipoJornada");
                int item2 = SDR.GetOrdinal("Descripcion");
                int item3 = SDR.GetOrdinal("Detalles");
                int item4 = SDR.GetOrdinal("ID_Estado");
         
                beTipoJornada obeTipoJornada = null;
                while (SDR.Read())
                {
                    obeTipoJornada = new beTipoJornada();
                    obeTipoJornada.ID_TipoJornada = SDR.GetInt32(item1);
                    obeTipoJornada.Descripcion = SDR.GetString(item2);
                    obeTipoJornada.Detalles = SDR.GetString(item3);
                    obeTipoJornada.ID_Estado = SDR.GetInt32(item4);
                    lbeTipoJornada.Add(obeTipoJornada);
                }
            }
            SDR.Close();
            return lbeTipoJornada;
        }



        public int Agregar(SqlConnection con, beTipoJornada obeTipoJornada)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_TipoJornadaInsertar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeTipoJornada.Descripcion;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Detalles", SqlDbType.Text);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeTipoJornada.Detalles;
            SqlParameter Parametro03 = cmd.Parameters.Add("@ID_Estado", SqlDbType.Int);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obeTipoJornada.ID_Estado;
            SqlParameter Parametro04 = cmd.Parameters.Add("@EXISTE", SqlDbType.Int);
            Parametro04.Direction = ParameterDirection.Output;
            i = cmd.ExecuteNonQuery();
            obeTipoJornada.EXISTE = (int)Parametro04.Value;
            return i;
        }

        public int Editar(SqlConnection con, beTipoJornada obeTipoJornada)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_TipoJornadaEditar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro00 = cmd.Parameters.Add("@ID_TipoJornada", SqlDbType.Int);
            Parametro00.Direction = ParameterDirection.Input;
            Parametro00.Value = obeTipoJornada.ID_TipoJornada;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeTipoJornada.Descripcion;
            SqlParameter Parametro01 = cmd.Parameters.Add("@Detalles", SqlDbType.Text);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeTipoJornada.Detalles;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int Eliminar(SqlConnection con, int ID_TipoJornada)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_TipoJornadaEliminar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_TipoJornada", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_TipoJornada;
            i = cmd.ExecuteNonQuery();

            return i;
        }
    }
}
