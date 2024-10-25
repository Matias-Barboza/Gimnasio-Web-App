using Gimnasio_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Utilidades
{
    public class Seguridad
    {
        public static string GetSHA256(string cadena) 
        {
            SHA256 hash = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder stringBuilder = new StringBuilder();

            stream = hash.ComputeHash(encoding.GetBytes(cadena));

            for (int i = 0; i < stream.Length; i++) 
            {
                stringBuilder.AppendFormat("{0:x2}", stream[i]);
            }

            return stringBuilder.ToString();
        }

        public static bool UsuarioEsAdmin(Usuario usuario)
        {
            return usuario.EsAdmin;
        }

        public static bool UsuarioEsProfesor(Usuario usuario)
        {
            return usuario.EsProfesor;
        }
    }
}
