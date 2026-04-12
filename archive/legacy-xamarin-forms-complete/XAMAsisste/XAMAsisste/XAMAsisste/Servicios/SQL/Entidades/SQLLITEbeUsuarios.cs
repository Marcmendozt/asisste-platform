using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAMAsisste.Servicios.SQL.Entidades
{
    [Table("Usuario")]
    public class SQLLITEbeUsuarios
    {
      
        [PrimaryKey, AutoIncrement]
        public int SQLITEID { get; set; }

        public int IDUsuario { get; set; } 

        public string Usuario { get; set; }

        public string IMEI { get; set; }
    }
}
