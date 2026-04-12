using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace WCFAPPAsisste.Reglas
{
    public class brGeneral
    {
        public string SQLCadenaConexion { get; set; }

        public brGeneral()
        {
            SQLCadenaConexion = ConfigurationManager.ConnectionStrings["SQLCadena"].ConnectionString;


        }
    }
}
