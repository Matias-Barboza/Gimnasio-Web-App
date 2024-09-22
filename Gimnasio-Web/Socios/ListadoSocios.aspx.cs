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
        public bool SoloActivos;

        protected void Page_Load(object sender, EventArgs e)
        {
            TituloListado.InnerText = "Listado de socios";

            if (Request.QueryString["estado"] != null && Request.QueryString["estado"] == "activos") 
            {
                TituloListado.InnerText += " activos";
                SoloActivos = true;
                CargarSocios(SoloActivos);
                return;
            }

            CargarSocios();
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarSocios(bool activos = false, string campoBusqueda = null) 
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();
                List<Socio> listaSocios = socioNegocio.ObtenerSocios(activos, campoBusqueda);

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

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            string campoBusqueda = CampoBusquedaTextBox.Text;

            if (!string.IsNullOrEmpty(campoBusqueda)) 
            {
                CargarSocios(SoloActivos, campoBusqueda);
            }
        }

        protected void SociosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarSocio") 
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                string idSocio = SociosGridView.DataKeys[indiceFila].Value.ToString();

                Response.Redirect($"/Socios/FormularioSocio.aspx?id={idSocio}");
            }
        }
    }
}