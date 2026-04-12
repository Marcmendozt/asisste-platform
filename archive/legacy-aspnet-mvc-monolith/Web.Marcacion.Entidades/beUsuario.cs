using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class beUsuario
    {
        public int ID_Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int ID_Persona { get; set; }
        public int ID_Perfil { get; set; }
        public string Perfil { get; set; }
        public int ID_Genero { get; set; }
        public string Genero { get; set; }
        public DateTime UltimaConexion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DocumentoIdentidad { get; set; }
        //Tipo Jornada

        public string NombreUsuario { get; set; }
        public int ID_TipoJornada { get; set; }

        public int ID_LugarTrabajo { get; set; }
    }
}
