using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Entidades;
using Web.Marcacion.Reglas;
using Web.Marcacion.Seguridad;

namespace Web.Marcacion.Controllers
{
    public class MaestrosController : Controller
    {

        #region ["Envio de Correo"]

        public ActionResult EnviarCorreo()
        {

            return View();
        }

        beCorreo obeCorreo = new beCorreo();
        public string SendMail(beCorreo obeCorreo)
        {
            string Respuesta = "";
            try
            {
                var Mensaje = new MailMessage();
                Mensaje.Subject = obeCorreo.Asunto;
                Mensaje.Body = obeCorreo.Mensaje;
                Mensaje.To.Add(obeCorreo.Destino);

                Mensaje.IsBodyHtml = true;

                var SMTP = new SmtpClient();
                SMTP.Send(Mensaje);
                Respuesta = "Correcto_" + "Mensaje enviado correctamente";
            }
            catch (Exception ex)
            {
                Respuesta = "Error_" + ex.Message;
            }
            return Respuesta;

        }

        #endregion


        #region["Reporte de asistencias"]
        public ActionResult Asistencia()
        {
            return View();
        }


        public JsonResult JListadoAsistenciaPerfil()
        {

            beUsuario oUsuario = (beUsuario)Session["DATOSUSUARIO"];
            beRespuesta obeRespuesta = new beRespuesta();
            brAsistencia obrAsistencia = new brAsistencia();
            try
            {
                if (oUsuario != null)
                {
                    if (oUsuario.Perfil == "Administrador")
                    {
                        obeRespuesta = obrAsistencia.JListadoPorPerfil();
                    }
                    else
                    {
                        obeRespuesta = obrAsistencia.JListadoPorUsuario(oUsuario.ID_Usuario);
                    }
                }
                else
                {

                    obeRespuesta.MensajeError = "SE ACABO LA SESIÓN";
                }
            }
            catch (Exception ex)
            {
                obeRespuesta.ExisteError = true;
                obeRespuesta.MensajeError = ex.Message;
            }


            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);

        }

        public JsonResult JListadoAsistenciaFiltradoPerfil(DateTime FechaInicio, DateTime FechaFin)
        {

            beUsuario oUsuario = (beUsuario)Session["DATOSUSUARIO"];
            beRespuesta obeRespuesta = new beRespuesta();
            brAsistencia obrAsistencia = new brAsistencia();
            try
            {
                if (oUsuario != null)
                {
                    if (oUsuario.Perfil == "Administrador")
                    {
                        obeRespuesta = obrAsistencia.FiltradoPorPerfil(FechaInicio, FechaFin);
                    }
                    else
                    {
                        obeRespuesta = obrAsistencia.FiltradoPorUsuario(oUsuario.ID_Usuario, FechaInicio, FechaFin);
                    }
                }
                else
                {

                    obeRespuesta.MensajeError = "SE ACABO LA SESIÓN";
                }
            }
            catch (Exception ex)
            {
                obeRespuesta.ExisteError = true;
                obeRespuesta.MensajeError = ex.Message;
            }


            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region ["Control de asistencias"]

        public ActionResult ControlAsistencia()
        {

            return View();
        }

        #endregion

        public FileResult DescargarArchivo(int ID_Usuario)
        {

            string Respuesta = "";
            string URL = "";
            string Archivo = "";
            string TipoArchivo = "";
            try
            {
                brFalta obrFalta = new brFalta();
                List<beFalta> lbeFalta = new List<beFalta>();
                beRespuesta obeRespuesta = obrFalta.JListadoFaltaPorUsuario(ID_Usuario);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    lbeFalta = (List<beFalta>)obeRespuesta.Data;
                    for (int i = 0; i < lbeFalta.Count; i++)
                    {
                        URL = lbeFalta[i].Url;
                        Archivo = lbeFalta[i].Documento;
                        TipoArchivo = lbeFalta[i].Extension;
                    }

                    Respuesta = "Correcto_";

                }
                else
                {
                    Respuesta = "Error_";
                }
            }
            catch (Exception E)
            {

                Respuesta = "Error_" + E.Message;
            }
            return File(URL + "\\" + Archivo, TipoArchivo, Archivo);

        }


