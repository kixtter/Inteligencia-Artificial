using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class GA
    {
        public GA() { }

        public string AlgoritmoGenetico
            (
                List<Individuo> listaPoblacion,
                string[][] plantilla,
                int cantidadGeneraciones = 600,//Limite default de generaciones que se pueden generar
                int cantidadMutaciones = 0,//Default de mutaciones
                int cantidadIndividuosXGeneracion = 100,//Default de reproducciones
                int cantidadIndividuosReproduccion = 50//Default de 'K' estados elegidos para la siguiente generacion
            )
        {
            string resultado = "";
            List<Individuo> NextGeneration;
            Random rnd = new Random();
            int generaciones = 0;

            do
            {
                NextGeneration = new List<Individuo>();

                if (generaciones == cantidadGeneraciones)
                    return "Alcanzo el limite de generaciones";

                for (int i = 0; i <= cantidadIndividuosXGeneracion; i++)
                {
                    Individuo a = listaPoblacion[rnd.Next(listaPoblacion.Count)];
                    Individuo b = listaPoblacion[rnd.Next(listaPoblacion.Count)];

                    string hijo = MetodosAuxiliares.Reproduccion(a, b);

                    if (cantidadMutaciones > 0)
                        MetodosAuxiliares.Mutacion(hijo, cantidadMutaciones);

                    NextGeneration.Add(new Individuo()
                    {
                        estado = hijo,
                        attacking = MetodosAuxiliares.CalculaCantidadAtaques((string[][])plantilla.Clone(), hijo)
                    });
                }

                listaPoblacion.Clear();
                listaPoblacion = (List<Individuo>)NextGeneration.OrderBy(elemento => elemento.attacking);
                NextGeneration.Clear();

                Individuo primeroLista = listaPoblacion.First();
                if (primeroLista.attacking == 0)
                    resultado = primeroLista.estado;

                listaPoblacion = listaPoblacion.GetRange(0, cantidadIndividuosReproduccion);
                generaciones++;

            } while (resultado == "");

            return resultado;
        }
    }
}
