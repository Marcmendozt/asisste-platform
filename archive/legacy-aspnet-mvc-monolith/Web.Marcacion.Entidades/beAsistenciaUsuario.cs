using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beAsistenciaUsuario
    {
        public DateTime Fecha { get; set; }
        public TimeSpan HoraIngresoReal { get; set; }
        public TimeSpan HoraSalidaReal { get; set; }
        public TimeSpan RetrasoEntrada { get; set; }

        public string UbicacionIngreso { get; set; }
        public string UbicacionSalida { get; set; }
        public bool Falta { get; set; }
        public string LugarTrabajo { get; set; }
        public string Geovalla { get; set; }
        public int Tolerancia { get; set; }

    }
}
