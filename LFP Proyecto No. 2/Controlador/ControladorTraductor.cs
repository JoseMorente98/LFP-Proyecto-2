using LFP_Proyecto_No._2.Modelo;
using System;
using System.Collections;

namespace LFP_Proyecto_No._2.Controlador
{
    class ControladorTraductor
    {
        private readonly static ControladorTraductor instancia = new ControladorTraductor();
        /**
        * VARIABLES GLOBALES 
        */
        string cadenaVariable = "";
        string variableActual = "";
        string tabulaciones = "";
        string declaracionFor = "";
        string condicionFor = "";
        string incrementoFor = "";
        string tokenAnterior = "";
        string inicioDeclaracion = "";
        int secuenciaSwitch = 0;
        Boolean anticiparFor = false;
        Token tokenAnalizado = null;
        String tokenInicio = "";
        string tipoVariable = "";
        string variableSwitch = "";
        int iteracionesSwitch = 0;
        int contadorTabInicial = 0;
        int contadorTabulacion = 0;

        private ControladorTraductor()
        {

        }

        public static ControladorTraductor Instancia
        {
            get
            {
                return instancia;
            }
        }

        public void InicializarVariables()
        {
            anticiparFor = false;
            tokenAnalizado = null;
            contadorTabInicial = 0;
            cadenaVariable = "";
            variableActual = "";
            tabulaciones = "";
            declaracionFor = "";
            condicionFor = "";
            incrementoFor = "";
            tokenAnterior = "";
            inicioDeclaracion = "";
            tokenInicio = "";
            tipoVariable = "";
            contadorTabulacion = 0;
            variableSwitch = "";
            secuenciaSwitch = 0;
            iteracionesSwitch = 0;
        }

