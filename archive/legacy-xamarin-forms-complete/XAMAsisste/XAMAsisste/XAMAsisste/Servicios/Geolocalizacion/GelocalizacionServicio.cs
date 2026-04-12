using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;

namespace XAMAsisste.Servicios.Geolocalizacion
{
    public static class GelocalizacionServicio
    {
        public static class ServicioGelocalizacion
        {
            public static async Task<Position> ObtenerUbicacionActual()
            {
                Position ubicacion = null;

                try
                {
                    var gps = CrossGeolocator.Current;
                    gps.DesiredAccuracy = 100;

                    if (!gps.IsGeolocationAvailable || !gps.IsGeolocationEnabled)
                        return null;

                    ubicacion = await gps.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                }
                catch (Exception ex) { }

                if (ubicacion == null)
                    return null;

                return ubicacion;
            }
        }
    }
}
