using LFP_Proyecto_No._2.Controlador;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFP_Proyecto_No._2.Analizador
{
    class AnalizadorLexico
    {
        private readonly static AnalizadorLexico instancia = new AnalizadorLexico();
        /**
         * VARIABLES GLOBALES
         **/
        string auxiliar = "";

        private AnalizadorLexico()
        {
        }

        public static AnalizadorLexico Instancia
        {
            get
            {
                return instancia;
            }
        }

        
        /**
         * ANALIZADOR LEXICO
         **/
        public async void analizador_Lexico(String entradaTexto)
        {
            ////
            int estado = 0;
            int columna = -1;
            int fila = 1;
            entradaTexto = entradaTexto + " ";

            char[] charsRead = new char[entradaTexto.Length];
            using (StringReader reader = new StringReader(entradaTexto))
            {
                await reader.ReadAsync(charsRead, 0, entradaTexto.Length);
            }

            StringBuilder reformattedText = new StringBuilder();
            using (StringWriter writer = new StringWriter(reformattedText))
            {
                for (int i = 0; i < charsRead.Length; i++)
                {
                    columna++;
                    Char c = entradaTexto[i];
                    switch (estado)
                    {
                        case 0:
                            //LETRAS
                            if (char.IsLetter(c))
                            {
                                estado = 1;
                                auxiliar += c;
                            }
                            //SALTO DE LINEA
                            else if (c.Equals('\n'))
                            {
                                estado = 0;
                                columna = 0;
                                fila++;
                            }
                            //ESPACIO EN BLANCO
                            else if (char.IsWhiteSpace(c))
                            {
                                columna++;
                                estado = 0;
                            }
                            //DIGITO
                            else if (char.IsDigit(c))
                            {
                                estado = 2;
                                auxiliar += c;
                            }
                            //SIMBOLOS
                            else if (char.IsSymbol(c))
                            {
                                if (c.Equals('<'))
                                {
                                    columna++;
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Menor_Que");
                                }
                                else if (c.Equals('>'))
                                {
                                    columna++;
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Mayor_Que");

                                }
                                else if (c.Equals('='))
                                {
                                    columna++;
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Igual");
                                }
                                else if (c.Equals('+'))
                                {
                                    columna++;
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Suma");
                                }

                                else
                                {
                                    columna++;
                                    ControladorToken.Instancia.agregarError(fila, (columna - 1), c.ToString(), "Simb_Desconocido_" + c);
                                    estado = 0;
                                    auxiliar = "";
                                }
                            }
                            //PUNTUACIÓN
                            else if (char.IsPunctuation(c))
                            {
                                if (c.Equals('"'))
                                {
                                    columna++;
                                    estado = 3;
                                    i--;
                                    columna--;
                                }
                                else if (c.Equals(','))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Coma");
                                }
                                else if (c.Equals('{'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Llave_Izquierda");
                                }
                                else if (c.Equals('}'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Llave_Derecha");
                                }
                                else if (c.Equals(';'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Punto_y_Coma");
                                }
                                else if (c.Equals(':'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Dos_puntos");
                                }
                                else if (c.Equals('.'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Punto");
                                }
                                else if (c.Equals('_'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Guion_Bajo");
                                }
                                else if (c.Equals('('))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, columna, c.ToString(), "S_Parentesis_Izquierdo");
                                }
                                else if (c.Equals(')'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, columna, c.ToString(), "S_Parentesis_Derecho");
                                }
                                else if (c.Equals('['))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, columna, c.ToString(), "S_Corchete_Izquierdo");
                                }
                                else if (c.Equals(']'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, columna, c.ToString(), "S_Corchete_Derecho");
                                }
                                else if (c.Equals('%'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Porcentaje");
                                }
                                else if (c.Equals('-'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Resta");
                                }
                                else if (c.Equals('/'))
                                {
                                    columna++;
                                    estado = 8;
                                    i--;
                                    columna--;
                                }
                                else if (c.Equals('*'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Mult");
                                }
                                else if (c.Equals('!'))
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - 1), c.ToString(), "S_Excl");
                                }
                                else if (c.Equals('\''))
                                {
                                    columna++;
                                    estado = 13;
                                    i--;
                                    columna--;
                                }
                                else
                                {
                                    columna++;
                                    ControladorToken.Instancia.agregarError(fila, (columna - 1), c.ToString(), "Simb_Desconocido_" + c);
                                    alertMessage("Se detectó un error en la fila " + fila + ", columna " + (columna - 1));
                                    estado = 0;
                                    auxiliar = "";
                                    columna--;
                                }

                            }
                            //SIMBOLOS DESCONOCIDOS
                            else
                            {
                                columna++;
                                ControladorToken.Instancia.agregarError(fila, (columna - 1), c.ToString(), "Simb_Desconocido_" + c);
                                alertMessage("Se detectó un error en la fila " + fila + ", columna " + (columna - 1));
                                estado = 0;
                                auxiliar = "";
                                columna--;
                            }
                            break;
                        case 1:
                            if (Char.IsLetterOrDigit(c) || c == '_')
                            {
                                auxiliar += c;
                                estado = 1;
                            }
                            else
                            {
                                string[] reservadasC = { "class", "static", "void", "string",
                                    "int", "new", "float", "char", "bool", "boolean", "if", "else", "default",
                                    "switch", "case", "break", "for",  "while" };

                                string[] reservadasMayus = { "Main", "Console", "WriteLine", "Count", "Length" };

                                if (Array.Exists(reservadasC, element => element.Equals(auxiliar.ToLower())))
                                {
                                    int lex = Array.IndexOf(reservadasC, auxiliar.ToLower());
                                    ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar.ToLower(), "PR_" + reservadasC[lex]);

                                }
                                else if (Array.Exists(reservadasMayus, element => element.Equals(auxiliar)))
                                {
                                    int lex = Array.IndexOf(reservadasMayus, auxiliar);
                                    ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "PR_" + reservadasMayus[lex]);
                                }
                                else
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Identificador");
                                }
                                auxiliar = "";
                                i--;
                                columna--;
                                estado = 0;
                            }
                            break;
                        case 2:
                            if (Char.IsDigit(c))
                            {
                                auxiliar += c;
                                estado = 2;
                            }
                            else if (c == '.')
                            {
                                estado = 6;
                                auxiliar += c;
                            }
                            else
                            {
                                ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Digito");
                                auxiliar = "";
                                i--;
                                columna--;
                                estado = 0;
                            }
                            break;
                        case 3:
                            if (c == '"')
                            {
                                auxiliar += c;
                                estado = 4;
                            }
                            break;
                        case 4:
                            if (c != '"')
                            {
                                if (c.Equals('\n')) { fila++; columna = 0; }
                                auxiliar += c;
                                estado = 4;
                            }
                            else
                            {
                                estado = 5;
                                i--;
                                columna--;
                            }
                            break;
                        case 5:
                            if (c == '"')
                            {
                                auxiliar += c;
                                ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Cadena");
                                estado = 0;
                                auxiliar = "";
                            }
                            break;
                        case 6:
                            if (char.IsDigit(c))
                            {
                                estado = 7;
                                auxiliar += c;
                            }
                            else
                            {
                                estado = 0;
                                auxiliar = "";
                            }
                            break;
                        case 7:
                            if (Char.IsDigit(c))
                            {
                                estado = 7;
                                auxiliar += c;
                            }
                            else
                            {
                                ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Digito");
                                auxiliar = "";
                                i--;
                                columna--;
                                estado = 0;
                            }
                            break;
                        //COMENTARIO
                        case 8:
                            if (c == '/')
                            {
                                auxiliar += c;
                                estado = 9;
                            }
                            break;
                        case 9:
                            if (c == '/')
                            {
                                auxiliar += c;
                                estado = 10;
                            }
                            else if (c == '*')
                            {
                                auxiliar += c;
                                estado = 11;
                            }
                            else
                            {
                                ControladorToken.Instancia.agregarToken(fila, (columna - 1), auxiliar.ToString(), "S_Division");
                                auxiliar = "";
                                columna--;
                                i--;
                                estado = 0;
                            }
                            break;
                        case 10:
                            if (!c.Equals('\n'))
                            {
                                auxiliar += c;
                                estado = 10;
                            }
                            else
                            {
                                ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "ComentarioLinea");
                                fila++; columna = 0;
                                estado = 0;
                                auxiliar = "";
                            }
                            break;
                        case 11:
                            if (c != '*')
                            {
                                if (c.Equals('\n')) { fila++; columna = 0; }
                                auxiliar += c;
                                estado = 11;
                            }
                            else
                            {
                                auxiliar += c;
                                estado = 12;
                            }
                            break;
                        case 12:
                            if (c == '/')
                            {
                                auxiliar += c;
                                ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "ComentarioMultiLinea");
                                estado = 0;
                                auxiliar = "";
                            }
                            break;
                         //CARACTER
                        case 13:
                            if (c == '\'')
                            {
                                auxiliar += c;
                                estado = 14;
                                columna++;
                            }
                            break;
                        case 14:
                            if (!c.Equals('\''))
                            {
                                auxiliar += c;
                                estado = 14;
                                columna++;
                            }
                            else
                            {
                                estado = 15;
                                columna--;
                                i--;
                            }
                            break;
                        case 15:
                            if (c == '\'')
                            {
                                auxiliar += c;
                                columna++;
                                if (auxiliar.Length == 3)
                                {
                                    ControladorToken.Instancia.agregarToken(fila, (columna - auxiliar.Length), auxiliar, "Character");
                                }
                                else
                                {
                                    ControladorToken.Instancia.agregarError(fila, (columna - auxiliar.Length), auxiliar, "Desconocido");
                                }
                                estado = 0;
                                auxiliar = "";
                            }
                            break;
                    }
                }
            }

        }

        /**
         * MENSAJE DE ALERTA
         **/
        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
