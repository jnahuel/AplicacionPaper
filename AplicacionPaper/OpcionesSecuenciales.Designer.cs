namespace AplicacionPaper
{
    partial class OpcionesSecuenciales
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxTiempoDescanso = new System.Windows.Forms.TextBox();
            this.tbxTiempoExcitacion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxTiempoEstudio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbSecuencia = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtn6Opciones = new System.Windows.Forms.RadioButton();
            this.rbtn2Opciones = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBoxSimbolos = new System.Windows.Forms.TextBox();
            this.btnModificarSimbolos = new System.Windows.Forms.Button();
            this.lstSimbolos = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tiempo de descanso [mS]";
            // 
            // tbxTiempoDescanso
            // 
            this.tbxTiempoDescanso.Location = new System.Drawing.Point(170, 17);
            this.tbxTiempoDescanso.Name = "tbxTiempoDescanso";
            this.tbxTiempoDescanso.Size = new System.Drawing.Size(100, 20);
            this.tbxTiempoDescanso.TabIndex = 1;
            // 
            // tbxTiempoExcitacion
            // 
            this.tbxTiempoExcitacion.Location = new System.Drawing.Point(170, 47);
            this.tbxTiempoExcitacion.Name = "tbxTiempoExcitacion";
            this.tbxTiempoExcitacion.Size = new System.Drawing.Size(100, 20);
            this.tbxTiempoExcitacion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tiempo de excitación [mS]";
            // 
            // tbxTiempoEstudio
            // 
            this.tbxTiempoEstudio.Location = new System.Drawing.Point(170, 77);
            this.tbxTiempoEstudio.Name = "tbxTiempoEstudio";
            this.tbxTiempoEstudio.Size = new System.Drawing.Size(100, 20);
            this.tbxTiempoEstudio.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tiempo del estudio [S]";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(337, 177);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(509, 177);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxTiempoDescanso);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxTiempoExcitacion);
            this.groupBox1.Controls.Add(this.tbxTiempoEstudio);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 110);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tiempos";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbSecuencia);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.rbtn6Opciones);
            this.groupBox2.Controls.Add(this.rbtn2Opciones);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(10, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 80);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // cmbSecuencia
            // 
            this.cmbSecuencia.DisplayMember = "0";
            this.cmbSecuencia.FormattingEnabled = true;
            this.cmbSecuencia.Items.AddRange(new object[] {
            "Secuencial",
            "Aleatoria",
            "Caos"});
            this.cmbSecuencia.Location = new System.Drawing.Point(149, 47);
            this.cmbSecuencia.Name = "cmbSecuencia";
            this.cmbSecuencia.Size = new System.Drawing.Size(121, 21);
            this.cmbSecuencia.TabIndex = 4;
            this.cmbSecuencia.ValueMember = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Secuencia";
            // 
            // rbtn6Opciones
            // 
            this.rbtn6Opciones.AutoSize = true;
            this.rbtn6Opciones.Location = new System.Drawing.Point(230, 18);
            this.rbtn6Opciones.Name = "rbtn6Opciones";
            this.rbtn6Opciones.Size = new System.Drawing.Size(31, 17);
            this.rbtn6Opciones.TabIndex = 2;
            this.rbtn6Opciones.Text = "6";
            this.rbtn6Opciones.UseVisualStyleBackColor = true;
            this.rbtn6Opciones.CheckedChanged += new System.EventHandler(this.rbtn6Opciones_CheckedChanged);
            // 
            // rbtn2Opciones
            // 
            this.rbtn2Opciones.AutoSize = true;
            this.rbtn2Opciones.Checked = true;
            this.rbtn2Opciones.Location = new System.Drawing.Point(180, 18);
            this.rbtn2Opciones.Name = "rbtn2Opciones";
            this.rbtn2Opciones.Size = new System.Drawing.Size(31, 17);
            this.rbtn2Opciones.TabIndex = 1;
            this.rbtn2Opciones.TabStop = true;
            this.rbtn2Opciones.Text = "2";
            this.rbtn2Opciones.UseVisualStyleBackColor = true;
            this.rbtn2Opciones.CheckedChanged += new System.EventHandler(this.rbtn2Opciones_CheckedChanged);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxSimbolos);
            this.groupBox3.Controls.Add(this.btnModificarSimbolos);
            this.groupBox3.Controls.Add(this.lstSimbolos);
            this.groupBox3.Location = new System.Drawing.Point(316, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 153);
            this.groupBox3.TabIndex = 17;
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
            this.lstSimbolos.Size = new System.Drawing.Size(166, 108);
            this.lstSimbolos.TabIndex = 6;
            this.lstSimbolos.SelectedIndexChanged += new System.EventHandler(this.lstSimbolos_SelectedIndexChanged);
            // 
            // OpcionesSecuenciales
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(609, 223);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Name = "OpcionesSecuenciales";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion - Secuencial";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxTiempoDescanso;
        private System.Windows.Forms.TextBox tbxTiempoExcitacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxTiempoEstudio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbSecuencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtn6Opciones;
        private System.Windows.Forms.RadioButton rbtn2Opciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBoxSimbolos;
        private System.Windows.Forms.Button btnModificarSimbolos;
        private System.Windows.Forms.ListBox lstSimbolos;
    }
}