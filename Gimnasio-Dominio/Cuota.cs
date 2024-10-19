using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Dominio
{
    public class Cuota
    {
        public int Id { get; set; }
        public Socio Socio { get; set; }
        public TipoCuota TipoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string MesQueAbona { get; set; }
        public decimal MontoAbonado { get; set; }
        public decimal ValorTipoCuota { get; set; }
        public bool Visible { get; set; }

        public Cuota() 
        {
            Socio = new Socio();
            TipoCuota = new TipoCuota();
        }
    }
}
