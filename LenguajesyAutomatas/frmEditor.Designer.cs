namespace LenguajesyAutomatas
{
    partial class frmEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsrEjecutarAnalizadorLexico = new System.Windows.Forms.ToolStripMenuItem();
			this.tsrEjecutarAnalizadorSintactico = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.editorTexto1 = new LenguajesyAutomatas.EditorTexto();
			this.dgvListaTokens = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dgvErrores = new System.Windows.Forms.DataGridView();
			this.dgvTablaSimbolo = new System.Windows.Forms.DataGridView();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dgvTSAtributos = new System.Windows.Forms.DataGridView();
			this.label5 = new System.Windows.Forms.Label();
			this.dgvTSMetodos = new System.Windows.Forms.DataGridView();
			this.label6 = new System.Windows.Forms.Label();
			this.dgvTSParametrosVariables = new System.Windows.Forms.DataGridView();
			this.validacionDeTiposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvListaTokens)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTablaSimbolo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTSAtributos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTSMetodos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTSParametrosVariables)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.tsrEjecutarAnalizadorLexico,
            this.tsrEjecutarAnalizadorSintactico,
            this.validacionDeTiposToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1060, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// archivoToolStripMenuItem
			// 
			this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.abrirToolStripMenuItem});
			this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
			this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.archivoToolStripMenuItem.Text = "Archivo";
			// 
			// nuevoToolStripMenuItem
			// 
			this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
			this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.nuevoToolStripMenuItem.Text = "Nuevo";
			this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
			// 
			// guardarToolStripMenuItem
			// 
			this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
			this.guardarToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.guardarToolStripMenuItem.Text = "Guardar";
			this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
			// 
			// abrirToolStripMenuItem
			// 
			this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
			this.abrirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.abrirToolStripMenuItem.Text = "Abrir";
			this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
			// 
			// tsrEjecutarAnalizadorLexico
			// 
			this.tsrEjecutarAnalizadorLexico.Name = "tsrEjecutarAnalizadorLexico";
			this.tsrEjecutarAnalizadorLexico.Size = new System.Drawing.Size(53, 20);
			this.tsrEjecutarAnalizadorLexico.Text = "Lexico";
			this.tsrEjecutarAnalizadorLexico.Click += new System.EventHandler(this.tsrEjecutarAnalizadorLexico_Click);
			// 
			// tsrEjecutarAnalizadorSintactico
			// 
			this.tsrEjecutarAnalizadorSintactico.Name = "tsrEjecutarAnalizadorSintactico";
			this.tsrEjecutarAnalizadorSintactico.Size = new System.Drawing.Size(71, 20);
			this.tsrEjecutarAnalizadorSintactico.Text = "Sintactico";
			this.tsrEjecutarAnalizadorSintactico.Click += new System.EventHandler(this.tsrEjecutarAnalizadorSintactico_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(27, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(627, 647);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.editorTexto1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(619, 621);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// editorTexto1
			// 
			this.editorTexto1.Location = new System.Drawing.Point(0, 0);
			this.editorTexto1.Name = "editorTexto1";
			this.editorTexto1.Size = new System.Drawing.Size(613, 603);
			this.editorTexto1.TabIndex = 0;
			// 
			// dgvListaTokens
			// 
			this.dgvListaTokens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgvListaTokens.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dgvListaTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvListaTokens.Location = new System.Drawing.Point(660, 49);
			this.dgvListaTokens.Name = "dgvListaTokens";
			this.dgvListaTokens.Size = new System.Drawing.Size(388, 62);
			this.dgvListaTokens.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(797, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 24);
			this.label1.TabIndex = 3;
			this.label1.Text = "Lista de tokens";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(830, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 24);
			this.label2.TabIndex = 5;
			this.label2.Text = "Errores";
			// 
			// dgvErrores
			// 
			this.dgvErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvErrores.Location = new System.Drawing.Point(660, 141);
			this.dgvErrores.Name = "dgvErrores";
			this.dgvErrores.Size = new System.Drawing.Size(388, 94);
			this.dgvErrores.TabIndex = 4;
			// 
			// dgvTablaSimbolo
			// 
			this.dgvTablaSimbolo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTablaSimbolo.Location = new System.Drawing.Point(656, 265);
			this.dgvTablaSimbolo.Name = "dgvTablaSimbolo";
			this.dgvTablaSimbolo.Size = new System.Drawing.Size(388, 94);
			this.dgvTablaSimbolo.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(811, 238);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 24);
			this.label3.TabIndex = 7;
			this.label3.Text = "TS Clases";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(809, 362);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124, 24);
			this.label4.TabIndex = 9;
			this.label4.Text = "TS Atributos";
			// 
			// dgvTSAtributos
			// 
			this.dgvTSAtributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTSAtributos.Location = new System.Drawing.Point(656, 389);
			this.dgvTSAtributos.Name = "dgvTSAtributos";
			this.dgvTSAtributos.Size = new System.Drawing.Size(388, 73);
			this.dgvTSAtributos.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(811, 465);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(122, 24);
			this.label5.TabIndex = 11;
			this.label5.Text = "TS Metodos";
			// 
			// dgvTSMetodos
			// 
			this.dgvTSMetodos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTSMetodos.Location = new System.Drawing.Point(656, 492);
			this.dgvTSMetodos.Name = "dgvTSMetodos";
			this.dgvTSMetodos.Size = new System.Drawing.Size(388, 73);
			this.dgvTSMetodos.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(753, 570);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(252, 24);
			this.label6.TabIndex = 13;
			this.label6.Text = "TS Parametros y variables";
			// 
			// dgvTSParametrosVariables
			// 
			this.dgvTSParametrosVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTSParametrosVariables.Location = new System.Drawing.Point(656, 597);
			this.dgvTSParametrosVariables.Name = "dgvTSParametrosVariables";
			this.dgvTSParametrosVariables.Size = new System.Drawing.Size(388, 73);
			this.dgvTSParametrosVariables.TabIndex = 12;
			// 
			// validacionDeTiposToolStripMenuItem
			// 
			this.validacionDeTiposToolStripMenuItem.Name = "validacionDeTiposToolStripMenuItem";
			this.validacionDeTiposToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
			this.validacionDeTiposToolStripMenuItem.Text = "Validacion de Tipos";
			this.validacionDeTiposToolStripMenuItem.Click += new System.EventHandler(this.validacionDeTiposToolStripMenuItem_Click);
			// 
			// frmEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(1060, 684);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.dgvTSParametrosVariables);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dgvTSMetodos);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dgvTSAtributos);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dgvTablaSimbolo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dgvErrores);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgvListaTokens);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MaximumSize = new System.Drawing.Size(1076, 723);
			this.MinimumSize = new System.Drawing.Size(1076, 723);
			this.Name = "frmEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmEditor_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvListaTokens)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTablaSimbolo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTSAtributos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTSMetodos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvTSParametrosVariables)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsrEjecutarAnalizadorLexico;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private EditorTexto editorTexto1;
        private System.Windows.Forms.DataGridView dgvListaTokens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.ToolStripMenuItem tsrEjecutarAnalizadorSintactico;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvTablaSimbolo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvTSAtributos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTSMetodos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvTSParametrosVariables;
        private System.Windows.Forms.ToolStripMenuItem validacionDeTiposToolStripMenuItem;
    }
}