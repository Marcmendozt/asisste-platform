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
    public class brLugarTrabajo : brGeneral
    {
        public beRespuesta JListadoLugarTrabajo()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daLugarTrabajo odaLugarTrabajo = new daLugarTrabajo();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaLugarTrabajo.ListadoLugarTrabajo(Conexion);
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
