
using Android.Content;
using Android.Locations;
using Xamarin.Forms;
using XAMAsisste.Droid.Clases;
using XAMAsisste.Servicios.Interfaces;

[assembly: Dependency(typeof(LocationZ))]
namespace XAMAsisste.Droid.Clases
{
    public class LocationZ : ILocSettings
    {

        public string OpenSettings()
        {
            string Respuesta = "";

            LocationManager manager = (LocationManager)MainActivity.LocationContext.GetSystemService(Context.LocationService);
            bool GPSActivo = manager.IsProviderEnabled(LocationManager.GpsProvider);
            if (GPSActivo == false)
            {
                Respuesta = "GPS DESACTIVADO";
            }
            else
            {
                Respuesta = "GPS ACTIVADO";
            }
            return Respuesta;
        }
    }
}