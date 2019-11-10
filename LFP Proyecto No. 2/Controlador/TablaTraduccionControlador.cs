using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Controlador
{
    class TablaTraduccionControlador
    {
        private readonly static TablaTraduccionControlador instancia = new TablaTraduccionControlador();
        private ArrayList arrayListTablaSimbolos = new ArrayList();
        private int idTablaTraduccion = 1;
        private TablaTraduccionControlador()
        {
        }

        public static TablaTraduccionControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void agregar(string lexema, string valor, string tipo)
        {
            TablaTraduccion t = new TablaTraduccion(idTablaTraduccion, lexema, valor, tipo);
            arrayListTablaSimbolos.Add(t);
            idTablaTraduccion++;
        }


        public ArrayList getTabla()
        {
            return this.arrayListTablaSimbolos;
        }
    }
}
