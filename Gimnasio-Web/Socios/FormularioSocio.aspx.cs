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
        public bool EsEdicion;
        public bool ConPrimeraCuota;

        protected void Page_Load(object sender, EventArgs e)
        {
            EsEdicion = true;

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
                TiposCuotasDropDownList.Items.Insert(0, new ListItem("Seleccione una opción", "0"));
            }
            catch (Exception)
            {
                TiposCuotasDropDownList.Items.Insert(0, "No se cargaron correctamente los tipos de cuotas disponibles. Intente nuevamente recargando la página.");
            }
        }

        public bool TipoCuotaYCantidadValidos() 
        {
            TipoCuotaValidator.Validate();
            CantidadRequiredValidator.Validate();
            CantidadSoloNumerosValidator.Validate();
            MayorACeroValidator.Validate();

            return TipoCuotaValidator.IsValid && CantidadRequiredValidator.IsValid && CantidadSoloNumerosValidator.IsValid &&
                   MayorACeroValidator.IsValid;
        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void ConPrimeraCuotaButton_ServerClick(object sender, EventArgs e)
        {
            ConPrimeraCuota = ConPrimeraCuotaCheckBox.Checked;
            Session.Add("ConPrimeraCuota", ConPrimeraCuota);

            if (!ConPrimeraCuotaCheckBox.Checked && !string.IsNullOrEmpty(MontoAbonarTextBox.Text)) 
            {
                MontoAbonarTextBox.Text = string.Empty;
            }
        }

        protected void CalcularButton_Click(object sender, EventArgs e)
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio;
                List<string> valoresCalculados;
                string idTipoCuotaStr = TiposCuotasDropDownList.SelectedItem.Value;
                string cantidadStr = CantidadTextBox.Text;
                int idTipoCuota;
                int cantidad;

                if (!TipoCuotaYCantidadValidos()) 
                {
                    return;
                }

                tipoCuotaNegocio = new TipoCuotaNegocio();
                idTipoCuota = int.Parse(idTipoCuotaStr);
                cantidad = int.Parse(cantidadStr);


                MontoAbonarTextBox.Text = tipoCuotaNegocio.CalcularMontoAbonar(idTipoCuota, cantidad);

                valoresCalculados = new List<string>() { idTipoCuotaStr, cantidadStr, MontoAbonarTextBox.Text };

                Session.Add("ValoresCalculados", valoresCalculados);
            }
            catch (Exception ex)
            {
                
            }
        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void TipoCuotaValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                int.TryParse(TiposCuotasDropDownList.SelectedItem.Value, out int idTipoCuota);

                args.IsValid = idTipoCuota > 0;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

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

        protected void ValoresCalculadosValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (Session["ValoresCalculados"] == null) 
                {
                    args.IsValid = true;
                    return;
                }

                List<string> valoresCalculados = (List<string>) Session["ValoresCalculados"];

                args.IsValid = TiposCuotasDropDownList.SelectedItem.Value == valoresCalculados[0] &&
                               CantidadTextBox.Text == valoresCalculados[1] &&
                               MontoAbonarTextBox.Text == valoresCalculados[2];
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}