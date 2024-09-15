using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gimnasio_AccesoDatos;
using Gimnasio_AccesoDatos.DataSetGimnasioTableAdapters;
using Gimnasio_Dominio;

namespace Gimnasio_Negocio
{
    public class SocioNegocio
    {
        public List<Socio> ObtenerSociosActivos()
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();
            DataTable sociosDataTable = sociosTableAdapter.ObtenerSociosActivos();
            List<Socio> listaSocios = new List<Socio>();

            foreach (DataSetGimnasio.SociosRow socioRow in sociosDataTable.Rows)
            {
                Socio socio = new Socio(
                    socioRow.id,
                    socioRow.dni,
                    socioRow.nombre,
                    socioRow.apellido,
                    socioRow.esta_activo
                    );

                listaSocios.Add(socio);
            }

            return listaSocios;
        }

        public List<Socio> ObtenerSocios(bool activos = false, string campoBusqueda = null)
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();
            DataTable sociosDataTable = new DataSetGimnasio.SociosDataTable();
            List<Socio> listaSocios = new List<Socio>();

            if (activos) 
            {
                sociosDataTable = sociosTableAdapter.ObtenerSociosActivos();
            }
            else if (!string.IsNullOrEmpty(campoBusqueda))
            {
                campoBusqueda = $"%{campoBusqueda}%";

                sociosDataTable = sociosTableAdapter.ObtenerSociosPorCampo(campoBusqueda);
            }
            else 
            {
                sociosDataTable = sociosTableAdapter.ObtenerSocios();
            }

            foreach (DataSetGimnasio.SociosRow socioRow in sociosDataTable.Rows)
            {
                Socio socio = new Socio(
                    socioRow.id,
                    socioRow.dni,
                    socioRow.nombre,
                    socioRow.apellido,
                    socioRow.esta_activo
                    );

                listaSocios.Add(socio);
            }

            return listaSocios;
        }

        public List<Socio> ObtenerSocios() 
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();
            DataTable sociosDataTable = sociosTableAdapter.ObtenerSocios();
            List<Socio> listaSocios = new List<Socio>();

            foreach (DataSetGimnasio.SociosRow socioRow in sociosDataTable.Rows)
            {
                Socio socio = new Socio(
                    socioRow.id,
                    socioRow.dni,
                    socioRow.nombre,
                    socioRow.apellido,
                    socioRow.esta_activo
                    );

                listaSocios.Add(socio);
            }

            return listaSocios;
        }
    }
}
