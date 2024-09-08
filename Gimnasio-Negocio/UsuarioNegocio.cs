using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gimnasio_Dominio;
using Gimnasio_AccesoDatos;
using System.Data;
using Gimnasio_AccesoDatos.DataSetGimnasioTableAdapters;

namespace Gimnasio_Negocio
{
    public class UsuarioNegocio
    {
        public bool ExisteUsuario(Usuario usuario) 
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();
            DataTable usuariosDataTable = usuariosTableAdapter.ExisteUsuario(usuario.NombreUsuario);

            return usuariosDataTable.Rows.Count == 1;
        }

        public Usuario ObtenerUsuarioPor(string nombreUsuario, string password) 
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();
            DataTable usuariosDataTable = usuariosTableAdapter.ObtenerUsuarioPor(nombreUsuario, password);
            Usuario usuarioBuscado = null;

            if (usuariosDataTable.Rows.Count == 0) 
            {
                return usuarioBuscado;   
            }

            DataSetGimnasio.UsuariosRow usuarioFila = (DataSetGimnasio.UsuariosRow) usuariosDataTable.Rows[0];

            usuarioBuscado = new Usuario(
                usuarioFila.id,
                usuarioFila.nombre_usuario,
                usuarioFila.password,
                usuarioFila.nombre,
                usuarioFila.apellido,
                usuarioFila.es_admin,
                usuarioFila.es_profesor
                );

            usuariosTableAdapter.Dispose();
            usuariosDataTable.Dispose();

            return usuarioBuscado;
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();
            DataTable usuariosDataTable = usuariosTableAdapter.ObtenerUsuarioPorId(id);
            Usuario usuarioBuscado = null;

            if (usuariosDataTable.Rows.Count == 0) 
            {
                return usuarioBuscado;
            }

            DataSetGimnasio.UsuariosRow usuarioFila = (DataSetGimnasio.UsuariosRow) usuariosDataTable.Rows[0];

            usuarioBuscado = new Usuario(
                usuarioFila.id,
                usuarioFila.nombre_usuario,
                usuarioFila.password,
                usuarioFila.nombre,
                usuarioFila.apellido,
                usuarioFila.es_admin,
                usuarioFila.es_profesor
                );

            usuariosTableAdapter.Dispose();
            usuariosDataTable.Dispose();

            return usuarioBuscado;
        }

        public List<Usuario> ObtenerUsuarios() 
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();
            DataTable usuariosDataTable = usuariosTableAdapter.ObtenerUsuarios();
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataSetGimnasio.UsuariosRow usuarioFila in usuariosDataTable.Rows) 
            {
                Usuario usuario = new Usuario(
                    usuarioFila.id,
                    usuarioFila.nombre_usuario,
                    usuarioFila.password,
                    usuarioFila.nombre,
                    usuarioFila.apellido,
                    usuarioFila.es_admin,
                    usuarioFila.es_profesor
                    );

                listaUsuarios.Add(usuario);
            }

            usuariosTableAdapter.Dispose();
            usuariosDataTable.Dispose();

            return listaUsuarios;
        }

        //-------------------------------------------------- OTRAS FUNCIONALIDADES ----------------------------------------------------------------------------
        public bool InformacionCorrectaUsuario(Usuario usuarioAComprobar) 
        {
            return ObtenerUsuarioPor(usuarioAComprobar.NombreUsuario, usuarioAComprobar.Password) != null;
        }
    }
}
