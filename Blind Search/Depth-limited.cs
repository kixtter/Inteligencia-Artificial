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

        public Nodo Busqueda_A_Profundidad_Limitada(string[][] problema, string solucionCadena, int limite, out bool limiteAlcanzado)
        {
            Nodo Padre = new Nodo();
            Padre.estado = problema;
            Padre.Padre = null;
            Padre.Costo = 0;
            Padre.accion = MetodosGenerales.Action.nulo;
            Padre.estadoCadena = MetodosGenerales.ArrayToString(problema);
            limiteAlcanzado = false;
            return BPL_Recursivo(Padre, EstadosPorExplorar, EstadosExplorados, solucionCadena, limite, out limiteAlcanzado);
        }

        public Nodo BPL_Recursivo(Nodo NextToExplore, List<Nodo> listEstadosPorExplorar, List<string> listEstadosExplorados, string solucionCadena, int limite, out bool limitReached)
        {
            Nodo IsResult = null;
            limitReached = false;

            if (NextToExplore == null)
                IsResult = null;
            else if (NextToExplore.estadoCadena == solucionCadena)
                return NextToExplore;
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
                        Nodo resultado = BPL_Recursivo(Hijo, listEstadosPorExplorar, listEstadosExplorados, solucionCadena, limite - 1, out IsLimit);

                        if (IsLimit)
                        {
                            limitReached = true;
                            IsResult = null;
                            break;
                        }
                        else if (resultado != null)
                        {
                            IsResult = resultado;
                            break;
                        }
                    }
                }
            }

            return IsResult;
        }
    }
}
