﻿using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Controlador
{
    class TokenControlador
    {
        private readonly static TokenControlador instancia = new TokenControlador();
        private ArrayList arrayListTokens = new ArrayList();
        private ArrayList arrayListErrors = new ArrayList();
        private int idToken = 1;
        private int idTokenError = 1;
        private TokenControlador()
        {
        }

        public static TokenControlador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void agregarToken(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idToken, lexema, descripcion, columna, fila);
            arrayListTokens.Add(token);
            idToken++;
        }

        public void agregarError(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idTokenError, lexema, descripcion, columna, fila);
            arrayListErrors.Add(token);
            idTokenError++;
        }

        public ArrayList getArrayListTokens()
        {
            return arrayListTokens;
        }

        public ArrayList getArrayListErrors()
        {
            return arrayListErrors;
        }

        public void resetClass()
        {
            arrayListErrors.Clear();
            arrayListTokens.Clear();
            idToken = 1;
            idTokenError = 1;
        }
    }
}