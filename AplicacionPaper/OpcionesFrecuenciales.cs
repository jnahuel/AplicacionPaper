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
    public partial class OpcionesFrecuenciales : Form
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Variables que debe devolver el formulario
        public List<Int32> listaPeriodos = new List<int>();
        public List<string> listaSimbolos = new List<string>();
        public Int32 cantidadDeOpcionesFrecuenciales;
        public Int32 tiempoDelEstudio;

        // Variables internas del formulario
        public List<Int32> listaFrecuencias = new List<int>();
        private Int32[] vectorFrecuenciasDefault = { 6, 9, 12, 15, 18, 21, 22, 23, 24, 25 };
        private string[] vectorSimbolosDefault = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        private int indiceFrecuencias;
        private int indiceSimbolos;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public OpcionesFrecuenciales(int Opciones, int tiempoEstudio, List<string> simbolosPrevios)
        {
            InitializeComponent();

            cantidadDeOpcionesFrecuenciales = Opciones;
            numOpciones.Value = cantidadDeOpcionesFrecuenciales;
            listaFrecuencias.Clear();
            lstFrecuencias.Items.Clear();
            listaSimbolos.Clear();
            lstSimbolos.Items.Clear();

            for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
            {
                listaFrecuencias.Add( vectorFrecuenciasDefault[ indice ] );
                lstFrecuencias.Items.Add( vectorFrecuenciasDefault[ indice ] );
            }

            if (simbolosPrevios.Count == 0)
            {
                for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
                {
                    listaSimbolos.Add(vectorSimbolosDefault[indice]);
                    lstSimbolos.Items.Add(vectorSimbolosDefault[indice]);
                }
            }
            else
            {
                for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
                {
                    listaSimbolos.Add(simbolosPrevios[indice]);
                    lstSimbolos.Items.Add(simbolosPrevios[indice]);
                }
            }

            txtBoxTiempoEstudio.Text = tiempoEstudio.ToString();

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
            tiempoDelEstudio = Int32.Parse(txtBoxTiempoEstudio.Text);

            for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
            {
                listaPeriodos.Add( 500 / listaFrecuencias[indice] );
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





        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                            DEMÁS COSAS                                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Contador de la cantidad de opciones
        private void numOpciones_ValueChanged(object sender, EventArgs e)
        {
            cantidadDeOpcionesFrecuenciales = Int32.Parse(numOpciones.Value.ToString());
            listaFrecuencias.Clear();
            lstFrecuencias.Items.Clear();
            listaSimbolos.Clear();
            lstSimbolos.Items.Clear();
            for (Int32 indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
            {
                listaFrecuencias.Add(vectorFrecuenciasDefault[indice]);
                lstFrecuencias.Items.Add(vectorFrecuenciasDefault[indice]);
                listaSimbolos.Add(vectorSimbolosDefault[indice]);
                lstSimbolos.Items.Add(vectorSimbolosDefault[indice]);
            }
        }


        // Cuando se selecciona un valor de la lista se lo debe mostrar en el recuadro aparte para poder modificarlo
        private void lstFrecuencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceFrecuencias = lstFrecuencias.SelectedIndex;
            txtBoxFrecuencias.Text = lstFrecuencias.Items[indiceFrecuencias].ToString();
        }

        // Cuando se selecciona un valor de la lista se lo debe mostrar en el recuadro aparte para poder modificarlo
        private void lstSimbolos_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceSimbolos = lstSimbolos.SelectedIndex;
            txtBoxSimbolos.Text = lstSimbolos.Items[indiceSimbolos].ToString();
        }


        // Tiene que asignar el valor del textBox al elemento seleccionado de la lista
        private void btnModificarFrecuencias_Click(object sender, EventArgs e)
        {
            // Se quita temporalmente el handler para poder modificar los valores
            lstFrecuencias.SelectedIndexChanged -= lstFrecuencias_SelectedIndexChanged;

            // Se borra la lista de valores actuales
            listaFrecuencias.Clear();

            // Se vuelcan los valores mostrados a la lista
            for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)    
                { listaFrecuencias.Add( Int32.Parse( lstFrecuencias.Items[indice].ToString() ) ); }

            // Se modifica el valor solicitado
            listaFrecuencias[indiceFrecuencias] = Int32.Parse( txtBoxFrecuencias.Text );

            // Se borra la lista desplegada en pantalla
            lstFrecuencias.Items.Clear();

            // Se vuelva la lista a la pantalla
            for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
                { lstFrecuencias.Items.Add( listaFrecuencias[indice] ); }

            // Se reestablece el handler
            lstFrecuencias.SelectedIndexChanged += lstFrecuencias_SelectedIndexChanged;
        }

        // Tiene que asignar el valor del textBox al elemento seleccionado de la lista
        private void btnModificarSimbolos_Click(object sender, EventArgs e)
        {
            // Se quita temporalmente el handler para poder modificar los valores
            lstSimbolos.SelectedIndexChanged -= lstSimbolos_SelectedIndexChanged;

            // Se borra la lista de valores actuales
            listaSimbolos.Clear();

            // Se vuelcan los valores mostrados a la lista
            for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
                { listaSimbolos.Add( lstSimbolos.Items[indice].ToString() ); }

            // Se modifica el valor solicitado
            listaSimbolos[indiceSimbolos] = txtBoxSimbolos.Text;

            // Se borra la lista desplegada en pantalla
            lstSimbolos.Items.Clear();

            // Se vuelva la lista a la pantalla
            for (int indice = 0; indice < cantidadDeOpcionesFrecuenciales; indice++)
                { lstSimbolos.Items.Add(listaSimbolos[indice]); }

            // Se reestablece el handler
            lstSimbolos.SelectedIndexChanged += lstSimbolos_SelectedIndexChanged;
        }



    }
}
