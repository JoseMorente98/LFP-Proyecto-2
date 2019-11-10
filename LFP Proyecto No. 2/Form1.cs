using LFP_Proyecto_No._2.Analizador;
using LFP_Proyecto_No._2.Controlador;
using LFP_Proyecto_No._2.Modelo;
using LFP_Proyecto_No.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TokenControlador.Instancia.resetClass();
            SimboloControlador.Instancia.clearArrayListSimbolo();
            SintacticoControlador.Instancia.clearArrayListSintactico();
            AnalizadorLexico.Instancia.analizador_Lexico(editorTexto.Text);
            foreach (Token item in TokenControlador.Instancia.getArrayListTokens())
            {
                Console.WriteLine(item.Descripcion);
            }
            if (TokenControlador.Instancia.getArrayListErrors().Count == 0)
            {
                AnalizadorSintactico.Instancia.obtenerLista(TokenControlador.Instancia.getArrayListTokens());
                this.consola.Text = "";
                this.consola.AppendText(AnalizadorSintactico.Instancia.returnT());
                //TraductorControlador.Instancia.obtenerLista(TokenControlador.Instancia.getArrayListTokens());
                //this.traduccion.Text = "";
                //traduccion.AppendText(TraductorControlador.Instancia.getTokensTraducidos());
            }
            else
            {
                this.consola.AppendText("Existen errores lexicos.");
            }

            
        }

        private void ReporteTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteControlador.Instancia.getReportTokens();
        }

        private void ReporteDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
            ReporteControlador.Instancia.getTablaSimbolos();
        }

        private void ReporteDeErroresSintacticosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteControlador.Instancia.getReporteError();
        }
    }
}
