using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Dominio
{
    internal class Jugadores
    {
        public Personas persona { get; set; }
        public string posicion { get; set; }
        public int nroCamiseta { get; set; }

        public Jugadores() { }

        Jugadores(Personas persona, string posicion, int nroCamiseta)
        {
            this.persona = persona;
            this.posicion = posicion;
            this.nroCamiseta = nroCamiseta;
        }
    }
}
