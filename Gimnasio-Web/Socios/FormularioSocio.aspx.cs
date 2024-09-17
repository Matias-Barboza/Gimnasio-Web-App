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
            ConfigurarFechaYMesActual();
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void ConfigurarFechaYMesActual() 
        {
            FechaPagoTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            MesQueAbonaTextBox.Text = DateTime.Today.ToString("MMMM", new CultureInfo("es-ES"));
        }
        
        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void ConPrimeraCuotaButton_ServerClick(object sender, EventArgs e)
        {
            ConPrimeraCuota = ConPrimeraCuotaCheckBox.Checked;
        }
    }
}