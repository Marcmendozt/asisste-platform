using System;
using System.Collections.Generic;
using System.Text;

namespace WCFAPPAsisste.Entidades
{
    public class beUsuario
    {
        public int ID_Usuario { get; set; }
        public string Nombre { get; set; }

        public string HoraEntrada { get; set; }

        public int Tolerancia { get; set; }

        public int ID_Movil { get; set; }

        public bool SessionMovil { get; set; }
    }
}
