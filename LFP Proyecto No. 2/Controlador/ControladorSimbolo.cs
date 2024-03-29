﻿using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFP_Proyecto_No._2.Controlador
{
    class ControladorSimbolo
    {
        private readonly static ControladorSimbolo instancia = new ControladorSimbolo();
        private ArrayList arrayListSimbolo = new ArrayList();
        private int idSimbolo = 1;

        private ControladorSimbolo()
        {
        }

        public static ControladorSimbolo Instancia
        {
            get
            {
                return instancia;
            }
        }

        /**
         * AGREGAR SIMBOLOS
         */
        public void agregarSimbolo(string nombre, string valor, string tipo)
        {
            Simbolo simbolo = new Simbolo(idSimbolo, nombre, valor, tipo);
            ArrayListSimbolo.Add(simbolo);
            idSimbolo++;
        }

        /**
         * BUSCAR SIMBOLO
         */
        public Simbolo buscarSimbolo(string nombre)
        {
            foreach (Simbolo s in ArrayListSimbolo)
            {
                if(s.Nombre.Equals(nombre))
                {
                    return s;
                }
            }
            return null;
        }

        /**
         * BUSCAR SIMBOLO
         */
        public Boolean buscar(string nombre)
        {
            foreach (Simbolo s in ArrayListSimbolo)
            {
                if (s.Nombre.Equals(nombre))
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * EDITAR SIMBOLO
         */
        public void editarSimbolo(string nombre, string valor)
        {
            foreach (Simbolo s in ArrayListSimbolo)
            {
                if (s.Nombre.Equals(nombre))
                {
                    s.Valor = valor;
                }
            }
        }

        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void clearArrayListSimbolo()
        {
            this.idSimbolo = 1;
            this.arrayListSimbolo.Clear();
        }

        public ArrayList ArrayListSimbolo { get => arrayListSimbolo; set => arrayListSimbolo = value; }

    }
}
