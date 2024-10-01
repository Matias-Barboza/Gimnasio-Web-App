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
        public bool Visible { get; set; }

        public Cuota() 
        {
            Socio = new Socio();
            TipoCuota = new TipoCuota();
        }

        public Cuota(int id, Socio socio, TipoCuota tipoCuota, DateTime fechaPago, DateTime fechaVencimiento, string mesQueAbona, decimal montoAbonado, bool visible)
        {
            Id = id;
            Socio = socio;
            TipoCuota = tipoCuota;
            FechaPago = fechaPago;
            FechaVencimiento = fechaVencimiento;
            MesQueAbona = mesQueAbona;
            MontoAbonado = montoAbonado;
            Visible = visible;
        }
    }
}
