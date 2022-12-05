using EquipoQ22.Datos.implementacion;
using EquipoQ22.Datos.interfaz;
using EquipoQ22.Dominio;
using EquipoQ22.Servicios.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Servicios.implementacion
{
    internal class ServicioEquipo : IServicio
    {
        private IEquipoDao dao;

        public ServicioEquipo()
        {
            dao = new EquipoDao();
        }
        public bool guardarAlta(Equipos equipos)
        {
            return dao.crear(equipos);
        }

        public List<Personas> obtenerDato()
        {
            return dao.toGetNombre();
        }
    }
}
