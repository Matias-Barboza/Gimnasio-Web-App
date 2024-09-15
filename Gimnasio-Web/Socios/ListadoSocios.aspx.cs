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
            if (Request.QueryString != null && Request.QueryString["estado"] == "activos") 
            {
                SoloActivos = true;
                CargarSocios(SoloActivos);
                return;
            }

            CargarSocios();
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarSocios(bool activos = false, string campoBusqueda = null) 
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

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void SociosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            string campoBusqueda = CampoBusquedaTextBox.Text;

            if (!string.IsNullOrEmpty(campoBusqueda)) 
            {
                CargarSocios(SoloActivos, campoBusqueda);
            }
        }

    }
}