using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFP_Proyecto_No._2.Controlador;
using LFP_Proyecto_No._2.Modelo;

namespace LFP_Proyecto_No._2.Analizador
    {
    class AnalizadorSintactico
    {
        private readonly static AnalizadorSintactico instancia = new AnalizadorSintactico();
        ArrayList listaTokens = new ArrayList();
        int tokenIndice = 0;
        Token tokenActual = null;
        string tok;
        string lex;
        String tokenInicio = "";
        bool errorSintactico = false;

        //-------TRADUCCIONES ---------

        //Variables variable
        string lexemaAuxiliar = "";
        private string variableDeclaracion = "";

        //Variables Arreglo
        private string nombreArregloDeclarado = "";

        //Variables switch
        string tipoInicio = "";
        string variableSwitch = "";
        string cuerpoSwitch = "";
        string cuerpoCase = "";
        string tipoInicioAux = "";
        int iteracionesSwitch = 0;

        public AnalizadorSintactico()
        {
        }

        public static AnalizadorSintactico Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void obtenerLista(ArrayList listaTokens)
        {
            this.listaTokens = listaTokens;
            tokenIndice = 0;
            tokenActual = (Token)listaTokens[tokenIndice];
            this.tok = ""; this.lex = "";
            Inicio();
        }

        public void Inicio()
        {
            ListaDeclaracion();
        }

        /**
         * LISTA DE DECLARACION
         **/
        #region LISTA DE DECLARACION
        public void ListaDeclaracion()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (tokenActual.Descripcion != null)
            {
                if (Array.Exists(reservadasVariable, element => element == tokenActual.Descripcion))
                {
                    InicioVariable();
                }
                else if (tokenActual.Descripcion.Equals("PR_class"))
                {
                    Clase();
                }
                else if (tokenActual.Descripcion.Equals("PR_if"))
                {
                    InicioIf();
                }
                else if (tokenActual.Descripcion.Equals("PR_switch"))
                {
                    InicioSwitch();
                }
                else if (tokenActual.Descripcion.Equals("PR_while"))
                {
                    InicioWhile();
                }
                else if (tokenActual.Descripcion.Equals("PR_for"))
                {
                    InicioFor();
                }
                else if (tokenActual.Descripcion.Equals("PR_static"))
                {
                    MetodoPrincipal();
                }
                else if (tokenActual.Descripcion.Equals("ComentarioLinea"))
                {
                    ComentarioLinea();
                }
                else if (tokenActual.Descripcion.Equals("ComentarioMultiLinea"))
                {
                    ComentarioMultiLinea();
                }
                else if (tokenActual.Descripcion.Equals("PR_Console"))
                {
                    InicioConsole();
                }
                else if (tokenActual.Descripcion.Equals("Identificador"))
                {
                    AsignacionSinTipo();
                }
                else
                {
                    //EPSILON
                }
            }
        }
        #endregion

        /**
         * DECLARACION DE VARIABLE
         **/
        #region DECLARACION VARIABLE 
        public void InicioVariable()
        {
            tokenInicio = "";
            tokenInicio = tokenActual.Descripcion;

            DeclaracionVariable();
            //SI VIENEN MAS DE UNA DECLARACIÓN
            ListaDeclaracionVariable();
        }

        public void ListaDeclaracionVariable()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (Array.Exists(reservadasVariable, element => element == tokenActual.Descripcion))
            {
                DeclaracionVariable();
                ListaDeclaracionVariable();
            }
            else
            {
                //EPSILON
            }

        }
        public void DeclaracionVariable()
        {
            tokenInicio = "";
            tokenInicio = tokenActual.Descripcion;
            Tipo();
        }

        public void Tipo()
        {
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };

            if (Array.Exists(reservadasVariable, element => element == tokenActual.Descripcion))
            {
                this.tipoInicio = tokenActual.Descripcion;
                Match(tokenActual.Descripcion);
                //DECLARA ARREGLO
                if (tokenActual.Descripcion.Equals("S_Corchete_Izquierdo"))
                {
                    DeclararArreglo();
                }
                else
                {
                    ListaIdentificador();
                    OpcionDeAsignacion();
                    PuntoYComa();
                }
            }
        }

        public void ListaIdentificador()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                this.lexemaAuxiliar = tokenActual.Lexema;
                variableDeclaracion = lexemaAuxiliar;
                Match("Identificador");
                ListaId1();
            }
        }
        public void ListaId1()
        {
            if (tokenActual.Descripcion.Equals("S_Coma"))
            {
                Match("S_Coma");
                ListaIdentificador();
            }
            else
            {
                //EPSILON
            }
        }
        public void OpcionDeAsignacion()
        {
            if (tokenActual.Lexema.Equals("="))
            {
                Match("S_Igual");
                ValorVariable();
            }
            else
            {
                //EPSILON
            }
        }
        public void ValorVariable()
        {
            if (tokenActual.Descripcion.Equals("Digito") && (tokenInicio.Equals("PR_int") || tokenInicio.Equals("PR_float")))
            {
                ControladorSimbolo.Instancia.agregarSimbolo(this.lexemaAuxiliar, tokenActual.Lexema, tokenInicio.Replace("PR_", ""));
                Match("Digito");
            }
            else if (tokenActual.Descripcion.Equals("Cadena") && tokenInicio.Equals("PR_string"))
            {
                ControladorSimbolo.Instancia.agregarSimbolo(this.lexemaAuxiliar, tokenActual.Lexema, tokenInicio.Replace("PR_", ""));
                Match("Cadena");
            }
            else if (tokenActual.Descripcion.Equals("Character") && (tokenInicio.Equals("PR_char")))
            {
                ControladorSimbolo.Instancia.agregarSimbolo(this.lexemaAuxiliar, tokenActual.Lexema, tokenInicio.Replace("PR_", ""));
                Match("Character");
            }
            else if (tokenActual.Descripcion.Equals("Identificador") && (tokenInicio.Equals("PR_bool") || tokenInicio.Equals("PR_boolean")))
            {
                ControladorSimbolo.Instancia.agregarSimbolo(this.lexemaAuxiliar, tokenActual.Lexema, tokenInicio.Replace("PR_", ""));
                Match("Identificador");
            }
            else
            {
                this.lex = "ERROR!  el tipo de variable " + this.tokenInicio + " no coincide con el valor de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);

                Match(tokenActual.Descripcion);
                this.errorSintactico = true;

            }
        }
        public void PuntoYComa()
        {
            if (tokenActual.Descripcion.Equals("S_Punto_y_Coma"))
            {
                Match(tokenActual.Descripcion);
                if (tokenActual.Descripcion.Equals("PR_switch"))
                {
                    this.variableSwitch = this.lexemaAuxiliar;
                    InicioSwitch();
                }
                else
                {
                    ListaDeclaracion();
                }
            }
            else if (tokenActual.Descripcion.Equals("S_Coma"))
            {
                Match("S_Coma");
                ListaIdentificador();
                OpcionDeAsignacion();
                PuntoYComa();
            }
            else
            {
                this.lex = "ERROR!  Se esperaba [ punto y coma  ] al final de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.tok = "";
                errorSintactico = true;
            }
        }
        #endregion

        /**
         * DECLARACION DE ARREGLO
         **/
        #region DECLARACIÓN ARRELO
        public void DeclararArreglo()
        {
            Match("S_Corchete_Izquierdo");
            Match("S_Corchete_Derecho");
            this.nombreArregloDeclarado = tokenActual.Lexema;
            Match("Identificador");
            OpcionAsignacionArreglo();
            PuntoYComa();
            ListaDeclaracion();
        }

        public void OpcionAsignacionArreglo()
        {
            if (tokenActual.Descripcion.Equals("S_Igual"))
            {
                Match("S_Igual");
                ExpresionArreglo();
            }
            else
            {
                //EPSILON
            }
        }

        //ARREGLO = {a, b, c}
        public void ExpresionArreglo()
        {
            if (tokenActual.Descripcion.Equals("S_Llave_Izquierda"))
            {
                Match("S_Llave_Izquierda");
                ListaValor();
                Match("S_Llave_Derecha");
            }
            else if (tokenActual.Descripcion.Equals("PR_new"))
            {
                this.ExpresionArreglo2();
            }
        }

        public void ListaValor()
        {
            if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
                AnidarElementos();
                ListaValor1();
            }
            else if (tokenActual.Descripcion.Equals("Cadena"))
            {
                Match("Cadena");
                AnidarElementos();
                ListaValor1();
            }
            else if (tokenActual.Descripcion.Equals("Character"))
            {
                Match("Character");
                AnidarElementos();
                ListaValor1();
            }
            else if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
                AnidarElementos();
                ListaValor1();
            }
        }

        public void AnidarElementos()
        {
            if (tokenActual.Descripcion.Equals("S_Suma"))
            {
                Match("S_Suma");
                ListaValor();
            }
            else
            {

            }
        }

        public void ListaValor1()
        {
            if (tokenActual.Descripcion.Equals("S_Coma"))
            {
                Match("S_Coma");
                ListaValor();
            }
            else
            {
                //EPSILON
            }
        }

        //ARREGLO NUEVO new tipo[]
        public void ExpresionArreglo2()
        {
            Match("PR_new");
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_string" };
            if (Array.Exists(reservadasVariable, element => element == tokenActual.Descripcion))
            {
                if (tokenActual.Descripcion.Equals(tokenInicio))
                {
                    Match(tokenActual.Descripcion);
                    Match("S_Corchete_Izquierdo");
                    Numero();
                    Match("S_Corchete_Derecho");
                }
                else
                {
                    this.lex = "ERROR! el tipo del Arreglo debe ser el mismo que el de su asignacion, " + tokenInicio + "[] = new " + tokenInicio + "[] en lugar de " + tokenActual.Descripcion + "[]";
                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                    this.errorSintactico = true;

                }
            }
        }

        public void Numero()
        {
            if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
            }
            else if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
            }
            else
            {
                //EPSILON
            }
        }
        #endregion

        /**
         * DECLARACION DE COMENTARIO
         **/
        #region COMENTARIO
        public void Comentario()
        {
            if (tokenActual.Descripcion.Equals("ComentarioLinea"))
            {
                ComentarioLinea();
            }
            else if (tokenActual.Descripcion.Equals("ComentarioMultiLinea"))
            {
                ComentarioMultiLinea();
            }
            else
            {
                //EPSILON
            }
        }

        public void ComentarioLinea()
        {
            if (tokenActual.Descripcion.Equals("ComentarioLinea"))
            {
                Match("ComentarioLinea");
                ListaDeclaracion();
            }
            else
            {

            }
        }

        public void ComentarioMultiLinea()
        {
            if (tokenActual.Descripcion.Equals("ComentarioMultiLinea"))
            {
                Match(tokenActual.Descripcion);
                ListaDeclaracion();
            }
            else
            {

            }
        }
        #endregion

        /**
         * DECLARACION DE CLASE
         **/
        #region CLASE
        public void Clase()
        {
            Match("PR_class");
            Match("Identificador");
            Match("S_Llave_Izquierda");
            Comentario();
            ListaDeclaracion();
            Match("S_Llave_Derecha");
        }

        public void MetodoPrincipal()
        {
            Match("PR_static");
            Match("PR_void");
            Match("PR_Main");
            Match("S_Parentesis_Izquierdo");
            ParametroMain();
            Match("S_Parentesis_Derecho");
            Match("S_Llave_Izquierda");
            ListaDeclaracion();
            Match("S_Llave_Derecha");
            Comentario();
        }

        public void ParametroMain()
        {
            if (tokenActual.Descripcion.Equals("PR_string"))
            {
                Match("PR_string");
                Match("S_Corchete_Izquierdo");
                Match("S_Corchete_Derecho");
                Match("Identificador");
            }
            else
            {
                //EPSILON
            }
        }
        #endregion

        /**
         * DECLARACION DE CONSOLE WRITELINE
         **/
        #region CONSOLE WRITELINE
        public void InicioConsole()
        {
            Match("PR_Console");
            Match("S_Punto");
            Match("PR_WriteLine");
            Match("S_Parentesis_Izquierdo");
            Expresion();
            Match("S_Parentesis_Derecho");
            Match("S_Punto_y_Coma");
            ListaDeclaracion();
        }

        public void CuerpoConsole()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
                Arreglo();
                MasArgumentos();
            }
            else if (tokenActual.Descripcion.Equals("Cadena"))
            {
                Match("Cadena");
                MasArgumentos();
            }
            else if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
                MasArgumentos();
            }
            else
            {
                //EPSILON
            }
        }

        public void Arreglo()
        {
            if (tokenActual.Descripcion.Equals("S_Corchete_Izquierdo"))
            {
                Match("S_Corchete_Izquierdo");
                TipoVariable();
                Match("S_Corchete_Derecho");
            }
            else
            {
                //EPSILON
            }
        }

        public void TipoVariable()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
            }
            else if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ identificador o digito ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }

        public void MasArgumentos()
        {
            if (tokenActual.Descripcion.Equals("S_Suma"))
            {
                Match("S_Suma");
                CuerpoConsole();
            }
        }
        #endregion

        /**
         * SENTENCIA DE IF
         **/
        #region SENTENCIA IF
        public void InicioIf()
        {
            Match("PR_if");
            Match("S_Parentesis_Izquierdo");
            //CONDICION
            IdentificadorIf();
            SimboloIf();
            IdentificadorIf();
            Match("S_Parentesis_Derecho");
            Match("S_Llave_Izquierda");
            //LISTA DECLARACION
            ListaDeclaracion();
            Match("S_Llave_Derecha");
            //ELSEIF
            ElseIf();
            //LISTA DECLARACION
            ListaDeclaracion();
        }

        public void IdentificadorIf()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
            }
            else if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ identificador o digito ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }

        public void SimboloIf()
        {
            switch (tokenActual.Descripcion)
            {
                case "S_Igual":
                    Match("S_Igual");
                    Match("S_Igual");
                    break;
                case "S_Mayor_Que":
                    Match("S_Mayor_Que");
                    switch (tokenActual.Descripcion)
                    {
                        case "S_Igual":
                            Match("S_Igual");
                            break;
                        default:
                            break;
                    }
                    break;
                case "S_Menor_Que":
                    Match("S_Menor_Que");
                    switch (tokenActual.Descripcion)
                    {
                        case "S_Igual":
                            Match("S_Igual");
                            break;
                        default:
                            break;
                    }
                    break;
                case "S_Excl":
                    Match("S_Excl");
                    Match("S_Igual");
                    break;
                default:
                    this.lex = "ERROR!  se esperaba [ operador ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                    this.errorSintactico = true;
                    break;
            }
        }

        public void ElseIf()
        {
            if (tokenActual.Descripcion.Equals("PR_else"))
            {
                Match("PR_else");
                Match("S_Llave_Izquierda");
                //LISTA DECLARACION
                ListaDeclaracion();
                Match("S_Llave_Derecha");
            }
            else
            {

            }
        }
        #endregion

        /**
         * SENTENCIA DE WHILE
         **/
        #region SENTENCIA WHILE
        public void InicioWhile()
        {
            Match("PR_while");
            Match("S_Parentesis_Izquierdo");
            //CONDICION
            IdentificadorWhile();
            SimboloWhile();
            IdentificadorWhile();
            Match("S_Parentesis_Derecho");
            Match("S_Llave_Izquierda");
            ListaDeclaracion();
            Match("S_Llave_Derecha");
            //LISTA DECLARACION
            ListaDeclaracion();
        }

        public void IdentificadorWhile()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
            }
            else if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ identificador o digito ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }

        public void SimboloWhile()
        {
            switch (tokenActual.Descripcion)
            {
                case "S_Igual":
                    Match("S_Igual");
                    Match("S_Igual");
                    break;
                case "S_Mayor_Que":
                    Match("S_Mayor_Que");
                    switch (tokenActual.Descripcion)
                    {
                        case "S_Igual":
                            Match("S_Igual");
                            break;
                        default:
                            break;
                    }
                    break;
                case "S_Menor_Que":
                    Match("S_Menor_Que");
                    switch (tokenActual.Descripcion)
                    {
                        case "S_Igual":
                            Match("S_Igual");
                            break;
                        default:
                            break;
                    }
                    break;
                case "S_Excl":
                    Match("S_Excl");
                    Match("S_Igual");
                    break;
                default:
                    this.lex = "ERROR!  se esperaba [ operador ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                    this.errorSintactico = true;
                    break;
            }
        }
        #endregion

        /**
         * SENTENCIA DE FOR
         **/
        #region SENTENCIA FOR 
        public void InicioFor()
        {
            Match("PR_for");
            Match("S_Parentesis_Izquierdo");
            DeclaracionFor();
            Match("S_Punto_y_Coma");
            ExpresionFor();
            Match("S_Punto_y_Coma");
            IncrementoDecremento();
            Match("S_Parentesis_Derecho");
            Match("S_Llave_Izquierda");
            //LISTA DECLARACION
            ListaDeclaracion();
            Match("S_Llave_Derecha");
            //LISTA DECLARACION
            ListaDeclaracion();
        }

        public void DeclaracionFor()
        {
            if (tokenActual.Descripcion.Equals("PR_int"))
            {
                String variable, tipo, igual;
                tipo = tokenActual.Lexema;
                Match("PR_int");
                variable = tokenActual.Lexema;
                Match("Identificador");
                Match("S_Igual");
                igual = tokenActual.Lexema;
                Match("Digito");
                ControladorSimbolo.Instancia.agregarSimbolo(variable, igual, tipo);
            }
            else if (tokenActual.Descripcion.Equals("Identificador"))
            {
                String variable, igual;
                variable = tokenActual.Lexema;
                Match("Identificador");
                Match("S_Igual");
                igual = tokenActual.Lexema;
                Match("Digito");
                if (ControladorSimbolo.Instancia.buscar(variable))
                {
                    ControladorSimbolo.Instancia.editarSimbolo(variable, igual);
                }
                else
                {
                    ControladorSimbolo.Instancia.agregarSimbolo(variable, igual, "0");
                }
            }
        } 

        public void ExpresionFor()
        {
            IdentificadorFor();
            SimboloFor();
            IdentificadorFor();
        }

        public void IdentificadorFor()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
            }
            else if (tokenActual.Descripcion.Equals("Digito"))
            {
                Match("Digito");
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ identificador o digito ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }

        public void SimboloFor()
        {
            switch (tokenActual.Descripcion)
            {
                case "S_Igual":
                    Match("S_Igual");
                    Match("S_Igual");
                    break;
                case "S_Mayor_Que":
                    Match("S_Mayor_Que");
                    switch (tokenActual.Descripcion)
                    {
                        case "S_Igual":
                            Match("S_Igual");
                            break;
                        default:
                            break;
                    }
                    break;
                case "S_Menor_Que":
                    Match("S_Menor_Que");
                    switch (tokenActual.Descripcion)
                    {
                        case "S_Igual":
                            Match("S_Igual");
                            break;
                        default:
                            break;
                    }
                    break;
                case "S_Excl":
                    Match("S_Excl");
                    Match("S_Igual");
                    break;
                default:
                    this.lex = "ERROR!  se esperaba [ operador ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                    this.errorSintactico = true;
                    break;
            }
        }

        public void IncrementoDecremento()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match("Identificador");
                switch (tokenActual.Descripcion)
                {
                    case "S_Suma":
                        Match("S_Suma");
                        Match("S_Suma");
                        break;
                    case "S_Resta":
                        Match("S_Resta");
                        Match("S_Resta");
                        break;
                }
            }
        }
        #endregion

        /**
         * SENTENCIA DE SWITCH
         **/
        #region SENTENCIA SWITCH
        public void InicioSwitch()
        {
            this.tipoInicioAux = tipoInicio;
            Match("PR_switch");
            Match("S_Parentesis_Izquierdo");
            //ASIGNACION
            AsignacionSwitch();
            Match("S_Parentesis_Derecho");
            Match("S_Llave_Izquierda");
            //CUERPO SWITCH
            CuerpoSwitch();
            Match("S_Llave_Derecha");
            ListaDeclaracion();
        }

        public void AsignacionSwitch()
        {
            if (tokenActual.Lexema.Equals(variableSwitch))
            {
                Match(tokenActual.Descripcion);
            }
            else
            {
                this.lex = "ERROR!  La variable [" + tokenActual.Lexema + "] no esta declarada";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.tok = "";
                errorSintactico = true;
            }
        }

        public void CuerpoSwitch()
        {
            if (tokenActual.Descripcion.Equals("PR_case"))
            {
                //va armando la traduccion del switch
                if (iteracionesSwitch == 0) { cuerpoSwitch = cuerpoCase + " if " + variableSwitch; iteracionesSwitch = 1; }
                else { cuerpoSwitch = cuerpoCase + "else if " + variableSwitch; }


                Match(tokenActual.Descripcion);
                if ((tokenActual.Descripcion.Equals("Cadena") && (tipoInicio.Equals("PR_string"))) ||
                    (tokenActual.Descripcion.Equals("Character") && (tipoInicio.Equals("PR_char"))) ||
                    (tokenActual.Descripcion.Equals("Digito") && (tipoInicio.Equals("PR_int") || tipoInicio.Equals("PR_float"))))
                {
                    cuerpoSwitch = cuerpoSwitch + " == " + tokenActual.Lexema;
                    Match(tokenActual.Descripcion);
                    if (tokenActual.Descripcion.Equals("S_Dos_puntos"))
                    {
                        cuerpoSwitch = cuerpoSwitch + ":";
                        Match(tokenActual.Descripcion);
                        CuerpoCase();
                        if (errorSintactico == false)
                        {
                            if (tokenActual.Descripcion.Equals("PR_break"))
                            {
                                Match(tokenActual.Descripcion);
                                if (tokenActual.Descripcion.Equals("S_Punto_y_Coma"))
                                {
                                    Match(tokenActual.Descripcion);
                                    CuerpoSwitch();
                                }
                                else
                                {
                                    this.lex = "ERROR! Se esperaban punto y coma en lugar de [" + tokenActual.Descripcion + " ]";
                                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                                    this.tok = "";
                                    errorSintactico = true;
                                }
                            }
                            else
                            {
                                this.lex = "ERROR! Se esperaban palabra reservada BREAK en lugar de [" + tokenActual.Descripcion + " ]";
                                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                                this.tok = "";
                                errorSintactico = true;
                            }
                        }
                    }
                    else
                    {
                        this.lex = "ERROR! Se esperaban dos puntos el lugar de [" + tokenActual.Descripcion + " ]";
                        ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                        this.tok = "";
                        errorSintactico = true;
                    }
                }
                else
                {
                    this.lex = "ERROR! El tipo de variable [ " + tipoInicio + "] no concuerda con el tipo de evaluacion [ " + tokenActual.Descripcion + " ] del case";
                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                    this.tok = "";
                    errorSintactico = true;
                }
            }
            else if (tokenActual.Descripcion.Equals("PR_default"))
            {
                Match(tokenActual.Descripcion);

                if (tokenActual.Descripcion.Equals("S_Dos_puntos"))
                {
                    cuerpoSwitch = "else" + ":";
                    Match(tokenActual.Descripcion);
                    ListaDeclaracion();
                    if (errorSintactico == false)
                    {
                        if (tokenActual.Descripcion.Equals("PR_break"))
                        {
                            Match(tokenActual.Descripcion);
                            if (tokenActual.Descripcion.Equals("S_Punto_y_Coma"))
                            {
                                Match(tokenActual.Descripcion);
                                CuerpoSwitch();
                            }
                            else
                            {
                                this.lex = "ERROR! Se esperaban punto y coma en lugar de [" + tokenActual.Descripcion + " ]";
                                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                                this.tok = "";
                                errorSintactico = true;
                            }
                        }
                        else
                        {
                            this.lex = "ERROR! Se esperaban palabra reservada BREAK en lugar de [" + tokenActual.Descripcion + " ]";
                            ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                            this.tok = "";
                            errorSintactico = true;
                        }
                    }
                }
                else
                {
                    this.lex = "ERROR! Se esperaban dos puntos el lugar de [" + tokenActual.Descripcion + " ]";
                    ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                    this.tok = "";
                    errorSintactico = true;
                }

            }
            else if (tokenActual.Descripcion.Equals("S_Llave_Derecha"))
            {

            }
            else
            {
                this.lex = "ERROR! Se esperaba palabra reservada CASE en lugar de [ " + tokenActual.Lexema + " ]";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.tok = "";
                errorSintactico = true;
            }
        }

        public void CuerpoCase()
        {
            ListaDeclaracion();

        }
        #endregion

        /**
         * DECLARACION SIN TIPO
         **/
        #region DECLARACION SIN TIPO
        public void AsignacionSinTipo()
        {
            if (tokenActual.Descripcion.Equals("Identificador"))
            {
                this.lexemaAuxiliar = tokenActual.Lexema;
                Match("Identificador");
                Match("S_Igual");
                Expresion();
                PuntoYComa();
                ListaDeclaracion();
            }
        }
        #endregion

        /**
         * EXPRESION DE OPERACIONES Y CONCATENACION
         **/
        #region EXPRESION ARITMETICA Y CONCATENACION
        public void Expresion()
        {
            Termino();
            ExpresionPrima();
        }

        public void ExpresionPrima()
        {
            if (tokenActual.Lexema.Equals("+"))
            {
                Match(tokenActual.Descripcion);
                EvaluarSiguiente1(tokenActual.Descripcion);
            }
            else if (tokenActual.Lexema.Equals("-"))
            {
                Match(tokenActual.Descripcion);
                EvaluarSiguiente1(tokenActual.Descripcion);
            }
            else
            {
                //EPSILON
            }
        }

        public void EvaluarSiguiente1(string texto)
        {
            if (!tokenActual.Lexema.Equals(texto))
            {
                Termino();
                ExpresionPrima();
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ Cadena, Digito o Identificador ] ";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }

        public void Termino()
        {
            Factor();
            TerminoPrima();
        }

        public void TerminoPrima()
        {
            if (tokenActual.Lexema.Equals("*"))
            {
                Match(tokenActual.Descripcion);
                EvaluaSigiente(tokenActual.Descripcion);
            }
            else if (tokenActual.Lexema.Equals("/"))
            {
                Match(tokenActual.Descripcion);
                EvaluaSigiente(tokenActual.Descripcion);
            }
            else
            {
                //EPSILON
            }
        }
        public void EvaluaSigiente(string texto)
        {
            if (!tokenActual.Lexema.Equals(texto))
            {
                Factor();
                TerminoPrima();
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ Cadena, Digito o Identificador ] ";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }
        public void Factor()
        {
            if (tokenActual.Lexema.Equals("("))
            {
                Match(tokenActual.Descripcion);
                Expresion();
                Match("S_Parentesis_Derecho");

            }
            else if (tokenActual.Descripcion.Equals("Digito") || tokenActual.Descripcion.Equals("Cadena"))
            {
                Match(tokenActual.Descripcion);
            }
            else if (tokenActual.Descripcion.Equals("Identificador"))
            {
                Match(tokenActual.Descripcion);
                Arreglo();
                if (tokenActual.Descripcion.Equals("S_Punto"))
                {
                    Match("S_Punto");
                    if (tokenActual.Descripcion.Equals("Identificador"))
                    {
                        Match("Identificador");
                    }
                    else
                    {
                        this.lex = "ERROR!  se esperaba [ Identificador ] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                        ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                        this.errorSintactico = true;
                    }
                }
                else
                {
                    //EPSILON
                }
            }
            else
            {
                this.lex = "ERROR!  se esperaba [ Cadena, Digito o Identificador ] ";
                ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                this.errorSintactico = true;
            }
        }
        #endregion

        /**
         * METODO PARA EMPAREJAR
         **/
        public void Match(String tipoToken)
        {
            if (errorSintactico)
            {

                if (tokenIndice < listaTokens.Count - 1)
                {
                    tokenIndice++;
                    tokenActual = (Token)listaTokens[tokenIndice];
                    if (tokenActual.Descripcion.Equals("S_Punto_y_Coma"))
                    {
                        errorSintactico = false;
                    }
                }
            }
            else
            {
                if (tokenIndice < listaTokens.Count - 1)
                {
                    if (tokenActual.Descripcion.Equals(tipoToken))
                    {
                        tokenIndice++;
                        tokenActual = (Token)listaTokens[tokenIndice];
                        lex = lex + " " + tokenActual.Lexema;
                    }
                    else
                    {
                        this.lex = "ERROR!  se esperaba [" + tipoToken + "] en lugar de [" + tokenActual.Descripcion + ", " + tokenActual.Lexema + "]";
                        ControladorSintactico.Instancia.agregarError(tokenActual.Descripcion, this.lex, tokenActual.Fila, tokenActual.Columna);
                        errorSintactico = true;
                    }
                }
            }

        }

    }

}