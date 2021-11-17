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
                int cantidadReinas,
                int cantidadGeneraciones,
                int cantidadMutaciones,
                int cantidadIndividuosXGeneracion,
                int cantidadIndividuosReproduccion,
                out int generacionesGeneradas
            )
        {
            string resultado = "";
            List<Individuo> NextGeneration;
            Random rnd = new Random();
            int generaciones = 0;
            generacionesGeneradas = 0;
            string[][] plantilla;

            do
            {
                NextGeneration = new List<Individuo>();

                if (generaciones == cantidadGeneraciones)
                {
                    generacionesGeneradas = generaciones;
                    return "Alcanzo el limite de generaciones";
                }

                for (int i = 0; i < cantidadIndividuosXGeneracion; i++)
                {
                    Individuo a = listaPoblacion[rnd.Next(listaPoblacion.Count)];
                    Individuo b = listaPoblacion[rnd.Next(listaPoblacion.Count)];

                    string hijo = MetodosAuxiliares.Reproduccion(a, b, cantidadReinas);

                    if (cantidadMutaciones > 0)
                        hijo = MetodosAuxiliares.Mutacion(hijo, cantidadMutaciones, cantidadReinas);

                    plantilla = MetodosAuxiliares.GeneraPlantilla(cantidadReinas);

                    NextGeneration.Add(new Individuo()
                    {
                        estado = hijo,
                        attacking = MetodosAuxiliares.CalculaCantidadAtaques(plantilla, hijo, cantidadReinas)
                    });
                }

                listaPoblacion.Clear();
                listaPoblacion = NextGeneration.OrderBy(elemento => elemento.attacking).ToList();
                NextGeneration.Clear();

                Individuo primeroLista = listaPoblacion.First();
                if (primeroLista.attacking == 0)
                {
                    resultado = primeroLista.estado;
                    generacionesGeneradas = generaciones + 1;
                }

                listaPoblacion = listaPoblacion.GetRange(0, cantidadIndividuosReproduccion);
                generaciones++;

            } while (resultado == "");

            return resultado;
        }
    }
}
