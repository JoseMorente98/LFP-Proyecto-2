﻿
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
    class SintacticoControlador
    {
        private readonly static SintacticoControlador instancia = new SintacticoControlador();
        private ArrayList arrayListSintactico = new ArrayList();
        private int idSintactico = 1;

        private SintacticoControlador()
        {
        }

        public static SintacticoControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public ArrayList ArrayListSintactico { get => arrayListSintactico; set => arrayListSintactico = value; }

        public void agregarError(string lexema, string descripcion, int fila, int columna)
        {
            Sintactico token = new Sintactico(idSintactico, lexema, descripcion, columna, fila);
            arrayListSintactico.Add(token);
            idSintactico++;
        }
        
        public void clearArrayListSintactico()
        {
            this.idSintactico = 1;
            this.ArrayListSintactico.Clear();
        }
    }
}