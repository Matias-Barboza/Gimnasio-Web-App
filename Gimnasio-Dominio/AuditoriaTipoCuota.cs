using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Dominio
{
    public class AuditoriaTipoCuota
    {
        public int Id { get; set; }
        public TipoCuota TipoCuota { get; set; }
        public Usuario Usuario { get; set; }
        public decimal NuevoValor { get; set; }
        public DateTime FechaCambio { get; set; }

        public AuditoriaTipoCuota()
        {
            TipoCuota = new TipoCuota();
            Usuario = new Usuario();
        }
    }
}
