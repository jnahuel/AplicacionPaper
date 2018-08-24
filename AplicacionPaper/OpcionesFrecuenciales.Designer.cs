namespace AplicacionPaper
{
    partial class OpcionesFrecuenciales
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBoxTiempoEstudio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numOpciones = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lstFrecuencias = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxFrecuencias = new System.Windows.Forms.TextBox();
            this.btnModificarFrecuencias = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBoxSimbolos = new System.Windows.Forms.TextBox();
            this.btnModificarSimbolos = new System.Windows.Forms.Button();
            this.lstSimbolos = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOpciones)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBoxTiempoEstudio);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numOpciones);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(10, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 80);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // txtBoxTiempoEstudio
            // 
            this.txtBoxTiempoEstudio.Location = new System.Drawing.Point(152, 47);
            this.txtBoxTiempoEstudio.Name = "txtBoxTiempoEstudio";
            this.txtBoxTiempoEstudio.Size = new System.Drawing.Size(100, 20);
            this.txtBoxTiempoEstudio.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tiempo del estudio [S]";
            // 
            // numOpciones
            // 
            this.numOpciones.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numOpciones.Location = new System.Drawing.Point(207, 18);
            this.numOpciones.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOpciones.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numOpciones.Name = "numOpciones";
            this.numOpciones.Size = new System.Drawing.Size(45, 20);
            this.numOpciones.TabIndex = 5;
            this.numOpciones.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numOpciones.ValueChanged += new System.EventHandler(this.numOpciones_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cantidad de opciones";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(493, 198);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(398, 198);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lstFrecuencias
            // 
            this.lstFrecuencias.FormattingEnabled = true;
            this.lstFrecuencias.Location = new System.Drawing.Point(21, 19);
            this.lstFrecuencias.Name = "lstFrecuencias";
            this.lstFrecuencias.Size = new System.Drawing.Size(166, 134);
            this.lstFrecuencias.TabIndex = 6;
            this.lstFrecuencias.SelectedIndexChanged += new System.EventHandler(this.lstFrecuencias_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxFrecuencias);
            this.groupBox1.Controls.Add(this.btnModificarFrecuencias);
            this.groupBox1.Controls.Add(this.lstFrecuencias);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 168);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frecuencias";
            // 
            // txtBoxFrecuencias
            // 
            this.txtBoxFrecuencias.Location = new System.Drawing.Point(193, 48);
            this.txtBoxFrecuencias.Name = "txtBoxFrecuencias";
            this.txtBoxFrecuencias.Size = new System.Drawing.Size(75, 20);
            this.txtBoxFrecuencias.TabIndex = 10;
            // 
            // btnModificarFrecuencias
            // 
            this.btnModificarFrecuencias.Location = new System.Drawing.Point(193, 19);
            this.btnModificarFrecuencias.Name = "btnModificarFrecuencias";
            this.btnModificarFrecuencias.Size = new System.Drawing.Size(75, 23);
            this.btnModificarFrecuencias.TabIndex = 9;
            this.btnModificarFrecuencias.Text = "Modificar";
            this.btnModificarFrecuencias.UseVisualStyleBackColor = true;
            this.btnModificarFrecuencias.Click += new System.EventHandler(this.btnModificarFrecuencias_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxSimbolos);
            this.groupBox3.Controls.Add(this.btnModificarSimbolos);
            this.groupBox3.Controls.Add(this.lstSimbolos);
            this.groupBox3.Location = new System.Drawing.Point(300, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 168);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Simbolos";
            // 
            // txtBoxSimbolos
            // 
            this.txtBoxSimbolos.Location = new System.Drawing.Point(193, 48);
            this.txtBoxSimbolos.Name = "txtBoxSimbolos";
            this.txtBoxSimbolos.Size = new System.Drawing.Size(75, 20);
            this.txtBoxSimbolos.TabIndex = 10;
            // 
            // btnModificarSimbolos
            // 
            this.btnModificarSimbolos.Location = new System.Drawing.Point(193, 19);
            this.btnModificarSimbolos.Name = "btnModificarSimbolos";
            this.btnModificarSimbolos.Size = new System.Drawing.Size(75, 23);
            this.btnModificarSimbolos.TabIndex = 9;
            this.btnModificarSimbolos.Text = "Modificar";
            this.btnModificarSimbolos.UseVisualStyleBackColor = true;
            this.btnModificarSimbolos.Click += new System.EventHandler(this.btnModificarSimbolos_Click);
            // 
            // lstSimbolos
            // 
            this.lstSimbolos.FormattingEnabled = true;
            this.lstSimbolos.Location = new System.Drawing.Point(21, 19);
            this.lstSimbolos.Name = "lstSimbolos";
            this.lstSimbolos.Size = new System.Drawing.Size(166, 134);
            this.lstSimbolos.TabIndex = 6;
            this.lstSimbolos.SelectedIndexChanged += new System.EventHandler(this.lstSimbolos_SelectedIndexChanged);
            // 
            // OpcionesFrecuenciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 274);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Name = "OpcionesFrecuenciales";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion - Frecuencial";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOpciones)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.NumericUpDown numOpciones;
        private System.Windows.Forms.ListBox lstFrecuencias;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBoxFrecuencias;
        private System.Windows.Forms.Button btnModificarFrecuencias;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBoxSimbolos;
        private System.Windows.Forms.Button btnModificarSimbolos;
        private System.Windows.Forms.ListBox lstSimbolos;
        private System.Windows.Forms.TextBox txtBoxTiempoEstudio;
        private System.Windows.Forms.Label label1;
    }
}