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
    public class daUsuario
    {
        public beUsuario Login(SqlConnection SC, string Usuario, string Clave)
        {
            //Instancia la clase de entidades en null
            beUsuario obeUsuario = null;
            //Llamamos al procedimiento almacenado + La conexion de Base de datos
            SqlCommand Comando = new SqlCommand("sp_Login", SC);
            Comando.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro1 = Comando.Parameters.Add("@Usuario", SqlDbType.VarChar,100);
            Parametro1.Direction = ParameterDirection.Input;
            Parametro1.Value = Usuario;
            SqlParameter Parametro02 = Comando.Parameters.Add("@Clave", SqlDbType.VarChar,255);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = Clave;
            SqlDataReader DataReader = Comando.ExecuteReader();
            if (DataReader.HasRows)
            {
                int item1 = DataReader.GetOrdinal("NombreCompleto");

                int item2 = DataReader.GetOrdinal("UltimaConexion");
                int item3 = DataReader.GetOrdinal("Genero");
                int item4 = DataReader.GetOrdinal("ID_Perfil");
                int item5 = DataReader.GetOrdinal("Descripcion");
                int item6 = DataReader.GetOrdinal("ID_Usuario");
                int item7 = DataReader.GetOrdinal("ID_Persona");
                int item8 = DataReader.GetOrdinal("ID_Genero");
                int item9 = DataReader.GetOrdinal("NumeroDocumento");
                int item10 = DataReader.GetOrdinal("Nombres");
                int item11 = DataReader.GetOrdinal("Apellidos");
                int item12 = DataReader.GetOrdinal("Usuario");
                while (DataReader.Read())
                {
                    obeUsuario = new beUsuario();
                    obeUsuario.NombreCompleto = DataReader.GetString(item1);
                    obeUsuario.UltimaConexion = DataReader.GetDateTime(item2);
                    obeUsuario.Genero = DataReader.GetString(item3);
                    obeUsuario.ID_Perfil = DataReader.GetInt32(item4);
                    obeUsuario.Perfil = DataReader.GetString(item5);
                    obeUsuario.ID_Usuario = DataReader.GetInt32(item6);
                    obeUsuario.ID_Persona = DataReader.GetInt32(item7);
                    obeUsuario.ID_Genero = DataReader.GetInt32(item8);
                    obeUsuario.DocumentoIdentidad = DataReader.GetString(item9);
                    obeUsuario.Nombres = DataReader.GetString(item10);
                    obeUsuario.Apellidos = DataReader.GetString(item11);
                    obeUsuario.Usuario = DataReader.GetString(item12);
                }
            }
            DataReader.Close();
            return obeUsuario;
        }


        public beUsuario BuscarUsuario(SqlConnection SC, string DocumentoIdentidad)
        {
            //Instancia la clase de entidades en null
            beUsuario obeUsuario = null;
            //Llamamos al procedimiento almacenado + La conexion de Base de datos
            SqlCommand Comando = new SqlCommand("sp_BuscarUsuario", SC);
            Comando.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro1 = Comando.Parameters.Add("@DocumentoIdentidad", SqlDbType.VarChar, 100);
            Parametro1.Direction = ParameterDirection.Input;
            Parametro1.Value = DocumentoIdentidad;
            SqlDataReader DataReader = Comando.ExecuteReader();
            if (DataReader.HasRows)
            {
                int item1 = DataReader.GetOrdinal("ID_Usuario");
                int item2 = DataReader.GetOrdinal("Nombres");
                int item3 = DataReader.GetOrdinal("Apellidos");
                int item4 = DataReader.GetOrdinal("Usuario");
                while (DataReader.Read())
                {
                    obeUsuario = new beUsuario();
                    obeUsuario.ID_Usuario = DataReader.GetInt32(item1);
                    obeUsuario.Nombres = DataReader.GetString(item2);
                    obeUsuario.Apellidos = DataReader.GetString(item3);
                    obeUsuario.NombreUsuario = DataReader.GetString(item4);
                }
            }
            DataReader.Close();
            return obeUsuario;
        }


        public int Agregar(SqlConnection con, beUsuario obeUsuario)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_UsuarioInsertar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obeUsuario.Usuario;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Clave", SqlDbType.VarChar,255);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obeUsuario.Clave;
            SqlParameter Parametro03 = cmd.Parameters.Add("@ID_Persona", SqlDbType.Int);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obeUsuario.ID_Persona;
            SqlParameter Parametro04 = cmd.Parameters.Add("@ID_Perfil", SqlDbType.Int);
            Parametro04.Direction = ParameterDirection.Input;
            Parametro04.Value = obeUsuario.ID_Perfil;
            SqlParameter Parametro05 = cmd.Parameters.Add("@ID_LugarTrabajo", SqlDbType.Int);
            Parametro05.Direction = ParameterDirection.Input;
            Parametro05.Value = obeUsuario.ID_LugarTrabajo;
            i = cmd.ExecuteNonQuery();
     
            return i;
        }

        public beUsuario RecuperarContraseña(SqlConnection con, string Usuario)
        {
            beUsuario obeUsuario = new beUsuario();
            SqlCommand SC = new SqlCommand("sp_UsuarioEnviarCredenciales", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@Usuario", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = Usuario;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("Clave");
                while (SDR.Read())
                {
                    obeUsuario = new beUsuario();
                    obeUsuario.Clave = SDR.GetString(item00);
                }
            }
            SDR.Close();
            return obeUsuario;
        }


        public beUsuario CapturarIDUsuario(SqlConnection con, string Usuario)
        {
            beUsuario obeUsuario = new beUsuario();
            SqlCommand SC = new SqlCommand("sp_UsuarioCapturar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@Usuario", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = Usuario;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("ID_Usuario");
                while (SDR.Read())
                {
                    obeUsuario = new beUsuario();
                    obeUsuario.ID_Usuario = SDR.GetInt32(item00);
                }
            }
            SDR.Close();
            return obeUsuario;
        }

        

        public int Eliminar(SqlConnection con, int ID_Usuario)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_UsuarioEliminar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = ID_Usuario;
            i = cmd.ExecuteNonQuery();
            return i;
        }

    }
}
