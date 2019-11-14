
using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFP_Proyecto_No._2.Controlador
{
    class ControladorSintactico
    {
        private readonly static ControladorSintactico instancia = new ControladorSintactico();
        private ArrayList arrayListSintactico = new ArrayList();
        private int idSintactico = 1;

        private ControladorSintactico()
        {
        }

        public static ControladorSintactico Instancia
        {
            get
            {
                return instancia;
            }
        }

        /**
         * AGREGAR ERROR SINTACTICO
         */
        public void agregarError(string lexema, string descripcion, int fila, int columna)
        {
            Sintactico token = new Sintactico(idSintactico, lexema, descripcion, columna, fila);
            arrayListSintactico.Add(token);
            idSintactico++;
        }

        /**
         * LIMPIAR ERRORES
         */
        public void clearArrayListSintactico()
        {
            this.idSintactico = 1;
            this.ArrayListSintactico.Clear();
        }

        public ArrayList ArrayListSintactico { get => arrayListSintactico; set => arrayListSintactico = value; }
    }
}
