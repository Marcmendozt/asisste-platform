using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Marcacion.Datos;
using Web.Marcacion.Entidades;

namespace Web.Marcacion.Reglas
{
    public class brAreaTrabajo : brGeneral
    {
        public beRespuesta JListadoAreaTrabajo()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daAreaTrabajo odaAreaTrabajo = new daAreaTrabajo();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaAreaTrabajo.ListadoAreaTrabajo(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }
    }
}
