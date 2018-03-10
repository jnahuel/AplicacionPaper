using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionPaper
{
    public partial class CuentaRegresiva : Form
    {
        public int cuenta;

        public CuentaRegresiva( int tiempoAContar )
        {
            InitializeComponent();

            // Se verifica por las dudas que el parametro suministrado sea correcto
            if (tiempoAContar > 0)
            {
                lbl.Text = tiempoAContar.ToString();
                cuenta = tiempoAContar;
                timer.Interval = 1000;
                timer.Enabled = true;
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (--cuenta > 0)
            {
                lbl.Text = cuenta.ToString();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }



    }
}
