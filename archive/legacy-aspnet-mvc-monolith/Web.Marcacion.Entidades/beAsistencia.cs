using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beAsistencia
    {

        //Listado

        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraIngresoMovil { get; set; }
        public TimeSpan HoraSalidaMovil { get; set; }
        public TimeSpan HoraIngresoReal { get; set; }
        public TimeSpan HoraSalidaReal { get; set; }
        public TimeSpan RetrasoEntrada { get; set; }

        public string UbicacionIngreso { get; set; }
        public string UbicacionSalida { get; set; }
        public bool Falta { get; set; }

        public int ID_LugarTrabajo { get; set; }
        public string LugarTrabajo { get; set; }

        public int Tolerancia { get; set; }

        //ID
        public string Geovalla { get; set; }
        public int ID_Asistencia { get; set; }
        public int ID_Usuario { get; set; }


    }
}
