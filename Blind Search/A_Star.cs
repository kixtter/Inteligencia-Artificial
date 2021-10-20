using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class A_Star
    {
        List<string> EstadosExplorados = new List<string>();
        List<Nodo> EstadosPorExplorar = new List<Nodo>();

        public A_Star() { }

        public Nodo A_Estrella(string [][] problema, string solucionCadena)
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

            //guarda en una lista la ubicación de cada numero del puzzle
            List<UbicacionNumeros> listUbicacionNum = new List<UbicacionNumeros>();
            string[][] strSolucion = MetodosGenerales.StringToMatriz(solucionCadena, problema.Length);
            for (int i = 0; i < strSolucion.Length; i++)
            {
                for (int j = 0; j < strSolucion[i].Length; j++)
                {
                    UbicacionNumeros ubi = new UbicacionNumeros();
                    int fila = 0;
                    int columna = 0;

                    if (strSolucion[i][j] != "0")
                    {
                        MetodosGenerales.CoordenadasDeNumero(strSolucion, strSolucion[i][j], out fila, out columna);
                        ubi.numero = strSolucion[i][j];
                        ubi.fila = fila;
                        ubi.columna = columna;

                        listUbicacionNum.Add(ubi);
                    }
                }
            }

            Padre.Distance_Manhattan = Padre.Costo + MetodosGenerales.DistanciaManhattan(problema, listUbicacionNum);
            EstadosPorExplorar.Add(Padre);

            do
            {
                if (EstadosPorExplorar.Count == 0)
                {
                    return null;
                }

                EstadosPorExplorar.OrderBy(t => t.Distance_Manhattan);
                NodoExpansion = EstadosPorExplorar.First();
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

                        Hijo.Distance_Manhattan = Hijo.Padre.Costo + MetodosGenerales.DistanciaManhattan(Hijo.estado, listUbicacionNum);
                        EstadosPorExplorar.Add(Hijo);
                    }
                }

            } while (Solucion == null);

            return Solucion;
        }
    }
}
