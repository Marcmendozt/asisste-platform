using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Marcacion.Entidades
{
    public class bePersona
    {
        // Tabla persona
        public int ID_Persona { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Movil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int ID_Estado { get; set; }
        //Tabla Perfil
        public int ID_Perfil { get; set; }

        //Tabla Tipo de documento
        public int ID_TipoDocumento { get; set; }
    

        //Tabla usuario
        public int ID_Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }

        //Tabla Cargo
        public int ID_Cargo { get; set; }
        public string Cargo { get; set; }

        //EXISTE
        public int EXISTE { get; set; }


        //
        public int ID_Genero { get; set; }
        public bool SessionMovil { get; set; }

        public int ID_LugarTrabajo { get; set; }

    }
}
