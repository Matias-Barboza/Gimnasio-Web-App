using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web.Usuarios
{
    public partial class ListadoUsuarios : System.Web.UI.Page
    {
        public bool MostrarResultadoBusqueda { get; set; }
        public bool SoloProfesores { get; set; }
        public const string TITULO_LISTADO_PROFESORES = "Listado de usuarios profesores";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tipo"] != null && Request.QueryString["tipo"] == "profesor") 
            {
                TituloListado.InnerText = TITULO_LISTADO_PROFESORES;
                SoloProfesores = true;
            }

            if (!IsPostBack) 
            {
                CargarUsuarios(SoloProfesores);
            }
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarUsuarios(bool soloProfesores = false, string campoBusqueda = null) 
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                List<Usuario> listaUsuarios = usuarioNegocio.ObtenerUsuariosPor(soloProfesores, campoBusqueda);

                if (MostrarResultadoBusqueda) 
                {
                    ResultadoBusquedaLabel.InnerText = listaUsuarios.Count.ToString();
                }

                if (listaUsuarios.Count == 0) 
                {
                    return;
                }

                UsuariosGridView.DataSource = listaUsuarios;
                UsuariosGridView.DataBind();
            }
            catch (Exception)
            {

            }
        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            string campoBusqueda = CampoBusquedaTextBox.Text;

            if (!string.IsNullOrEmpty(campoBusqueda))
            {
                MostrarResultadoBusqueda = true;
                CargarUsuarios(SoloProfesores, campoBusqueda);
                return;
            }

            CargarUsuarios(SoloProfesores);
        }
    }
}