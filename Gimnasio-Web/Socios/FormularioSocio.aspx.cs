using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web.Socios
{
    public partial class FormularioSocio : System.Web.UI.Page
    {
        public bool ConPrimeraCuota;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarDropDownListTiposCuotas();
                ConfigurarFechaYMesActual();
                return;
            }

            if (Session["ConPrimeraCuota"] is bool valorActual) 
            {
                ConPrimeraCuota = valorActual;
            }
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void ConfigurarFechaYMesActual()
        {
            FechaPagoTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            MesQueAbonaTextBox.Text = DateTime.Today.ToString("MMMM", new CultureInfo("es-ES"));
        }

        public void CargarDropDownListTiposCuotas() 
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                List<TipoCuota> listaTiposCuotas = tipoCuotaNegocio.ObtenerTiposCuotaVisibles();

                if (listaTiposCuotas.Count == 0) 
                {
                    TiposCuotasDropDownList.Items.Insert(0, "No se encontraron tipos de cuotas disponibles");
                    return;
                }
                
                TiposCuotasDropDownList.DataSource = listaTiposCuotas;
                TiposCuotasDropDownList.DataTextField = "Descripcion";
                TiposCuotasDropDownList.DataValueField = "Id";
                TiposCuotasDropDownList.DataBind();
                TiposCuotasDropDownList.Items.Insert(0, "Seleccione una opción");
            }
            catch (Exception)
            {
                TiposCuotasDropDownList.Items.Insert(0, "No se cargaron correctamente los tipos de cuotas disponibles. Intente nuevamente recargando la página.");
            }
        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void ConPrimeraCuotaButton_ServerClick(object sender, EventArgs e)
        {
            ConPrimeraCuota = ConPrimeraCuotaCheckBox.Checked;
            Session.Add("ConPrimeraCuota", ConPrimeraCuota);
        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void DniUnicoValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!DniRequiredValidator.IsValid || !DniLongitudValidator.IsValid || !DniSoloNumerosValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                SocioNegocio socioNegocio = new SocioNegocio();

                args.IsValid = !socioNegocio.ExisteDniRegistrado(DniSocioTextBox.Text);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void MayorACeroValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!CantidadRequiredValidator.IsValid || !CantidadSoloNumerosValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = int.TryParse(CantidadTextBox.Text, out int cantidad) && cantidad > 0;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}