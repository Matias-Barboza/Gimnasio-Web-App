using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio_Dominio
{
    public class TipoCuota
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
        public bool Visible { get; set; }

        public TipoCuota() { }

        public TipoCuota(int id, string descripcion, decimal valor, bool visible)
        {
            Id = id;
            Descripcion = descripcion;
            Valor = valor;
            Visible = visible;
        }
    }
}
