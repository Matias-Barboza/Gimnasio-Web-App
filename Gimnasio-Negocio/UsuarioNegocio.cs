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
        public bool AñadirUsuario(Usuario usuario) 
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();

            return usuariosTableAdapter.AñadirUsuario(usuario.Nombre,
                                                      usuario.Apellido,
                                                      usuario.NombreUsuario,
                                                      usuario.Password,
                                                      usuario.EsAdmin,
                                                      usuario.EsProfesor) == 1;
        }

        public bool ExisteNombreUsuario(Usuario usuario) 
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();
            DataTable usuariosDataTable = usuariosTableAdapter.ObtenerNombresUsuarios(usuario.NombreUsuario);
            List<string> nombresUsuarios = new List<string>();

            if (usuariosDataTable.Rows.Count == 0) 
            {
                return false;
            }

            foreach (DataSetGimnasio.UsuariosRow usuarioFila in usuariosDataTable.Rows)
            {
                nombresUsuarios.Add(usuarioFila.nombre_usuario);
            }

            usuariosTableAdapter.Dispose();
            usuariosDataTable.Dispose();

            return nombresUsuarios.Contains(usuario.NombreUsuario);
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

        public List<Usuario> ObtenerUsuariosPor(bool soloProfesores = false, string campoBusqueda = null) 
        {
            UsuariosTableAdapter usuariosTableAdapter = new UsuariosTableAdapter();
            DataTable usuariosDataTable = !string.IsNullOrEmpty(campoBusqueda) ? usuariosDataTable = usuariosTableAdapter.ObtenerUsuariosPor($"{campoBusqueda}%") :
                                                                                 usuariosDataTable = usuariosTableAdapter.ObtenerUsuarios();
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataSetGimnasio.UsuariosRow usuarioFila in usuariosDataTable.Rows)
            {
                Usuario usuario = new Usuario();

                usuario.Id = usuarioFila.id;
                usuario.NombreUsuario = usuarioFila.nombre_usuario;
                usuario.Nombre = usuarioFila.nombre;
                usuario.Apellido = usuarioFila.apellido;
                usuario.EsAdmin = usuarioFila.es_admin;
                usuario.EsProfesor = usuarioFila.es_profesor;

                listaUsuarios.Add(usuario);
            }

            if (soloProfesores) 
            {
                listaUsuarios = listaUsuarios.Where(u => u.EsProfesor).ToList();
            }

            return listaUsuarios;
        }

        //-------------------------------------------------- OTRAS FUNCIONALIDADES ----------------------------------------------------------------------------
        public bool InformacionCorrectaUsuario(Usuario usuarioAComprobar) 
        {
            return ObtenerUsuarioPor(usuarioAComprobar.NombreUsuario, usuarioAComprobar.Password) != null;
        }
    }
}
