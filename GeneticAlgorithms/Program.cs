using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            #region VARIABLES
            bool salir = false;
            bool LimiteGeneracional = false;
            bool Mutaciones = false;
            bool cambiarIndividuosReproduccion = false;
            bool cambiarIndividiosXGeneracion = false;
            int CantidadReinas = 0;
            int CantidadGeneraciones = 0;
            int CantidadMutaciones = 0;
            int CantidadIndividuosXGeneracion = 0;
            int CantidadIndividuosReproduccion = 0;
            string YesNo = "";

            #endregion

            do
            {
                CantidadReinas = 0;
                CantidadGeneraciones = 600;//Limite default de generaciones que se pueden generar
                CantidadMutaciones = 0;//Default de mutaciones
                CantidadIndividuosXGeneracion = 100;//Default de reproducciones
                CantidadIndividuosReproduccion = 50;//Default de 'K' estados elegidos para la siguiente generacion
                LimiteGeneracional = false;
                Mutaciones = false;
                cambiarIndividuosReproduccion = false;
                cambiarIndividiosXGeneracion = false;
                YesNo = "";

                while (CantidadReinas <= 0)
                {
                    Console.WriteLine("Ingresa la cantidad de reinas que tendrá el tablero: ");
                    try
                    {
                        CantidadReinas = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ingresar la cantidad de reinas, por favor ingrese un numero entero mayor a cero");
                    }
                }
                
                #region Pregunta sobre generaciones
                while (YesNo != "s" && YesNo != "n")
                {
                    Console.WriteLine("¿Desea cambiar el limite de generaciones? (600 generaciones permitidas)\nEscriba S(si) o N(no): ");
                    YesNo = Console.ReadLine().ToLower();

                    if (YesNo != "s" && YesNo != "n")
                        Console.WriteLine("Error, solo puede indicar S o N como respuesta");
                    else if (YesNo == "s")
                        LimiteGeneracional = true;
                }

                if (LimiteGeneracional)
                {
                    CantidadGeneraciones = 0;
                    while (CantidadGeneraciones <= 0)
                    {
                        Console.WriteLine("Ingresa la cantidad de generaciones que desea generar: ");
                        try
                        {
                            CantidadGeneraciones = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ingresar la cantidad de generaciones, por favor ingrese un numero entero mayor a cero");
                        }
                    }
                }
                #endregion

                YesNo = "";

                #region Cantidad de elegidos para reproduccion
                while (YesNo != "s" && YesNo != "n")
                {
                    Console.WriteLine("¿Desea cambiar la cantidad de individuos que se pueden reproducir? (50 individuos se pueden reproducir)\nEscriba S(si) o N(no): ");
                    YesNo = Console.ReadLine().ToLower();

                    if (YesNo != "s" && YesNo != "n")
                        Console.WriteLine("Error, solo puede indicar S o N como respuesta");
                    else if (YesNo == "s")
                        cambiarIndividuosReproduccion = true;
                }

                if (cambiarIndividuosReproduccion)
                {
                    CantidadIndividuosReproduccion = 0;
                    while (CantidadIndividuosReproduccion <= 1)
                    {
                        Console.WriteLine("¿Cuántos individuos será elegidos para Reproducirse?: ");
                        try
                        {
                            CantidadIndividuosReproduccion = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ingresar la cantidad de individuos, por favor ingrese un numero entero mayor a Uno");
                        }
                    }
                }
                #endregion

                YesNo = "";

                #region Cantidad de individuos por generacion
                while (YesNo != "s" && YesNo != "n")
                {
                    Console.WriteLine("¿Desea cambiar la cantidad de individuos que existiran por generación? (100 individuos por generacion)\nEscriba S(si) o N(no): ");
                    YesNo = Console.ReadLine().ToLower();

                    if (YesNo != "s" && YesNo != "n")
                        Console.WriteLine("Error, solo puede indicar S o N como respuesta");
                    else if (YesNo == "s")
                        cambiarIndividiosXGeneracion = true;
                }

                if (cambiarIndividiosXGeneracion)
                {
                    CantidadIndividuosXGeneracion = 0;
                    while (CantidadIndividuosXGeneracion <= 0)
                    {
                        Console.WriteLine("¿Cuántos individuos quiere que existan en cada generación?: ");
                        try
                        {
                            CantidadIndividuosXGeneracion = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ingresar la cantidad de individuos, por favor ingrese un numero entero mayor a cero");
                        }
                    }
                }
                #endregion

                YesNo = "";

                #region Pregunta sobre mutaciones
                while (YesNo != "s" && YesNo != "n")
                {
                    Console.WriteLine("¿Desea que cambiar la cantida de Mutaciones? (0 mutaciones permitidas)\nEscriba S(si) o N(no): ");
                    YesNo = Console.ReadLine().ToLower();

                    if (YesNo != "s" && YesNo != "n")
                        Console.WriteLine("Error, solo puede indicar S o N como respuesta");
                    else if (YesNo == "s")
                        Mutaciones = true;
                }

                if (Mutaciones)
                {
                    int limiteMutaciones = CantidadReinas / 8;//Solo puede mutarse 1/8
                    CantidadMutaciones = 0;
                    while (CantidadMutaciones <= 0)
                    {
                        Console.WriteLine("Mínimo 1 mutaciones y Máximo " + limiteMutaciones + "mutaciones");
                        Console.WriteLine("Ingresa la cantidad de mutaciones que desea agregar: ");
                        try
                        {
                            CantidadGeneraciones = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al ingresar la cantidad de mutaciones, por favor ingrese un numero entero mayor a cero");
                        }
                    }
                }
                #endregion

                YesNo = "";

                int generacionesGeneradas = 0;
                List<Individuo> listaPoblacion = MetodosAuxiliares.GeneraPrimeraPoblacion(CantidadReinas, CantidadIndividuosReproduccion);
                string cadena = new GA().AlgoritmoGenetico
                    (
                        listaPoblacion, 
                        CantidadGeneraciones, 
                        CantidadMutaciones, 
                        CantidadIndividuosXGeneracion, 
                        CantidadIndividuosReproduccion, 
                        out generacionesGeneradas
                    );

                if (generacionesGeneradas == CantidadGeneraciones)
                    Console.WriteLine(cadena + " " + generacionesGeneradas + " generaciones generadas");
                else
                    Console.WriteLine(cadena);

                Console.WriteLine("SI DESEA SALIR PRESIONE ESC, SI DESEA CONTINUAR PRESIONE ENTER");
                ConsoleKeyInfo key = Console.ReadKey();
                salir = key.Key == ConsoleKey.Escape ? true : false;
            } while (!salir);
        }
    }
}
