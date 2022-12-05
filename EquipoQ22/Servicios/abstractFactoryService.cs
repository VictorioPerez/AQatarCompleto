using EquipoQ22.Servicios.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Servicios
{
    internal abstract class abstractFactoryService
    {
        public abstract IServicio crearServicio();
    }
}
