namespace AplicacionPaper
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comunicacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secuencialToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionVisualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datosPersonalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reiniciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secuencialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frecuencialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem,
            this.accionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(441, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comunicacionToolStripMenuItem,
            this.secuencialToolStripMenuItem1,
            this.configuracionVisualToolStripMenuItem,
            this.datosPersonalesToolStripMenuItem});
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuracionToolStripMenuItem.Text = "Configuración";
            // 
            // comunicacionToolStripMenuItem
            // 
            this.comunicacionToolStripMenuItem.Name = "comunicacionToolStripMenuItem";
            this.comunicacionToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.comunicacionToolStripMenuItem.Text = "Comunicación";
            this.comunicacionToolStripMenuItem.Click += new System.EventHandler(this.comunicacionToolStripMenuItem_Click);
            // 
            // secuencialToolStripMenuItem1
            // 
            this.secuencialToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secuencialToolStripMenuItem,
            this.frecuencialToolStripMenuItem});
            this.secuencialToolStripMenuItem1.Name = "secuencialToolStripMenuItem1";
            this.secuencialToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.secuencialToolStripMenuItem1.Text = "Tipo de estudio";
            // 
            // configuracionVisualToolStripMenuItem
            // 
            this.configuracionVisualToolStripMenuItem.Name = "configuracionVisualToolStripMenuItem";
            this.configuracionVisualToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.configuracionVisualToolStripMenuItem.Text = "Visual";
            this.configuracionVisualToolStripMenuItem.Click += new System.EventHandler(this.menuConfiguracionVisual_Click);
            // 
            // datosPersonalesToolStripMenuItem
            // 
            this.datosPersonalesToolStripMenuItem.Name = "datosPersonalesToolStripMenuItem";
            this.datosPersonalesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.datosPersonalesToolStripMenuItem.Text = "Datos personales";
            this.datosPersonalesToolStripMenuItem.Click += new System.EventHandler(this.datosPersonalesToolStripMenuItem_Click);
            // 
            // accionesToolStripMenuItem
            // 
            this.accionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarToolStripMenuItem,
            this.detenerToolStripMenuItem,
            this.reiniciarToolStripMenuItem});
            this.accionesToolStripMenuItem.Name = "accionesToolStripMenuItem";
            this.accionesToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.accionesToolStripMenuItem.Text = "Acciones";
            // 
            // iniciarToolStripMenuItem
            // 
            this.iniciarToolStripMenuItem.Name = "iniciarToolStripMenuItem";
            this.iniciarToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.iniciarToolStripMenuItem.Text = "Iniciar";
            this.iniciarToolStripMenuItem.Click += new System.EventHandler(this.iniciarToolStripMenuItem_Click);
            // 
            // detenerToolStripMenuItem
            // 
            this.detenerToolStripMenuItem.Name = "detenerToolStripMenuItem";
            this.detenerToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.detenerToolStripMenuItem.Text = "Detener";
            this.detenerToolStripMenuItem.Click += new System.EventHandler(this.detenerToolStripMenuItem_Click);
            // 
            // reiniciarToolStripMenuItem
            // 
            this.reiniciarToolStripMenuItem.Name = "reiniciarToolStripMenuItem";
            this.reiniciarToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.reiniciarToolStripMenuItem.Text = "Reiniciar";
            this.reiniciarToolStripMenuItem.Click += new System.EventHandler(this.reiniciarToolStripMenuItem_Click);
            // 
            // secuencialToolStripMenuItem
            // 
            this.secuencialToolStripMenuItem.Name = "secuencialToolStripMenuItem";
            this.secuencialToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.secuencialToolStripMenuItem.Text = "Secuencial";
            this.secuencialToolStripMenuItem.Click += new System.EventHandler(this.secuencialToolStripMenuItem_Click);
            // 
            // frecuencialToolStripMenuItem
            // 
            this.frecuencialToolStripMenuItem.Name = "frecuencialToolStripMenuItem";
            this.frecuencialToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.frecuencialToolStripMenuItem.Text = "Frecuencial";
            this.frecuencialToolStripMenuItem.Click += new System.EventHandler(this.frecuencialToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 324);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pantalla principal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comunicacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionVisualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reiniciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datosPersonalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secuencialToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem secuencialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frecuencialToolStripMenuItem;
    }
}

