using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFP_Proyecto_No._2.Analizador
{
    class NuevoAnalizador
    {
        private readonly static NuevoAnalizador instancia = new NuevoAnalizador();
        ArrayList listaTokens = new ArrayList();
        int indice = 0;
        Token preAnalisis = null;
        string tok;
        string lex;
        String tokenInicio = "";
        //-------TRADUCCIONES ---------

        //Variables variable
        string lexemaAuxiliar = "";
        string variableDeclaracion = "";

        //Variables arreglo
        string nombreArregloDeclarado = "";
        string nombreArregloVacio = "";
        string contenidoDeclarado = "";
        string contenidoVacio = "";

        //Variables console
        string print = "";

        //Variables if
        string condicionIf = "";
        //Variables switch
        string tipoInicio = "";
        string variableSwitch = "";
        string valorVariableSwitch = "";
        string cuerpoSwitch = "";
        string cuerpoCase = "";
        string tipoInicioAux = "";
        int iteracionesSwitch = 0;
        //guarda el token actual para evitar cosas como console.writeline( + algo ) o  console.writeline(  algo + ) lo que daria error sintactico
        String tokenPrevio = "";

        public NuevoAnalizador()
        {
        }

        public static NuevoAnalizador Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void obtenerLista(ArrayList listaTokens)
        {
            this.listaTokens = listaTokens;
            indice = 0;
            preAnalisis = (Token)listaTokens[indice];
            this.tok = ""; this.lex = "";
            Inicio();
        }

        public void Inicio()
        {
            ListaDeclaracion();
        }

        public void ListaDeclaracion()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if(preAnalisis.Descripcion != null)
            {
                if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
                {
                    InicioVariable();
                }
                else if (preAnalisis.Descripcion.Equals("PR_class"))
                {
                    Clase();
                }
                else if (preAnalisis.Descripcion.Equals("PR_static"))
                {
                    MetodoPrincipal();
                }
                else if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
                {
                    ComentarioLinea();
                }
                else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
                {
                    ComentarioMultiLinea();
                }
                else
                {
                    //EPSILON
                }
            }
        }

        #region DECLARACION VARIABLE 
        public void InicioVariable()
        {
            tokenInicio = "";
            tokenInicio = preAnalisis.Descripcion;

            DeclaracionVariable();
            //por si viene mas de una declaracion
            ListaDeclaracionVariable();
        }
        public void ListaDeclaracionVariable()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                DeclaracionVariable();
                ListaDeclaracionVariable();
            }
            else
            {
                //Epsilon
            }

        }
        public void DeclaracionVariable()
        {
            tokenInicio = "";
            tokenInicio = preAnalisis.Descripcion;
            Tipo();
        }

        public void Tipo()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                this.tipoInicio = preAnalisis.Descripcion;
                Parea(preAnalisis.Descripcion);
                //Por si se esta declarando un arreglo;
                if (preAnalisis.Descripcion.Equals("S_Corchete_Izquierdo"))
                {
                    Parea("S_Corchete_Izquierdo");
                    DeclararArreglo();
                }
                else
                {
                    ListaId();
                    OpcAsignacion();
                    PuntoComa();
                }
            }
        }

        public void ListaId()
        {
            if (preAnalisis.Descripcion.Equals("Identificador"))
            {
                this.lexemaAuxiliar = preAnalisis.Lexema;
                variableDeclaracion = lexemaAuxiliar;
                Parea("Identificador");
                ListaId1();
            }
        }
        public void ListaId1()
        {
            if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");
                ListaId();
            }
            else
            {
                //Epsilon
            }
        }
        public void OpcAsignacion()
        {
            if (preAnalisis.Lexema.Equals("="))
            {
                Parea("S_Igual");
                Expresion();
            }
            else
            {
                //Epsilon
            }
        }
        public void Expresion()
        {
            if (preAnalisis.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
            {
                Parea("Digito");
            }
            else if (preAnalisis.Descripcion.Equals("Cadena") && (tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string")))
            {
                Parea("Cadena");
            }
            else if (preAnalisis.Descripcion.Equals("Identificador") && (tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean")))
            {
                Parea("Identificador");
            }
            else
            {
                this.lex = ">>Error sintactico: el tipo de variable " + this.tokenInicio + " no coincide con el valor de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                Parea(preAnalisis.Descripcion);
            }
        }
        public void PuntoComa()
        {
            if (preAnalisis.Descripcion.Equals("S_Punto_y_Coma"))
            {
                Parea("S_Punto_y_Coma");
                ListaDeclaracion();
            }
            else if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");
                ListaId();
                OpcAsignacion();
                PuntoComa();
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba [ punto y coma  ] al final de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                Console.WriteLine(">>Error sintactico: Se esperaba [ punto y coma  ] al final de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]");
            }
        }
        #endregion

        #region ARREGLO
        public void DeclararArreglo()
        {
            if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
            {
                Parea("S_Corchete_Derecho");
                this.nombreArregloDeclarado = preAnalisis.Lexema;
                Parea("Identificador");
                OpcAsignacionArreglo();
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba corchete de cierre en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
            }
        }

        public void OpcAsignacionArreglo()
        {
            if (preAnalisis.Descripcion.Equals("S_Igual"))
            {
                Parea("S_Igual");
                ExpresionArreglo();
            }
            else
            {
                //Epsilon
            }
        }


        // Estos metodos sirven por si el arreglo se declara de una vez, es decir arreglo = {a, b, c}
        public void ExpresionArreglo()
        {
            if (preAnalisis.Descripcion.Equals("S_Llave_Izquierda"))
            {
                Parea("S_Llave_Izquierda");
                ListaValor();
                Parea("S_Llave_Derecha");
                Parea("S_Punto_y_Coma");
                ListaDeclaracion();
            }
            else if (preAnalisis.Descripcion.Equals("PR_new"))
            {
                Parea("PR_new");
                this.ExpresionArreglo2();
            }
            else
            {
                this.lex = ">>Error sintactico: Se esperaba Llave apertura en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
            }
        }
        public void ListaValor()
        {

            if ((preAnalisis.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
                ||
                (preAnalisis.Descripcion.Equals("Cadena") && (tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string")))
                ||
                (preAnalisis.Descripcion.Equals("Identificador") && (tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean"))))
            {
                this.contenidoDeclarado = "[" + preAnalisis.Lexema;
                Parea(preAnalisis.Descripcion);
                ListaValor1();
                this.contenidoDeclarado = this.contenidoDeclarado + "]";

            }
            else
            {
                this.lex = ">>Error Sintactico: para un arreglo tipo " + tokenInicio + " se esperan valores numericos en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
            }

        }
        public void ListaValor1()
        {
            if (preAnalisis.Descripcion.Equals("S_Coma"))
            {
                Parea("S_Coma");

                if (preAnalisis.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
                {
                    this.contenidoDeclarado = this.contenidoDeclarado + "," + preAnalisis.Lexema;
                    Parea("Digito");
                    ListaValor1();
                }
                else if (preAnalisis.Descripcion.Equals("Cadena") && ((tokenInicio.Equals("PR_char") || tokenInicio.Equals("PR_string"))))
                {
                    this.contenidoDeclarado = this.contenidoDeclarado + "," + preAnalisis.Lexema;
                    Parea("Cadena");
                    ListaValor1();
                }
                else if (preAnalisis.Descripcion.Equals("Identificador") && ((tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean"))))
                {
                    this.contenidoDeclarado = this.contenidoDeclarado + "," + preAnalisis.Lexema;
                    Parea("Identificador");
                    ListaValor1();
                }
                else
                {
                    Parea(preAnalisis.Descripcion);
                    this.lex = ">>Error Sintactico: para un arreglo tipo " + tokenInicio + " se esperan valores tipo " + tokenInicio + " en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                }
            }
            else
            {
                //Epsilon
            }
        }

        // estos metodos sirven por si el arreglo se declara como new, es decir new tipo[]

        public void ExpresionArreglo2()
        {
            Console.WriteLine("TIPO DE VARIABLE" + preAnalisis.Descripcion);
            Console.WriteLine("TOKEN DE INICIO" + tokenInicio);
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };
            if (Array.Exists(reservadasVariable, element => element == preAnalisis.Descripcion))
            {
                Console.WriteLine(preAnalisis.Descripcion.Equals(tokenInicio));
                if(preAnalisis.Descripcion.Equals(tokenInicio))
                {
                    Parea(preAnalisis.Descripcion);
                    Parea("S_Corchete_Izquierdo");
                    Parea("S_Corchete_Derecho");
                    Parea("S_Punto_y_Coma");
                    ListaDeclaracion();
                } else
                {
                    //Parea(preAnalisis.Descripcion);
                    this.lex = ">>Error Sintactico: el tipo del arreglo debe ser el mismo que el de su asignacion, " + tokenInicio + "[] = new " + tokenInicio + "[] en lugar de " + preAnalisis.Descripcion + "[]";
                    Console.WriteLine(this.lex);
                }
            }

            /*if (preAnalisis.Descripcion.Equals(tokenInicio))
            {
              

                    /*Parea(preAnalisis.Descripcion);
                    if (preAnalisis.Descripcion.Equals("S_Corchete_Izquierdo"))
                    {
                        Parea("S_Corchete_Izquierdo");
                        if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
                        {
                            Parea("S_Corchete_Derecho");
                            PuntoComa();
                            ListaDeclaracion();
                        }
                        else if (preAnalisis.Descripcion.Equals("Identificador") || preAnalisis.Descripcion.Equals("Digito"))
                        {
                            Parea(preAnalisis.Descripcion);
                            if (preAnalisis.Descripcion.Equals("S_Corchete_Derecho"))
                            {
                                Parea("S_Corchete_Derecho");
                                PuntoComa();
                                ListaDeclaracion();
                            }
                            else
                            {
                                this.lex = ">>Error Sintactico: se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + "]";
                            }
                        }
                        else
                        {
                            this.lex = ">>Error Sintactico: se esperaba parentesis de cierre en lugar de [" + preAnalisis.Descripcion + "]";
                        }
                    }
                    else
                    {
                        this.lex = ">>Error Sintactico: se esperaba parentesis de apertura en lugar de [" + preAnalisis.Descripcion + "]";
                    }
                }
            else
            {
                this.lex = ">>Error Sintactico: el tipo del arreglo debe ser el mismo que el de su asignacion, " + tokenInicio + "[] = new " + tokenInicio + "[] en lugar de " + preAnalisis.Descripcion + "[]";
            }*/
        }
        #endregion

        #region COMENTARIO
        public void Comentario()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                ComentarioLinea();
            }
            else if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                ComentarioMultiLinea();
            }
            else
            {
                //EPSILON
            }
        }

        //COMENTARIO LINEA
        public void ComentarioLinea()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioLinea"))
            {
                ///traduccionComentario(preAnalisis.Lexema, preAnalisis.Descripcion);
                Parea("ComentarioLinea");
                ListaDeclaracion();
            }
            else
            {

            }
        }

        //COMENTARIO MULTILINEA
        public void ComentarioMultiLinea()
        {
            if (preAnalisis.Descripcion.Equals("ComentarioMultiLinea"))
            {
                // traduccionComentario(preAnalisis.Lexema, preAnalisis.Descripcion);
                Parea(preAnalisis.Descripcion);
                ListaDeclaracion();
            }
            else
            {

            }
        }
        #endregion

        #region CLASE
        public void Clase()
        {
            Parea("PR_class");
            Parea("Identificador");
            Parea("S_Llave_Izquierda");
            Comentario();
            ListaDeclaracion();
            Parea("S_Llave_Derecha");
            
        }

        public void MetodoPrincipal()
        {
            Parea("PR_static");
            Parea("PR_void");
            Parea("PR_Main");
            Parea("S_Parentesis_Izquierdo");
            ParametroMain();
            Parea("S_Parentesis_Derecho");
            Parea("S_Llave_Izquierda");
            ListaDeclaracion();
            Parea("S_Llave_Derecha");
            Comentario();
        }

        public void ParametroMain()
        {
            if (preAnalisis.Descripcion.Equals("PR_string"))
            {
                Parea("PR_string");
                Parea("S_Corchete_Izquierdo");
                Parea("S_Corchete_Derecho");
                Parea("Identificador");
            }
            else
            {
                //EPSILON
            }
        }
        #endregion


        public void Parea(String tipoToken)
        {
            Console.WriteLine("ACTUAL " + preAnalisis.Descripcion + "==" + tipoToken);
            Console.WriteLine("INDICE" + indice);
            Console.WriteLine("LIMITE" + (listaTokens.Count - 1));
            if(preAnalisis.Descripcion.Equals(tipoToken))
            {
                if (indice < (listaTokens.Count - 1))
                {
                    if (preAnalisis.Descripcion.Equals(tipoToken))
                    {
                        indice++;
                        preAnalisis = (Token)listaTokens[indice];
                        lex = lex + " " + preAnalisis.Lexema;
                    } else
                    {
                        this.lex = ">> Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                        Console.WriteLine(">> Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]");
                    }
                }
                else
                {
                    preAnalisis = new Token(0, preAnalisis.Lexema, preAnalisis.Descripcion, 0, 0) ;
                }
            } else
            {
                this.lex = ">> Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                Console.WriteLine(">> Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]");
            }
            /*if (preAnalisis.Descripcion.Equals(tipoToken))
            {
                tok = tok + " " + tipoToken;
                Console.WriteLine("INDICE " + indice);
                Console.WriteLine("LIMITE" + (listaTokens.Count - 1));
                Console.WriteLine("ACTUAL " + preAnalisis.Descripcion);
                Console.WriteLine("ANALIZADO " + tipoToken);
                if (finAnalisis == false) {
                    if (indice < (listaTokens.Count - 1))
                    {
                        indice++;
                        preAnalisis = (Token)listaTokens[indice];
                        lex = lex + " " + preAnalisis.Lexema;
                    }
                    else
                    {
                        finAnalisis = true;
                    }
                }                
            }
            else
            {
                //Se genera un error sintactico y se agrega a la lista de errores sintacitos
                this.lex = ">> Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]";
                Console.WriteLine(">> Error sintactico se esperaba [" + tipoToken + "] en lugar de [" + preAnalisis.Descripcion + ", " + preAnalisis.Lexema + "]");
            }*/
        }

        public string returnT()
        {
            Console.WriteLine(this.lex + this.tok);
            return this.lex + "\n" + this.tok + "\n";
        }

    }
}
