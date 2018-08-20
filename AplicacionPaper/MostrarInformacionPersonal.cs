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
        private string[] textosDosOpciones = new string[] { "SI", "NO" };
        private string[] textosSeisOpciones = new string[] { "A", "B", "C", "D", "E", "F" };
        private List<string> textosAuxiliares = new List<string>();
        private int totalDeOpciones;
        private string estudio;


        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public MostrarInformacionPersonal(  string apellido, string nombre, int edad, bool experiencia, int tiempoDescanso, int tiempoExcitacion,
                                            int tiempoTotal, int cantidadSimbolos, string letraNormal, string letraResaltada, string colorFondo ,
                                            string tamanioLetra, string secuencia, int filas, List<string> simbolosPrevios,
                                            string tipoDeEstudio, List<Int32> listaDePeriodos)
        {
            InitializeComponent();

            // Se quitan los botones y las opciones de maximizar y minimizar la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            estudio = tipoDeEstudio;

            // Verificacion del parametro cantidadSimbolos
            if (tipoDeEstudio == "Secuencial")
            {
                switch (cantidadSimbolos)
                {
                    case 6:
                        totalDeOpciones = 6;
                        break;
                    case 2:
                    default:
                        totalDeOpciones = 2;
                        break;
                }
            }
            else
            {
                if (tipoDeEstudio == "Frecuencial")
                {
                    totalDeOpciones = listaDePeriodos.Count;
                }
            }

            // Se colocan los tiempos suministrados en los displays
            this.tbxApellido.Text = apellido;
            this.tbxNombre.Text = nombre;
            this.tbxEdad.Text = edad.ToString();
            if (experiencia == true) { rbtnSi.Checked = true; }
            else { rbtnNo.Checked = true; }
            this.tbxTiempoTotal.Text = tiempoTotal.ToString();
            this.tbxSimbolos.Text = totalDeOpciones.ToString();
            this.tbxLetraNormal.Text = letraNormal;
            this.tbxLetraResaltada.Text = letraResaltada;
            this.tbxFondo.Text = colorFondo;
            this.tbxTamanio.Text = tamanioLetra;
            this.tbxSecuencia.Text = secuencia;
            this.tbxFilas.Text = filas.ToString();

            // Se modifican los textboxs para que sólo sean readonly
            this.tbxApellido.ReadOnly = true;
            this.tbxNombre.ReadOnly = true;
            this.tbxEdad.ReadOnly = true;
            this.tbxTiempoTotal.ReadOnly = true;
            this.tbxSimbolos.ReadOnly = true;
            this.tbxLetraNormal.ReadOnly = true;
            this.tbxLetraResaltada.ReadOnly = true;
            this.tbxFondo.ReadOnly = true;
            this.tbxTamanio.ReadOnly = true;
            this.tbxSecuencia.ReadOnly = true;
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
            this.tbxSecuencia.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbxTiempoTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;

            // Con fondo blanco
            this.tbxApellido.BackColor = Color.LightGray;
            this.tbxNombre.BackColor = Color.LightGray;
            this.tbxEdad.BackColor = Color.LightGray;
            this.tbxTiempoTotal.BackColor = Color.LightGray;
            this.tbxSimbolos.BackColor = Color.LightGray;
            this.tbxLetraNormal.BackColor = Color.LightGray;
            this.tbxLetraResaltada.BackColor = Color.LightGray;
            this.tbxFondo.BackColor = Color.LightGray;
            this.tbxTamanio.BackColor = Color.LightGray;
            this.tbxSecuencia.BackColor = Color.LightGray;
            this.tbxFilas.BackColor = Color.LightGray;

            // Cambio de los datos que se muestran en funcion del tipo de estudio
            if (tipoDeEstudio == "Secuencial")
            {
                lblDescanso.Text = "Tiempo de descanso [mS]";
                cmbFrecuencias.Dispose();
                this.tbxTiempoDescanso.Text = tiempoDescanso.ToString();
                this.tbxTiempoExcitacion.Text = tiempoExcitacion.ToString();
                this.tbxTiempoDescanso.ReadOnly = true;
                this.tbxTiempoExcitacion.ReadOnly = true;
                this.tbxTiempoDescanso.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.tbxTiempoExcitacion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.tbxTiempoDescanso.BackColor = Color.LightGray;
                this.tbxTiempoExcitacion.BackColor = Color.LightGray;
            }
            else
            {
                if (tipoDeEstudio == "Frecuencial")
                {
                    // Se eliminan los recursos que no se van a usar
                    tbxTiempoDescanso.Dispose();
                    tbxTiempoExcitacion.Dispose();
                    lblExcitacion.Dispose();
                    // Se muestran los nuevos recursos
                    lblDescanso.Text = "Lista de frecuencias";

                    for (int indice = 0; indice < listaDePeriodos.Count; indice++)
                        cmbFrecuencias.Items.Add( Math.Round((double) 500 / listaDePeriodos[indice], 2) );
                }
            }


            // Finalmente, se listan los símbolos disponibles para elegir en el combobox en función de la cantidad de opciones actuales
            if (simbolosPrevios.Count == 0)
            {
                switch (totalDeOpciones)
                {
                    case 2:
                        for (int indice = 0; indice < totalDeOpciones; indice++)
                            textosAuxiliares.Add(textosDosOpciones[indice]);
                        break;
                    case 6:
                        for (int indice = 0; indice < totalDeOpciones; indice++)
                            textosAuxiliares.Add(textosSeisOpciones[indice]);
                        break;
                }
            }
            else
                textosAuxiliares = simbolosPrevios;

            for (int indiceAuxiliar = 0; indiceAuxiliar < totalDeOpciones; indiceAuxiliar++)
                cmbOpciones.Items.Add(textosAuxiliares[indiceAuxiliar]);                            // Con esto se agregan las opciones a la lista

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
            if (estudio == "Frecuencial")
            {
                cmbFrecuencias.SelectedIndexChanged -= cmbFrecuencias_SelectedIndexChanged;
                cmbFrecuencias.SelectedIndex = cmbOpciones.SelectedIndex;
                cmbFrecuencias.SelectedIndexChanged += cmbFrecuencias_SelectedIndexChanged;
            }
        }

        private void cmbFrecuencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOpciones.SelectedIndexChanged -= cmbOpciones_SelectedIndexChanged;
            cmbOpciones.SelectedIndex = cmbFrecuencias.SelectedIndex;
            cmbOpciones.SelectedIndexChanged += cmbOpciones_SelectedIndexChanged;
            opcionElegida = textosAuxiliares[cmbOpciones.SelectedIndex];
        }
    }
}