        #region ["Perfil"]
        public ActionResult Perfil()
        {
            return View();
        }

        public JsonResult JListadoPerfiles()
        {
            brPerfil obrPerfil = new brPerfil();
            beRespuesta obeRespuesta = obrPerfil.JListadoPerfil();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }


        public string AgregarPerfil(bePerfil obePerfil)
        {
            string Respuesta = "";
            obePerfil.ID_Estado = 1;
            brPerfil obrPerfil = new brPerfil();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrPerfil.Agregar(obePerfil);
                if (obePerfil.EXISTE == 1)
                {
                    Respuesta = "Error_" + "EL PERFIL YA EXISTE";
                }
                else
                {
                    if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                    { Respuesta = "Correcto_"; }
                    else { Respuesta = "Error_" + obeRespuesta.MensajeError; }
                }
            }
            catch (Exception ex)
            { Respuesta = "Error_" + ex.Message; }
            return Respuesta;
        }

        public string EditarPerfil(bePerfil obePerfil)
        {
            string Respuesta = "";
            brPerfil obrPerfil = new brPerfil();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrPerfil.Editar(obePerfil);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else { Respuesta = "Error_" + obeRespuesta.MensajeError; }
            }
            catch (Exception ex)
            {
                Respuesta = "Error_" + ex.Message;
            }
            return Respuesta;
        }


        public string EliminarPerfil(int ID_Perfil)
        {
            string Respuesta = "";
            brPerfil obrPerfil = new brPerfil();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrPerfil.Eliminar(ID_Perfil);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                { Respuesta = "Correcto_"; }
                else { Respuesta = "Error_" + obeRespuesta.MensajeError; }
            }
            catch (Exception ex)
            { Respuesta = "Error_" + ex.Message; }
            return Respuesta;
        }

        #endregion



        #region ["TipoJornada"]
        public ActionResult TipoJornada()
        {
            return View();
        }


        public JsonResult JListadoTipoJornada()
        {
            brTipoJornada obrTipoJornada = new brTipoJornada();
            beRespuesta obeRespuesta = obrTipoJornada.JListadoTipoJornada();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }


        public string AgregarTipoJornada(beTipoJornada obeTipoJornada)
        {
            string Respuesta = "";
            obeTipoJornada.ID_Estado = 1;
            brTipoJornada obrTipoJornada = new brTipoJornada();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {

                obeRespuesta = obrTipoJornada.Agregar(obeTipoJornada);
                if (obeTipoJornada.EXISTE == 1)
                {
                    Respuesta = "Error_" + "LA JORNADA YA EXISTE";
                }
                else
                {
                    if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                    {
                        Respuesta = "Correcto_";
                    }
                    else
                    {
                        Respuesta = "Error_" + obeRespuesta.MensajeError;
                    }

                }


            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;
        }

        public string EditarTipoJornada(beTipoJornada obeTipoJornada)
        {
            string Respuesta = "";
            obeTipoJornada.ID_Estado = 1;
            brTipoJornada obrTipoJornada = new brTipoJornada();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrTipoJornada.Editar(obeTipoJornada);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;
        }

        public string EliminarTipoJornada(int ID_TipoJornada)
        {
            string Respuesta = "";

            brTipoJornada obrTipoJornada = new brTipoJornada();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrTipoJornada.Eliminar(ID_TipoJornada);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }

            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;
        }
        #endregion

        #region ["Movil"]

        public JsonResult JListadoMovilUsuario(int ID_Usuario)
        {
            brMovil obrMovil = new brMovil();
            beRespuesta obeRespuesta = obrMovil.JListadoMovil(ID_Usuario);
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region ["Jornadas Asignadas"]
        public ActionResult JornadasAsignadas()
        {
            return View();
        }

        public JsonResult JListadoJornadaEmpleado()
        {
            brJornadaEmpleado obrJornadaEmpleado = new brJornadaEmpleado();
            beRespuesta obeRespuesta = obrJornadaEmpleado.JListadoJornadaEmpleado();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }

        public string EditarJornadaEmpleado(beJornadaEmpleado obeJornadaEmpleado)
        {
            string Respuesta = "";

            brJornadaEmpleado obrJornadaEmpleado = new brJornadaEmpleado();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrJornadaEmpleado.Editar(obeJornadaEmpleado);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }

            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;
        }
        #endregion





        #region ["Persona"]
        public ActionResult Empleados()
        {

            return View();

        }

        public JsonResult JListadoPersona()
        {
            brPersona obrPersona = new brPersona();
            beRespuesta obeRespuesta = obrPersona.JListadoPersona();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }


        public string VerificarExistencia(string Correo)
        {
            string Respuesta = "";
            brPersona obrPersona = new brPersona();
            beRespuesta obeRespuesta = new beRespuesta();
            bePersona obePersona = new bePersona();
            obeRespuesta = obrPersona.TraerCorreo(Correo);
            obePersona = (bePersona)obeRespuesta.Data;
            try
            {
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    if (obePersona.ID_Persona == 0)
                    {
                        Respuesta = "CORRECTO_" + 0;
                    }
                    else
                    {
                        Respuesta = "CORRECTO_" + 1 + "_" + obePersona.NombreUsuario;

                    }
                }
                else
                {
                    Respuesta = "ERROR_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "ERROR_" + ex.Message;
            }
            return Respuesta;

        }

        public string BuscarUsuario(string DocumentoIdentidad)
        {
            string Respuesta = "";
            brUsuario obrUsuario = new brUsuario();
            beRespuesta obeRespuesta = new beRespuesta();
            obeRespuesta = obrUsuario.BuscarUsuario(DocumentoIdentidad);
            var obeUsuario = (beUsuario)obeRespuesta.Data;
            try
            {
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                   
                        Respuesta = "CORRECTO_" + obeUsuario.Nombres + "_" + obeUsuario.Apellidos + "_" + obeUsuario.ID_Usuario + "_" + obeUsuario.NombreUsuario;
                }
                else
                {
                    Respuesta = "ERROR_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "ERROR_" + ex.Message;
            }
            return Respuesta;

        }

        public string AgregarPersona(bePersona obePersona)
        {
            string Respuesta = "";
            obePersona.ID_Estado = 1;
            brPersona obrPersona = new brPersona();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {

                obeRespuesta = obrPersona.Agregar(obePersona);
                if (obePersona.EXISTE == 1)
                {
                    Respuesta = "Error_" + "EL CORREO YA EXISTE";
                }
                else
                {
                    if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                    {
                        obeRespuesta = obrPersona.TraerCorreo(obePersona.Correo);
                        obePersona = (bePersona)obeRespuesta.Data;


                        Respuesta = "Correcto_" + obePersona.ID_Persona;
                    }
                    else
                    {
                        Respuesta = "Error_" + obeRespuesta.MensajeError;
                    }

                }


            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;
        }
        #endregion



        #region ["Usuario"]
        public string AgregarUsuario(beUsuario obeUsuario)
        {
            string Respuesta = "";

            brUsuario obrUsuario = new brUsuario();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                var PassEncrip = Seguridad.Seguridad.Encriptar(obeUsuario.Clave);
                obeUsuario.Clave = PassEncrip;
                obeRespuesta = obrUsuario.Agregar(obeUsuario);

                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    obeRespuesta = obrUsuario.CapturarIDUsuario(obeUsuario.Usuario);

                    obeUsuario = (beUsuario)obeRespuesta.Data;
                    obeCorreo.Asunto = "BIENVENIDO AL SISTEMA ASISSTE";
                    var PassDescrip = Seguridad.Seguridad.Desencriptar(obeUsuario.Clave);
                    obeUsuario.Clave = PassDescrip;
                    obeCorreo.Mensaje = "USUARIO :" + obeUsuario.Usuario + "\n" + "CONTRASEÑA :" + obeUsuario.Clave;
                    obeCorreo.Destino = obeUsuario.Usuario;
                    SendMail(obeCorreo);
                    Respuesta = "Correcto_" + obeUsuario.ID_Usuario;
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;

        }

        public string EditarPersona(bePersona obePersona)
        {
            string Respuesta = "";

            brPersona obrPersona = new brPersona();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrPersona.Editar(obePersona);

                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;

        }
        public string AgregarEmpleadoJornada(int ID_Usuario, int ID_Horario)
        {

            string Respuesta = "";
            brJornadaEmpleado obrJornadaEmpleado = new brJornadaEmpleado();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrJornadaEmpleado.Agregar(ID_Usuario, ID_Horario);

                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {

                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "ERROR_" + ex.Message;
            }

            return Respuesta;
        }


        public string RecuperarCorreo(string Usuario)
        {
            string Respuesta = "";
            brUsuario obrUsuario = new brUsuario();
            beRespuesta obeRespuesta = new beRespuesta();
            beUsuario obeUsuario = new beUsuario();
            try
            {
                obeRespuesta = obrUsuario.RecuperarContraseña(Usuario);
                obeUsuario = (beUsuario)obeRespuesta.Data;
                if (obeUsuario.Clave != null)
                {
                    if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                    {
                        var PassDescrip = Desencripta(obeUsuario.Clave);
                 
                        obeCorreo.Asunto = "RECUPERACIÓN DE CONTRASEÑA";
                        obeCorreo.Mensaje = "SU CLAVE ES :" + PassDescrip;
                        obeCorreo.Destino = Usuario;
                        SendMail(obeCorreo);
                        Respuesta = "Correcto_";
                    }
                    else
                    {
                        Respuesta = "Error_" + obeRespuesta.MensajeError;
                    }
                }
                else
                {
                    Respuesta = "Error_" + "EL USUARIO NO EXISTE";

                }

            }
            catch (Exception EX)
            {

                Respuesta = "Error_" + EX.Message;
            }

            return Respuesta;

        }

        public string Desencripta(string valor)
        {
            return Seguridad.Seguridad.Desencriptar(valor);
        }
        public string EliminarUsuario(int ID_Usuario)
        {
            string Respuesta = "";

            brUsuario obrUsuario = new brUsuario();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrUsuario.Eliminar(ID_Usuario);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }

            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;
        }
        #endregion




        #region ["Inicio"]
        public ActionResult Inicio()
        {

            return View();
        }

        public JsonResult JListadoInicio()
        {
            beUsuario oUsuario = (beUsuario)Session["DATOSUSUARIO"];

            brModulos obrModulos = new brModulos();

            return Json(oUsuario, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region ["Tipo de documento"]

        public ActionResult TipoDocumento()
        {

            return View();

        }

        public JsonResult JListadoTipoDocumento()
        {
            brTipoDocumento obrTipoDocumento = new brTipoDocumento();
            beRespuesta obeRespuesta = obrTipoDocumento.JListadoTipoDocumento();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ["Cargo"]

        public ActionResult Cargo()
        {

            return View();

        }

        public JsonResult JListadoCargo()
        {
            brCargo obrCargo = new brCargo();
            beRespuesta obeRespuesta = obrCargo.JListadoCargo();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region ["Áreas de trabajo"]

        public ActionResult AreaTrabajo()
        {

            return View();

        }


        public JsonResult JListadoAreaTrabajo()
        {
            brAreaTrabajo obrAreaTrabajo = new brAreaTrabajo();
            beRespuesta obeRespuesta = obrAreaTrabajo.JListadoAreaTrabajo();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ["Horarios"]
        public ActionResult Horarios()
        {
            return View();

        }

        public JsonResult JListadoHorarios()
        {
            brHorarios obrHorarios = new brHorarios();
            beRespuesta obeRespuesta = obrHorarios.JListadoHorarios();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JListadoHorariosTipoJornada(int ID_TipoJornada)
        {
            brHorarios obrHorarios = new brHorarios();
            beRespuesta obeRespuesta = obrHorarios.JListadoHorariosTipoJornada(ID_TipoJornada);
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }

        public string AgregarHorarios(beHorarios obeHorarios)
        {
            string Respuesta = "";

            brHorarios obrHorarios = new brHorarios();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrHorarios.Agregar(obeHorarios);

                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                { Respuesta = "Correcto_"; }
                else { Respuesta = "Error_" + obeRespuesta.MensajeError; }

            }
            catch (Exception ex)
            { Respuesta = "Error_" + ex.Message; }
            return Respuesta;
        }

        public string EditarHorarios(beHorarios obeHorarios)
        {
            string Respuesta = "";

            brHorarios obrHorarios = new brHorarios();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrHorarios.Editar(obeHorarios);

                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                { Respuesta = "Correcto_"; }
                else { Respuesta = "Error_" + obeRespuesta.MensajeError; }

            }
            catch (Exception ex)
            { Respuesta = "Error_" + ex.Message; }
            return Respuesta;
        }

        public string EliminarHorarios(int ID_Horario)
        {
            string Respuesta = "";
            brHorarios obrHorarios = new brHorarios();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrHorarios.Eliminar(ID_Horario);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                { Respuesta = "Correcto_"; }
                else { Respuesta = "Error_" + obeRespuesta.MensajeError; }
            }
            catch (Exception ex)
            { Respuesta = "Error_" + ex.Message; }
            return Respuesta;
        }
        #endregion


        #region ["Falta"]
        public ActionResult Falta()
        {

            return View();

        }

        public JsonResult JListadoFalta()
        {
            beUsuario oUsuario = (beUsuario)Session["DATOSUSUARIO"];
            beRespuesta obeRespuesta = new beRespuesta();
            brFalta obrFalta = new brFalta();
            try
            {
                if (oUsuario != null)
                {
                    if (oUsuario.Perfil == "Administrador")
                    {
                        obeRespuesta = obrFalta.JListadoFalta();
                    }
                    else
                    {
                        obeRespuesta = obrFalta.JListadoFaltaPorUsuario(oUsuario.ID_Usuario);
                    }
                }
                else
                {

                    obeRespuesta.MensajeError = "SE ACABO LA SESIÓN";
                }
            }
            catch (Exception ex)
            {
                obeRespuesta.ExisteError = true;
                obeRespuesta.MensajeError = ex.Message;
            }


            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }

        public string GuardarArchivo(HttpPostedFileBase Archivo, DateTime Fecha, int ID_Usuario, string NombreDeUsuario)
        {

            string Respuesta = "";
            brAsistencia obrAsistencia = new brAsistencia();
            brFalta obrFalta = new brFalta();
            beFalta obeFalta = new beFalta();
            beRespuesta obeRespuesta = new beRespuesta();
            obeRespuesta = obrAsistencia.VerificarExistenciasAsistenciasAPI(ID_Usuario, Fecha);
            var Asistencia = (beAsistencia)obeRespuesta.Data;
            var FechaAsistencia = Fecha.ToString("dd/MM/yyyy");
            if (Asistencia.Fecha.ToString("dd/MM/yyyy") != FechaAsistencia)
            {
                obeRespuesta = obrFalta.VerificarFalta(ID_Usuario, Fecha);
                var Falta = (beFalta)obeRespuesta.Data;
                var FechaFalta = Fecha.ToString("dd/MM/yyyy");
                if (Falta.Fecha.ToString("dd/MM/yyyy") != FechaFalta)
                {
                    var FecharRegistrar = Fecha.ToString("dd.MM.yyyy");
                    var Ubicacion = Server.MapPath("~Faltas" + "/" + NombreDeUsuario + "/" + FecharRegistrar + "/" + Archivo.FileName);
                    var Extension = Path.GetExtension(Ubicacion);
                    var URL = Server.MapPath("~Faltas" + "/" + NombreDeUsuario + "/" + FecharRegistrar);
                    if (!Directory.Exists(Ubicacion))
                    {
                        beUsuario obeUsuario = new beUsuario();
                        var SessionUsuario = (beUsuario)Session["DATOSUSUARIO"];
                        obeFalta.ID_Usuario = ID_Usuario;
                        obeFalta.Fecha = Fecha;
                        obeFalta.Documento = Archivo.FileName;
                        obeFalta.Extension = Extension;
                        obeFalta.Url = URL;
                        if (SessionUsuario.Perfil == "Administrador")
                        {
                            obeFalta.ID_Estado = 4;
                        }
                        else
                        {
                            obeFalta.ID_Estado = 3;
                        }
                        obeRespuesta = obrFalta.RegistrarFalta(obeFalta);
                        if (obeRespuesta.ExisteError != true && obeRespuesta.MensajeError != "")
                        {
                            Directory.CreateDirectory(URL);
                            Archivo.SaveAs(Ubicacion);
                            Respuesta = "CORRECTO_SE SUBIÓ EL ARCHIVO CON EXITO!!";
                        }
                        else
                        {

                            Respuesta = "ERROR_" + obeRespuesta.MensajeError;
                        }


                    }
                    else
                    {
                        Respuesta = "ERROR_YA EXISTE UN ARCHIVO DE FALTA REGISTRADO";
                    }

                }
                else
                {
                    Respuesta = "ERROR_YA EXISTE UN ARCHIVO DE FALTA REGISTRADO";
                }
            }
            else
            {

                Respuesta = "ERROR_YA EXISTE UNA MARCACIÓN DE ASISTENCIA";
            }

            return Respuesta;
        }

        #endregion


        #region ["Lugar de trabajo"]
        public ActionResult LugarTrabajo()
        {
            return View();

        }

        public JsonResult JListadoLugarTrabajo()
        {
            brLugarTrabajo obrLugarTrabajo = new brLugarTrabajo();
            beRespuesta obeRespuesta = obrLugarTrabajo.JListadoLugarTrabajo();
            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region ["Ubicaciones predeterminadas"]
        public ActionResult UbicacionesPredeterminadas()
        {

            return View();

        }

        public string EditarUbicacion(beUbicacion obeUbicacion)
        {
            string Respuesta = "";

            brUbicacion obrUbicacion = new brUbicacion();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrUbicacion.Editar(obeUbicacion);

                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                {
                    Respuesta = "Correcto_";
                }
                else
                {
                    Respuesta = "Error_" + obeRespuesta.MensajeError;
                }
            }
            catch (Exception ex)
            {

                Respuesta = "Error_" + ex.Message;
            }

            return Respuesta;

        }



        #endregion

        #region ["Ubicaciones por usuario"]
        public ActionResult UbicacionesPorUsuario()
        {

            return View();

        }

        public string EliminarUbicacion(int ID_Ubicacion)
        {
            string Respuesta = "";
            brUbicacion obrUbicacion = new brUbicacion();
            beRespuesta obeRespuesta = new beRespuesta();
            try
            {
                obeRespuesta = obrUbicacion.Eliminar(ID_Ubicacion);
                if (obeRespuesta.ExisteError == false && obeRespuesta.MensajeError == null)
                { Respuesta = "Correcto_"; }
                else { Respuesta = "Error_" + obeRespuesta.MensajeError; }
            }
            catch (Exception ex)
            { Respuesta = "Error_" + ex.Message; }
            return Respuesta;
        }
        public JsonResult JListadoUbicacion()
        {

            beUsuario oUsuario = (beUsuario)Session["DATOSUSUARIO"];
            beRespuesta obeRespuesta = new beRespuesta();
            brUbicacion obrUbicacion = new brUbicacion();
            try
            {
                if (oUsuario != null)
                {
                    if (oUsuario.Perfil == "Administrador")
                    {
                        obeRespuesta = obrUbicacion.JListadoUbicacion();
                    }
                    else
                    {
                        obeRespuesta = obrUbicacion.JListadoUbicacionPorUsuario(oUsuario.ID_Usuario);
                    }
                }
                else
                {

                    obeRespuesta.MensajeError = "SE ACABO LA SESIÓN";
                }
            }
            catch (Exception ex)
            {
                obeRespuesta.ExisteError = true;
                obeRespuesta.MensajeError = ex.Message;
            }


            return Json(obeRespuesta, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}