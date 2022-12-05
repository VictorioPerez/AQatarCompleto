using EquipoQ22.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Datos.interfaz
{
    internal interface IEquipoDao
    {
        List<Personas> toGetNombre();
        bool crear(Equipos equipos);
    }
}