        public void obtenerLista(ArrayList listaTokens)
        {
            InicializarVariables();
            
            string[] reservadasVariable = { "PR_int", "PR_float", "PR_char", "PR_bool", "PR_boolean", "PR_double",  "PR_string"};
            tipoVariable = "";
            for (int i = 0; i < listaTokens.Count; i++)
            {
                tokenAnalizado = (Token)listaTokens[i];
                //Console.WriteLine("SIGUIENTE" + tokenAnalizado.Lexema + " LEXEMA " + tokenAnalizado.Descripcion);

                /**
                 * TRADUCCION DE SENTENCIA FOR
                 */
                #region TRADUCCION SENTENCIA FOR
                if (tokenAnalizado.Descripcion.Equals("PR_static"))
                {
                    Token t = (Token)listaTokens[i + 3];
                    //Console.WriteLine("PREDECIR PARENTESIS" + t.Lexema);
                    if (t.Lexema.Equals("("))
                    {
                        Token t2 = (Token)listaTokens[i + 4];
                       // Console.WriteLine("PREDECIR PARENTESIS" + t2.Lexema);
                        if (t2.Lexema.Equals(")"))
                        {
                            i += 4;
                        } else
                        {
                            i += 8;
                        }
                    }
                }
                else if (tokenAnalizado.Descripcion.Equals("PR_for"))
                {
                    inicioDeclaracion = tokenAnalizado.Descripcion;
                    declaracionFor = "";
                    condicionFor = "";
                    incrementoFor = "";
                    declaracionFor = "for ";
                    anticiparFor = true;
                    //AsignarVariable("for");
                    for (int j = i + 3; j < listaTokens.Count; j++)
                    {
                        tokenAnalizado = (Token)listaTokens[j];
                        if (tokenAnalizado.Lexema.Equals(";"))
                        {
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            declaracionFor = declaracionFor + tokenAnalizado.Lexema;
                            Console.WriteLine("DECLARACION FOR" + declaracionFor);
                        }
                    }
                    for (int j = i + 7; j < listaTokens.Count; j++)
                    {
                        tokenAnalizado = (Token)listaTokens[j];
                        Console.WriteLine("BANDERA " + tokenAnalizado.Lexema);
                        if(tokenAnalizado.Descripcion.Equals("S_Mayor_Que"))
                        {
                            Console.WriteLine("SI ENTRA A MAYOR");
                            Token t3 = (Token)listaTokens[j + 1];
                            Console.WriteLine("QUE TIENE" +t3.Descripcion);
                            if (t3.Descripcion.Equals("S_Igual"))
                            {
                                Token t2 = (Token)listaTokens[j + 2];
                                if (t2.Descripcion.Equals("Identificador"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                else if (t2.Descripcion.Equals("Digito"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                            }
                            else
                            {
                                if (t3.Descripcion.Equals("Identificador"))
                                {
                                    condicionFor = condicionFor + t3.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                else if (t3.Descripcion.Equals("Digito"))
                                {
                                    condicionFor = condicionFor + t3.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                            }
                            break;
                        } else if (tokenAnalizado.Descripcion.Equals("S_Menor_Que"))
                        {
                            Console.WriteLine("SI ENTRA A MAYOR");
                            Token t3 = (Token)listaTokens[j + 1];
                            Console.WriteLine("QUE TIENE" + t3.Descripcion);
                            if (t3.Descripcion.Equals("S_Igual"))
                            {
                                Token t2 = (Token)listaTokens[j + 2];
                                if (t2.Descripcion.Equals("Identificador"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                else if (t2.Descripcion.Equals("Digito"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                            }
                            else
                            {
                                if (t3.Descripcion.Equals("Identificador"))
                                {
                                    condicionFor = condicionFor + t3.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                else if (t3.Descripcion.Equals("Digito"))
                                {
                                    condicionFor = condicionFor + t3.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                            }
                            break;
                        } else if(tokenAnalizado.Descripcion.Equals("S_Igual"))
                        {
                            Token t = (Token)listaTokens[j + 1];
                            if (t.Descripcion.Equals("S_Igual"))
                            {
                                Token t2 = (Token)listaTokens[i + 2];
                                if (t2.Descripcion.Equals("Identificador"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                else if (t2.Descripcion.Equals("Digito"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                break;
                            }
                        }
                        else if (tokenAnalizado.Descripcion.Equals("S_Excl"))
                        {
                            Token t3 = (Token)listaTokens[j + 1];
                            if (t3.Descripcion.Equals("S_Igual"))
                            {
                                Token t2 = (Token)listaTokens[j + 2];
                                if (t2.Descripcion.Equals("Identificador"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                else if (t2.Descripcion.Equals("Digito"))
                                {
                                    condicionFor = condicionFor + t2.Lexema;
                                    Console.WriteLine("CONDICION FOR" + condicionFor);
                                }
                                break;
                            }
                        }
                        /*switch (tokenAnalizado.Descripcion)
                        {
                            case "S_Igual":
                                Token t = (Token)listaTokens[i + 1];
                                if(t.Descripcion.Equals("S_Igual"))
                                {
                                    Token t2 = (Token)listaTokens[i + 2];
                                    if (t2.Descripcion.Equals("Identificador"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    } else if (t2.Descripcion.Equals("Digito"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                }
                                break;
                            case "S_Mayor_Que":
                                Console.WriteLine("SI ENTRA A MAYOR");
                                Token t3 = (Token)listaTokens[i + 1];
                                if (t3.Descripcion.Equals("S_Igual"))
                                {
                                    Token t2 = (Token)listaTokens[i + 2];
                                    if (t2.Descripcion.Equals("Identificador"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                    else if (t2.Descripcion.Equals("Digito"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                } else
                                {
                                    if (t3.Descripcion.Equals("Identificador"))
                                    {
                                        condicionFor = condicionFor + t3.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                    else if (t3.Descripcion.Equals("Digito"))
                                    {
                                        condicionFor = condicionFor + t3.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                }
                                break;
                            case "S_Menor_Que":
                                Token t4 = (Token)listaTokens[i + 1];
                                if (t4.Descripcion.Equals("S_Igual"))
                                {
                                    Token t2 = (Token)listaTokens[i + 2];
                                    if (t2.Descripcion.Equals("Identificador"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                    else if (t2.Descripcion.Equals("Digito"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                }
                                else
                                {
                                    if (t4.Descripcion.Equals("Identificador"))
                                    {
                                        condicionFor = condicionFor + t4.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                    else if (t4.Descripcion.Equals("Digito"))
                                    {
                                        condicionFor = condicionFor + t4.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                }
                                break;
                            case "S_Excl":
                                Token t5 = (Token)listaTokens[i + 1];
                                if (t5.Descripcion.Equals("S_Igual"))
                                {
                                    Token t2 = (Token)listaTokens[i + 2];
                                    if (t2.Descripcion.Equals("Identificador"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                    else if (t2.Descripcion.Equals("Digito"))
                                    {
                                        condicionFor = condicionFor + t2.Lexema;
                                        Console.WriteLine("CONDICION FOR" + condicionFor);
                                        break;
                                    }
                                }
                                break;
                        }*/
                    }
                    for (int j = i + 11; j < listaTokens.Count; j++)
                    {
                        tokenAnalizado = (Token)listaTokens[j];
                        Console.WriteLine("final token" + tokenAnalizado.Lexema);

                        if (tokenAnalizado.Lexema.Equals(")"))
                        {
                            Console.WriteLine("final " + tokenAnalizado.Lexema);
                            i = i + 10;
                            Console.WriteLine(" token" + tokenAnalizado.Lexema);
                            break;
                        }
                        else
                        {
                            //incrementoFor = incrementoFor + tokenAnalizado.Lexema;
                        }
                    }
                    string[] cont = declaracionFor.Split('=');
                    Console.WriteLine(declaracionFor);
                    Console.WriteLine(cont[0]);
                    Console.WriteLine(cont[1]);
                    ControladorTraduccion.Instancia.agregar(tabulaciones + cont[0] + " in range(" + cont[1] + "," + condicionFor + "):", "for");
                    if (incrementoFor.Contains("++"))
                        {
                            //incrementoFor = incrementoFor.Replace("++", "+=1");
                        }
                        else
                        {
                            ///incrementoFor = incrementoFor.Replace("--", "-=1");
                        }
                    //ControladorTraduccion.Instancia.agregar(tabulaciones + ""+incrementoFor, "for");
                }
                #endregion

                /**
                 * TRADUCCION DE VARIABLES
                 */
                #region TRADUCCION DE VARIABLES
                else if (Array.Exists(reservadasVariable, element => element == tokenAnalizado.Descripcion) || variableActual.Equals("variable"))
                {
                    AsignarVariable("variable");
                    if (tipoVariable.Equals("")){tipoVariable = tokenAnalizado.Lexema; }

                    //VARIABLES
                    if (tokenAnalizado.Descripcion.Equals("Identificador"))
                    {
                        for (int j = i; j < listaTokens.Count; j++)
                        {
                            Token temp = (Token)listaTokens[j];
                            if (temp.Lexema.Equals(";"))
                            {
                                if (((Token)listaTokens[j - 1]).Descripcion.Equals("Identificador") && ((Token)listaTokens[j - 2]).Descripcion.Contains("PR_"))
                                {
                                    EnviarTablaSimbolo(tabulaciones + ((Token)listaTokens[j - 1]).Lexema, tipoVariable, "");
                                }
                                else if (cadenaVariable.Contains(","))
                                {
                                    string[] words = cadenaVariable.Split(',');
                                    for (int n = 0; n < words.Length; n++)
                                    {
                                        if (!words[n].Contains("="))
                                        {
                                            EnviarTablaSimbolo(tabulaciones + words[n], tipoVariable, "");

                                        }
                                        else
                                        {
                                            ControladorTraduccion.Instancia.agregar(tabulaciones + words[n], "variable");
                                        }
                                    }
                                    tipoVariable = "";
                                }
                                else
                                {
                                    ControladorTraduccion.Instancia.agregar(tabulaciones + cadenaVariable, "variable");
                                }
                                i = j;
                                variableActual = "";
                                cadenaVariable = "";
                                break;
                            }
                            else
                            {
                                cadenaVariable = cadenaVariable + temp.Lexema;
                            }
                        }
                    }
                    //ARREGLOS
                    else if (tokenAnalizado.Lexema.Equals("["))
                    {
                        for (int j = i + 2; j < listaTokens.Count; j++)
                        {
                            Token temp = (Token)listaTokens[j];
                            if (temp.Lexema.Equals(";"))
                            {
                                cadenaVariable = cadenaVariable.Replace("{", "[");
                                cadenaVariable = cadenaVariable.Replace("}", "]");
                                ControladorTraduccion.Instancia.agregar(tabulaciones + cadenaVariable, "array");
                                string[] partesArreglo = cadenaVariable.Split('=');
                                EnviarTablaSimbolo(partesArreglo[0], "array", partesArreglo[1]);
                                i = j;
                                variableActual = "";
                                cadenaVariable = "";
                                break;
                            }
                            else
                            {
                                if (temp.Descripcion.Contains("PR_"))
                                {
                                }
                                else
                                {
                                    cadenaVariable = cadenaVariable + temp.Lexema;
                                }
                            }
                        }

                    }
                }
                #endregion

                /**
                 * TRADUCCION SENTENCIA IF 
                 */
                #region TRADUCCION SENTENCIA IF
                else if (tokenAnalizado.Descripcion.Equals("PR_if"))
                {
                    tokenInicio = "if ";
                    anticiparFor = false;
                    for (int j = i+2; j < listaTokens.Count; j++)
                    {
                        tokenAnalizado = (Token)listaTokens[j];
                        if (tokenAnalizado.Lexema.Equals(")"))
                        {
                            tokenInicio = tokenInicio + ":";
                            ControladorTraduccion.Instancia.agregar(tabulaciones + tokenInicio, " if");
                            tokenInicio = "";
                            i = j;
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio +  tokenAnalizado.Lexema;
                        }
                    }
                }
                else if (tokenAnalizado.Descripcion.Equals("PR_else"))
                {
                    anticiparFor = false;
                    ControladorTraduccion.Instancia.agregar(tabulaciones + "else:", "else");
                    anticiparFor = true;
                }
                #endregion

                /**
                 * TRADUCCION SENTENCIA WHILE
                 */
                #region TRADUCCION SENTENCIA WHILE
                else if (tokenAnalizado.Descripcion.Equals("PR_while"))
                {
                    tokenInicio = "while ";
                    anticiparFor = false;
                    for (int j = i + 2; j < listaTokens.Count; j++)
                    {
                        tokenAnalizado = (Token)listaTokens[j];
                        if (tokenAnalizado.Lexema.Equals(")"))
                        {
                            ControladorTraduccion.Instancia.agregar(tabulaciones + tokenInicio+":", "while");
                            tokenInicio = "";
                            i = j;
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + "" + tokenAnalizado.Lexema;
                        }
                    }
                }
                #endregion

                /**
                 * TRADUCCION DE COMENTARIO
                 */
                #region TRADUCCION COMENTARIO
                else if (tokenAnalizado.Descripcion.Equals("ComentarioLinea"))
                {
                    string comment = tokenAnalizado.Lexema;
                    comment = comment.Replace("//", "#");
                    ControladorTraduccion.Instancia.agregar(tabulaciones + comment, "comentario");
                }
                else if (tokenAnalizado.Descripcion.Equals("ComentarioMultiLinea"))
                {
                    string comment = tokenAnalizado.Lexema;
                    comment = comment.Replace("/*", "\'\'\'");
                    comment = comment.Replace("*/", "\'\'\'");
                    ControladorTraduccion.Instancia.agregar(tabulaciones + comment, "comentario");
                }
                #endregion

                /**
                 * TRADUCCION DE CONSOLE PENDIENTE
                 */
                #region TRADUCCION CONSOLE WRITELINE
                if (tokenAnalizado.Descripcion.Equals("PR_Console"))
                {
                    tokenInicio = "print(";
                    anticiparFor = false;
                    for (int j = i + 4; j < listaTokens.Count; j++)
                    {
                        tokenAnalizado = (Token)listaTokens[j];
                        if (tokenAnalizado.Lexema.Equals(";"))
                        {
                            ControladorTraduccion.Instancia.agregar(tabulaciones + tokenInicio + "", "console");
                            tokenInicio = "";
                            i = j;
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + tokenAnalizado.Lexema;
                        }
                    }
                }
                #endregion

                /**
                 * TRADUCCION DE SWITCH
                 */
                #region TRADUCCION SWITCH
                if (tokenAnalizado.Descripcion.Equals("PR_switch"))
                {
                    iteracionesSwitch = 0;
                    contadorTabulacion--;
                    tabularEspacio(contadorTabulacion);
                    variableSwitch = ((Token)listaTokens[i+2]).Lexema;
                }
                else if (tokenAnalizado.Descripcion.Equals("PR_case"))
                {
                    tokenInicio = "";
                    for (int m = i+1; m < listaTokens.Count; m++)
                    {
                        tokenAnalizado = (Token)listaTokens[m];
                        if (tokenAnalizado.Lexema.Equals(":"))
                        {
                            if (iteracionesSwitch == 0)
                            {
                                ControladorTraduccion.Instancia.agregar(tabulaciones + "if " + variableSwitch + "=="  + tokenInicio+":", "switch");
                            }
                            else
                            {
                                ControladorTraduccion.Instancia.agregar(tabulaciones + "elif " + variableSwitch + "=="+ tokenInicio + ":", "switch");
                            }
                            contadorTabulacion++;
                            i = m;
                            tabularEspacio(contadorTabulacion);
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + "" + tokenAnalizado.Lexema;
                        }
                    }
                    iteracionesSwitch++;
                }
                else if (tokenAnalizado.Descripcion.Equals("PR_break"))
                {
                    contadorTabulacion--;
                    tabularEspacio(contadorTabulacion);
                }
                else if (tokenAnalizado.Descripcion.Equals("PR_default"))
                {
                    ControladorTraduccion.Instancia.agregar(tabulaciones + "else:", "switch");
                    contadorTabulacion++;
                    tabularEspacio(contadorTabulacion);
                }
                #endregion

                /**
                 * TRADUCCIONES DE VARIABLES SIN TIPO 
                 */
                #region TRADUCCION SIN TIPO
                else if (tokenAnalizado.Descripcion.Equals("Identificador") && tokenAnterior.Equals("{"))
                {
                    tokenInicio = "";
                    for (int m = i; m < listaTokens.Count; m++)
                    {
                        tokenAnalizado = (Token)listaTokens[m];
                        if (tokenAnalizado.Lexema.Equals(";"))
                        {
                            ControladorTraduccion.Instancia.agregar(tabulaciones + tokenInicio, "declaracion");
                            i = m;
                            tokenInicio = "";
                            break;
                        }
                        else
                        {
                            tokenInicio = tokenInicio + "" + tokenAnalizado.Lexema;
                        }
                    }
                }
                #endregion

                /***
                 * LLAVE IZQUIERDA 
                 */
                #region LLAVE IZQUIERDA
                else if (tokenAnalizado.Descripcion.Equals("S_Llave_Izquierda"))
                {
                    if(contadorTabInicial > 1)
                    {
                        contadorTabulacion++;
                        tokenAnterior = tokenAnalizado.Lexema;
                        tabularEspacio(contadorTabulacion);
                    } else
                    {
                        contadorTabInicial++;
                    }
                }
                #endregion

                /***
                 * LLAVE DERECHA 
                 */
                #region LLAVE DERECHA
                else if (tokenAnalizado.Descripcion.Equals("S_Llave_Derecha"))
                {
                    //Envia hasta el final el aumento del for
                    /*if (inicioDeclaracion.Equals("PR_for") )
                    {
                        Console.WriteLine(incrementoFor);
                        if (incrementoFor.Contains("++"))
                        {

                            incrementoFor = incrementoFor.Replace("++", "+=1");
                        }
                        else
                        {
                            incrementoFor = incrementoFor.Replace("--", "-= 1");
                        }
                        ControladorTraduccion.Instancia.agregar(tabulaciones + ""+incrementoFor, "for");
                    }*/
                    variableSwitch = "";
                    tokenAnterior = "";
                    //contadorTabInicial++;
                    if (contadorTabInicial > 1)
                    {
                        contadorTabulacion--;
                        tabularEspacio(contadorTabulacion);
                    }
                }
                #endregion                
            }
        }

        public void tabularEspacio(int contador)
        {
            tabulaciones = "";
            for (int i = 0; i < contador; i++)
            {
                tabulaciones = tabulaciones + "\t"; 
            }
        }

        public void AsignarVariable(string cadena)
        {
            this.variableActual = cadena;
        }

        public void EnviarTablaSimbolo(string nombre, string tipo, string valor)
        {
            if (tipo.ToLower().Equals("int"))
            {
                ControladorTraduccion.Instancia.agregar(nombre + " = 0", "variable");
                ControladorSimbolo.Instancia.agregarSimbolo(nombre, "0", "Int");
            }
            else if (tipo.ToLower().Equals("string"))
            {
                ControladorTraduccion.Instancia.agregar(nombre + " = " + "\"" + "\"", "variable");
                ControladorSimbolo.Instancia.agregarSimbolo(nombre, "\"" + "\"", "String");
            }
            else if (tipo.ToLower().Equals("float") || tipo.ToLower().Equals("double"))
            {
                ControladorTraduccion.Instancia.agregar(nombre + " = 0.0", "variable");
                ControladorSimbolo.Instancia.agregarSimbolo(nombre, "0.0", "Float");
            }
            else if (tipo.ToLower().Equals("bool") || tipo.ToLower().Equals("boolean"))
            {
                ControladorTraduccion.Instancia.agregar(nombre + " = false", "variable");
                ControladorSimbolo.Instancia.agregarSimbolo(nombre, "fale", "Boolean");
            }
            else if (tipo.ToLower().Equals("char"))
            {
                ControladorTraduccion.Instancia.agregar(nombre + " = ' '", "variable");
                ControladorSimbolo.Instancia.agregarSimbolo(nombre, "' '", "Char");
            }
            else if (tipo.ToLower().Equals("array"))
            {
                ControladorSimbolo.Instancia.agregarSimbolo(nombre, valor, "Array");
            }

        }
    }
}
