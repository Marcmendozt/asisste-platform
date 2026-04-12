using Newtonsoft.Json;
using ShellLogin.Services.Routing;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XAMAsisste.Entidades;
using XAMAsisste.Servicios.Identidad;

namespace XAMAsisste.VistaModelos
{
    class FaltasVistaModelo : BaseVistaModelo
    {
        private EnrutamientoServicio PEnrutamientoServicio;
        private readonly IdentidadServicio PIdentidadServicio;

        public FaltasVistaModelo(EnrutamientoServicio ERS = null, IdentidadServicio ITS = null)
        {

            PEnrutamientoServicio = ERS ?? Locator.Current.GetService<EnrutamientoServicio>();
            this.PIdentidadServicio = ITS ?? Locator.Current.GetService<IdentidadServicio>();
              BuscarArchivo = new Command(() => VBuscarArchivo());
            EnviarArchivo = new Command(() => VEnviarArchivo());

        }

        public ICommand BuscarArchivo { get; set; }
        public ICommand EnviarArchivo { get; set; }

   

        private string nombreArchivo;
    
        public string NombreArchivo
        {
            get { return nombreArchivo; }
            set
            {
                nombreArchivo = value;
                OnPropertyChanged();
            }
        }

      


        public async Task<bool> VInicio() {
            
            var httpCliente = new HttpClient();
            bool Val = true;
            HttpResponseMessage Respuesta = null;
            var ID_Usuario = Application.Current.Properties["ID_Usuario"];
            try
            {
                beUsuarios obeUsuarios = new beUsuarios();
                int IDUser = Convert.ToInt32(ID_Usuario);
                string Fecha = DateTime.Now.ToString("yyyy/MM/dd");
                Respuesta = await httpCliente.GetAsync($"http://201.240.192.217/api/Faltas?ID_Usuario=" + IDUser + "&Fecha="+ Fecha);
                var Contenido = await Respuesta.Content.ReadAsStringAsync();
                obeUsuarios = JsonConvert.DeserializeObject<beUsuarios>(Contenido);
                DateTime JFecha = DateTime.Parse(obeUsuarios.Fecha);
                if (JFecha.ToString("yyyy/MM/dd") == DateTime.Now.ToString(("yyyy/MM/dd")))
                {
                    Val = true;
                
                }
                else {
                    Val = false;
                }
            }
            catch (Exception ex)
            {

                
            }
            return Val;
          
        }




        FileResult pickResult;
        private async void VBuscarArchivo() {

            
            try
            {
                var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                         { DevicePlatform.Android, new[] { "application/pdf","image/png","image/jpeg"}},
                         
                });

              
                    pickResult = await FilePicker.PickAsync(new PickOptions { FileTypes = customFileType, });
                    

                if (pickResult != null)
                {
                    NombreArchivo = pickResult.FileName;
                }
           

                
            }
            catch (Exception ex)
            {

                await PEnrutamientoServicio.Mensajes("ERROR!!", ex.Message, "OK");
            }



        }

        private async void VEnviarArchivo() {

            if (pickResult != null)
            {
                HttpResponseMessage Respuesta = null;
                var stream = new MultipartFormDataContent();
                stream.Add(new StreamContent(await pickResult.OpenReadAsync()), "file", pickResult.FileName);
                if (stream.Headers.ContentLength < 3999999)
                {
                    
                    var Usuario = Application.Current.Properties["Usuario"];
                    var ID_Usuario = Application.Current.Properties["ID_Usuario"];
                    var httpCliente = new HttpClient();
                    
                    Respuesta = await httpCliente.PostAsync($"http://201.240.192.217/api/Faltas?ID_Usuario="+ ID_Usuario + "&Usuario=" + Usuario + "&fechaactual=" + DateTime.Now.ToString("yyyy/MM/dd") + "&ID_Estado=" + 3, stream);

                    var Contenido = await Respuesta.Content.ReadAsStringAsync();
                    await PEnrutamientoServicio.Mensajes("MENSAJE",Contenido, "OK");
                    await VInicio();
                    NombreArchivo = "";

                }
                else
                {
                    await PEnrutamientoServicio.Mensajes("ERROR!!!", "EL ARCHIVO NO DEBE PESAR MÁS DE 3MB", "OK");
                }



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
