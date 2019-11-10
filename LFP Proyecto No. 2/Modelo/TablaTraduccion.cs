using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Modelo
{
    class TablaTraduccion
    {
        private int idTablaTraduccion;
        private string lexema;
        private string valor;
        private string tipo;

        public TablaTraduccion(int idTablaTraduccion, string lexema, string valor, string tipo)
        {
            this.IdTablaTraduccion = idTablaTraduccion;
            this.Lexema = lexema;
            this.Valor = valor;
            this.Tipo = tipo;
        }

        public int IdTablaTraduccion { get => idTablaTraduccion; set => idTablaTraduccion = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
