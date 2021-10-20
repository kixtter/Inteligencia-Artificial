using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class MetodosGenerales
    {
        /// <summary>
        /// Lista de acciones que pueden hacerse
        /// </summary>
        public enum Action
        {
            nulo,
            izquierda,
            arriba,
            derecha,
            abajo
        }

        /// <summary>
        /// Crea una matriz con los valores que abarquen la dimensión del puzzle de forma automatica
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static string[][] DimensionToArray(int dimension)
        {
            //Variables
            int tamaño = dimension * dimension;
            string[] numeros = new string[tamaño];
            Random random = new Random();
            string[][] resultado = new string[dimension][];

            //Llena un array con todos los valores del 0 a (dimension * dimension)
            for (int i = 0; i < tamaño; i++)
                numeros[i] = i.ToString();

            while (tamaño > 1)
            {
                int posicion = random.Next(tamaño--);
                string aux = numeros[tamaño];
                numeros[tamaño] = numeros[posicion];
                numeros[posicion] = aux;
            }

            int contador = 0;
            for (int i = 0; i < dimension; i++)
            {
                string[] columna = new string[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    columna[j] = numeros[contador];
                    contador++;
                }
                resultado[i] = columna;
            }

            return resultado;
        }

        /// <summary>
        /// Convierte la matriz en una cadena
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ArrayToString(string[][] array)
        {
            string concatena = "";
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    concatena += array[i][j] + "-";
                }
            }

            return concatena.Substring(0, concatena.Length - 1);
        }

        /// <summary>
        /// Convierte la una cadena en matriz
        /// </summary>
        /// <param name="problemaPuzzle"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static string[][] StringToMatriz(string problemaPuzzle, int dimension)
        {
            string[] problema = problemaPuzzle.Split('-');
            string[][] matriz = new string[dimension][];
            string[] fila;
            int acum = 0;

            for (int i = 0; i < matriz.Length; i++)
            {
                fila = new string[dimension];
                for (int j = 0; j < fila.Length; j++)
                {
                    fila[j] = problema[acum];
                    acum++;
                }
                matriz[i] = fila;
            }

            return matriz;
        }

        /// <summary>
        /// Explora la lista de estados explorados para ver si el estado generado es nuevo o no
        /// </summary>
        /// <param name="explorados"></param>
        /// <param name="puzzle"></param>
        /// <returns>true:Existe/false:No existe</returns>
        public static bool ExploraEstadosExplorados(List<string> explorados, string puzzle)
        {
            if (explorados.Exists(elemento => elemento == puzzle))
                return true;
            return false;
        }

        /// <summary>
        /// Explora la lista de estados por explorar para ver si no se esta repitiendo el estado
        /// </summary>
        /// <param name="PorExplorar"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public static bool ExploraEstadosPorExplorar(List<Nodo> PorExplorar, string estado)
        {
            if (PorExplorar == null)
                return false;
            else if (PorExplorar.Exists(elemento => elemento.estadoCadena == estado))
                return true;
            return false;
        }

        /// <summary>
        /// Crea la lista de acciones que se llevaron a cabo para poder llegar al resultado
        /// </summary>
        /// <param name="nodoFinal"></param>
        /// <returns></returns>
        public static string AccionesRealizadas(Nodo nodoFinal)
        {
            string ListaAcciones = "";
            List<string> TotalAcciones = new List<string>();
            while (nodoFinal != null)
            {
                TotalAcciones.Add(nodoFinal.accion.ToString());
                nodoFinal = nodoFinal.Padre;
            }

            TotalAcciones.Reverse();
            foreach (string accion in TotalAcciones)
            {
                ListaAcciones += accion + "\n";
            }

            return ListaAcciones;
        }

        /// <summary>
        /// Obtiene en que coordenadas ser encuentra el numero que se indique
        /// </summary>
        /// <param name="problema"></param>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        public static void CoordenadasDeNumero(string[][] problema, string numero, out int fila, out int columna)
        {
            fila = columna = 0;
            for (int i = 0; i < problema.Length; i++)
            {
                for (int j = 0; j < problema[i].Length; j++)
                {
                    if (problema[i][j] == numero)
                    {
                        fila = i;
                        columna = j;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Calcula la distancia total para que cada numero este en su lugar
        /// </summary>
        /// <param name="problema"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static int DistanciaManhattan(string[][] problema, List<UbicacionNumeros> lista)
        {
            int total = 0;

            for (int i = 0; i < problema.Length; i++)
            {
                for (int j = 0; j < problema[i].Length; j++)
                {
                    int fila = 0;
                    int columna = 0;
                    if (problema[i][j] != "0")
                    {
                        CoordenadasDeNumero(problema, problema[i][j], out fila, out columna);
                        UbicacionNumeros elemento = lista.Find(t => t.numero == problema[i][j]);
                        total += Math.Abs(fila - elemento.fila) + Math.Abs(columna - elemento.columna);
                    }
                }
            }

            return total;
        }

        /// <summary>
        /// Crea el nodo hijo
        /// </summary>
        /// <param name="Padre"></param>
        /// <param name="EstadosPorExplorar"></param>
        /// <param name="EstadosExplorados"></param>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        /// <param name="accion"></param>
        /// <returns></returns>
        public static Nodo Hijo(Nodo Padre,List<Nodo> EstadosPorExplorar, List<string> EstadosExplorados, int fila, int columna, MetodosGenerales.Action accion)
        {
            Nodo Hijo = new Nodo();
            int nuevaFila = 0;
            int nuevaColumna = 0;
            string[][] estadoHijo = StringToMatriz(Padre.estadoCadena, Padre.estado.Length);

            try
            {
                switch (accion)
                {
                    case Action.izquierda:
                        {
                            nuevaColumna = columna - 1;
                            estadoHijo[fila][columna] = estadoHijo[fila][nuevaColumna];
                            estadoHijo[fila][nuevaColumna] = "0";
                        }
                        break;
                    case Action.arriba:
                        {
                            nuevaFila = fila - 1;
                            estadoHijo[fila][columna] = estadoHijo[nuevaFila][columna];
                            estadoHijo[nuevaFila][columna] = "0"; 
                        }
                        break;
                    case Action.derecha:
                        {
                            nuevaColumna = columna + 1;
                            estadoHijo[fila][columna] = estadoHijo[fila][nuevaColumna];
                            estadoHijo[fila][nuevaColumna] = "0";
                        }
                        break;
                    case Action.abajo:
                        {
                            nuevaFila = fila + 1;
                            estadoHijo[fila][columna] = estadoHijo[nuevaFila][columna];
                            estadoHijo[nuevaFila][columna] = "0"; 
                        }
                        break;
                }
            }catch(Exception ex)
            {
                return null;
            }

            string estadoCadena = ArrayToString(estadoHijo);

            if (ExploraEstadosExplorados(EstadosExplorados, estadoCadena) || ExploraEstadosPorExplorar(EstadosPorExplorar, estadoCadena))
                return null;

            Hijo.estado = estadoHijo;
            Hijo.Padre = Padre;
            Hijo.Costo = Padre.Costo + 1;
            Hijo.estadoCadena = estadoCadena;
            Hijo.accion = accion;

            return Hijo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCadena">true:Indica que es el problema/false:Indica que es la solucion</param>
        /// <returns></returns>
        public static string[][] ValidaCadena(bool TipoCadena, int dimension)
        {
            int IsInt = -1;
            int tamañoSinRepetidos = 0;
            bool IsEntero = true;
            bool dentroLimite = true;
            bool IsStringReady = false;
            string strTipoCadena = TipoCadena ? "inicial" : "solución";
            string cadena = "";
            string[] elementosPuzzle = new string[0];

            while (!IsStringReady)
            {
                Console.WriteLine($"Teclea el estado {strTipoCadena.ToUpper()} del puzzle(Se recomienda separar todo con guiones'-')):");
                Console.Write("-----------> ");
                cadena = Console.ReadLine();
                elementosPuzzle = cadena.Split('-');

                if (elementosPuzzle.Length != (dimension * dimension))
                {
                    Console.WriteLine("La cantidad de numeros con coincide con la dimension del puzzle: " + dimension.ToString() + "x" + dimension.ToString());
                    continue;
                }

                for (int i = 0; i < elementosPuzzle.Length; i++)
                {
                    if (!int.TryParse(elementosPuzzle[i], out IsInt))
                    {
                        IsEntero = false;
                        break;
                    }
                    if (!(IsInt < (dimension * dimension)))
                    {
                        dentroLimite = false;
                        break;
                    }
                }

                if (!IsEntero)
                {
                    Console.WriteLine("Uno de los elementos del puzzle no es un número.");
                    continue;
                }
                if (!dentroLimite)
                {
                    Console.WriteLine("Uno de los elementos del puzzle es mayor a " + ((dimension * dimension) - 1) );
                    continue;
                }

                Array.Sort(elementosPuzzle);
                tamañoSinRepetidos = elementosPuzzle.Distinct().Count();

                if (tamañoSinRepetidos < (dimension * dimension))
                {
                    Console.WriteLine("Hay números repetidos dentro del puzzle");
                    continue;
                }

                IsStringReady = true;
            }

            return StringToMatriz(cadena, dimension);
        }
    }
}
