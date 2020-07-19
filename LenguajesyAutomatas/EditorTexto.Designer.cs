namespace LenguajesyAutomatas
{
    partial class EditorTexto
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picNumeracion = new System.Windows.Forms.PictureBox();
            this.rtxTexto = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picNumeracion)).BeginInit();
            this.SuspendLayout();
            // 
            // picNumeracion
            // 
            this.picNumeracion.Location = new System.Drawing.Point(5, 10);
            this.picNumeracion.Name = "picNumeracion";
            this.picNumeracion.Size = new System.Drawing.Size(53, 587);
            this.picNumeracion.TabIndex = 9;
            this.picNumeracion.TabStop = false;
            this.picNumeracion.Paint += new System.Windows.Forms.PaintEventHandler(this.picNumeracion_Paint);
            // 
            // rtxTexto
            // 
            this.rtxTexto.AcceptsTab = true;
            this.rtxTexto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.rtxTexto.Location = new System.Drawing.Point(59, 8);
            this.rtxTexto.Name = "rtxTexto";
            this.rtxTexto.Size = new System.Drawing.Size(557, 587);
            this.rtxTexto.TabIndex = 8;
            this.rtxTexto.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // EditorTexto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picNumeracion);
            this.Controls.Add(this.rtxTexto);
            this.Name = "EditorTexto";
            this.Size = new System.Drawing.Size(623, 603);
            this.Load += new System.EventHandler(this.EditorTexto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picNumeracion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picNumeracion;
        private System.Windows.Forms.RichTextBox rtxTexto;
        private System.Windows.Forms.Timer timer1;
    }
}
