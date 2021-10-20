using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blind_Search
{
    class Nodo
    {
        public string[][] estado { get; set; }
        public Nodo Padre { get; set; }
        public int Costo { get; set; }
        public string estadoCadena { get; set; }
        public MetodosGenerales.Action accion;
        public int Distance_Manhattan { get; set; }

        public Nodo() { }
    }

    class UbicacionNumeros
    {
        public string numero { get; set; }
        public int fila { get; set; }
        public int columna { get; set; }

        public UbicacionNumeros() { }
    }
}
