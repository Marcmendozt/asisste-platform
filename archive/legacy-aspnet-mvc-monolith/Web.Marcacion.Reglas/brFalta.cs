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
    public class brFalta : brGeneral
    {
        public beRespuesta JListadoFalta()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daFalta odaFalta = new daFalta();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaFalta.ListarFalta(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta JListadoFaltaPorUsuario(int ID_Usuario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daFalta odaFalta = new daFalta();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaFalta.ListarFaltaPorUsuario(Conexion, ID_Usuario);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }

        public beRespuesta VerificarFalta(int ID_Usuario, DateTime Fecha)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daFalta odaFalta = new daFalta();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();

                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaFalta.VerificarFalta(con, ID_Usuario, Fecha);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }

        public beRespuesta RegistrarFalta(beFalta obeFalta)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daFalta odaFalta = new daFalta();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {

                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaFalta.RegistrarFalta(con, obeFalta);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }
    }
}
