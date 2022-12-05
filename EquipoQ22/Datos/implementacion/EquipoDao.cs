using EquipoQ22.Datos.interfaz;
using EquipoQ22.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EquipoQ22.Datos.implementacion
{
    internal class EquipoDao : IEquipoDao
    {
        public bool crear(Equipos equipos)
        {
            return helperDao.obtenerInstancia().cargarMaestroDetalle("SP_INSERTAR_EQUIPO","SP_INSERTAR_DETALLE_EQUIPO",equipos);
        }

        public List<Personas> toGetNombre()
        {
            List<Personas> lPersonas = new List<Personas>();
            DataTable dt = helperDao.obtenerInstancia().combo("SP_CONSULTAR_PERSONAS");
            foreach (DataRow dr in dt.Rows)
            {
                Personas persona = new Personas();
                persona.IDPersona = Convert.ToInt32(dr["id_persona"]);
                persona.nombreCompleto = Convert.ToString(dr["nombre_completo"]);
                persona.clase = Convert.ToInt32(dr["clase"]);
                lPersonas.Add(persona);
            }
            return lPersonas;
        }
    }
}
