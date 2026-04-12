using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beTipoDocumento
    {
        public int ID_TipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public int Longitud { get; set; }
        public int ID_Estado { get; set; }
    }
}
