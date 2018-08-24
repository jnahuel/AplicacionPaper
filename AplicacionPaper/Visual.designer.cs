namespace AplicacionPaper
{
    partial class Visual
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTamanioLetra = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rdBtnDosFilas = new System.Windows.Forms.RadioButton();
            this.rdBtnUnaFila = new System.Windows.Forms.RadioButton();
            this.cmbColoresLetraResaltada = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbColoresLetraNormal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbColoresFondo = new System.Windows.Forms.ComboBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.panelMuestra = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            this.panelMuestra.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color de fondo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbTamanioLetra);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.rdBtnDosFilas);
            this.groupBox2.Controls.Add(this.rdBtnUnaFila);
            this.groupBox2.Controls.Add(this.cmbColoresLetraResaltada);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbColoresLetraNormal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbColoresFondo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(566, 88);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parámetros";
            // 
            // cmbTamanioLetra
            // 
            this.cmbTamanioLetra.FormattingEnabled = true;
            this.cmbTamanioLetra.Location = new System.Drawing.Point(454, 42);
            this.cmbTamanioLetra.Name = "cmbTamanioLetra";
            this.cmbTamanioLetra.Size = new System.Drawing.Size(96, 21);
            this.cmbTamanioLetra.TabIndex = 11;
            this.cmbTamanioLetra.SelectedIndexChanged += new System.EventHandler(this.cmbTamanioLetra_SelectedIndexChanged);
            this.cmbTamanioLetra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.detectarTeclasEspeciales);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tamaño de letra";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Filas";
            // 
            // rdBtnDosFilas
            // 
            this.rdBtnDosFilas.AutoSize = true;
            this.rdBtnDosFilas.Location = new System.Drawing.Point(413, 43);
            this.rdBtnDosFilas.Name = "rdBtnDosFilas";
            this.rdBtnDosFilas.Size = new System.Drawing.Size(31, 17);
            this.rdBtnDosFilas.TabIndex = 8;
            this.rdBtnDosFilas.Text = "2";
            this.rdBtnDosFilas.UseVisualStyleBackColor = true;
            this.rdBtnDosFilas.CheckedChanged += new System.EventHandler(this.rdBtnDosFilas_CheckedChanged);
            this.rdBtnDosFilas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.detectarTeclasEspeciales);
            // 
            // rdBtnUnaFila
            // 
            this.rdBtnUnaFila.AutoSize = true;
            this.rdBtnUnaFila.Checked = true;
            this.rdBtnUnaFila.Location = new System.Drawing.Point(376, 43);
            this.rdBtnUnaFila.Name = "rdBtnUnaFila";
            this.rdBtnUnaFila.Size = new System.Drawing.Size(31, 17);
            this.rdBtnUnaFila.TabIndex = 7;
            this.rdBtnUnaFila.TabStop = true;
            this.rdBtnUnaFila.Text = "1";
            this.rdBtnUnaFila.UseVisualStyleBackColor = true;
            this.rdBtnUnaFila.CheckedChanged += new System.EventHandler(this.rdBtnUnaFila_CheckedChanged);
            this.rdBtnUnaFila.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.detectarTeclasEspeciales);
            // 
            // cmbColoresLetraResaltada
            // 
            this.cmbColoresLetraResaltada.FormattingEnabled = true;
            this.cmbColoresLetraResaltada.Location = new System.Drawing.Point(250, 42);
            this.cmbColoresLetraResaltada.Name = "cmbColoresLetraResaltada";
            this.cmbColoresLetraResaltada.Size = new System.Drawing.Size(96, 21);
            this.cmbColoresLetraResaltada.TabIndex = 6;
            this.cmbColoresLetraResaltada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.detectarTeclasEspeciales);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Color de letra resaltada";
            // 
            // cmbColoresLetraNormal
            // 
            this.cmbColoresLetraNormal.FormattingEnabled = true;
            this.cmbColoresLetraNormal.Location = new System.Drawing.Point(129, 42);
            this.cmbColoresLetraNormal.Name = "cmbColoresLetraNormal";
            this.cmbColoresLetraNormal.Size = new System.Drawing.Size(96, 21);
            this.cmbColoresLetraNormal.TabIndex = 4;
            this.cmbColoresLetraNormal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.detectarTeclasEspeciales);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Color de letra normal";
            // 
            // cmbColoresFondo
            // 
            this.cmbColoresFondo.FormattingEnabled = true;
            this.cmbColoresFondo.Location = new System.Drawing.Point(19, 42);
            this.cmbColoresFondo.Name = "cmbColoresFondo";
            this.cmbColoresFondo.Size = new System.Drawing.Size(96, 21);
            this.cmbColoresFondo.TabIndex = 2;
            this.cmbColoresFondo.SelectedIndexChanged += new System.EventHandler(this.cmbColoresFondo_SelectedIndexChanged);
            this.cmbColoresFondo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.detectarTeclasEspeciales);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(207, 465);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(311, 465);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(6, 7);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(250, 150);
            this.btnA.TabIndex = 1;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            // 
            // btnB
            // 
            this.btnB.Location = new System.Drawing.Point(254, 155);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(250, 150);
            this.btnB.TabIndex = 2;
            this.btnB.Text = "D";
            this.btnB.UseVisualStyleBackColor = true;
            // 
            // btnC
            // 
            this.btnC.Location = new System.Drawing.Point(6, 155);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(250, 150);
            this.btnC.TabIndex = 3;
            this.btnC.Text = "C";
            this.btnC.UseVisualStyleBackColor = true;
            // 
            // btnD
            // 
            this.btnD.Location = new System.Drawing.Point(254, 7);
            this.btnD.Name = "btnD";
            this.btnD.Size = new System.Drawing.Size(250, 150);
            this.btnD.TabIndex = 4;
            this.btnD.Text = "B";
            this.btnD.UseVisualStyleBackColor = true;
            // 
            // panelMuestra
            // 
            this.panelMuestra.Controls.Add(this.btnA);
            this.panelMuestra.Controls.Add(this.btnD);
            this.panelMuestra.Controls.Add(this.btnC);
            this.panelMuestra.Controls.Add(this.btnB);
            this.panelMuestra.Location = new System.Drawing.Point(12, 106);
            this.panelMuestra.Name = "panelMuestra";
            this.panelMuestra.Size = new System.Drawing.Size(566, 353);
            this.panelMuestra.TabIndex = 8;
            // 
            // Visual
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(590, 505);
            this.Controls.Add(this.panelMuestra);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.Name = "Visual";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración visual";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Visual_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelMuestra.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.RadioButton rdBtnDosFilas;
        public System.Windows.Forms.RadioButton rdBtnUnaFila;
        public System.Windows.Forms.ComboBox cmbColoresLetraResaltada;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbColoresLetraNormal;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbColoresFondo;
        public System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.Button btnA;
        public System.Windows.Forms.Button btnB;
        public System.Windows.Forms.Button btnC;
        public System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Panel panelMuestra;
        public System.Windows.Forms.ComboBox cmbTamanioLetra;
        public System.Windows.Forms.Label label5;
    }
}