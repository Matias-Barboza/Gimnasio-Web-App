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
            if (Session["ConPrimeraCuota"] is bool valorActual)
            {
                ConPrimeraCuota = valorActual;
            }

            if (Request.QueryString["id"] is string id && int.TryParse(id, out int idSocioEditar))
            {
                TituloFormulario.InnerText = "Formulario de edición de socio";

                EsEdicion = true;

                if (!IsPostBack)
                {
                    CargarDatosSocioAEditar(idSocioEditar);
                }
            }

            if (!IsPostBack)
            {
                CargarDropDownListTiposCuotas();
                ConfigurarFechaYMesActual();
                ConfigurarDatosSessionExistentes();
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

        public void ConfigurarDatosSessionExistentes() 
        {
            if (Session["ConPrimeraCuota"] is bool valorTrue && valorTrue) 
            {
                EliminarDatosSession();
            }

            ConPrimeraCuota = false;
        }

        public void ValidarTipoCuotaYCantidad()
        {
            TiposCuotasDropDownList.SelectedIndex = 1;
            CantidadTextBox.Text = "1";
            MontoAbonarTextBox.Text = "1";
            Page.Validate();
        }

        public void VincularDatosFormularioASocio(Socio socio)
        {
            socio.Dni = DniSocioTextBox.Text;
            socio.Nombre = NombreSocioTextBox.Text;
            socio.Apellido = ApellidoSocioTextBox.Text;
        }

        public void VincularDatosACuota(Cuota cuota, int idSocio)
        {
            int cantidad = int.Parse(CantidadTextBox.Text);

            cuota.Socio.Id = idSocio;
            cuota.TipoCuota.Id = int.Parse(TiposCuotasDropDownList.SelectedItem.Value);
            cuota.FechaPago = DateTime.Today;
            cuota.FechaVencimiento = CuotaNegocio.FechaVencimientoCalculada(cuota.FechaPago, cuota.TipoCuota.Id, cantidad);
            cuota.MesQueAbona = MesQueAbonaTextBox.Text;
            cuota.MontoAbonado = decimal.Parse(MontoAbonarTextBox.Text);
        }

        public void EliminarDatosSession()
        {
            Session.Remove("ConPrimeraCuota");
            Session.Remove("ValoresCalculados");
        }

        public void CargarDatosSocioAEditar(int idSocioEditar) 
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();
                Socio socioAEditar = socioNegocio.ObtenerSocioPorId(idSocioEditar);

                VincularDatosSocioAFormulario(socioAEditar);
            }
            catch (Exception ex)
            {

            }
        }

        public void VincularDatosSocioAFormulario(Socio socioAEditar) 
        {
            CodigoSocioTextBox.Text = socioAEditar.Id.ToString();
            DniSocioTextBox.Text = socioAEditar.Dni;
            NombreSocioTextBox.Text = socioAEditar.Nombre;
            ApellidoSocioTextBox.Text = socioAEditar.Apellido;
        }

        //-------------------------------------------------- FUNCIONES ----------------------------------------------------------------------------------------
        public bool TipoCuotaYCantidadValidos() 
        {
            TipoCuotaValidator.Validate();
            CantidadRequiredValidator.Validate();
            CantidadSoloNumerosValidator.Validate();
            MayorACeroValidator.Validate();

            return TipoCuotaValidator.IsValid && CantidadRequiredValidator.IsValid && 
                   CantidadSoloNumerosValidator.IsValid && MayorACeroValidator.IsValid;
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

        protected void RegistrarSocioButton_Click(object sender, EventArgs e)
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();
                CuotaNegocio cuotaNegocio = new CuotaNegocio();
                int idNuevoSocio;
                int idCuotaNueva;
                Socio socioNuevo = new Socio();
                Cuota cuotaNueva = new Cuota();

                if (!ConPrimeraCuota) 
                {
                    ValidarTipoCuotaYCantidad();
                }

                if (!Page.IsValid) 
                {
                    return;
                }

                VincularDatosFormularioASocio(socioNuevo);

                idNuevoSocio = socioNegocio.AñadirSocio(socioNuevo);

                if (ConPrimeraCuota) 
                {
                    VincularDatosACuota(cuotaNueva, idNuevoSocio);
                    idCuotaNueva = cuotaNegocio.AñadirCuota(cuotaNueva);
                }

                EliminarDatosSession();

                Response.Redirect("/Socios/ListadoSocios.aspx", false);
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void EditarSocioButton_Click(object sender, EventArgs e)
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();
                Socio socioEditar = new Socio();

                ValidarTipoCuotaYCantidad();

                if(!Page.IsValid) 
                {
                    return;
                }

                socioEditar.Id = int.Parse(Request.QueryString["id"]);

                VincularDatosFormularioASocio(socioEditar);

                socioNegocio.ActualizarSocio(socioEditar);

                Response.Redirect("/Socios/ListadoSocios.aspx");
            }
            catch (Exception ex)
            {

            }
        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void DniUnicoValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                SocioNegocio socioNegocio = new SocioNegocio();

                if (!DniRequiredValidator.IsValid || !DniLongitudValidator.IsValid || !DniSoloNumerosValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                if (EsEdicion)
                {
                    int idSocio = int.Parse(Request.QueryString["id"]);
                    bool dniPerteneceASocio = socioNegocio.DniPerteneceASocio(idSocio, DniSocioTextBox.Text);
                    
                    if (dniPerteneceASocio) 
                    {
                        args.IsValid = true;
                        return;
                    }
                }

                args.IsValid = !socioNegocio.ExisteDniRegistrado(DniSocioTextBox.Text);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

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