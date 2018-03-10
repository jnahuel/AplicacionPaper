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
    public partial class MostrarInformacionPersonal : Form
    {
        // Variables
        public string opcionElegida;

        // Arrays de caracteres que presentarán los botones
        string[] textosDosOpciones = new string[] { "SI", "NO" };
        string[] textosSeisOpciones = new string[] { "A", "B", "C", "D", "E", "F" };
        string[] textosAuxiliares;

        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public MostrarInformacionPersonal(  string apellido, string nombre, int edad, bool experiencia, int tiempoDescanso, int tiempoExcitacion,
                                            int tiempoTotal, int cantidadSimbolos, string letraNormal, string letraResaltada, string colorFondo ,
                                            string tamanioLetra, string tamanioPantalla, int filas )
        {
            InitializeComponent();

            // Se quitan los botones y las opciones de maximizar y minimizar la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Se colocan los tiempos suministrados en los displays
            this.tbxApellido.Text = apellido;
            this.tbxNombre.Text = nombre;
            this.tbxEdad.Text = edad.ToString();
            if (experiencia == true) { rbtnSi.Checked = true; }
            else { rbtnNo.Checked = true; }
            this.tbxTiempoDescanso.Text = tiempoDescanso.ToString();
            this.tbxTiempoExcitacion.Text = tiempoExcitacion.ToString();
            this.tbxTiempoTotal.Text = tiempoTotal.ToString();
            this.tbxSimbolos.Text = cantidadSimbolos.ToString();
            this.tbxLetraNormal.Text = letraNormal;
            this.tbxLetraResaltada.Text = letraResaltada;
            this.tbxFondo.Text = colorFondo;
            this.tbxTamanio.Text = tamanioLetra;
            this.tbxTamanioPantalla.Text = tamanioPantalla;
            this.tbxFilas.Text = filas.ToString();

            // Se modifican los textboxs para que sólo sean readonly
            this.tbxApellido.ReadOnly = true;
            this.tbxNombre.ReadOnly = true;
            this.tbxEdad.ReadOnly = true;
            this.tbxTiempoDescanso.ReadOnly = true;
            this.tbxTiempoExcitacion.ReadOnly = true;
            this.tbxTiempoTotal.ReadOnly = true;
            this.tbxSimbolos.ReadOnly = true;
            this.tbxLetraNormal.ReadOnly = true;
            this.tbxLetraResaltada.ReadOnly = true;
            this.tbxFondo.ReadOnly = true;
            this.tbxTamanio.ReadOnly = true;
            this.tbxTamanioPantalla.ReadOnly = true;
            this.tbxFilas.ReadOnly = true;

            // Alineados a derecha
            this.tbxApellido.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxEdad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxFilas.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxFondo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxLetraNormal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxLetraResaltada.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxNombre.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxSimbolos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxTamanio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxTamanioPantalla.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxTiempoDescanso.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxTiempoExcitacion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxTiempoTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;

            // Con fondo blanco
            this.tbxApellido.BackColor = Color.LightGray;
            this.tbxNombre.BackColor = Color.LightGray;
            this.tbxEdad.BackColor = Color.LightGray;
            this.tbxTiempoDescanso.BackColor = Color.LightGray;
            this.tbxTiempoExcitacion.BackColor = Color.LightGray;
            this.tbxTiempoTotal.BackColor = Color.LightGray;
            this.tbxSimbolos.BackColor = Color.LightGray;
            this.tbxLetraNormal.BackColor = Color.LightGray;
            this.tbxLetraResaltada.BackColor = Color.LightGray;
            this.tbxFondo.BackColor = Color.LightGray;
            this.tbxTamanio.BackColor = Color.LightGray;
            this.tbxTamanioPantalla.BackColor = Color.LightGray;
            this.tbxFilas.BackColor = Color.LightGray;

            // Finalmente, se listan los símbolos disponibles para elegir en el combobox en función de la cantidad de opciones actuales
            if (cantidadSimbolos == 2) { textosAuxiliares = textosDosOpciones; }
            else { textosAuxiliares = textosSeisOpciones; }

            for (int indiceAuxiliar = 0; indiceAuxiliar < textosAuxiliares.Length; indiceAuxiliar++)
                cmbOpciones.Items.Add( textosAuxiliares[ indiceAuxiliar ] );                            // Con esto se agregan las opciones a la lista

            cmbOpciones.SelectedIndex = 0;              // Se deja seleccionado por defecto el primer item

        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                        FUNCIONES PARA ACEPTAR O CANCELAR EL FORMULARIO                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Boton "Aceptar"
        private void btnAceptar_Click(object sender, EventArgs e) { this.DialogResult = System.Windows.Forms.DialogResult.OK; }

        // Boton "Cancelar"
        private void btnCancelar_Click(object sender, EventArgs e) { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                            DEMÁS COSAS                                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Cuando se modifica el indice de la lista, se debe actualizar la opción elegida
        private void cmbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            opcionElegida = textosAuxiliares[ cmbOpciones.SelectedIndex ];
        }
    }
}
