using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Modelo
{
    class Traduccion
    {
        private int idTraduccion;
        private string cadena;
        private string tipo;

        public Traduccion(int idTraduccion, string cadena, string tipo)
        {
            this.IdTraduccion = idTraduccion;
            this.Cadena = cadena;
            this.Tipo = tipo;
        }

        public int IdTraduccion { get => idTraduccion; set => idTraduccion = value; }
        public string Cadena { get => cadena; set => cadena = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
