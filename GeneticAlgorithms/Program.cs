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
            int CantidadReinas = 0;
            int CantidadGeneraciones = 0;
            int CantidadMutaciones = 0;
            int CantidadIndividuosXGeneracion = 0;
            int CantidadIndividuosReproduccion = 0;
            string YesNo = "";
            string[][] plantilla;

            #endregion

            do
            {
                CantidadReinas = 0;
                CantidadGeneraciones = 0;
                CantidadMutaciones = 0;
                CantidadIndividuosXGeneracion = 0;
                CantidadIndividuosReproduccion = 0;
                LimiteGeneracional = false;
                Mutaciones = false;
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

                //Creación de la plantilla que contiene solo ceros(0)
                plantilla = MetodosAuxiliares.GeneraPlantilla(CantidadReinas);

                #region Pregunta sobre generaciones
                while (YesNo != "s" && YesNo != "n")
                {
                    Console.WriteLine("¿Desea que haya un limite de generaciones?\nEscriba S(si) o N(no): ");
                    YesNo = Console.ReadLine().ToLower();

                    if (YesNo != "s" && YesNo != "n")
                        Console.WriteLine("Error, solo puede indicar S o N como respuesta");
                    else if (YesNo == "s")
                        LimiteGeneracional = true;
                }

                if (LimiteGeneracional)
                {
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

                #region Pregunta sobre mutaciones
                while (YesNo != "s" && YesNo != "n")
                {
                    Console.WriteLine("¿Desea que haya Mutaciones?\nEscriba S(si) o N(no): ");
                    YesNo = Console.ReadLine().ToLower();

                    if (YesNo != "s" && YesNo != "n")
                        Console.WriteLine("Error, solo puede indicar S o N como respuesta");
                    else if (YesNo == "s")
                        Mutaciones = true;
                }

                if (Mutaciones)
                {
                    int limiteMutaciones = CantidadReinas / 8;//Solo puede mutarse 1/8
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



                Console.WriteLine("SI DESEA SALIR PRESIONE ESC, SI DESEA CONTINUAR PRESIONE ENTER");
                ConsoleKeyInfo key = Console.ReadKey();
                salir = key.Key == ConsoleKey.Escape ? true : false;
            } while (!salir);
        }
    }
}
