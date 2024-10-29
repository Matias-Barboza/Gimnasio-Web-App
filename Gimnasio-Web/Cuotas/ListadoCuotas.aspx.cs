using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web.Cuotas
{
    public partial class ListadoCuotas : System.Web.UI.Page
    {
        private const string TITULO_PAGINA_LISTADO_VENCIDAS = "Listado de cuotas vencidas";
        private const string TITULO_PAGINA_LISTADO_PROXIMAS_VENCERSE = "Listado de cuotas próximas a vencerse";
        public bool SoloVencidas;
        public bool SoloProximasAVencerse;
        public bool MostrarResultadoBusqueda;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["estado"] != null)
            {

                if (Request.QueryString["estado"] == "vencidas") 
                {
                    TituloListado.InnerText = TITULO_PAGINA_LISTADO_VENCIDAS;
                    SoloVencidas = true;
                }

                if (Request.QueryString["estado"] == "proximas") 
                {
                    TituloListado.InnerText = TITULO_PAGINA_LISTADO_PROXIMAS_VENCERSE;
                    SoloProximasAVencerse = true;
                }
            }

            if (!IsPostBack) 
            {
                CargarCuotas(soloVencidas: SoloVencidas, soloProximasAVencerse: SoloProximasAVencerse);
            }
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        private void CargarCuotas(bool soloVencidas = false, bool soloProximasAVencerse = false, string campoBusqueda = null)
        {
            try
            {
                CuotaNegocio cuotaNegocio = new CuotaNegocio();
                List<Cuota> listaCuotas = cuotaNegocio.ObtenerCuotasConSocioYTipo(soloVencidas, soloProximasAVencerse, campoBusqueda);
                
                if (MostrarResultadoBusqueda) 
                {
                    ResultadoBusquedaLabel.InnerText = listaCuotas.Count().ToString();                
                }

                if (listaCuotas.Count == 0) 
                {
                    return;
                }

                listaCuotas = cuotaNegocio.OrdenarCuotasSegun(listaCuotas, soloVencidas, soloProximasAVencerse);

                CuotasGridView.DataSource = listaCuotas;
                CuotasGridView.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void VincularDatosSocio(int idCuota) 
        {
            try
            {
                CuotaNegocio cuotaNegocio = new CuotaNegocio();
                Cuota cuota = cuotaNegocio.ObtenerCuotaConSocioYTipoPorIdCuota(idCuota);

                if (cuota.Id != 0) 
                {
                    CodigoSocioTextBox.Text = cuota.Socio.Id.ToString();
                    NombreSocioTextBox.Text = cuota.Socio.Nombre;
                    ApellidoSocioTextBox.Text = cuota.Socio.Apellido;
                }
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
                CargarCuotas(SoloVencidas, SoloProximasAVencerse, campoBusqueda);
                return;
            }

            CargarCuotas();
        }

        protected void CuotasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indiceFila = Convert.ToInt32(e.CommandArgument);
            int idCuota = Convert.ToInt32(CuotasGridView.DataKeys[indiceFila].Value);

            if (e.CommandName == "EditarCuota") 
            {

                Response.Redirect($"/Cuotas/FormularioCuota.aspx?id={idCuota}", true);
            }

            if (e.CommandName == "VerSocio") 
            {
                VincularDatosSocio(idCuota);
            }
        }
    }
}