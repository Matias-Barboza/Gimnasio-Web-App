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
    public class AuditoriaTipoCuotaNegocio
    {
        public bool ActualizarIdUsuarioAuditoria(AuditoriaTipoCuota auditoriaTipoCuota, int idUsuario) 
        {
            AuditoriaValorTiposCuotaTableAdapter auditoriasTableAdapter = new AuditoriaValorTiposCuotaTableAdapter();

            return auditoriasTableAdapter.ActualizarIdUsuarioAuditoria(idUsuario , auditoriaTipoCuota.Id) == 1;
        }

        public AuditoriaTipoCuota ObtenerUltimaAuditoriaPorIdTipoCuota(int idTipoCuota) 
        {
            AuditoriaValorTiposCuotaTableAdapter auditoriasTableAdapter = new AuditoriaValorTiposCuotaTableAdapter();
            DataTable dataTableAuditorias = auditoriasTableAdapter.ObtenerUltimaAuditoriaPorIdTipoCuota(idTipoCuota);
            AuditoriaTipoCuota auditoriaTipoCuota = new AuditoriaTipoCuota();

            if (dataTableAuditorias.Rows.Count != 0) 
            {
                DataSetGimnasio.AuditoriaValorTiposCuotaRow auditoriaFila = (DataSetGimnasio.AuditoriaValorTiposCuotaRow) dataTableAuditorias.Rows[0];

                auditoriaTipoCuota.Id = auditoriaFila.id;
                auditoriaTipoCuota.TipoCuota.Id = auditoriaFila.id_tipo_cuota;
                auditoriaTipoCuota.TipoCuota.Valor = auditoriaFila.valor_anterior;
                auditoriaTipoCuota.Usuario.Id = auditoriaFila.Isid_usuarioNull() ? 0 : auditoriaFila.id_usuario;
                auditoriaTipoCuota.NuevoValor = auditoriaFila.valor_nuevo;
                auditoriaTipoCuota.FechaCambio = auditoriaFila.fecha_cambio;
            }

            return auditoriaTipoCuota;
        }

        public List<AuditoriaTipoCuota> ObtenerAuditoriasConDescripcionYUsuarioPorIdTipoCuota(int idTipoCuota) 
        {
            AuditoriasConDescripcionYUsuarioTableAdapter auditoriasTableAdapter = new AuditoriasConDescripcionYUsuarioTableAdapter();
            DataTable dataTableAuditorias = auditoriasTableAdapter.ObtenerAuditoriasConDescripcionYUsuarioPorIdTipoCuota(idTipoCuota);
            List<AuditoriaTipoCuota> listaAuditorias = new List<AuditoriaTipoCuota>();

            foreach (DataSetGimnasio.AuditoriasConDescripcionYUsuarioRow auditoriaFila in dataTableAuditorias.Rows)
            {
                AuditoriaTipoCuota auditoriaTipoCuota = new AuditoriaTipoCuota();

                auditoriaTipoCuota.Id = auditoriaFila.id;
                auditoriaTipoCuota.TipoCuota.Descripcion = auditoriaFila.descripcion;
                auditoriaTipoCuota.TipoCuota.Valor = auditoriaFila.valor_anterior;
                auditoriaTipoCuota.Usuario.NombreUsuario = auditoriaFila.nombre_usuario;
                auditoriaTipoCuota.NuevoValor = auditoriaFila.valor_nuevo;
                auditoriaTipoCuota.FechaCambio = auditoriaFila.fecha_cambio;

                listaAuditorias.Add(auditoriaTipoCuota);
            }

            return listaAuditorias;
        }
    }
}
