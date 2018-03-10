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
    public partial class TomarDatosPersonales : Form
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Variables que debe devolver el formulario
        public string apellido;
        public string nombre;
        public int edad;
        public bool experiencia;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public TomarDatosPersonales( string apellido, string nombre, int edad, bool experiencia )
        {
            InitializeComponent();

            // Se quitan los botones y las opciones de maximizar y minimizar la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Se colocan los datos suministrados en los displays
            this.tbxApellido.Text = apellido;
            this.tbxNombre.Text = nombre;
            this.tbxEdad.Text = edad.ToString();
            if (experiencia == true) { rbtnSi.Checked = true; }
            else { rbtnNo.Checked = true; }
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

            // Verificación del parámetro "Apellido"
            if (string.IsNullOrEmpty( tbxApellido.Text ))
            {
                error = true;
                MessageBox.Show("Por favor, complete el casillero correspondiente al apellido");
            }
            else { apellido = this.tbxApellido.Text; }

            // Verificación del parámetro "Nombre"
            if (string.IsNullOrEmpty( tbxNombre.Text ))
            {
                error = true;
                MessageBox.Show("Por favor, complete el casillero correspondiente al nombre");
            }
            else { nombre = this.tbxNombre.Text; }

            // Verificación del parámetro "Edad"
            if (string.IsNullOrEmpty( tbxEdad.Text ))
            {
                error = true;
                MessageBox.Show("Por favor, complete el casillero correspondiente a la edad");
            }
            else
            {
                try { this.edad = Int32.Parse( tbxEdad.Text ); }
                catch { }
                if (edad <= 0)
                {
                    error = true;
                    MessageBox.Show("Por favor, ingrese un número válido para la edad (Sólo positivos)");
                }
            }

            if (rbtnSi.Checked == true) { experiencia = true; }
            else { experiencia = false; }

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
