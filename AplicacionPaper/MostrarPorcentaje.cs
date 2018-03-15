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
    public partial class MostrarPorcentaje : Form
    {
        public MostrarPorcentaje()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        public void actualizarPorcentaje(int porcentaje)
        {
            lblPorcentaje.Text = porcentaje.ToString();
        }
    }
}
