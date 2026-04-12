using System;
using System.Collections.Generic;
using System.Text;

namespace XAMAsisste.Entidades
{
    public class beUsuarios
    {
        public string Usuario { get; set; }

        public bool SessionMovil { get; set; }

        public int ID_Usuario { get; set; }
        public int ID_Estado { get; set; }

        public string Fecha { get; set; }

        public TimeSpan HoraSalida { get; set; }

    }
}
