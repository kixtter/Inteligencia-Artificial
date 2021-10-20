using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class Depth_first
    {
        List<string> EstadosExplorados = new List<string>();
        List<Nodo> EstadosPorExplorar = new List<Nodo>();

        public Depth_first() { }

        public Nodo Busqueda_A_Profundidad(string[][] problema, string solucionCadena)
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

            if (MetodosGenerales.ArrayToString(problema) == solucionCadena)
            {
                return Padre;
            }

            EstadosPorExplorar.Add(Padre);

            do
            {
                if (EstadosPorExplorar.Count == 0)
                    return null;

                NodoExpansion = EstadosPorExplorar.Last();
                EstadosPorExplorar.Remove(NodoExpansion);

                EstadosExplorados.Add(MetodosGenerales.ArrayToString(NodoExpansion.estado));
                int fila = 0;
                int columna = 0;
                MetodosGenerales.CoordenadasDeNumero(NodoExpansion.estado, "0", out fila, out columna);

                foreach (MetodosGenerales.Action accion in (MetodosGenerales.Action[])Enum.GetValues(typeof(MetodosGenerales.Action)))
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


            return Solucion;
        }
    }
}
