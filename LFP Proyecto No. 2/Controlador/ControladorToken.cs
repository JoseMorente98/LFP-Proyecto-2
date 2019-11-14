using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Controlador
{
    class ControladorToken
    {
        private readonly static ControladorToken instancia = new ControladorToken();
        private ArrayList arrayListTokens = new ArrayList();
        private ArrayList arrayListErrors = new ArrayList();
        private int idToken = 1;
        private int idTokenError = 1;
        private ControladorToken()
        {
        }

        public static ControladorToken Instancia
        {
            get
            {
                return instancia;
            }
        }

        /**
         * AGREGAR TOKEN
         */
        public void agregarToken(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idToken, lexema, descripcion, columna, fila);
            ArrayListTokens.Add(token);
            idToken++;
        }

        /**
         * AGREGAR TOKEN ERROR
         */
        public void agregarError(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idTokenError, lexema, descripcion, columna, fila);
            ArrayListErrors.Add(token);
            idTokenError++;
        }

        /**
         * LIMPIAR ARRAY LIST
         */
        public void clearArrayList()
        {
            ArrayListErrors.Clear();
            ArrayListTokens.Clear();
            idToken = 1;
            idTokenError = 1;
        }

        public ArrayList ArrayListTokens { get => arrayListTokens; set => arrayListTokens = value; }
        public ArrayList ArrayListErrors { get => arrayListErrors; set => arrayListErrors = value; }
    }
}
