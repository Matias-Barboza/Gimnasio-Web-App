using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gimnasio_Negocio;
using Gimnasio_Dominio;

namespace Gimnasio_Web.Socios
{
    public partial class ListadoSocios : System.Web.UI.Page
    {
        private const string TITULO_LISTADO_ACTIVOS = "Listado de socios activos";
        public bool SoloActivos;
        public bool MostrarResultadoBusqueda;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["estado"] != null && Request.QueryString["estado"] == "activos") 
            {
                TituloListado.InnerText = TITULO_LISTADO_ACTIVOS;
                SoloActivos = true;
            }

            if (!IsPostBack) 
            {
                CargarSocios(activos: SoloActivos);
            }
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarSocios(bool activos = false, string campoBusqueda = null) 
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();
                List<Socio> listaSocios = socioNegocio.ObtenerSocios(activos, campoBusqueda);

                if (MostrarResultadoBusqueda) 
                {
                    ResultadoBusquedaLabel.InnerText = listaSocios.Count.ToString();
                }

                if (listaSocios.Count == 0) 
                {
                    return;
                }

                SociosGridView.DataSource = listaSocios;
                SociosGridView.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void CambiarEstadoSocio(int idSocio) 
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();
                Socio socio = socioNegocio.ObtenerSocioPorId(idSocio);

                socioNegocio.ActualizarEstadoActividadSocio(socio);
            }
            catch (Exception ex)
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
                CargarSocios(SoloActivos, campoBusqueda);
                return;
            }

            CargarSocios(SoloActivos);
        }

        protected void SociosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indiceFila = Convert.ToInt32(e.CommandArgument);
            int idSocio = Convert.ToInt32(SociosGridView.DataKeys[indiceFila].Value);

            if (e.CommandName == "EditarSocio") 
            {
                Response.Redirect($"/Socios/FormularioSocio.aspx?id={idSocio}");
            }
        }
    }
}