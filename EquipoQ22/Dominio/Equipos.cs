using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Dominio
{
    internal class Equipos
    {
        public string pais { get; set; }
        public string directorTecnico { get; set; }

        //lista jugadores
        public List<Jugadores> lJugador { get; set; }

        public Equipos()
        {
            lJugador = new List<Jugadores>();
        }

        public void agregar(Jugadores jugadorNuevo)
        {
            lJugador.Add(jugadorNuevo);
        }
        public void quitar(int posicion)
        {
            lJugador.RemoveAt(posicion);
        }
    }
}
