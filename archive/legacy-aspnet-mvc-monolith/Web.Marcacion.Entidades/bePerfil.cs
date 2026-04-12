using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class bePerfil
    {
        [Key]
        public int ID_Perfil { get; set; }
        public string Descripcion { get; set; }
        public int ID_Estado { get; set; }
        public int EXISTE { get; set; }
    }
}
