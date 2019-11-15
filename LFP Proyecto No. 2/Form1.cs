using LFP_Proyecto_No._2.Analizador;
using LFP_Proyecto_No._2.Controlador;
using LFP_Proyecto_No._2.Modelo;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace LFP_Proyecto_No._2
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            LimpiarAnalizar();
            AnalizadorLexico.Instancia.Analizador(editorTexto.Text);
            if (ControladorToken.Instancia.ArrayListErrors.Count == 0)
            {
                AnalizadorSintactico.Instancia.obtenerLista(ControladorToken.Instancia.ArrayListTokens);
                if(ControladorSintactico.Instancia.ArrayListSintactico.Count == 0)
                {
                    ControladorTraductor.Instancia.obtenerLista(ControladorToken.Instancia.ArrayListTokens);
                    foreach (Traduccion t in ControladorTraduccion.Instancia.ArrayListTraduccion)
                    {
                        Console.WriteLine(t.Cadena);
                        traduccion.AppendText(t.Cadena + "\n");
                    }
                    try
                    {
                        consola.Text = EjecutarCodigo(editorTexto.Text);
                    }
                    catch (Exception)
                    {

                    }
                    GuardarPython();
                } else
                {
                    this.consola.AppendText("Existen errores sintacticos D:");
                    ControladorReporte.Instancia.getReporteError();
                }
            }
            else
            {
                this.consola.AppendText("Existen errores lexicos D:");
                ControladorReporte.Instancia.getReportTokensError();
            }
        }

        private void ReporteTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControladorReporte.Instancia.getReportTokens();
        }

        private void ReporteDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControladorReporte.Instancia.getReportTokensError();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * LIMPIAR VARIABLES 
         */
         public void LimpiarAnalizar()
        {
            traduccion.Text = "";
            consola.Text = "";
            ControladorToken.Instancia.clearArrayList();
            ControladorSimbolo.Instancia.clearArrayListSimbolo();
            ControladorSintactico.Instancia.clearArrayListSintactico();
            ControladorTraduccion.Instancia.clearArrayListTraduccion();
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "cs files (*.cs)|*.cs";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }

            if (File.Exists(filePath))
            {
                StreamReader streamReader = new StreamReader(filePath);
                string line;
                editorTexto.Text = "";
                try
                {
                    line = streamReader.ReadLine();
                    while (line != null)
                     {
                            editorTexto.AppendText(line + "\n");
                            line = streamReader.ReadLine();
                     }
                    editorTexto.AppendText("\n");
                }
                catch (Exception)
                {
                    alertMessage("Ha ocurrido un error D:");
                }
                streamReader.Close();
            }
        }

        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReporteDeSimbolosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControladorReporte.Instancia.getTablaSimbolos();
        }

        private void ReporteDeErroresSintacticosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControladorReporte.Instancia.getReporteError();
        }

        private void GenerarGraficaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControladorGrafo.Instancia.generarTexto(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void ManualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath + "\\[LFP]ManualTecnicoProyecto2_201801237.pdf";
            System.Diagnostics.Process.Start(appPath);
        }

        private void ManualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath + "\\[LFP]ManualUsuarioProyecto2_201801237.pdf";
            System.Diagnostics.Process.Start(appPath);
        }

        private void AcercaDeProyectoNo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nombre: José Rafael Morente González\n" +
                "Carnet: 201801237\n" +
                "Curso: Lenguajes Foramales y de Programación\n" +
                "Seccion: B-", "Información del Estudiante",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerarTraducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LimpiarAnalizar();
            AnalizadorLexico.Instancia.Analizador(editorTexto.Text);
            if (ControladorToken.Instancia.ArrayListErrors.Count == 0)
            {
                AnalizadorSintactico.Instancia.obtenerLista(ControladorToken.Instancia.ArrayListTokens);
                if (ControladorSintactico.Instancia.ArrayListSintactico.Count == 0)
                {
                    ControladorTraductor.Instancia.obtenerLista(ControladorToken.Instancia.ArrayListTokens);
                    foreach (Traduccion t in ControladorTraduccion.Instancia.ArrayListTraduccion)
                    {
                        Console.WriteLine(t.Cadena);
                        traduccion.AppendText(t.Cadena + "\n");
                    }
                    try
                    {
                        consola.Text = EjecutarCodigo(editorTexto.Text);
                    }
                    catch (Exception)
                    {

                    }
                    GuardarPython();
                }
                else
                {
                    this.consola.AppendText("Existen errores sintacticos D:");
                    ControladorReporte.Instancia.getReporteError();
                }
            }
            else
            {
                this.consola.AppendText("Existen errores lexicos D:");
                ControladorReporte.Instancia.getReportTokensError();
            }
        }

        public string EjecutarCodigo(string entrada)
        {
            CompilerParameters compilerParameters = new CompilerParameters();

            compilerParameters.GenerateInMemory = true;
            compilerParameters.TreatWarningsAsErrors = false;
            compilerParameters.GenerateExecutable = false;
            compilerParameters.CompilerOptions = "/optimize";

            string[] references = { "System.dll" };
            compilerParameters.ReferencedAssemblies.AddRange(references);

            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("using System;\n");
            stringBuilder.Append("using System.Collections.Generic;\n");
            stringBuilder.Append("using System.Text;\n");
            stringBuilder.Append("using System.Threading.Tasks;\n");

            stringBuilder.Append("namespace Proyecto2{ \n");
            stringBuilder.Append("}\n");
            stringBuilder.Append(entrada);
            stringBuilder.Replace("static", "static public");
            stringBuilder.Replace("string[] args", "");

            CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();
            CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());

            if (compilerResults.Errors.HasErrors)
            {
                string text = "Compile error: ";
                foreach (CompilerError compilerError in compilerResults.Errors)
                {
                    text += "rn" + compilerError.ToString();
                }
                throw new Exception(text);
            }

            Module module = compilerResults.CompiledAssembly.GetModules()[0];
            Type type = null;
            MethodInfo methodInfo = null;

            if (module != null)
            {
                type = module.GetType("MyProgram");
            }

            if (type != null)
            {
                methodInfo = type.GetMethod("Main");
            }

            if (methodInfo != null)
            {
                StringWriter stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                Console.WriteLine(methodInfo.Invoke(null, null));
                string consoleOutput = stringWriter.ToString();

                return consoleOutput;
            }

            return "";
        }

        public void ExpoloreAssembly(Assembly assembly)
        {
            Console.WriteLine("Modules in the assembly:");
            foreach (Module m in assembly.GetModules())
            {
                Console.WriteLine("{0}", m);

                foreach (Type t in m.GetTypes())
                {
                    Console.WriteLine("t{0}", t.Name);

                    foreach (MethodInfo mi in t.GetMethods())
                    {
                        Console.WriteLine("tt{0}", mi.Name);
                    }
                }
            }
        }

        private void AbrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save CS Files";
            saveFileDialog.DefaultExt = "cs";
            saveFileDialog.Filter = "CS Files (*.cs)|*.cs";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String dir = saveFileDialog.FileName;
                StreamWriter streamWriter = new StreamWriter(@dir);
                try
                {
                    streamWriter.WriteLine(editorTexto.Text);
                    streamWriter.WriteLine("\n");
                }
                catch
                {
                    alertMessage("Ha ocurrido un error D:");
                }
                streamWriter.Close();
            }
        }

        //GUARDAR COMO
        public void GuardarPython()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save PY Files";
            saveFileDialog.DefaultExt = "py";
            saveFileDialog.Filter = "PY Files (*.py)|*.py";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String dir = saveFileDialog.FileName;
                StreamWriter streamWriter = new StreamWriter(@dir);
                try
                {
                    streamWriter.WriteLine(traduccion.Text);
                    streamWriter.WriteLine("\n");
                }
                catch
                {
                    alertMessage("Ha ocurrido un error D:");
                }
                streamWriter.Close();
            }
        }
    }
}
