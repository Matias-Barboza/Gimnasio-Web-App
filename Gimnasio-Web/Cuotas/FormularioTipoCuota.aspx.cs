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
    public partial class FormularioTipoCuota : System.Web.UI.Page
    {
        private const string TITULO_PAGINA_HISTORIAL = "Historial de cuota";
        public bool EsEdicion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            bool convertido = int.TryParse(Request.QueryString["id"], out int idTipoCuota);

            if (Request.QueryString.Count == 1) 
            {
                if (convertido) 
                {
                    EsEdicion = true;
                }
            }

            if (Request.QueryString.Count == 2) 
            {
                bool.TryParse(Request.QueryString["historial"], out bool mostrarHistorial);

                if (idTipoCuota > 0 && mostrarHistorial) 
                {
                    TituloPagina.InnerText = TITULO_PAGINA_HISTORIAL;
                    CargarHistorialTipoCuota(idTipoCuota);
                }
            }

            if (!IsPostBack) 
            {
                CargarDatosTipoCuota(idTipoCuota);
            }
        }

        //-------------------------------------------------- METÓDOS ------------------------------------------------------------------------------------------
        public void CargarDatosTipoCuota(int idTipoCuota) 
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                TipoCuota tipoCuota = tipoCuotaNegocio.ObtenerTipoCuotaPorId(idTipoCuota);

                if (tipoCuota.Id == 0) 
                {
                    return;
                }

                CodigoTipoCuotaTextBox.Text = tipoCuota.Id.ToString();
                DescripcionTextBox.Text = tipoCuota.Descripcion;
                MontoActualTextBox.Text = tipoCuota.Valor.ToString("C2").Substring(2);
            }
            catch (Exception ex)
            {

            }
        }

        public void CargarHistorialTipoCuota(int idTipoCuota) 
        {
            try
            {
                AuditoriaTipoCuotaNegocio auditoriaTipoCuotaNegocio = new AuditoriaTipoCuotaNegocio();
                List<AuditoriaTipoCuota> listaAuditoriasTipoCuota = auditoriaTipoCuotaNegocio.ObtenerAuditoriasConDescripcionYUsuarioPorIdTipoCuota(idTipoCuota);
                listaAuditoriasTipoCuota = listaAuditoriasTipoCuota.OrderByDescending(a => a.FechaCambio).ToList();

                if (listaAuditoriasTipoCuota.Count == 0) 
                {
                    return;
                }

                HistorialTipoCuotaGridView.DataSource = listaAuditoriasTipoCuota;
                HistorialTipoCuotaGridView.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}