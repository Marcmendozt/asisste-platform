using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beRespuesta
    {
        public bool ExisteError { get; set; }
        public string MensajeError { get; set; }
        public object Data { get; set; }
        public string Cadena { get; set; }

     
    }
}
