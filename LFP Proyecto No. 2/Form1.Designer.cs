namespace LFP_Proyecto_No._2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AbrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarTraducciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeErroresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.editorTexto = new System.Windows.Forms.RichTextBox();
            this.traduccion = new System.Windows.Forms.RichTextBox();
            this.consola = new System.Windows.Forms.RichTextBox();
            this.reporteDeSimbolosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeErroresSintacticosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.documentoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1240, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AbrirToolStripMenuItem,
            this.abrirArchivoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // AbrirToolStripMenuItem
            // 
            this.AbrirToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.AbrirToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.AbrirToolStripMenuItem.Name = "AbrirToolStripMenuItem";
            this.AbrirToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.AbrirToolStripMenuItem.Text = "Abrir";
            this.AbrirToolStripMenuItem.Click += new System.EventHandler(this.AbrirToolStripMenuItem_Click);
            // 
            // abrirArchivoToolStripMenuItem
            // 
            this.abrirArchivoToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.abrirArchivoToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            this.abrirArchivoToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.abrirArchivoToolStripMenuItem.Text = "Guardar Como";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // documentoToolStripMenuItem
            // 
            this.documentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarTraducciónToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.documentoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.documentoToolStripMenuItem.Name = "documentoToolStripMenuItem";
            this.documentoToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.documentoToolStripMenuItem.Text = "Documento";
            // 
            // generarTraducciónToolStripMenuItem
            // 
            this.generarTraducciónToolStripMenuItem.Name = "generarTraducciónToolStripMenuItem";
            this.generarTraducciónToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.generarTraducciónToolStripMenuItem.Text = "Generar Traducción";
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteTokensToolStripMenuItem,
            this.reporteDeErroresToolStripMenuItem,
            this.reporteDeSimbolosToolStripMenuItem,
            this.reporteDeErroresSintacticosToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // reporteTokensToolStripMenuItem
            // 
            this.reporteTokensToolStripMenuItem.Name = "reporteTokensToolStripMenuItem";
            this.reporteTokensToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.reporteTokensToolStripMenuItem.Text = "Reporte de Tokens";
            this.reporteTokensToolStripMenuItem.Click += new System.EventHandler(this.ReporteTokensToolStripMenuItem_Click);
            // 
            // reporteDeErroresToolStripMenuItem
            // 
            this.reporteDeErroresToolStripMenuItem.Name = "reporteDeErroresToolStripMenuItem";
            this.reporteDeErroresToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.reporteDeErroresToolStripMenuItem.Text = "Reporte de Errores";
            this.reporteDeErroresToolStripMenuItem.Click += new System.EventHandler(this.ReporteDeErroresToolStripMenuItem_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(20, 92);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(101, 20);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Editor de Texto";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(710, 92);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(76, 20);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Traducción";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(23, 518);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(58, 20);
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Consola";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(629, 386);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 7;
            this.metroButton1.Text = "Analizar";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton1.Click += new System.EventHandler(this.MetroButton1_Click);
            // 
            // editorTexto
            // 
            this.editorTexto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.editorTexto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editorTexto.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorTexto.ForeColor = System.Drawing.Color.White;
            this.editorTexto.Location = new System.Drawing.Point(23, 115);
            this.editorTexto.Name = "editorTexto";
            this.editorTexto.Size = new System.Drawing.Size(600, 400);
            this.editorTexto.TabIndex = 8;
            this.editorTexto.Text = "";
            // 
            // traduccion
            // 
            this.traduccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.traduccion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.traduccion.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traduccion.ForeColor = System.Drawing.Color.White;
            this.traduccion.Location = new System.Drawing.Point(710, 115);
            this.traduccion.Name = "traduccion";
            this.traduccion.Size = new System.Drawing.Size(550, 582);
            this.traduccion.TabIndex = 9;
            this.traduccion.Text = "";
            // 
            // consola
            // 
            this.consola.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.consola.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consola.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consola.ForeColor = System.Drawing.Color.White;
            this.consola.Location = new System.Drawing.Point(23, 541);
            this.consola.Name = "consola";
            this.consola.Size = new System.Drawing.Size(600, 156);
            this.consola.TabIndex = 10;
            this.consola.Text = "";
            // 
            // reporteDeSimbolosToolStripMenuItem
            // 
            this.reporteDeSimbolosToolStripMenuItem.Name = "reporteDeSimbolosToolStripMenuItem";
            this.reporteDeSimbolosToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.reporteDeSimbolosToolStripMenuItem.Text = "Reporte de Simbolos";
            this.reporteDeSimbolosToolStripMenuItem.Click += new System.EventHandler(this.ReporteDeSimbolosToolStripMenuItem_Click);
            // 
            // reporteDeErroresSintacticosToolStripMenuItem
            // 
            this.reporteDeErroresSintacticosToolStripMenuItem.Name = "reporteDeErroresSintacticosToolStripMenuItem";
            this.reporteDeErroresSintacticosToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.reporteDeErroresSintacticosToolStripMenuItem.Text = "Reporte de Errores Sintacticos";
            this.reporteDeErroresSintacticosToolStripMenuItem.Click += new System.EventHandler(this.ReporteDeErroresSintacticosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.consola);
            this.Controls.Add(this.traduccion);
            this.Controls.Add(this.editorTexto);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Style = MetroFramework.MetroColorStyle.White;
            this.Text = "Proyecto No. 2";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AbrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.ToolStripMenuItem documentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarTraducciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeErroresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.RichTextBox traduccion;
        private System.Windows.Forms.RichTextBox consola;
        private System.Windows.Forms.RichTextBox editorTexto;
        private System.Windows.Forms.ToolStripMenuItem reporteDeSimbolosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeErroresSintacticosToolStripMenuItem;
    }
}

