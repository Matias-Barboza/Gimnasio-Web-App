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
        public bool AñadirTipoCuota(TipoCuota tipoCuota) 
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();

            return tiposCuotasTableAdapter.AñadirTipoCuota(tipoCuota.Descripcion, tipoCuota.Valor, tipoCuota.CantidadEnDias) == 1;
        }

        public bool ActualizarMontoTipoCuota(TipoCuota tipoCuota)
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            
            return tiposCuotasTableAdapter.ActualizarMontoTipoCuota(tipoCuota.Valor, tipoCuota.Id) == 1;
        }
        
        public bool ActualizarEstadoTipoCuota(TipoCuota tipoCuota)
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            
            return tiposCuotasTableAdapter.ActualizarEstadoTipoCuota(!tipoCuota.Visible, tipoCuota.Id) == 1;
        }

        public bool ExisteTipoCuotaConId(int idTipoCuota) 
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();

            return tiposCuotasTableAdapter.ExisteTipoCuotaConId(idTipoCuota) != null;
        }

        public TipoCuota ObtenerTipoCuotaPorId(int idTipoCuota) 
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            DataTable tiposCuotaDataTable = tiposCuotasTableAdapter.ObtenerTipoCuotaPorId(idTipoCuota);
            TipoCuota tipoCuotaBuscada = new TipoCuota();

            if (tiposCuotaDataTable.Rows.Count != 0) 
            {
                DataSetGimnasio.TiposCuotasRow tipoCuotaFila = (DataSetGimnasio.TiposCuotasRow) tiposCuotaDataTable.Rows[0];

                tipoCuotaBuscada.Id = tipoCuotaFila.id;
                tipoCuotaBuscada.Descripcion = tipoCuotaFila.descripcion;
                tipoCuotaBuscada.Valor = tipoCuotaFila.valor;
                tipoCuotaBuscada.CantidadEnDias = tipoCuotaFila.cantidad_en_dias;
                tipoCuotaBuscada.Visible = tipoCuotaFila.visible;
            }

            return tipoCuotaBuscada;
        }

        public int ObtenerCantidadDiasTipoCuotaPorId(int idTipoCuota) 
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();

            return (int) tiposCuotasTableAdapter.ObtenerCantidadDiasTipoCuotaPorId(idTipoCuota);
        }

        public List<TipoCuota> ObtenerTiposCuotaPor(string campoBusqueda = null, bool soloVisibles = false)
        {
            TiposCuotasTableAdapter tiposCuotasTableAdapter = new TiposCuotasTableAdapter();
            DataTable tiposCuotasDataTable = new DataTable();
            List<TipoCuota> listaTiposCuotas = new List<TipoCuota>();

            if (campoBusqueda != null) 
            {
                decimal.TryParse(campoBusqueda, out decimal campoBusquedaMonto);
                campoBusqueda = $"%{campoBusqueda}%";

                tiposCuotasDataTable = tiposCuotasTableAdapter.ObtenerTiposCuotasPor(campoBusqueda, campoBusquedaMonto);
            }
            else 
            {
                tiposCuotasDataTable = tiposCuotasTableAdapter.ObtenerTiposCuotas();
            }

            foreach (DataSetGimnasio.TiposCuotasRow tipoCuotaFila in tiposCuotasDataTable.Rows)
            {
                TipoCuota tipoCuota = new TipoCuota()
                {
                    Id = tipoCuotaFila.id,
                    Descripcion = tipoCuotaFila.descripcion,
                    Valor = tipoCuotaFila.valor,
                    CantidadEnDias = tipoCuotaFila.cantidad_en_dias,
                    Visible = tipoCuotaFila.visible
                };

                listaTiposCuotas.Add(tipoCuota);
            }

            if (soloVisibles) 
            {
                listaTiposCuotas = listaTiposCuotas.Where(t => t.Visible).ToList();
            }

            return listaTiposCuotas;
        }

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
                        tiposCuotasRow.cantidad_en_dias,
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
                        tiposCuotasRow.cantidad_en_dias,
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
