using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool EsAdmin { get; set; }
        public bool EsProfesor { get; set; }

        public Usuario() 
        {
        }

        public Usuario(int id, string nombreUsuario, string password, string nombre, string apellido, bool esAdmin, bool esProfesor)
        {
            Id = id;
            NombreUsuario = nombreUsuario;
            Password = password;
            Nombre = nombre;
            Apellido = apellido;
            EsAdmin = esAdmin;
            EsProfesor = esProfesor;
        }
    }
}
