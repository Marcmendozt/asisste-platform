
using System.Configuration;

namespace Web.Marcacion.Reglas
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
