using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public int AñadirSocio(Socio socio) 
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();

            return (int) sociosTableAdapter.AñadirSocio(socio.Dni, socio.Nombre, socio.Apellido);
        }

        public int ActualizarSocio(Socio socio) 
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();

            return sociosTableAdapter.ActualizarSocio(socio.Dni, socio.Nombre, socio.Apellido, socio.Id);
        }

        public bool ActualizarEstadoActividadSocio(Socio socio) 
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();
            
            return (int) sociosTableAdapter.ActualizarEstadoActividadSocio(!socio.EstaActivo, socio.Id) == 1;
        }

        public Socio ObtenerSocioPorId(int idSocio) 
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();
            DataTable sociosDataTable = sociosTableAdapter.ObtenerSocioPorId(idSocio);
            Socio socioEncontrado = new Socio();

            if (sociosDataTable.Rows.Count != 0) 
            {
                DataSetGimnasio.SociosRow socioFila = (DataSetGimnasio.SociosRow) sociosDataTable.Rows[0];

                socioEncontrado.Id = socioFila.id;
                socioEncontrado.Dni = socioFila.dni;
                socioEncontrado.Nombre = socioFila.nombre;
                socioEncontrado.Apellido = socioFila.apellido;
                socioEncontrado.EstaActivo = socioFila.esta_activo;
            }

            return socioEncontrado;
        }

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

        public bool DniPerteneceASocio(int idSocio, string dniAComprobar)  
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();

            return (int) sociosTableAdapter.DniPerteneceASocio(idSocio, dniAComprobar) == 1;
        }

        public bool ExisteDniRegistrado(string dniAComprobar) 
        {
            SociosTableAdapter sociosTableAdapter = new SociosTableAdapter();

            return sociosTableAdapter.ExisteDniRegistrado(dniAComprobar).Count == 1;
        }
    }
}
