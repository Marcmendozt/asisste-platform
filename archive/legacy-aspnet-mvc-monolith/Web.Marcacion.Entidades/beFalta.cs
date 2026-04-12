using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beFalta
    {
        public int ID_Falta { get; set; }
        public int ID_Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Documento { get; set; }

        public string Extension { get; set; }
         public int ID_Estado { get; set; }
        public string Url { get; set; }
    }
}
