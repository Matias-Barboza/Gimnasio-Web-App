﻿using Gimnasio_Dominio;
using Gimnasio_Web.Cuotas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Gimnasio_Utilidades
{
    public class PermisoHelper
    {
        public static bool PaginaNecesitaPermisoAdmin(Page page) 
        {
            return page is ListadosTiposCuota || page is FormularioTipoCuota || page is HistorialTipoCuota;
        }
    }
}