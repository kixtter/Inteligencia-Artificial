using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            #region VARIABLES
            string[][] puzzle;
            int dimension = 0;
            int algoritmo = 0;
            int limite = 0;
            int exploredStates = 0;
            int statesToExplore = 0;
            string manualAutomatica = "";
            string solucionCadena = "";
            bool salir = false;
            bool limiteAlcancado = false;
            DateTime dtInicio = new DateTime();
            DateTime dtFinal = new DateTime();
            Nodo solucion = null;

            string mensaje = "Teclea el número que corresponda a la dimensión de puzzle:\n" +
                    "1.- 2x2\n" +
                    "2.- 3x3\n" +
                    "3.- 4x4\n" +
                    "4.- 5x5";
            string mensajePuzzle = "Teclea S(si) si quieres que el puzzle se genere de forma automática y N(no) para ingresar el puzzle de forma manual.";
            string mensajeAlgoritmo = "Teclea el número del algoritmo que desees probar:\n" + 
                    "1.- A lo ancho\n" + 
                    "2.- A profundidad\n" +
                    "3.- A profundidad limitada\n" +
                    "4.- A profundidad iterativa\n" + 
                    "5.- A estrella";

            #endregion

            do
            {
                puzzle = new string[0][];
                dimension = 0;
                algoritmo = 0;
                manualAutomatica = "";
                solucionCadena = "";
                exploredStates = 0;
                statesToExplore = 0;
                limite = 0;
                limiteAlcancado = false;

                while (dimension < 2 || dimension > 5)
                {
                    Console.WriteLine(mensaje);
                    Console.Write("-----------> ");

                    try
                    {
                        dimension = Convert.ToInt32(Console.ReadLine()) + 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ingresar la dimensión del puzzle. Ingrese un valor dentro de los disponibles.\n\n");
                    }
                }

                while (manualAutomatica != "s" && manualAutomatica != "n")
                {
                    Console.WriteLine(mensajePuzzle);
                    Console.Write("-----------> ");
                    manualAutomatica = Console.ReadLine().ToLower();

                    if (manualAutomatica != "s" && manualAutomatica != "n")
                        Console.WriteLine("Error escoger la generación del puzzle. Ingrese un opcion dentro de los disponibles.\n\n");
                }

                switch (manualAutomatica)
                {
                    case "s":
                        {
                            puzzle = MetodosGenerales.DimensionToArray(dimension);
                        }
                        break;
                    case "n":
                        {
                            puzzle = MetodosGenerales.ValidaCadena(true, dimension);
                        }
                        break;
                }

                //Sirve para pedir la cadena solución
                solucionCadena = MetodosGenerales.ArrayToString(MetodosGenerales.ValidaCadena(false, dimension));

                while (algoritmo == 0)
                {
                    Console.WriteLine(mensajeAlgoritmo);
                    Console.Write("-----------> ");
                    if(!int.TryParse(Console.ReadLine().ToLower(), out algoritmo))
                    {
                        Console.WriteLine("Debe escoger de entre las opciones disponibles");
                    }
                    else if (algoritmo <= 0 && algoritmo > 5)
                    {
                        Console.WriteLine("Debe escoger de entre las opciones disponibles");
                    }
                }

                dtInicio = DateTime.Now;
                switch (algoritmo)
                {
                    case 1:
                        {
                            solucion = new Breadth_first().Busqueda_A_Lo_Ancho(puzzle, solucionCadena, out exploredStates, out statesToExplore);
                        }
                        break;
                    case 2:
                        {
                            solucion = new Depth_first().Busqueda_A_Profundidad(puzzle, solucionCadena, out exploredStates, out statesToExplore);
                        }
                        break;
                    case 3:
                        {
                            while (limite == 0)
                            {
                                Console.WriteLine("Indica el limite de búsqueda");
                                Console.Write("-----------> ");
                                if (!int.TryParse(Console.ReadLine().ToLower(), out limite))
                                {
                                    Console.WriteLine("El limite debe indicarlo con numeros");
                                }
                                else if (limite == 0)
                                {
                                    Console.WriteLine("El limite debe ser diferente de cero");
                                }
                            }
                            solucion = new Depth_limited().Busqueda_A_Profundidad_Limitada(puzzle, solucionCadena, limite, out limiteAlcancado, out exploredStates);
                            if (limiteAlcancado)
                            {
                                Console.WriteLine("******Limite alcanzado******");
                            }
                        }
                        break;
                    case 4:
                        {
                            solucion = new Iterative_Deepening().Profundidad_Iterativa(puzzle, solucionCadena, out exploredStates);
                        }
                        break;
                    case 5:
                        {
                            solucion = new A_Star().A_Estrella(puzzle, solucionCadena, out exploredStates, out statesToExplore);
                        }
                        break;
                }
                dtFinal = DateTime.Now;

                if (solucion != null)
                {
                    Console.WriteLine("Problema: " + MetodosGenerales.ArrayToString(puzzle));
                    Console.WriteLine("Solucion: " + solucionCadena);
                    Console.WriteLine("Cantidad de movimientos: " + solucion.Costo);
                    Console.WriteLine("Acciones realizadas: " + MetodosGenerales.AccionesRealizadas(solucion));
                    Console.WriteLine("Tiempo transcurrido: " + dtFinal.Subtract(dtInicio));
                    Console.WriteLine("Nodos faltantes por expandir: " + statesToExplore);
                    Console.WriteLine("Nodo explorados: " + exploredStates);
                }
                else
                {
                    Console.WriteLine("No fue posible encontrar el resultado");
                    Console.WriteLine("Nodos faltantes por expandir: " + statesToExplore);
                    Console.WriteLine("Nodo explorados: " + exploredStates);
                }

                Console.WriteLine("SI DESEA SALIR PRESIONE ESC, SI DESEA CONTINUA PRESIONE ENTER");
                ConsoleKeyInfo key  = Console.ReadKey();
                salir = key.Key == ConsoleKey.Escape ? true : false;

            } while (!salir);
        }
    }
}
