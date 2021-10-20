using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class Breadth_first
    {
        List<string> EstadosExplorados = new List<string>();
        List<Nodo> EstadosPorExplorar = new List<Nodo>();

        public Breadth_first() { }

        public Nodo Busqueda_A_Lo_Ancho(string[][] problema, string solucionCadena, out int exploredState, out int statesToExplore)
        {
            Nodo Padre = new Nodo();
            Padre.estado = problema;
            Padre.Padre = null;
            Padre.Costo = 0;
            Padre.accion = MetodosGenerales.Action.nulo;
            Padre.estadoCadena = MetodosGenerales.ArrayToString(problema);
            Nodo Solucion = null;
            Nodo NodoExpansion = null;
            Nodo Hijo = null;
            exploredState = 0;
            statesToExplore = 0;

            if (MetodosGenerales.ArrayToString(problema) == solucionCadena)
            {
                exploredState = EstadosExplorados.Count;
                statesToExplore = EstadosPorExplorar.Count;
                return Padre;
            }

            EstadosPorExplorar.Add(Padre);

            do
            {
                if (EstadosPorExplorar.Count == 0)
                {
                    exploredState = EstadosExplorados.Count;
                    statesToExplore = EstadosPorExplorar.Count;
                    return null;
                }

                NodoExpansion = EstadosPorExplorar.First();
                EstadosPorExplorar.Remove(NodoExpansion);

                EstadosExplorados.Add(MetodosGenerales.ArrayToString(NodoExpansion.estado));
                int fila = 0;
                int columna = 0;
                MetodosGenerales.CoordenadasDeNumero(NodoExpansion.estado, "0", out fila, out columna);

                foreach (MetodosGenerales.Action accion in (MetodosGenerales.Action[]) Enum.GetValues(typeof(MetodosGenerales.Action)))
                {
                    if (accion != MetodosGenerales.Action.nulo)
                    {
                        Hijo = MetodosGenerales.Hijo(NodoExpansion, EstadosPorExplorar, EstadosExplorados, fila, columna, accion);
                        if (Hijo == null)
                            continue;

                        if (Hijo.estadoCadena == solucionCadena)
                        {
                            Solucion = Hijo;
                            break;
                        }

                        EstadosPorExplorar.Add(Hijo);
                    }
                }


            } while (Solucion == null);

            exploredState = EstadosExplorados.Count;
            statesToExplore = EstadosPorExplorar.Count;
            return Solucion;
        }
    }
}
