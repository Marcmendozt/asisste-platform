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
    public class brJornadaEmpleado : brGeneral
    {
        public beRespuesta JListadoJornadaEmpleado()
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daJornadaEmpleado odaJornadaEmpleado = new daJornadaEmpleado();
            using (SqlConnection Conexion = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    Conexion.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaJornadaEmpleado.ListadoJornadaEmpleado(Conexion);
                }
                catch (Exception ex)
                {

                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;
                }
            }
            return obeRespuesta;
        }


        public beRespuesta Editar(beJornadaEmpleado obeJornadaEmpleado)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daJornadaEmpleado odaJornadaEmpleado = new daJornadaEmpleado();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaJornadaEmpleado.Editar(con, obeJornadaEmpleado);
                }
                catch (SqlException ex)
                {
                    obeRespuesta.ExisteError = true;
                    obeRespuesta.MensajeError = ex.Message;

                }
            }
            return obeRespuesta;
        }

        public beRespuesta Agregar(int ID_Usuario, int ID_Horario)
        {
            beRespuesta obeRespuesta = new beRespuesta();
            daJornadaEmpleado odaJornadaEmpleado = new daJornadaEmpleado();
            using (SqlConnection con = new SqlConnection(SQLCadenaConexion))
            {
                try
                {
                    con.Open();
                    obeRespuesta.ExisteError = false;
                    obeRespuesta.Data = odaJornadaEmpleado.Agregar(con, ID_Usuario, ID_Horario);
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
