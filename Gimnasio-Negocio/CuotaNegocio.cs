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
    public class CuotaNegocio
    {
        public int AñadirCuota(Cuota cuota) 
        {
            CuotasTableAdapter cuotasTableAdapter = new CuotasTableAdapter();
            
            return (int) cuotasTableAdapter.AñadirCuota(
                                            cuota.Socio.Id,
                                            cuota.TipoCuota.Id,
                                            cuota.FechaPago.ToString("yyyy-MM-dd"),
                                            cuota.FechaVencimiento.ToString("yyyy-MM-dd"),
                                            cuota.MesQueAbona,
                                            cuota.MontoAbonado
                                            );
        }

        public bool ActualizarCuota(Cuota cuota) 
        {
            CuotasTableAdapter cuotasTableAdapter = new CuotasTableAdapter();

            bool cuotaActualizada = (int) cuotasTableAdapter.ActualizarCuota(
                                             cuota.Socio.Id,
                                             cuota.TipoCuota.Id,
                                             cuota.FechaPago.ToString("yyyy-MM-dd"),
                                             cuota.FechaVencimiento.ToString("yyyy-MM-dd"),
                                             cuota.MesQueAbona,
                                             cuota.MontoAbonado,
                                             cuota.Visible,
                                             cuota.Id
                                             ) == 1;

            return cuotaActualizada;
        }

        public Cuota ObtenerUltimaCuotaPorIdSocio(int idSocio) 
        {
            CuotasTableAdapter cuotasTableAdapter = new CuotasTableAdapter();
            DataTable cuotasDataTable = cuotasTableAdapter.ObtenerUltimaCuotaPorIdSocio(idSocio);
            Cuota ultimaCuota = new Cuota();

            if (cuotasDataTable.Rows.Count != 0) 
            {
                DataSetGimnasio.CuotasRow cuotaFila = (DataSetGimnasio.CuotasRow) cuotasDataTable.Rows[0];

                ultimaCuota.Id = cuotaFila.id;
                ultimaCuota.FechaPago = cuotaFila.fecha_pago;
                ultimaCuota.FechaVencimiento = cuotaFila.fecha_vencimiento;
                ultimaCuota.MesQueAbona = cuotaFila.mes_que_abona;
                ultimaCuota.MontoAbonado = cuotaFila.monto_abonado;
                ultimaCuota.Visible = cuotaFila.visible;
                ultimaCuota.Socio.Id = cuotaFila.id_socio;
                ultimaCuota.TipoCuota.Id = cuotaFila.id_tipo_cuota;
            }

            return ultimaCuota;
        }

        public Cuota ObtenerCuotaConSocioYTipoPorIdCuota(int idCuota) 
        {
            CuotasConSocioYTipoTableAdapter cuotasConSocioYTipoTableAdapter = new CuotasConSocioYTipoTableAdapter();
            DataTable cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotaConSocioYTipoPorIdCuota(idCuota);
            Cuota cuotaBuscada = new Cuota();

            if (cuotasDataTable.Rows.Count != 0) 
            {
                DataSetGimnasio.CuotasConSocioYTipoRow cuotaFila = (DataSetGimnasio.CuotasConSocioYTipoRow) cuotasDataTable.Rows[0];

                cuotaBuscada.Id = cuotaFila.id_cuota;
                cuotaBuscada.FechaPago = cuotaFila.fecha_pago;
                cuotaBuscada.FechaVencimiento = cuotaFila.fecha_vencimiento;
                cuotaBuscada.MesQueAbona = cuotaFila.mes_que_abona;
                cuotaBuscada.MontoAbonado = cuotaFila.monto_abonado;
                cuotaBuscada.Socio.Id = cuotaFila.id_socio;
                cuotaBuscada.Socio.Nombre = cuotaFila.nombre_socio;
                cuotaBuscada.Socio.Apellido = cuotaFila.apellido_socio;
                cuotaBuscada.TipoCuota.Descripcion = cuotaFila.descripcion_tipo_cuota;
            }

            return cuotaBuscada;
        }

        public List<Cuota> ObtenerCuotasConSocioYTipo(bool soloVencidas = false, bool soloProximasAVencerse = false, string campoBusqueda = null) 
        {
            CuotasConSocioYTipoTableAdapter cuotasConSocioYTipoTableAdapter = new CuotasConSocioYTipoTableAdapter();
            DataTable cuotasDataTable = new DataTable();
            List<Cuota>  listaCuotas = new List<Cuota>();

            if (!string.IsNullOrEmpty(campoBusqueda)) 
            {
                DateTime.TryParse(campoBusqueda, out DateTime fechaResultado);
                string fechaBusqueda = fechaResultado.ToShortDateString();
                decimal.TryParse(campoBusqueda, out decimal campoBusquedaMonto);

                if (soloVencidas)
                {
                    cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotasConSocioYTipoVencidasPor(campoBusqueda, fechaBusqueda, campoBusquedaMonto);
                }
                else if (soloProximasAVencerse)
                {
                    cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotasConSocioYTipoProximasVencersePor(campoBusqueda, fechaBusqueda, campoBusquedaMonto);
                }
                else
                {
                    cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotasConSocioYTipoPor(campoBusqueda, fechaBusqueda, campoBusquedaMonto);
                }
            }
            else 
            {
                if (soloVencidas)
                {
                    cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotasConSocioYTipoVencidas();
                }
                else if (soloProximasAVencerse)
                {
                    cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotasConSocioYTipoProximasVencerse();
                }
                else
                {
                    cuotasDataTable = cuotasConSocioYTipoTableAdapter.ObtenerCuotasConSocioYTipo();
                }
            }

            foreach (DataSetGimnasio.CuotasConSocioYTipoRow cuotaFila in cuotasDataTable.Rows)
            {
                Cuota cuota = new Cuota();

                cuota.Id = cuotaFila.id_cuota;
                cuota.FechaPago = cuotaFila.fecha_pago;
                cuota.FechaVencimiento = cuotaFila.fecha_vencimiento;
                cuota.MesQueAbona = cuotaFila.mes_que_abona;
                cuota.MontoAbonado = cuotaFila.monto_abonado;
                cuota.Socio.Id = cuotaFila.id_socio;
                cuota.Socio.Nombre = cuotaFila.nombre_socio;
                cuota.Socio.Apellido = cuotaFila.apellido_socio;
                cuota.TipoCuota.Descripcion = cuotaFila.descripcion_tipo_cuota;

                listaCuotas.Add(cuota);
            }

            return listaCuotas;
        }

        //-------------------------------------------------- OTROS --------------------------------------------------------------------------------------------
        public static DateTime FechaVencimientoCalculada(DateTime fechaPago, int idTipoCuota, int cantidad) 
        {
            if (idTipoCuota == 1) 
            {
                return fechaPago.AddDays(cantidad);
            }
            else if (idTipoCuota == 2) 
            {
                return fechaPago.AddDays(cantidad * 7);
            }
            else if (idTipoCuota == 3) 
            {
                return fechaPago.AddMonths(cantidad);
            }

            return fechaPago;
        }
    }
}
