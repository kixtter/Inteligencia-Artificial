using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class Iterative_Deepening
    {
        List<string> EstadosExplorados = new List<string>();
        List<Nodo> EstadosPorExplorar = new List<Nodo>();

        public Iterative_Deepening() { }

        public Nodo Profundidad_Iterativa(string[][] problema, string solucionCadena, out int exploredState)
        {
            Nodo Padre = new Nodo();
            Padre.estado = problema;
            Padre.Padre = null;
            Padre.Costo = 0;
            Padre.accion = MetodosGenerales.Action.nulo;
            Padre.estadoCadena = MetodosGenerales.ArrayToString(problema);

            int limite = 0;
            Nodo Solucion = null;
            Depth_limited Busqueda_Limitada = new Depth_limited();
            bool limite_Alcanzado = false;
            exploredState = 0;

            do
            {
                limite++;
                EstadosPorExplorar.Clear();

                Solucion = Busqueda_Limitada.BPL_Recursivo(Padre, EstadosPorExplorar, EstadosExplorados, solucionCadena, limite, out limite_Alcanzado, out exploredState);
            } while (Solucion == null);

            return Solucion;
        }
    }
}
