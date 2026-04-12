using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beMovil
    {
        public int ID_Movil { get; set; }
        public int ID_Usuario { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string Idiom { get; set; }
        public string DeviceType { get; set; }
        public string IMEI { get; set; }
    }
}
