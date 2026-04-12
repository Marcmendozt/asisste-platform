using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beModulos
    {
        //ID
        public int ID_Modulo { get; set; }
        public int ID_Perfil { get; set; }
        //
        public int ID_ModuloPerfil { get; set; }
        public int ID_SubModulo { get; set; }
        public string DescripcionModulo { get; set; }
        public string DescripcionImage { get; set; }
        public string DescripcionModuloSub { get; set; }
        public string Direccion { get; set; }
    }
}
