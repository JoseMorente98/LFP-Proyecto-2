using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Controlador
{
    class ControladorTraduccion
    {
        private readonly static ControladorTraduccion instancia = new ControladorTraduccion();
        private ArrayList arrayListTraduccion = new ArrayList();
        private int idTraduccion = 1;

        private ControladorTraduccion()
        {
        }

        public static ControladorTraduccion Instancia
        {
            get
            {
                return instancia;
            }
        }

        public ArrayList ArrayListTraduccion { get => arrayListTraduccion; set => arrayListTraduccion = value; }

        /**
         * AGREGAR TRADUCCION
         */
        public void agregar(string texto, string tipo)
        {
            Traduccion traduccion = new Traduccion(idTraduccion, texto, tipo);
            ArrayListTraduccion.Add(traduccion);
            idTraduccion++;
        }

        /**
         * LIMPIAR TRADUCCION
         */
        public void clearArrayListTraduccion()
        {
            idTraduccion = 1;
            ArrayListTraduccion.Clear();
        }
    }
}
