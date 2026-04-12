using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using XAMAsisste.Servicios.SQL.Entidades;

namespace XAMAsisste.Servicios.SQL.BaseDatos
{
    public class BDAsisste : SQLiteConnection
    {
        public BDAsisste(string Ruta) : base(Ruta)
        {
            CrearTablas();
        }

        void CrearTablas()
        {
            CreateTable<SQLLITEbeUsuarios>();
        }

        public SQLLITEbeUsuarios ObtenerUsuario(object ID_Usuario)
        {
           
            SQLLITEbeUsuarios obeUsuarios = new SQLLITEbeUsuarios();
      
         
                var Respuesta = Table<SQLLITEbeUsuarios>().Where(T_Usuario => T_Usuario.IDUsuario.Equals(ID_Usuario)).FirstOrDefault();
            if (Respuesta != null)
                {
                    obeUsuarios = (SQLLITEbeUsuarios)Respuesta;
                }
                else {

                obeUsuarios = null;
                }
       

            return obeUsuarios;
        }


        public List<SQLLITEbeUsuarios> ListarUsuarioUnico() {
            List<SQLLITEbeUsuarios> lSQLLITEbeUsuarios = new List<SQLLITEbeUsuarios>();
            var Respuesta = Table<SQLLITEbeUsuarios>().ToList();
            if (Respuesta != null)
            {
                lSQLLITEbeUsuarios = (List<SQLLITEbeUsuarios>)Respuesta;
            }
            return lSQLLITEbeUsuarios;

        }


        public void GuardarUsuario(string Usuario,int ID_Usuario)
        {
            SQLLITEbeUsuarios obeUsuarios = new SQLLITEbeUsuarios();
            obeUsuarios.Usuario = Usuario;
            obeUsuarios.IDUsuario= ID_Usuario;
            this.Insert(obeUsuarios);
        }


    }
}
