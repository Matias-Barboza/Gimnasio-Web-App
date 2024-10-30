using Gimnasio_Dominio;
using Gimnasio_Web.Cuotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Web.UI;

namespace Gimnasio_Web.Utilidades
{
    public class PermisoHelper
    {
        public static bool HaySesionIniciada(HttpSessionState session) 
        {
            return session["UsuarioSessionActual"] != null;
        }

        public static bool PaginaNecesitaPermisoAdmin(Page page) 
        {
            return page is ListadosTiposCuota || page is FormularioTipoCuota || page is HistorialTipoCuota;
        }
    }
}
