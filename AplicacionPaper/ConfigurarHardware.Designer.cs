namespace AplicacionPaper
{
    partial class ConfigurarHardware
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
            this.LienzoCasco = new System.Windows.Forms.PictureBox();
            this.LienzoSenial = new System.Windows.Forms.PictureBox();
            this.cmbCanalNormal = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxDefault = new System.Windows.Forms.CheckBox();
            this.cbxSRB2No = new System.Windows.Forms.CheckBox();
            this.cbxSRB2Si = new System.Windows.Forms.CheckBox();
            this.cbxSRB1No = new System.Windows.Forms.CheckBox();
            this.cbxSRB1Si = new System.Windows.Forms.CheckBox();
            this.cbxBiasNo = new System.Windows.Forms.CheckBox();
            this.cbxBiasSi = new System.Windows.Forms.CheckBox();
            this.cbxEncendidoNo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxEncendidoSi = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEntrada = new System.Windows.Forms.ComboBox();
            this.cmbGanancia = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGraficarZ = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxContactoN = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxContactoP = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCanalZ = new System.Windows.Forms.ComboBox();
            this.btnMedirZ = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxEstado = new System.Windows.Forms.TextBox();
            this.btnConfigurarCanal = new System.Windows.Forms.Button();
            this.btnGraficarCanal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LienzoCasco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LienzoSenial)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LienzoCasco
            // 
            this.LienzoCasco.Location = new System.Drawing.Point(380, 16);
            this.LienzoCasco.Name = "LienzoCasco";
            this.LienzoCasco.Size = new System.Drawing.Size(500, 500);
            this.LienzoCasco.TabIndex = 0;
            this.LienzoCasco.TabStop = false;
            // 
            // LienzoSenial
            // 
            this.LienzoSenial.Location = new System.Drawing.Point(10, 270);
            this.LienzoSenial.Name = "LienzoSenial";
            this.LienzoSenial.Size = new System.Drawing.Size(360, 210);
            this.LienzoSenial.TabIndex = 1;
            this.LienzoSenial.TabStop = false;
            // 
            // cmbCanalNormal
            // 
            this.cmbCanalNormal.BackColor = System.Drawing.Color.White;
            this.cmbCanalNormal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanalNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbCanalNormal.FormattingEnabled = true;
            this.cmbCanalNormal.Location = new System.Drawing.Point(90, 92);
            this.cmbCanalNormal.Name = "cmbCanalNormal";
            this.cmbCanalNormal.Size = new System.Drawing.Size(83, 21);
            this.cmbCanalNormal.TabIndex = 3;
            this.cmbCanalNormal.SelectedIndexChanged += new System.EventHandler(this.cmbCanalNormal_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDefault);
            this.groupBox1.Controls.Add(this.cbxSRB2No);
            this.groupBox1.Controls.Add(this.cbxSRB2Si);
            this.groupBox1.Controls.Add(this.cbxSRB1No);
            this.groupBox1.Controls.Add(this.cbxSRB1Si);
            this.groupBox1.Controls.Add(this.cbxBiasNo);
            this.groupBox1.Controls.Add(this.cbxBiasSi);
            this.groupBox1.Controls.Add(this.cbxEncendidoNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxEncendidoSi);
            this.groupBox1.Controls.Add(this.cmbCanalNormal);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbEntrada);
            this.groupBox1.Controls.Add(this.cmbGanancia);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones de configuracion";
            // 
            // cbxDefault
            // 
            this.cbxDefault.AutoSize = true;
            this.cbxDefault.Location = new System.Drawing.Point(315, 94);
            this.cbxDefault.Name = "cbxDefault";
            this.cbxDefault.Size = new System.Drawing.Size(15, 14);
            this.cbxDefault.TabIndex = 33;
            this.cbxDefault.UseVisualStyleBackColor = true;
            this.cbxDefault.CheckedChanged += new System.EventHandler(this.cbxDefault_CheckedChanged);
            // 
            // cbxSRB2No
            // 
            this.cbxSRB2No.AutoSize = true;
            this.cbxSRB2No.Location = new System.Drawing.Point(315, 69);
            this.cbxSRB2No.Name = "cbxSRB2No";
            this.cbxSRB2No.Size = new System.Drawing.Size(40, 17);
            this.cbxSRB2No.TabIndex = 32;
            this.cbxSRB2No.Text = "No";
            this.cbxSRB2No.UseVisualStyleBackColor = true;
            this.cbxSRB2No.CheckedChanged += new System.EventHandler(this.cbxSRB2No_CheckedChanged);
            // 
            // cbxSRB2Si
            // 
            this.cbxSRB2Si.AutoSize = true;
            this.cbxSRB2Si.Location = new System.Drawing.Point(275, 69);
            this.cbxSRB2Si.Name = "cbxSRB2Si";
            this.cbxSRB2Si.Size = new System.Drawing.Size(35, 17);
            this.cbxSRB2Si.TabIndex = 31;
            this.cbxSRB2Si.Text = "Si";
            this.cbxSRB2Si.UseVisualStyleBackColor = true;
            this.cbxSRB2Si.CheckedChanged += new System.EventHandler(this.cbxSRB2Si_CheckedChanged);
            // 
            // cbxSRB1No
            // 
            this.cbxSRB1No.AutoSize = true;
            this.cbxSRB1No.Location = new System.Drawing.Point(315, 44);
            this.cbxSRB1No.Name = "cbxSRB1No";
            this.cbxSRB1No.Size = new System.Drawing.Size(40, 17);
            this.cbxSRB1No.TabIndex = 30;
            this.cbxSRB1No.Text = "No";
            this.cbxSRB1No.UseVisualStyleBackColor = true;
            this.cbxSRB1No.CheckedChanged += new System.EventHandler(this.cbxSRB1No_CheckedChanged);
            // 
            // cbxSRB1Si
            // 
            this.cbxSRB1Si.AutoSize = true;
            this.cbxSRB1Si.Location = new System.Drawing.Point(275, 44);
            this.cbxSRB1Si.Name = "cbxSRB1Si";
            this.cbxSRB1Si.Size = new System.Drawing.Size(35, 17);
            this.cbxSRB1Si.TabIndex = 29;
            this.cbxSRB1Si.Text = "Si";
            this.cbxSRB1Si.UseVisualStyleBackColor = true;
            this.cbxSRB1Si.CheckedChanged += new System.EventHandler(this.cbxSRB1Si_CheckedChanged);
            // 
            // cbxBiasNo
            // 
            this.cbxBiasNo.AutoSize = true;
            this.cbxBiasNo.Location = new System.Drawing.Point(315, 19);
            this.cbxBiasNo.Name = "cbxBiasNo";
            this.cbxBiasNo.Size = new System.Drawing.Size(40, 17);
            this.cbxBiasNo.TabIndex = 28;
            this.cbxBiasNo.Text = "No";
            this.cbxBiasNo.UseVisualStyleBackColor = true;
            this.cbxBiasNo.CheckedChanged += new System.EventHandler(this.cbxBiasNo_CheckedChanged);
            // 
            // cbxBiasSi
            // 
            this.cbxBiasSi.AutoSize = true;
            this.cbxBiasSi.Location = new System.Drawing.Point(275, 19);
            this.cbxBiasSi.Name = "cbxBiasSi";
            this.cbxBiasSi.Size = new System.Drawing.Size(35, 17);
            this.cbxBiasSi.TabIndex = 27;
            this.cbxBiasSi.Text = "Si";
            this.cbxBiasSi.UseVisualStyleBackColor = true;
            this.cbxBiasSi.CheckedChanged += new System.EventHandler(this.cbxBiasSi_CheckedChanged);
            // 
            // cbxEncendidoNo
            // 
            this.cbxEncendidoNo.AutoSize = true;
            this.cbxEncendidoNo.Location = new System.Drawing.Point(135, 19);
            this.cbxEncendidoNo.Name = "cbxEncendidoNo";
            this.cbxEncendidoNo.Size = new System.Drawing.Size(40, 17);
            this.cbxEncendidoNo.TabIndex = 26;
            this.cbxEncendidoNo.Text = "No";
            this.cbxEncendidoNo.UseVisualStyleBackColor = true;
            this.cbxEncendidoNo.CheckedChanged += new System.EventHandler(this.cbxEncendidoNo_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Canal";
            // 
            // cbxEncendidoSi
            // 
            this.cbxEncendidoSi.AutoSize = true;
            this.cbxEncendidoSi.Location = new System.Drawing.Point(95, 19);
            this.cbxEncendidoSi.Name = "cbxEncendidoSi";
            this.cbxEncendidoSi.Size = new System.Drawing.Size(35, 17);
            this.cbxEncendidoSi.TabIndex = 25;
            this.cbxEncendidoSi.Text = "Si";
            this.cbxEncendidoSi.UseVisualStyleBackColor = true;
            this.cbxEncendidoSi.CheckedChanged += new System.EventHandler(this.cbxEncendidoSi_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Opciones por default";
            // 
            // cmbEntrada
            // 
            this.cmbEntrada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbEntrada.FormattingEnabled = true;
            this.cmbEntrada.Location = new System.Drawing.Point(90, 67);
            this.cmbEntrada.Name = "cmbEntrada";
            this.cmbEntrada.Size = new System.Drawing.Size(83, 21);
            this.cmbEntrada.TabIndex = 12;
            this.cmbEntrada.SelectedIndexChanged += new System.EventHandler(this.cmbEntrada_SelectedIndexChanged);
            // 
            // cmbGanancia
            // 
            this.cmbGanancia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGanancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbGanancia.Location = new System.Drawing.Point(90, 42);
            this.cmbGanancia.Name = "cmbGanancia";
            this.cmbGanancia.Size = new System.Drawing.Size(83, 21);
            this.cmbGanancia.TabIndex = 11;
            this.cmbGanancia.SelectedIndexChanged += new System.EventHandler(this.cmbGanancia_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "SRB2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "SRB1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "BIAS incluido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tipo de entrada";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ganancia";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Encendido";
            // 
            // btnGraficarZ
            // 
            this.btnGraficarZ.Enabled = false;
            this.btnGraficarZ.Location = new System.Drawing.Point(200, 235);
            this.btnGraficarZ.Name = "btnGraficarZ";
            this.btnGraficarZ.Size = new System.Drawing.Size(125, 25);
            this.btnGraficarZ.TabIndex = 23;
            this.btnGraficarZ.Text = "Graficar Impedancia";
            this.btnGraficarZ.UseVisualStyleBackColor = true;
            this.btnGraficarZ.Click += new System.EventHandler(this.btnGraficarZ_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxContactoN);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbxContactoP);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmbCanalZ);
            this.groupBox2.Location = new System.Drawing.Point(10, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 55);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Medicion de impedancia";
            // 
            // cbxContactoN
            // 
            this.cbxContactoN.AutoSize = true;
            this.cbxContactoN.Location = new System.Drawing.Point(315, 25);
            this.cbxContactoN.Name = "cbxContactoN";
            this.cbxContactoN.Size = new System.Drawing.Size(15, 14);
            this.cbxContactoN.TabIndex = 27;
            this.cbxContactoN.UseVisualStyleBackColor = true;
            this.cbxContactoN.CheckedChanged += new System.EventHandler(this.cbxContactoN_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(250, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Contacto N";
            // 
            // cbxContactoP
            // 
            this.cbxContactoP.AutoSize = true;
            this.cbxContactoP.Location = new System.Drawing.Point(215, 25);
            this.cbxContactoP.Name = "cbxContactoP";
            this.cbxContactoP.Size = new System.Drawing.Size(15, 14);
            this.cbxContactoP.TabIndex = 25;
            this.cbxContactoP.UseVisualStyleBackColor = true;
            this.cbxContactoP.CheckedChanged += new System.EventHandler(this.cbxContactoP_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(150, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Contacto P";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Canal";
            // 
            // cmbCanalZ
            // 
            this.cmbCanalZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanalZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbCanalZ.FormattingEnabled = true;
            this.cmbCanalZ.Location = new System.Drawing.Point(45, 22);
            this.cmbCanalZ.Name = "cmbCanalZ";
            this.cmbCanalZ.Size = new System.Drawing.Size(83, 21);
            this.cmbCanalZ.TabIndex = 23;
            this.cmbCanalZ.SelectedIndexChanged += new System.EventHandler(this.cmbCanalZ_SelectedIndexChanged);
            // 
            // btnMedirZ
            // 
            this.btnMedirZ.Location = new System.Drawing.Point(50, 235);
            this.btnMedirZ.Name = "btnMedirZ";
            this.btnMedirZ.Size = new System.Drawing.Size(125, 25);
            this.btnMedirZ.TabIndex = 25;
            this.btnMedirZ.Text = "Medir impedancia";
            this.btnMedirZ.UseVisualStyleBackColor = true;
            this.btnMedirZ.Click += new System.EventHandler(this.btnMedirZ_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(295, 493);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 26;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 498);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Estado del canal";
            // 
            // tbxEstado
            // 
            this.tbxEstado.Location = new System.Drawing.Point(100, 495);
            this.tbxEstado.Name = "tbxEstado";
            this.tbxEstado.ReadOnly = true;
            this.tbxEstado.Size = new System.Drawing.Size(180, 20);
            this.tbxEstado.TabIndex = 28;
            // 
            // btnConfigurarCanal
            // 
            this.btnConfigurarCanal.Location = new System.Drawing.Point(50, 140);
            this.btnConfigurarCanal.Name = "btnConfigurarCanal";
            this.btnConfigurarCanal.Size = new System.Drawing.Size(125, 25);
            this.btnConfigurarCanal.TabIndex = 31;
            this.btnConfigurarCanal.Text = "Configurar canal";
            this.btnConfigurarCanal.UseVisualStyleBackColor = true;
            this.btnConfigurarCanal.Click += new System.EventHandler(this.btnConfigurarCanal_Click);
            // 
            // btnGraficarCanal
            // 
            this.btnGraficarCanal.Location = new System.Drawing.Point(200, 140);
            this.btnGraficarCanal.Name = "btnGraficarCanal";
            this.btnGraficarCanal.Size = new System.Drawing.Size(125, 25);
            this.btnGraficarCanal.TabIndex = 30;
            this.btnGraficarCanal.Text = "Graficar canal";
            this.btnGraficarCanal.UseVisualStyleBackColor = true;
            this.btnGraficarCanal.Click += new System.EventHandler(this.btnGraficarCanal_Click);
            // 
            // ConfigurarHardware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 531);
            this.Controls.Add(this.btnConfigurarCanal);
            this.Controls.Add(this.btnGraficarCanal);
            this.Controls.Add(this.LienzoSenial);
            this.Controls.Add(this.tbxEstado);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnMedirZ);
            this.Controls.Add(this.LienzoCasco);
            this.Controls.Add(this.btnGraficarZ);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurarHardware";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion del Hardware";
            ((System.ComponentModel.ISupportInitialize)(this.LienzoCasco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LienzoSenial)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LienzoCasco;
        private System.Windows.Forms.PictureBox LienzoSenial;
        private System.Windows.Forms.ComboBox cmbCanalNormal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbGanancia;
        private System.Windows.Forms.ComboBox cmbEntrada;
        private System.Windows.Forms.Button btnGraficarZ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxContactoN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbxContactoP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnMedirZ;
        private System.Windows.Forms.ComboBox cmbCanalZ;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.CheckBox cbxSRB2No;
        private System.Windows.Forms.CheckBox cbxSRB2Si;
        private System.Windows.Forms.CheckBox cbxSRB1No;
        private System.Windows.Forms.CheckBox cbxSRB1Si;
        private System.Windows.Forms.CheckBox cbxBiasNo;
        private System.Windows.Forms.CheckBox cbxBiasSi;
        private System.Windows.Forms.CheckBox cbxEncendidoNo;
        private System.Windows.Forms.CheckBox cbxEncendidoSi;
        private System.Windows.Forms.CheckBox cbxDefault;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxEstado;
        private System.Windows.Forms.Button btnConfigurarCanal;
        private System.Windows.Forms.Button btnGraficarCanal;
    }
}