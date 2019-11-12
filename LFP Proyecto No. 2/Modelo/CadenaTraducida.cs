using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Modelo
{
    class CadenaTraducida
    {
        private int id;
        private String cadena;
        private string tipo;

        public int Id { get => id; set => id = value; }
        public string Cadena { get => cadena; set => cadena = value; }
        public string Tipo { get => tipo; set => tipo = value; }

        public CadenaTraducida(int id, string cadena, string tipo)
        {
            this.Id = id;
            this.Cadena = cadena;
            this.Tipo = tipo;
        }

        public CadenaTraducida()
        {
        }
    }
}
