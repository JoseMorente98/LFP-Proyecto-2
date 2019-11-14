using LFP_Proyecto_No._2.Modelo;
using System.Collections;
using System.Diagnostics;

namespace LFP_Proyecto_No._2.Controlador
{
    class ControladorGrafo
    {
        private readonly static ControladorGrafo instancia = new ControladorGrafo();
        public static ControladorGrafo Instancia
        {
            get
            {
                return instancia;
            }
        }

        /**
        *  GENERAR TEXTO
        **/
        public void generarTexto(string path)
        {
            ArrayList arrayListSimbolos = ControladorSimbolo.Instancia.ArrayListSimbolo;
            string cuerpoNodos = "";
            string ordenNodos = "";

            for (int i = 0; i  < arrayListSimbolos.Count; i++)
            {
                Simbolo simbolo = (Simbolo)arrayListSimbolos[i];
                if (simbolo.Tipo.Equals("Array"))
                {
                    if (!simbolo.Valor.Equals("[]"))
                    {
                        string valores = simbolo.Valor;
                        valores = valores.Replace('[', ' ');
                        valores = valores.Replace(']', ' ');

                        string[] contenido = valores.Trim().Split(',');

                        for (int j = 0; j < contenido.Length; j++)
                        {
                            cuerpoNodos = cuerpoNodos + contenido[j] + "[shape = box, color = blue] " + "\n";
                            ordenNodos = ordenNodos + contenido[j] + "->";
                        }
                        generarGrafo(cuerpoNodos, ordenNodos, simbolo.Nombre, path);
                        cuerpoNodos = ordenNodos = "";
                    }
                }
            }
         
        }

        /**
         *  GENERAR GRAFO
         **/
        public void generarGrafo(string cuerpo, string orden, string nombre, string path)
        {
            string texto =
            "digraph G {" +
              "rankdir = LR " + "\n" +
              "Inicio[shape = box, color = blue] "+ "\n" +
              cuerpo + "\n" +
              "Fin[shape = box, color = blue] " +
              "Inicio->" + orden + "Fin"+
             "}";

            System.IO.File.WriteAllText(path + "\\" + nombre + ".dot", texto);
            var command = "dot -Tpng \"" + path + "\\" + nombre + ".dot\"  -o \"" + path + "\\" + nombre + ".png\"   ";
            var processStartInfo = new ProcessStartInfo("cmd", "/C" + command);
            var process = new System.Diagnostics.Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}
