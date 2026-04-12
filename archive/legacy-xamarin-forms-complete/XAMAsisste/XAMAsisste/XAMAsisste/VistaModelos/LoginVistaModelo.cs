using ShellLogin.Services.Routing;
using Splat;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using System;
using XAMAsisste.Servicios.Identidad;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using XAMAsisste.Entidades;
using System.Net;
using XAMAsisste.Servicios.SQL.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace XAMAsisste.VistaModelos
{
    class LoginVistaModelo : BaseVistaModelo
    {
        private EnrutamientoServicio _NavegacionServicio;
        private readonly IdentidadServicio IIdentidadServicio;

        public LoginVistaModelo(EnrutamientoServicio ERS = null, IdentidadServicio ITS = null)
        {

            _NavegacionServicio = ERS ?? Locator.Current.GetService<EnrutamientoServicio>();
            this.IIdentidadServicio = ITS ?? Locator.Current.GetService<IdentidadServicio>();
            IniciaSesion = new Command(() => VIniciaSesion());



        }


        public ICommand IniciaSesion { get; set; }




        private string nombreusuario;
        public string NombreUsuario
        {
            get { return nombreusuario; }
            set
            {
                nombreusuario = value;
                OnPropertyChanged();
            }
        }


        private string clave;
        public string Clave
        {
            get { return clave; }
            set
            {
                clave = value;
                OnPropertyChanged();
            }
        }

        string NomUsuario = "";
        static CancellationTokenSource SCTS = new CancellationTokenSource();
        public CancellationToken PCT = SCTS.Token;


        //App.Current.Properties["ID_Movil"] = Usuarios.IdMovil;
        //       var ID_Movil = Application.Current.Properties["ID_Movil"];
        public async void BuscarCredenciales(CancellationToken CT)
        {

            HttpClient Cliente = new HttpClient();
            try
            {
                string URILogin = $"http://201.240.192.217/api/Maestros?Usuario=" + NombreUsuario + "&Clave=" + Clave + "";
                using (var Solicitud = new HttpRequestMessage(HttpMethod.Get, URILogin))
                {
                    using (var Respuesta = await Cliente.SendAsync(Solicitud, HttpCompletionOption.ResponseHeadersRead, CT))
                    {
                        var Stream = await Respuesta.Content.ReadAsStreamAsync();
                        if (Respuesta.IsSuccessStatusCode)
                        {
                            var CUsuarios = DeserializeJsonFromStream<beUsuarios>(Stream);
                            App.Current.Properties["Usuario"] = CUsuarios.Usuario;
                            App.Current.Properties["ID_Usuario"] = CUsuarios.ID_Usuario;
                            var ID_Usuario = Application.Current.Properties["ID_Usuario"];
                            var Usuario = Application.Current.Properties["Usuario"];

                            if (CUsuarios.Usuario == null)
                            {
                                await _NavegacionServicio.Mensajes("VERIFICAR", "EL USUARIO NO EXISTE", "OK");
                            }
                            else if (CUsuarios.ID_Estado == 1)
                            {
                                if (CUsuarios.SessionMovil == false)
                                {

                                    var httpCliente = new HttpClient();
                                    HttpResponseMessage RespuestaRegistrarMovil = null;
                                    var JSONListaMovil = new
                                    {
                                        ID_Usuario = ID_Usuario,
                                        Model = "",
                                        Manufacturer = "",
                                        Name = "",
                                        Version = "",
                                        Platform = "",
                                        Idiom = "",
                                        DeviceType = "",
                                        IMEI = ""
                                    };
                                    var JSONStringMovil = JsonConvert.SerializeObject(JSONListaMovil);
                                    var ContenidoSalidaMovil = new StringContent(JSONStringMovil, Encoding.UTF8, "application/json");
                                    RespuestaRegistrarMovil = await httpCliente.PostAsync($"http://201.240.192.217/api/Usuarios", ContenidoSalidaMovil);
                                    var ContenidoRespuestaMovil = await RespuestaRegistrarMovil.Content.ReadAsStringAsync();
                                    await _NavegacionServicio.Mensajes("VERIFICAR", ContenidoRespuestaMovil, "OK");
                                    App.BD.GuardarUsuario(Convert.ToString(Usuario), Convert.ToInt32(ID_Usuario));
                                    await _NavegacionServicio.Navegar("///Principal/Inicio");
                                }
                                else
                                {
                                    var Lista = (SQLLITEbeUsuarios)App.BD.ObtenerUsuario(ID_Usuario);
                                    if (Lista == null)
                                    {
                                        bool Respons = await _NavegacionServicio.MensajesSalida("VERIFICAR", "¿ESTÁ SEGURO DE INGRESAR CON OTRO USUARIO?", "OK", "SALIR");
                                        if (Respons == true)
                                        {
                                            await _NavegacionServicio.Mensajes("MENSAJE", "EL MARCADO DE ASISTENCIA SE GUARDARA CON EL NOMBRE DE USUARIO ANTERIOR", "CONTINUAR");
                                            await _NavegacionServicio.Navegar("///Principal/Inicio");
                                        }
                                        else
                                        {

                                            NombreUsuario = "";
                                            Clave = "";
                                        }
                                    }
                                    else {
                                        await _NavegacionServicio.Navegar("///Principal/Inicio");
                                    }
                                    
                                }

                            }
                            else if (Respuesta.StatusCode == HttpStatusCode.InternalServerError)
                            {

                                await _NavegacionServicio.Mensajes("ERROR!", "Algo ha ido mal en el servidor", "OK");

                            }
                        }
                    }
                }

            } catch (Exception ex)
            {

                await _NavegacionServicio.Mensajes("VERIFICAR", ex.Message, "OK");
            }


        }




        private void VIniciaSesion()
        {

            if (NombreUsuario == null && Clave == null)
            {
                _NavegacionServicio.Mensajes("CAMPOS VACIOS", "FALTA RELLENAR ALGUNOS CAMPOS", "OK");
            }
            else
            {
                BuscarCredenciales(PCT);
            }

        }


        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

    }
}
