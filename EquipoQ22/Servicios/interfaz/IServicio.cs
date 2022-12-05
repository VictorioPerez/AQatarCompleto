using EquipoQ22.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Servicios.interfaz
{
    internal interface IServicio
    {
        List<Personas> obtenerDato();
        bool guardarAlta(Equipos equipos);
    }
}
