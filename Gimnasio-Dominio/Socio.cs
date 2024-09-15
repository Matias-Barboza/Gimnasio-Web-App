using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Dominio
{
    public class Socio
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool EstaActivo { get; set; }

        public Socio() { }

        public Socio(int id, string dni, string nombre, string apellido, bool estaActivo)
        {
            Id = id;
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            EstaActivo = estaActivo;
        }
    }
}
