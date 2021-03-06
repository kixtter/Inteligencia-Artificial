using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Individuo
    {
        public Individuo() { }

        public string estado { get; set; }
        public int attacking { get; set; }
    }

    class MetodosAuxiliares
    {
        public static List<Individuo> GeneraPrimeraPoblacion(int CantidadReinas, int CantidadIndividuosParaReproduccion)
        {
            Random rnd = new Random();
            List<Individuo> lista = new List<Individuo>();
            string[][] plantilla;

            while (lista.Count < CantidadIndividuosParaReproduccion)
            {
                string cadena = "";
                for (int i = 0; i < CantidadReinas; i++)
                {
                    cadena += rnd.Next(CantidadReinas) + "-";
                }

                cadena = cadena.Substring(0, cadena.Length - 1);
                plantilla = GeneraPlantilla(CantidadReinas);
                lista.Add(new Individuo() { estado = cadena, attacking = CalculaCantidadAtaques(plantilla, cadena, CantidadReinas) });
            }

            return lista;
        }

        public static int CalculaCantidadAtaques(string[][] plantilla,  string estado, int cantidadReinas)
        {
            string[] puzzle = estado.Split('-');
            int attack = 0;

            for (int i = 0; i < puzzle.Length; i++)
            {
                int x = Convert.ToInt32(puzzle[i]);
                plantilla[x][i] = "1";
            }

            for (int i = 0; i < plantilla.Length; i++)
            {
                for (int j = 0; j < plantilla[i].Length; j++)
                {
                    if (plantilla[i][j] == "1")
                    {
                        int ejeX = i;
                        int ejeY = j;

                        //Busca en la fila si hay reinas
                        for (int x = j + 1; x < plantilla[i].Length; x++)
                        {
                            if (plantilla[i][x] == "1")
                                attack++;
                        }

                        //Busca en la diagonal izquierda-abajo
                        for (int a = ejeX - 1; a >= 0; a--)
                        {
                            ejeY++;
                            if (ejeY >= plantilla.Length)
                                break;

                            if (plantilla[a][ejeY] == "1")
                                attack++;
                        }

                        ejeX = i;
                        ejeY = j;
                        //Busca en la diagonal derecha-abajo
                        for (int a = ejeX + 1; a < plantilla[i].Length; a++)
                        {
                            ejeY++;
                            if (ejeY >= plantilla.Length)
                                break;

                            if (plantilla[a][ejeY] == "1")
                                attack++;
                        }
                    }
                }
            }

            return attack;
        }

        public static string[][] GeneraPlantilla(int cantidadReinas)
        {
            string[] fila = new string[cantidadReinas];
            string[][] plantilla = new string[cantidadReinas][];

            for (int i = 0; i< fila.Length; i++)
            {
                fila[i] = "0";
            }

            for (int i = 0; i < plantilla.Length; i++)
            {
                plantilla[i] = (string[])fila.Clone();
            }

            return plantilla;
        }

        public static string Reproduccion(Individuo a, Individuo b, int CantidadReinas)
        {
            Random rnd = new Random();
            int split = rnd.Next(1, CantidadReinas);
            string[] elemento1 = a.estado.Split('-');
            string[] elemento2 = b.estado.Split('-');
            string concatenacion = "";

            for (int i = 0; i < split; i++)
                concatenacion += elemento1[i] + "-";

            for (int i = split; i < elemento2.Length; i++)
                concatenacion += elemento2[i] + "-";

            return concatenacion.Substring(0, concatenacion.Length - 1);
        }

        public static string Mutacion(string estado, int cantidadMutaciones, int cantidadReinas)
        {
            Random rnd = new Random();
            string[] puzzle = estado.Split('-');
            string resultado = "";

            for (int i = 0; i < cantidadMutaciones; i++)
                puzzle[rnd.Next(cantidadReinas)] = rnd.Next(cantidadReinas).ToString();

            for (int i = 0; i < puzzle.Length; i++)
                resultado += puzzle[i] + "-";

            return resultado.Substring(0, resultado.Length - 1);
        }
    }
}
