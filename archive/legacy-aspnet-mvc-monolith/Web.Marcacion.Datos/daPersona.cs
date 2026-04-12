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
    public class daPersona
    {
        public List<bePersona> ListadoPersona(SqlConnection con)
        {
            List<bePersona> lbePersona = new List<bePersona>();
            SqlCommand SC = new SqlCommand("sp_PersonaListar", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("ID_Persona");
                int item1 = SDR.GetOrdinal("ID_Usuario");
                int item2 = SDR.GetOrdinal("Usuario");
                int item3 = SDR.GetOrdinal("Nombres");
                int item4 = SDR.GetOrdinal("Apellidos");
                int item5 = SDR.GetOrdinal("ID_TipoDocumento");
                int item6 = SDR.GetOrdinal("NumeroDocumento");
                int item7 = SDR.GetOrdinal("Genero");
                int item8 = SDR.GetOrdinal("Correo");
                int item9 = SDR.GetOrdinal("Movil");
                int item10 = SDR.GetOrdinal("FechaNacimiento");
                int item11 = SDR.GetOrdinal("ID_Cargo");
                int item12 = SDR.GetOrdinal("Cargo");
                int item13 = SDR.GetOrdinal("ID_Estado");
                int item14 = SDR.GetOrdinal("Clave");
                int item15 = SDR.GetOrdinal("ID_Perfil");
                int item16 = SDR.GetOrdinal("SessionMovil");
                int item17 = SDR.GetOrdinal("ID_LugarTrabajo");
                int item18 = SDR.GetOrdinal("ID_Genero");
                bePersona obePersona = null;
                while (SDR.Read())
                {
                    obePersona = new bePersona();
                    obePersona.ID_Persona = SDR.GetInt32(item00);
                    obePersona.ID_Usuario = SDR.GetInt32(item1);
                    obePersona.NombreUsuario = SDR.GetString(item2);
                    obePersona.Nombres = SDR.GetString(item3);
                    obePersona.Apellidos = SDR.GetString(item4);
                    obePersona.ID_TipoDocumento = SDR.GetInt32(item5);
                    obePersona.NumeroDocumento = SDR.GetString(item6);
                    obePersona.Genero = SDR.GetString(item7);
                    obePersona.Correo = SDR.GetString(item8);
                    obePersona.Movil = SDR.GetString(item9);
                    obePersona.FechaNacimiento = SDR.GetDateTime(item10);
                    obePersona.ID_Cargo = SDR.GetInt32(item11);
                    obePersona.Cargo = SDR.GetString(item12);
                    obePersona.ID_Estado = SDR.GetInt32(item13);
                    obePersona.Clave = SDR.GetString(item14);
                    obePersona.ID_Perfil = SDR.GetInt32(item15);
                    obePersona.SessionMovil = SDR.GetBoolean(item16);
                    obePersona.ID_LugarTrabajo = SDR.GetInt32(item17);
                    obePersona.ID_Genero = SDR.GetInt32(item18);
                    lbePersona.Add(obePersona);
                }
            }
            SDR.Close();
            return lbePersona;
        }



        public int Agregar(SqlConnection con, bePersona obePersona)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_EmpleadoInsertar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = cmd.Parameters.Add("@Nombres", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obePersona.Nombres;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obePersona.Apellidos;
            SqlParameter Parametro03 = cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 100);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obePersona.NumeroDocumento;
            SqlParameter Parametro04 = cmd.Parameters.Add("@ID_Genero", SqlDbType.Int);
            Parametro04.Direction = ParameterDirection.Input;
            Parametro04.Value = obePersona.ID_Genero;
            SqlParameter Parametro05 = cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
            Parametro05.Direction = ParameterDirection.Input;
            Parametro05.Value = obePersona.Correo;
            SqlParameter Parametro06 = cmd.Parameters.Add("@Movil", SqlDbType.VarChar, 100);
            Parametro06.Direction = ParameterDirection.Input;
            Parametro06.Value = obePersona.Movil;
            SqlParameter Parametro07 = cmd.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime);
            Parametro07.Direction = ParameterDirection.Input;
            Parametro07.Value = obePersona.FechaNacimiento;
            SqlParameter Parametro08 = cmd.Parameters.Add("@ID_Cargo", SqlDbType.Int);
            Parametro08.Direction = ParameterDirection.Input;
            Parametro08.Value = obePersona.ID_Cargo;
            SqlParameter Parametro09 = cmd.Parameters.Add("@ID_TipoDocumento", SqlDbType.Int);
            Parametro09.Direction = ParameterDirection.Input;
            Parametro09.Value = obePersona.ID_TipoDocumento;
            SqlParameter Parametro10 = cmd.Parameters.Add("@ID_Estado", SqlDbType.Int);
            Parametro10.Direction = ParameterDirection.Input;
            Parametro10.Value = obePersona.ID_Estado;
            SqlParameter Parametro11 = cmd.Parameters.Add("@EXISTE", SqlDbType.Int);
            Parametro11.Direction = ParameterDirection.Output;
            i = cmd.ExecuteNonQuery();
            obePersona.EXISTE = (int)Parametro11.Value;
            return i;
        }


        public int Editar(SqlConnection con, bePersona obePersona)
        {
            int i = -1;
            SqlCommand cmd = new SqlCommand("sp_PersonaEditar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro00 = cmd.Parameters.Add("@ID_Persona", SqlDbType.Int);
            Parametro00.Direction = ParameterDirection.Input;
            Parametro00.Value = obePersona.ID_Persona;
            SqlParameter Parametro01 = cmd.Parameters.Add("@Nombres", SqlDbType.VarChar, 100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = obePersona.Nombres;
            SqlParameter Parametro02 = cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100);
            Parametro02.Direction = ParameterDirection.Input;
            Parametro02.Value = obePersona.Apellidos;
            SqlParameter Parametro03 = cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 100);
            Parametro03.Direction = ParameterDirection.Input;
            Parametro03.Value = obePersona.NumeroDocumento;
            SqlParameter Parametro04 = cmd.Parameters.Add("@ID_Genero", SqlDbType.Int);
            Parametro04.Direction = ParameterDirection.Input;
            Parametro04.Value = obePersona.ID_Genero;
            SqlParameter Parametro05 = cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
            Parametro05.Direction = ParameterDirection.Input;
            Parametro05.Value = obePersona.Correo;
            SqlParameter Parametro06 = cmd.Parameters.Add("@Movil", SqlDbType.VarChar, 100);
            Parametro06.Direction = ParameterDirection.Input;
            Parametro06.Value = obePersona.Movil;
            SqlParameter Parametro07 = cmd.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime);
            Parametro07.Direction = ParameterDirection.Input;
            Parametro07.Value = obePersona.FechaNacimiento;
            SqlParameter Parametro08 = cmd.Parameters.Add("@ID_Cargo", SqlDbType.Int);
            Parametro08.Direction = ParameterDirection.Input;
            Parametro08.Value = obePersona.ID_Cargo;
            SqlParameter Parametro09 = cmd.Parameters.Add("@ID_TipoDocumento", SqlDbType.Int);
            Parametro09.Direction = ParameterDirection.Input;
            Parametro09.Value = obePersona.ID_TipoDocumento;
            //Tabla Usuario
            SqlParameter Parametro001 = cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int);
            Parametro001.Direction = ParameterDirection.Input;
            Parametro001.Value = obePersona.ID_Usuario;
            SqlParameter Parametro002 = cmd.Parameters.Add("@NombreUsuario", SqlDbType.VarChar,100);
            Parametro002.Direction = ParameterDirection.Input;
            Parametro002.Value = obePersona.NombreUsuario;
            SqlParameter Parametro003 = cmd.Parameters.Add("@Clave", SqlDbType.VarChar,100);
            Parametro003.Direction = ParameterDirection.Input;
            Parametro003.Value = obePersona.Clave;
            SqlParameter Parametro004 = cmd.Parameters.Add("@ID_Perfil", SqlDbType.Int);
            Parametro004.Direction = ParameterDirection.Input;
            Parametro004.Value = obePersona.ID_Perfil;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public bePersona TraerIDPersona(SqlConnection con,string Correo) {
            bePersona obePersona = new bePersona();
            SqlCommand SC = new SqlCommand("sp_PersonaListarCorreo", con);
            SC.CommandType = CommandType.StoredProcedure;
            SqlParameter Parametro01 = SC.Parameters.Add("@Correo", SqlDbType.VarChar,100);
            Parametro01.Direction = ParameterDirection.Input;
            Parametro01.Value = Correo;
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                int item00 = SDR.GetOrdinal("ID_Persona");
           
     
                while (SDR.Read())
                {
                    obePersona = new bePersona();
                    obePersona.ID_Persona = SDR.GetInt32(item00);
         
                }
            }
            SDR.Close();
            return obePersona;
        }

    }
}
