using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beHorarios
    {
        public int ID_Horario { get; set; }
        public int ID_TipoJornada { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan Salida { get; set; }
        public int Tolerancia { get; set; }


        //T_TipoJornada

        public string Jornada { get; set; }
    }
}
