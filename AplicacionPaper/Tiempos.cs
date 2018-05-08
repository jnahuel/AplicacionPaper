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
    public partial class Tiempos : Form
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Variables que debe devolver el formulario
        public int tiempoDeDescanso;
        public int tiempoDeExcitacion;
        public int tiempoTotal;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public Tiempos(int tiempoDeDescanso, int tiempoDeExcitacion, int tiempoTotal)
        {
            InitializeComponent();

            // Se quitan los botones y las opciones de maximizar y minimizar la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Se colocan los tiempos suministrados en los displays
            tbxTiempoDescanso.Text = tiempoDeDescanso.ToString();
            tbxTiempoExcitacion.Text = tiempoDeExcitacion.ToString();
            tbxTiempoEstudio.Text = tiempoTotal.ToString();

            // Ubicación gráfica de los controls
            label1.Location = new Point( 15, 15 );
            label2.Location = new Point( 15, 45 );
            label3.Location = new Point( 15, 75 );
            tbxTiempoDescanso.Location = new Point(160, 10);
            tbxTiempoExcitacion.Location = new Point( 160, 40 );
            tbxTiempoEstudio.Location = new Point( 160, 70 );

        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                        FUNCIONES PARA ACEPTAR O CANCELAR EL FORMULARIO                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Funcion encargada de realizar las acciones para una correcta terminación del formulario
        private void terminarCorrectamente()
        {
            bool error = false;

            // Antes de devolver los valores, se debe verificar que estos sean correctos (Sólo numeros positivos)

            // Verificación del parámetro "Tiempo de Descanso"
            if (string.IsNullOrEmpty(tbxTiempoDescanso.Text))
            {
                error = true;
                MessageBox.Show("Por favor, complete el casillero del tiempo de descanso");
            }
            else
            {
                try { this.tiempoDeDescanso = Int32.Parse(tbxTiempoDescanso.Text); }
                catch { }
                if (tiempoDeDescanso <= 0)
                {
                    error = true;
                    MessageBox.Show("Por favor, ingrese un número válido para el tiempo de descanso (Sólo positivos)");
                }
            }

            // Verificación del parámetro "Tiempo de Excitación"
            if (string.IsNullOrEmpty( tbxTiempoExcitacion.Text ))
            {
                error = true;
                MessageBox.Show("Por favor, complete el casillero del tiempo de excitación");
            }
            else
            {
                try { this.tiempoDeExcitacion = Int32.Parse(tbxTiempoExcitacion.Text); }
                catch { }
                if (tiempoDeExcitacion <= 0)
                {
                    error = true;
                    MessageBox.Show("Por favor, ingrese un número válido para el tiempo de excitacion (Sólo positivos)");
                }
            }

            // Verificación del parámetro "Tiempo del Estudio"
            if (string.IsNullOrEmpty( tbxTiempoEstudio.Text ))
            {
                error = true;
                MessageBox.Show("Por favor, complete el casillero del tiempo del estudio");
            }
            else
            {
                try { this.tiempoTotal = Int32.Parse(tbxTiempoEstudio.Text); }
                catch{}
                if (tiempoTotal <= 0)
                {
                    error = true;
                    MessageBox.Show("Por favor, ingrese un número válido para el tiempo del estudio (Sólo positivos)");
                }
            }

            // Si no hay errores, se devuelven los valores
            if (!error) { this.DialogResult = System.Windows.Forms.DialogResult.OK; }
        }

        // Funcion encargada de realizar las acciones para una incorrecta terminación del formulario
        private void terminarIncorrectamente()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        // Boton "Aceptar"
        private void btnAceptar_Click(object sender, EventArgs e) { terminarCorrectamente(); }

        // Boton "Cancelar"
        private void btnCancelar_Click(object sender, EventArgs e) { terminarIncorrectamente(); }

    }
}
