using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beJornadaEmpleado
    {
        public int ID_EmpleadoJornada { get; set; }
        public int ID_Horario { get; set; }
        public int ID_TipoJornada { get; set; }
        //Jornada
        public string TipoJornada { get; set; } 
        public string NombreCompleto { get; set; }
        public string AreaTrabajo { get; set; }
        public string Cargo { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan Salida { get; set; }

        public int Tolerancia { get; set; }
    }
}
