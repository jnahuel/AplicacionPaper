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
    public partial class OpcionesSecuenciales : Form
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
        public int totalDeOpciones;
        public string tipoDeSecuencia;
        public List<string> listaSimbolos = new List<string>();

        // Variables internas del formulario
        private string[] vectorSimbolosDefault2Opciones = { "SI", "NO" };
        private string[] vectorSimbolosDefault6Opciones = { "A", "B", "C", "D", "E", "F" };
        private int indiceSimbolos;
        public List<string> listaAuxiliar = new List<string>();



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public OpcionesSecuenciales(int tiempoDeDescanso, int tiempoDeExcitacion, int tiempoTotal, int cantidadDeOpciones, string tipoDeSecuencia, List<string> simbolosPrevios)
        {
            InitializeComponent();

            // Se quitan los botones y las opciones de maximizar y minimizar la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Verificacion del parametro cantidadDeOpciones
            switch (cantidadDeOpciones)
            {
                case 2:
                default:
                    totalDeOpciones = 2;
                    break;
                case 6:
                    totalDeOpciones = 6;
                    break;
            }

            // Se colocan los tiempos suministrados en los displays
            tbxTiempoDescanso.Text = tiempoDeDescanso.ToString();
            tbxTiempoExcitacion.Text = tiempoDeExcitacion.ToString();
            tbxTiempoEstudio.Text = tiempoTotal.ToString();

            // Se muestra la opcion del tipo de secuencia en la lista desplegable
            switch (tipoDeSecuencia)
            {
                case "Secuencial":
                    cmbSecuencia.SelectedIndex = 0;
                    break;
                case "Caos":
                    cmbSecuencia.SelectedIndex = 2;
                    break;
                case "Aleatoria":
                default:
                    cmbSecuencia.SelectedIndex = 1;
                    break;
            }

            // Se vuelcan a la pantalla los simbolos por default o los simbolos previos
            lstSimbolos.Items.Clear();
            if (simbolosPrevios.Count == 0)
            {
                switch (totalDeOpciones)
                {
                    case 2:
                        for (int indice = 0; indice < totalDeOpciones; indice++)
                        {
                            listaAuxiliar.Add(vectorSimbolosDefault2Opciones[indice]);
                            lstSimbolos.Items.Add(vectorSimbolosDefault2Opciones[indice]);
                        }
                        break;

                    case 6:
                        for (int indice = 0; indice < totalDeOpciones; indice++)
                        {
                            listaAuxiliar.Add(vectorSimbolosDefault6Opciones[indice]);
                            lstSimbolos.Items.Add(vectorSimbolosDefault6Opciones[indice]);
                        }
                        break;
                }
            }
            else
            {
                for (int indice = 0; indice < totalDeOpciones; indice++)
                {
                    listaAuxiliar.Add(simbolosPrevios[indice]);
                    lstSimbolos.Items.Add(simbolosPrevios[indice]);
                }
            }

            listaSimbolos = listaAuxiliar;

            switch (totalDeOpciones)
            {
                case 2:
                    rbtn2Opciones.Checked = true;
                    break;
                case 6:
                    rbtn6Opciones.Checked = true;
                    break;
            }



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

            switch (cmbSecuencia.SelectedIndex)
            {
                case 0:
                    tipoDeSecuencia = "Secuencial";
                    break;
                case 1:
                    tipoDeSecuencia = "Aleatoria";
                    break;
                case 2:
                    tipoDeSecuencia = "Caos";
                    break;
            }

            listaSimbolos = listaAuxiliar;

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



        // 2 opciones
        private void rbtn2Opciones_CheckedChanged(object sender, EventArgs e)
        {
            if (totalDeOpciones != 2 && rbtn2Opciones.Checked == true)
            {
                totalDeOpciones = 2;

                // Se quita temporalmente el handler para poder modificar los valores
                lstSimbolos.SelectedIndexChanged -= lstSimbolos_SelectedIndexChanged;

                // Se borran las listas actuales
                lstSimbolos.Items.Clear();
                listaAuxiliar.Clear();

                // Se transfieren los valores por default
                for (int indice = 0; indice < totalDeOpciones; indice++)
                {
                    listaAuxiliar.Add(vectorSimbolosDefault2Opciones[indice]);
                    lstSimbolos.Items.Add(vectorSimbolosDefault2Opciones[indice]);
                }

                listaSimbolos = listaAuxiliar;

                // Se reestablece el handler
                lstSimbolos.SelectedIndexChanged += lstSimbolos_SelectedIndexChanged;
            }
        }

        // 6 opciones
        private void rbtn6Opciones_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn6Opciones.Checked == true)
            {
                totalDeOpciones = 6;

                // Se quita temporalmente el handler para poder modificar los valores
                lstSimbolos.SelectedIndexChanged -= lstSimbolos_SelectedIndexChanged;

                // Se borran las listas actuales
                lstSimbolos.Items.Clear();
                listaAuxiliar.Clear();

                // Se transfieren los valores por default
                for (int indice = 0; indice < totalDeOpciones; indice++)
                {
                    listaAuxiliar.Add(vectorSimbolosDefault6Opciones[indice]);
                    lstSimbolos.Items.Add(vectorSimbolosDefault6Opciones[indice]);
                }

                listaSimbolos = listaAuxiliar;

                // Se reestablece el handler
                lstSimbolos.SelectedIndexChanged += lstSimbolos_SelectedIndexChanged;
            }

        }

        private void lstSimbolos_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceSimbolos = lstSimbolos.SelectedIndex;
            txtBoxSimbolos.Text = lstSimbolos.Items[indiceSimbolos].ToString();
        }

        private void btnModificarSimbolos_Click(object sender, EventArgs e)
        {
            // Se quita temporalmente el handler para poder modificar los valores
            lstSimbolos.SelectedIndexChanged -= lstSimbolos_SelectedIndexChanged;

            // Se borra la lista de valores actuales
            listaAuxiliar.Clear();

            // Se vuelcan los valores mostrados a la lista
            for (int indice = 0; indice < totalDeOpciones; indice++)
                listaAuxiliar.Add(lstSimbolos.Items[indice].ToString());

            // Se modifica el valor solicitado
            listaAuxiliar[indiceSimbolos] = txtBoxSimbolos.Text;

            // Se borra la lista desplegada en pantalla
            lstSimbolos.Items.Clear();

            // Se vuelva la lista a la pantalla
            for (int indice = 0; indice < totalDeOpciones; indice++)
                lstSimbolos.Items.Add(listaAuxiliar[indice]);

            listaSimbolos = listaAuxiliar;

            // Se reestablece el handler
            lstSimbolos.SelectedIndexChanged += lstSimbolos_SelectedIndexChanged;
        }



    }
}
