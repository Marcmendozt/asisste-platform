using System;
using System.Collections.Generic;
using System.Text;

namespace WCFAPPAsisste.Entidades
{
    public class beRespuesta
    {
        public bool ExisteError { get; set; }
        public string MensajeError { get; set; }
        public object Data { get; set; }
    }
}
