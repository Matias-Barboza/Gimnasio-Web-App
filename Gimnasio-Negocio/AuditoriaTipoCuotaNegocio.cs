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
        public List<AuditoriaTipoCuota> ObtenerAuditoriasConDescripcionYUsuarioPorIdTipoCuota(int idTipoCuota) 
        {
            AuditoriasConDescripcionYUsuarioTableAdapter auditorias = new AuditoriasConDescripcionYUsuarioTableAdapter();
            DataTable dataTableAuditorias = auditorias.ObtenerAuditoriasConDescripcionYUsuarioPorIdTipoCuota(idTipoCuota);
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
