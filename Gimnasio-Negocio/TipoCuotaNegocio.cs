using Gimnasio_AccesoDatos;
using Gimnasio_AccesoDatos.DataSetGimnasioTableAdapters;
using Gimnasio_Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Negocio
{
    public class TipoCuotaNegocio
    {
        public List<TipoCuota> ObtenerTiposCuotaVisibles()
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            DataTable tiposCuotasDataTable = tiposCuotasTableAdapter.ObtenerTiposCuotasVisibles();
            List<TipoCuota> listaTiposCuotas = new List<TipoCuota>();

            foreach (DataSetGimnasio.TiposCuotasRow tiposCuotasRow in tiposCuotasDataTable.Rows)
            {
                TipoCuota tipoCuota = new TipoCuota(
                        tiposCuotasRow.id,
                        tiposCuotasRow.descripcion,
                        tiposCuotasRow.valor,
                        tiposCuotasRow.visible
                    );

                listaTiposCuotas.Add(tipoCuota);
            }

            return listaTiposCuotas;
        }

        public List<TipoCuota> ObtenerTiposCuota() 
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            DataTable tiposCuotasDataTable = tiposCuotasTableAdapter.ObtenerTiposCuotas();
            List<TipoCuota> listaTiposCuotas = new List<TipoCuota>();

            foreach (DataSetGimnasio.TiposCuotasRow tiposCuotasRow in tiposCuotasDataTable.Rows)
            {
                TipoCuota tipoCuota = new TipoCuota(
                        tiposCuotasRow.id,
                        tiposCuotasRow.descripcion,
                        tiposCuotasRow.valor,
                        tiposCuotasRow.visible
                    );

                listaTiposCuotas.Add(tipoCuota);
            }

            return listaTiposCuotas;
        }

        //-------------------------------------------------- ADICIONALES --------------------------------------------------------------------------------------
        public string CalcularMontoAbonar(int idTipoCuota, int cantidad) 
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            decimal valorActual = tiposCuotasTableAdapter.ObtenerValorActualPorId(idTipoCuota) ?? 0;
            decimal montoAbonar = 0;

            if (valorActual != 0) 
            {
                montoAbonar = valorActual * cantidad;
            }

            return montoAbonar.ToString("C2").Substring(2);
        }
    }
}
