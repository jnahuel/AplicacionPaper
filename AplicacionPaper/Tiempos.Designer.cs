namespace AplicacionPaper
{
    partial class Tiempos
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tiempo de descanso [mS]";
            // 
            // tbxTiempoDescanso
            // 
            this.tbxTiempoDescanso.Location = new System.Drawing.Point(159, 12);
            this.tbxTiempoDescanso.Name = "tbxTiempoDescanso";
            this.tbxTiempoDescanso.Size = new System.Drawing.Size(100, 20);
            this.tbxTiempoDescanso.TabIndex = 1;
            // 
            // tbxTiempoExcitacion
            // 
            this.tbxTiempoExcitacion.Location = new System.Drawing.Point(159, 40);
            this.tbxTiempoExcitacion.Name = "tbxTiempoExcitacion";
            this.tbxTiempoExcitacion.Size = new System.Drawing.Size(100, 20);
            this.tbxTiempoExcitacion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tiempo de excitación [mS]";
            // 
            // tbxTiempoEstudio
            // 
            this.tbxTiempoEstudio.Location = new System.Drawing.Point(159, 70);
            this.tbxTiempoEstudio.Name = "tbxTiempoEstudio";
            this.tbxTiempoEstudio.Size = new System.Drawing.Size(100, 20);
            this.tbxTiempoEstudio.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tiempo del estudio [S]";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(30, 105);
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
            this.btnCancelar.Location = new System.Drawing.Point(170, 105);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // Tiempos
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.tbxTiempoEstudio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxTiempoExcitacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxTiempoDescanso);
            this.Controls.Add(this.label1);
            this.Name = "Tiempos";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Configuración de tiempos";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}