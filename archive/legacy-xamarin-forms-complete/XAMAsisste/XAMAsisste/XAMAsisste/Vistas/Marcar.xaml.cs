using Android.Content;
using Android.Locations;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAMAsisste.Entidades;
using XAMAsisste.Servicios.Interfaces;
using XAMAsisste.Servicios.SQL.Entidades;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace XAMAsisste.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Marcar : ContentPage
    {
        public Marcar()
        {
            MostrarUsuarioOriginal();
            GPSActivado();
            InitializeComponent();
            IniciarReloj();
            VerificarExistenciasAsistencia();
            //VerificarMarcado();
         

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
     
            VerificarGPSActivado();
     
            VerificarExistenciasAsistencia();
        }



        bool RespuestaGPS;
        string Verificar = "";
        private async void GPSActivado()
        {
            Verificar = DependencyService.Get<ILocSettings>().OpenSettings();
            if (Verificar == "GPS DESACTIVADO")
            {
                RespuestaGPS = await DisplayAlert("GPS!", "¿ACTIVAR GPS?", "SI", "NO");
                if (RespuestaGPS == true)
                {
                    Intent AjustesActivarGPS = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                    Forms.Context.StartActivity(AjustesActivarGPS);
                }
                else
                {
                    await DisplayAlert("ADVERTENCIA!", "AL APLICATIVO NO PODRA GUARDAR SU UBICACIÓN", "OK");
                }

            }
            else
            {
                ObtenerUbicacion();
            }
        }

        string UsuarioSQLLite;
        public void MostrarUsuarioOriginal()
        {


            var Lista = (List<SQLLITEbeUsuarios>)App.BD.ListarUsuarioUnico();
            try
            {
                if (Lista.Count == 0 || Lista == null)
                {
                    DisplayAlert("ADVERTENCIA", "USUARIO ORIGINAL ELIMINADO!", "OK");
                }
                else
                {

                    UsuarioSQLLite = Lista[0].Usuario;

                }

            }
            catch (Exception ex)
            {

                DisplayAlert("ERROR!!", ex.Message, "OK");
            }






        }

        private void VerificarGPSActivado()
        {
            Verificar = DependencyService.Get<ILocSettings>().OpenSettings();
            if (RespuestaGPS == true || Verificar == "GPS ACTIVADO")
            {
                ObtenerUbicacion();
            }
            else
            {
                Latitud = 0;
                Longitud = 0;
            }

        }


        double Latitud;
        double Longitud;

        private void IniciarReloj()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                Device.BeginInvokeOnMainThread(() =>

                    lblTiempo.Text = DateTime.Now.ToString("H:mm:ss")
                        );

                return true;
            });

        }



        private async void ObtenerUbicacion()
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    try
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                            await DisplayAlert("Ubicación", "Necesito los permisos de ubicación", "OK");
                        }
                        else if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                            await DisplayAlert("Ubicación", "Necesito los permisos de ubicación", "OK");
                        }
                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert("Ubicación", ex.Message, "OK");
                    }
                }
                else
                {

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                    if (status == PermissionStatus.Granted)
                    {
                        Xamarin.Essentials.Location location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));
                        lblLati.Text = location.Latitude.ToString();
                        lblLong.Text = location.Longitude.ToString();
                        Latitud = location.Latitude;
                        Longitud = location.Longitude;
                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        await DisplayAlert("Ubicación", status.ToString(), "OK");
                    }

                }


            }
            catch (Exception ex)
            {

                await DisplayAlert("Ubicación", ex.Message, "OK");
            }

        }






        private async void VerificarExistenciasAsistencia()
        {

            HttpResponseMessage RespuestaFalta = null;
            HttpResponseMessage RespuestaAsistencia = null;
            var ID_Usuario = Application.Current.Properties["ID_Usuario"];
            var httpCliente = new HttpClient();

            RespuestaFalta = await httpCliente.GetAsync($"http://201.240.192.217/api/Faltas?ID_Usuario=" + ID_Usuario + "&Fecha=" + DateTime.Now.ToString("yyyy/MM/dd"));
            RespuestaAsistencia = await httpCliente.GetAsync($"http://201.240.192.217/api/Asistencia?ID_Usuario=" + ID_Usuario + "&Fecha=" + DateTime.Now.ToString("yyyy/MM/dd"));

            var DATAFALTAS = await RespuestaFalta.Content.ReadAsStringAsync();
            var DATAASISTENCIAS = await RespuestaAsistencia.Content.ReadAsStringAsync();

            var CUsuarios = JsonConvert.DeserializeObject<beUsuarios>(DATAFALTAS);
            var CAsistencias = JsonConvert.DeserializeObject<beUsuarios>(DATAASISTENCIAS);
            DateTime FechaFalta = DateTime.Parse(CUsuarios.Fecha);
            DateTime FechaAsistencia = DateTime.Parse(CAsistencias.Fecha);


            if (FechaAsistencia.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd")){
                if (CAsistencias.HoraSalida.ToString() == "00:00:00")
                {
                    btn.Text = "Marcar Salida";
                    btn.IsEnabled = true;
                }
                else {
                    btn.Text = "Ya no se puede marcar";
                    btn.IsEnabled = false;
                }

            }
            else
            {
                btn.Text = "Marcar entrada";
                btn.IsEnabled = true;
            }
        }




        private async void MarcarAsistencia(object sender, EventArgs e)
        {
            HttpResponseMessage RespuestaVerificarGeovalla = null;
            HttpResponseMessage RespuestaRegistrarGeovalla = null;
            HttpResponseMessage RespuestaRegistrarSalida = null;
            var ID_Usuario = Application.Current.Properties["ID_Usuario"];
            if (btn.Text == "Marcar Salida")
            {
                var httpCliente = new HttpClient();
                var JSONSalidaAsistencia = new
                {
                    ID_Usuario = ID_Usuario,
                    HoraSalida = lblTiempo.Text,
                    UbicacionSalida = Latitud.ToString() + "," + Longitud.ToString(),
                };
                var JSONStringSalida = JsonConvert.SerializeObject(JSONSalidaAsistencia);
                var ContenidoSalidaAsistencia = new StringContent(JSONStringSalida, Encoding.UTF8, "application/json");
                RespuestaRegistrarSalida = await httpCliente.PostAsync($"http://201.240.192.217/api/AsistenciaSalida", ContenidoSalidaAsistencia);
                var ContenidoRespuestaSalida = await RespuestaRegistrarSalida.Content.ReadAsStringAsync();
                VerificarExistenciasAsistencia();
                await DisplayAlert("MENSAJE", ContenidoRespuestaSalida, "OK");

            }
            else
            {
                if (Latitud == 0 && Longitud == 0)
                {
                    await DisplayAlert("ERROR!!", "NO SE PUDO OBTENER LA UBICACIÓN CORRECTAMENTE", "OK");
                }
                else
                {
                    if (Latitud == 0 && Longitud == 0)
                    {
                        await DisplayAlert("ERROR!!", "NO SE PUDO OBTENER LA UBICACIÓN CORRECTAMENTE", "OK");
                    }
                    else
                    {
                        var httpCliente = new HttpClient();

                        RespuestaVerificarGeovalla = await httpCliente.GetAsync($"http://201.240.192.217/api/Usuarios?ID_Usuario=" + ID_Usuario);
                        var DataGeovalla = await RespuestaVerificarGeovalla.Content.ReadAsStringAsync();
                        var CGeovalla = JsonConvert.DeserializeObject<beUbicacion>(DataGeovalla);
                        if (CGeovalla.Ubicacion == null)
                        {
                            await DisplayAlert("MENSAJE!!", "NO EXISTE UBICACIÓN REGISTRADA, SE HARÁ EL PRIMER REGISTRO", "OK");
                            var jsonObject = new
                            {
                                ID_Usuario = ID_Usuario,
                                Ubicacion = Latitud.ToString() + "," + Longitud.ToString(),
                            };
                            var jsonString = JsonConvert.SerializeObject(jsonObject);
                            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                            RespuestaRegistrarGeovalla = await httpCliente.PostAsync($"http://201.240.192.217/api/Asistencia", content);
                            var Contenido = await RespuestaRegistrarGeovalla.Content.ReadAsStringAsync();
                            var jsonObjectInicio = new
                            {
                                ID_Usuario = ID_Usuario,
                                HoraIngreso = lblTiempo.Text,
                                UbicacionIngreso = Latitud.ToString() + "," + Longitud.ToString(),
                                NombreUsuario = UsuarioSQLLite,
                                Geovalla = "DENTRO DEL RANGO PERMITIDO"
                            };
                            var jsonStringInicio = JsonConvert.SerializeObject(jsonObjectInicio);
                            var contentInicio = new StringContent(jsonStringInicio, Encoding.UTF8, "application/json");
                            RespuestaRegistrarGeovalla = await httpCliente.PostAsync($"http://201.240.192.217/api/Maestros", contentInicio);
                            var ContenidoInicio = await RespuestaRegistrarGeovalla.Content.ReadAsStringAsync();
                            VerificarExistenciasAsistencia();
                            await DisplayAlert("MENSAJE", ContenidoInicio, "OK");
                        }
                        else
                        {
                            double distance = 0;
                            double EarthRadius = 6371;
                            string[] CortarUbicacion = CGeovalla.Ubicacion.Split(',');
                            double _latitud = (Latitud - Double.Parse(CortarUbicacion[0])) * (Math.PI / 180);
                            double _longitud = (Longitud - Double.Parse(CortarUbicacion[1])) * (Math.PI / 180);
                            double _a = Math.Sin(_latitud / 2) * Math.Sin(_latitud / 2) + Math.Cos(Double.Parse(CortarUbicacion[0]) * (Math.PI / 180)) * Math.Cos(Latitud * (Math.PI / 180)) * Math.Sin(_longitud / 2) * Math.Sin(_longitud / 2);
                            double _c = 2 * Math.Atan2(Math.Sqrt(_a), Math.Sqrt(1 - _a));

                            distance = (EarthRadius * _c) * 1000;
                            var Rango = "";
                            if (distance < 61.3988238130978)
                            {
                                Rango = "DENTRO DEL RANGO PERMITIDO";
                            }
                            else
                            {
                                Rango = "ESTA FUERA DEL RANGO PERMITIDO";
                            }
                            var jsonObject = new
                            {
                                ID_Usuario = ID_Usuario,
                                HoraIngreso = lblTiempo.Text,
                                UbicacionIngreso = Latitud.ToString() + "," + Longitud.ToString(),
                                NombreUsuario = UsuarioSQLLite,
                                Geovalla = Rango
                            };
                            var jsonString = JsonConvert.SerializeObject(jsonObject);
                            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                            RespuestaRegistrarGeovalla = await httpCliente.PostAsync($"http://201.240.192.217/api/Maestros", content);
                            var Contenido = await RespuestaRegistrarGeovalla.Content.ReadAsStringAsync();
                            await DisplayAlert("MENSAJE", Contenido, "OK");
                            VerificarExistenciasAsistencia();
                        }
                    }

                }

            }

        }


        private async void MostrarMapa(object sender, EventArgs e)
        {
            var location = new Xamarin.Essentials.Location(Latitud, Longitud);
            var options = new MapLaunchOptions { Name = "Posición actual" };

            try
            {
                await Map.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR!", ex.Message, "OK");
            }

        }



    }
}