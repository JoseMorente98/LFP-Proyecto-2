using LFP_Proyecto_No._2.Analizador;
using LFP_Proyecto_No._2.Controlador;
using LFP_Proyecto_No._2.Modelo;
using System;
using System.IO;
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
            AnalizadorLexico.Instancia.analizador_Lexico(editorTexto.Text);
            if (ControladorToken.Instancia.ArrayListErrors.Count == 0)
            {
                AnalizadorSintactico.Instancia.obtenerLista(ControladorToken.Instancia.ArrayListTokens);
                consola.AppendText(AnalizadorSintactico.Instancia.returnT());
                Console.WriteLine("SINTACTICO" + ControladorSintactico.Instancia.ArrayListSintactico.Count);
                if(ControladorSintactico.Instancia.ArrayListSintactico.Count == 0)
                {
                    ControladorTraductor.Instancia.obtenerLista(ControladorToken.Instancia.ArrayListTokens);
                    Console.WriteLine("el arreglo traduccion es " + ControladorTraduccion.Instancia.ArrayListTraduccion.Count);
                    
                    foreach (Traduccion t in ControladorTraduccion.Instancia.ArrayListTraduccion)
                    {
                        Console.WriteLine(t.Cadena);
                        traduccion.AppendText(t.Cadena + "\n");
                    }
                } else
                {

                }
            }
            else
            {
                this.consola.AppendText("Verifica los errores lexicos D:");
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
    }
}
