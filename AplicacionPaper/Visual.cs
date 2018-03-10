using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AplicacionPaper
{
    public partial class Visual : Form
    {

        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Variables que debe devolver el formulario
        public string colorFondo;
        public string colorLetraResaltada;
        public string colorLetraNormal;
        public string tamanioLetra;
        public byte cantidadFilas;

        // Cadenas de almacenamiento de los colores disponibles
        string[] coloresPredefinidos = { "Aquamarine", "Beige", "Black", "Blue", "BlueViolet", "Brown", "Chocolate", "Cyan", "DarkBlue",
                                       "DarkCyan", "DarkGray", "DarkGreen", "DarkMagenta", "DarkOrange", "DarkRed", "DarkViolet", "Fuchsia",
                                       "Gold", "Gray", "Green", "Indigo", "LightBlue", "LightCyan", "LightGray", "LightGreen", "LightPink",
                                       "LightYellow", "Magenta", "Maroon", "Orange", "OrangeRed", "Pink", "Purple", "Red", "Turquoise", "Violet",
                                       "White", "Yellow", "YellowGreen"};

        // Cadenas de almacenamiento de los caracteres disponibles
        string[] caracteresPredefinidos = { "A", "B", "C", "D", "E", "F" };

        // Cadenas de almacenamiento de los tamaños de letras disponibles disponibles
        string[] tamaniosDeLetra = { "Muy grande", "Grande", "Media", "Chica", "Muy chica" };

        // Variable para el Thread
        static Thread threadGrafico;

        // Variable para el total de los botones
        const int totalBotones = 4;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public Visual( string colorFondo, string colorLetraResaltada, string colorLetraNormal, string tamanioLetra, byte cantidadFilas )
        {
            InitializeComponent();

            // Se quitan los botones y las opciones de maximizar y minimizar la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Se invalidan las verificaciones para evitar usar delegados, ya que sólo hay un thread
            CheckForIllegalCrossThreadCalls = false;

            // Agregar los tamaños a la lista desplegable
            for (int indiceAuxiliar = 0; indiceAuxiliar < tamaniosDeLetra.Length; indiceAuxiliar++) 
            {   cmbTamanioLetra.Items.Add( tamaniosDeLetra[indiceAuxiliar] );   }

            // Se selecciona el tamaño de la letra suministrada
            cmbTamanioLetra.SelectedIndex = Array.IndexOf(tamaniosDeLetra, tamanioLetra );

            // Agregar los colores a las listas desplegables
            for (int indiceAuxiliar = 0; indiceAuxiliar < coloresPredefinidos.Length; indiceAuxiliar++)
            {
                cmbColoresFondo.Items.Add(coloresPredefinidos[indiceAuxiliar]);
                cmbColoresLetraNormal.Items.Add(coloresPredefinidos[indiceAuxiliar]);
                cmbColoresLetraResaltada.Items.Add(coloresPredefinidos[indiceAuxiliar]);
            }

            // Se ubican los colores pasados en el constructor
            cmbColoresFondo.SelectedIndex = Array.IndexOf( coloresPredefinidos, colorFondo );
            cmbColoresLetraNormal.SelectedIndex = Array.IndexOf( coloresPredefinidos, colorLetraNormal );
            cmbColoresLetraResaltada.SelectedIndex = Array.IndexOf( coloresPredefinidos, colorLetraResaltada );

            // Se deshabilita la opción de modificar el texto de las listas desplegables por parte del usuario
            cmbColoresFondo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColoresLetraNormal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColoresLetraResaltada.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTamanioLetra.DropDownStyle = ComboBoxStyle.DropDownList;

            // Se reacomoda los botones de acuerdo a la cantidad de filas usadas
            reacomodarBotones( cantidadFilas );
            if (cantidadFilas == 1) { rdBtnUnaFila.Checked = true; }
            else { rdBtnDosFilas.Checked = true; }

            // Este cambio no se puede hacer en un foreach
            btnA.FlatStyle = FlatStyle.Flat;
            btnB.FlatStyle = FlatStyle.Flat;
            btnC.FlatStyle = FlatStyle.Flat;
            btnD.FlatStyle = FlatStyle.Flat;

            // Se crea y lanza el thread de control gráfico
            threadGrafico = new Thread(new ThreadStart(funcionThreadGrafico));
            threadGrafico.Start();
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                        FUNCIONES PARA ACEPTAR O CANCELAR EL FORMULARIO                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Funcion encargada de realizar las acciones para una correcta terminación del formulario
        private void terminarCorrectamente()
        {
            // Remueve el handler de cierre del formulario
            this.FormClosing -= new FormClosingEventHandler( Visual_FormClosing );

            // Cancela el thread y recibe la excepcion generada
            try { threadGrafico.Abort(); }
            catch (ThreadAbortException) { };

            // Al haber aceptado, se deben devolver los valores elegidos
            this.colorFondo = coloresPredefinidos[cmbColoresFondo.SelectedIndex];
            this.colorLetraResaltada = coloresPredefinidos[cmbColoresLetraResaltada.SelectedIndex];
            this.colorLetraNormal = coloresPredefinidos[cmbColoresLetraNormal.SelectedIndex];
            this.tamanioLetra = tamaniosDeLetra[cmbTamanioLetra.SelectedIndex];
            if (rdBtnDosFilas.Checked == true) { this.cantidadFilas = 2; }
            else { this.cantidadFilas = 1; }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        // Funcion encargada de realizar las acciones para una incorrecta terminación del formulario
        private void terminarIncorrectamente()
        {
            // Cancela el thread y recibe la excepcion generada
            try { threadGrafico.Abort(); }
            catch (ThreadAbortException) { };
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        // Boton "Aceptar"
        private void btnAceptar_Click(object sender, EventArgs e) { terminarCorrectamente(); }

        // Boton "Cancelar"
        private void btnCancelar_Click(object sender, EventArgs e) { terminarIncorrectamente(); }

        // Este handler se usa cuando se cierra el formulario por medio del boton "X" u otro método en lugar de "Aceptar"
        private void Visual_FormClosing(object sender, FormClosingEventArgs e)  {   terminarIncorrectamente();  }

        // Detección de la tecla enter (para aceptar) o escape (para cancelar) dentro de los controles gráficos
        private void detectarTeclasEspeciales(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return) { terminarCorrectamente(); }
            if (e.KeyChar == (char)Keys.Escape) { terminarIncorrectamente(); }
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                        FUNCIONES PARA CONTROLAR LA PARTE GRÁFICA                                                         */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Cambiar el color de fondo de los botones
        private void cmbColoresFondo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnA.BackColor = Color.FromName(coloresPredefinidos[ cmbColoresFondo.SelectedIndex]);
            btnB.BackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnC.BackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnD.BackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);

            // El control de "FlatAppearance" no puede realizarse dentro de un foreach
            btnA.FlatAppearance.BorderColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnB.FlatAppearance.BorderColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnC.FlatAppearance.BorderColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnD.FlatAppearance.BorderColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnA.FlatAppearance.MouseDownBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnB.FlatAppearance.MouseDownBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnC.FlatAppearance.MouseDownBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnD.FlatAppearance.MouseDownBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnA.FlatAppearance.MouseOverBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnB.FlatAppearance.MouseOverBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnC.FlatAppearance.MouseOverBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
            btnD.FlatAppearance.MouseOverBackColor = Color.FromName(coloresPredefinidos[cmbColoresFondo.SelectedIndex]);
        }

        // Se deben reacomodar los botones para que ocupen una sola fila en el panel
        private void rdBtnUnaFila_CheckedChanged(object sender, EventArgs e) { reacomodarBotones(1); }

        // Se deben reacomodar los botones para que ocupen dos filas en el panel
        private void rdBtnDosFilas_CheckedChanged(object sender, EventArgs e) { reacomodarBotones(2); }

        // Se debe ajustar el tamaño de las letras
        private void cmbTamanioLetra_SelectedIndexChanged(object sender, EventArgs e)   {   actualizarLetras(); }

        // Funcion para ajustar las letras de todos los botones
        private void actualizarLetras()
        {
            int escalaLetra = (cmbTamanioLetra.SelectedIndex + 1) * 2;

            foreach (Control boton in panelMuestra.Controls)
            {
                if (boton is Button)
                {
                    if (boton.Height > boton.Width) { boton.Font = new Font("Times new roman", boton.Width / escalaLetra); }
                    else { boton.Font = new Font("Times new roman", boton.Height / escalaLetra); }
                }
            }
        }

        // Funcion para reacomodar los botones. Recibe la cantidad de filas
        public void reacomodarBotones(int filas)
        {
            int anchoBoton = panelMuestra.Width / (totalBotones / filas);
            int altoBoton = panelMuestra.Height / filas;
            int botonesModificados = 0;
            int segundaFila = 0;

            foreach (Control boton in panelMuestra.Controls)
            {
                if (boton is Button)
                {
                    boton.Height = altoBoton;
                    boton.Width = anchoBoton;
                    boton.Location = new Point(anchoBoton * botonesModificados++, altoBoton * segundaFila);
                    if ((botonesModificados == totalBotones / 2) && (filas > 1))
                    {
                        segundaFila = 1;
                        botonesModificados = 0;
                    }
                }
            }

            actualizarLetras();
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          THREAD GRÁFICO                                                                  */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Se encarga de ir alternando el botón encendido
        private void funcionThreadGrafico()
        {
            int indiceColorExcitacion, indiceColorNormal;

            while (true)
            {
                indiceColorExcitacion = cmbColoresLetraResaltada.SelectedIndex;
                indiceColorNormal = cmbColoresLetraNormal.SelectedIndex;

                // Primero se hace una pasada para cambiar el color de las letras en descanso
                foreach (Control boton in panelMuestra.Controls)
                {   if (boton is Button) { boton.ForeColor = Color.FromName(coloresPredefinidos[indiceColorNormal]); }  }

                foreach (Control boton in panelMuestra.Controls )
                {
                    if (boton is Button)
                    {
                        boton.ForeColor = Color.FromName( coloresPredefinidos[ indiceColorExcitacion ]);
                        Thread.Sleep(500);
                        boton.ForeColor = Color.FromName( coloresPredefinidos[ indiceColorNormal ]);
                        Thread.Sleep(500);
                    }
                }
            }
        }

    
    }
}
