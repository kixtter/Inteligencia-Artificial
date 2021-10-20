using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class Depth_limited
    {
        List<string> EstadosExplorados = new List<string>();
        List<Nodo> EstadosPorExplorar = new List<Nodo>();

        public Depth_limited() { }

        public Nodo Busqueda_A_Profundidad_Limitada(string[][] problema, string solucionCadena, int limite, out bool limiteAlcanzado, out int exploredState)
        {
            Nodo Padre = new Nodo();
            Padre.estado = problema;
            Padre.Padre = null;
            Padre.Costo = 0;
            Padre.accion = MetodosGenerales.Action.nulo;
            Padre.estadoCadena = MetodosGenerales.ArrayToString(problema);
            limiteAlcanzado = false;
            exploredState = 0;
            EstadosExplorados.Add(MetodosGenerales.ArrayToString(Padre.estado));
            return BPL_Recursivo(Padre, EstadosPorExplorar, EstadosExplorados, solucionCadena, limite, out limiteAlcanzado, out exploredState);
        }

        public Nodo BPL_Recursivo(Nodo NextToExplore, List<Nodo> listEstadosPorExplorar, List<string> listEstadosExplorados, string solucionCadena, int limite, out bool limitReached, out int exploredState)
        {
            Nodo IsResult = null;
            limitReached = false;
            exploredState = 0;

            if (NextToExplore == null)
            {
                IsResult = null;
            }
            else if (NextToExplore.estadoCadena == solucionCadena)
            {
                exploredState = EstadosExplorados.Count;
                return NextToExplore;
            }
            else if (limite == 0)
            {
                listEstadosPorExplorar.Add(NextToExplore);
                limitReached = true;
                IsResult = null;
            }
            else
            {
                int fila = 0;
                int columna = 0;
                MetodosGenerales.CoordenadasDeNumero(NextToExplore.estado, "0", out fila, out columna);

                foreach (MetodosGenerales.Action accion in (MetodosGenerales.Action[])Enum.GetValues(typeof(MetodosGenerales.Action)))
                {
                    if (accion != MetodosGenerales.Action.nulo)
                    {
                        bool IsLimit = false;
                        Nodo Hijo = MetodosGenerales.Hijo(NextToExplore, null, listEstadosExplorados, fila, columna, accion);
                        if(Hijo != null)
                            EstadosExplorados.Add(MetodosGenerales.ArrayToString(Hijo.estado));
                        Nodo resultado = BPL_Recursivo(Hijo, listEstadosPorExplorar, listEstadosExplorados, solucionCadena, limite - 1, out IsLimit, out exploredState);

                        if (IsLimit)
                        {
                            limitReached = true;
                            continue;
                            //IsResult = null;
                            //break;
                        }
                        else if (resultado != null)
                        {
                            IsResult = resultado;
                            break;
                        }
                    }
                }
            }

            exploredState = EstadosExplorados.Count;
            return IsResult;
        }
    }
}
