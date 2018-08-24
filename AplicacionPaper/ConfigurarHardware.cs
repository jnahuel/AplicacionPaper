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
    public partial class ConfigurarHardware : Form
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Variables que debe devolver el formulario
        public DatosCasco Casco;

        // Variables
        private List<int> listaDatosEjeY;
        private Random aleatorio;
        private int maximoTiempoDeGrafica;
        private int maximoTramasPorSegundo;
        private int maximoDatosGrafica;
        private Graphics imagenSenial;
        private Graphics imagenCasco;
        private Image FondoFormulario;
        private Image FondoRedimensionado;
        private int indiceExtra = 0;
        private System.Windows.Forms.Timer timerInicial;

        private Int32 maximoValorDeEntrada;
        private Int32 minimoValorDeEntrada;

        // Variables de control para los Threads
        private bool terminarThreadGrafico;
        private Thread threadGrafico;
        private const int dosSegundos = 2;

        // Variables auxiliares para almacenar los datos de las configuraciones del casco
        private int canalAuxiliar;
        private int gananciaAuxiliar;
        private int tipoDeEntradaAuxiliar;
        private bool encendidoAuxiliar;
        private bool BiasAuxiliar;
        private bool SRB1Auxiliar;
        private bool SRB2Auxiliar;
        private int canalImpedanciaAuxiliar;
        private bool medirImpedanciaContactoP;
        private bool medirImpedanciaContactoN;


        // Variables extra para hacer mas legible el codigo
        private const int cantidadDeCanales = 8;
        private int margenIzquierda = 50;
        private int margenDerecha = 5;
        private int margenSuperior = 5;
        private int margenInferior = 5;
        private int divisionesHorizontales = 4;
        private int divisionesVerticales = 4;

        // Definicion de las coordenadas de los contactos en el grafico
        private const int X_Fp1 = 150;
        private const int Y_Fp1 = 60;
        private const int X_Fp2 = 300;
        private const int Y_Fp2 = Y_Fp1;
        private const int X_C3 = 115;
        private const int Y_C3 = 250;
        private const int X_C4 = 335;
        private const int Y_C4 = Y_C3;
        private const int X_P7 = 65;
        private const int Y_P7 = 360;
        private const int X_P8 = 380;
        private const int Y_P8 = Y_P7;
        private const int X_O1 = X_Fp1;
        private const int Y_O1 = 425;
        private const int X_O2 = X_Fp2;
        private const int Y_O2 = Y_O1;
        private const int RadioContacto = 50;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        public ConfigurarHardware( DatosCasco CondicionesInicialesDelCasco )
        {
            InitializeComponent();

            // Inicializar las variables
            listaDatosEjeY = new List<int>();
            aleatorio = new Random();
            maximoTiempoDeGrafica = 5;
            maximoTramasPorSegundo = 250;                  // Constante de la cantidad de tramas por segundo
            maximoDatosGrafica = maximoTiempoDeGrafica * maximoTramasPorSegundo;

            // Generar valores aleatorios
            for (int indice = 0; indice < maximoDatosGrafica; indice++)
                listaDatosEjeY.Add(aleatorio.Next(0, 100));

            // Inicializar la grafica de las seniales
            imagenSenial = LienzoSenial.CreateGraphics();

            // Inicializar la grafica de los contactos
            imagenCasco = LienzoCasco.CreateGraphics();
            FondoFormulario = new Bitmap(@"D:\RepoGit\ConiguracionDelCasco\OpenBCICyton.jpg");
            FondoRedimensionado = new Bitmap(FondoFormulario, LienzoCasco.Width, LienzoCasco.Height);
            LienzoCasco.BackgroundImage = FondoRedimensionado;


            /* Inicializacion de las opciones de configuracion de los canales */

            // Agregado de los numeros de los canales a la lista
            for (int indice = 1; indice <= cantidadDeCanales; indice++)
            {
                cmbCanalNormal.Items.Add( "Canal " + indice.ToString() );
                cmbCanalZ.Items.Add( "Canal " + indice.ToString() );
            }

            // Agregado de los valores de las ganancias
            cmbGanancia.Items.Add("1");
            cmbGanancia.Items.Add("2");
            cmbGanancia.Items.Add("4");
            cmbGanancia.Items.Add("6");
            cmbGanancia.Items.Add("8");
            cmbGanancia.Items.Add("12");
            cmbGanancia.Items.Add("24");

            // Agregado de las opciones de entrada
            cmbEntrada.Items.Add("Normal");
            cmbEntrada.Items.Add("Shorted");
            cmbEntrada.Items.Add("BIAS mes.");
            cmbEntrada.Items.Add("MVDD");
            cmbEntrada.Items.Add("Temp.");
            cmbEntrada.Items.Add("Testing");
            cmbEntrada.Items.Add("BIAS DRP");
            cmbEntrada.Items.Add("BIAS DRN");

            // Copia de los valores iniciales
            Casco = CondicionesInicialesDelCasco;

            // En funcion de los valores iniciales para el canal 1, se modifican los indicadores
            cmbCanalNormal.SelectedIndex = DatosCasco.CANAL_1 - 1;
            cmbGanancia.SelectedIndex = Casco.LeerGananciaDelCanal(DatosCasco.CANAL_1);
            cmbEntrada.SelectedIndex = Casco.LeerTipoDeEntradaDelCanal(DatosCasco.CANAL_1);
            if (Casco.LeerEncendidoDelCanal(DatosCasco.CANAL_1) == DatosCasco.CANAL_PRENDIDO)
                    { cbxEncendidoSi.Checked = true; }
            else    { cbxEncendidoNo.Checked = true; }

            if (Casco.LeerBiasDelCanal(DatosCasco.CANAL_1) == DatosCasco.BIAS_INCLUDE)
                    { cbxBiasSi.Checked = true; }
            else    { cbxBiasNo.Checked = true; }
            if (Casco.LeerSRB1DelCanal(DatosCasco.CANAL_1) == DatosCasco.SRB1_CONNECT_ALL)
                    { cbxSRB1Si.Checked = true; }
            else    { cbxSRB1No.Checked = true; }
            if (Casco.LeerSRB2DelCanal(DatosCasco.CANAL_1) == DatosCasco.SRB2_CONNECT)
                    { cbxSRB2Si.Checked = true; }
            else    { cbxSRB2No.Checked = true; }

            cmbCanalZ.SelectedIndex = DatosCasco.CANAL_1 - 1;
            if (Casco.LeerImpedanciaContactoPDelCanal(DatosCasco.CANAL_1) == DatosCasco.NO_MEDIR_IMPEDANCIA_P)
                    { cbxContactoP.Checked = false; }
            else
                    { cbxContactoP.Checked = true; }
            if (Casco.LeerImpedanciaContactoNDelCanal(DatosCasco.CANAL_1) == DatosCasco.NO_MEDIR_IMPEDANCIA_N)
                    { cbxContactoN.Checked = false; }
            else
                    { cbxContactoN.Checked = true; }

            // Valor por default para la variable auxiliar
            canalAuxiliar = 0;
            canalImpedanciaAuxiliar = 0;


            // Creacion de un objeto Timer para realizar las acciones iniciales en la grafica y que no queden en el constructor del formulario
            timerInicial = new System.Windows.Forms.Timer();
            timerInicial.Interval = 100;
            timerInicial.Tick += timerInicial_Tick;
            timerInicial.Start();
        }





        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          CONTROLES GRAFICOS                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/


        // Boton aceptar. Solo se habilita cuando estan detenidas las graficas
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        /********** CONFIGURACION GENERAL DEL CANAL **********/


        // Seleccion del numero de canal
        private void cmbCanalNormal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tomar el valor del nuevo canal a mostrar
            canalAuxiliar = cmbCanalNormal.SelectedIndex;

            // Mostrar en pantalla los valores del nuevo canal seleccionado

            // Ganancia
            cmbGanancia.SelectedIndexChanged -= cmbGanancia_SelectedIndexChanged;
            cmbGanancia.SelectedIndex = Casco.LeerGananciaDelCanal(canalAuxiliar);
            cmbGanancia.SelectedIndexChanged += cmbGanancia_SelectedIndexChanged;

            // Tipo de entrada
            cmbEntrada.SelectedIndexChanged -= cmbEntrada_SelectedIndexChanged;
            cmbEntrada.SelectedIndex = Casco.LeerTipoDeEntradaDelCanal(canalAuxiliar);
            cmbEntrada.SelectedIndexChanged += cmbEntrada_SelectedIndexChanged;

            // Encendido
            if (Casco.LeerEncendidoDelCanal(canalAuxiliar) == DatosCasco.CANAL_PRENDIDO)
                cbxEncendidoSi.Checked = true;
            else
                cbxEncendidoNo.Checked = true;

            // Bias
            if (Casco.LeerBiasDelCanal(canalAuxiliar) == DatosCasco.BIAS_INCLUDE)
                cbxBiasSi.Checked = true;
            else
                cbxBiasNo.Checked = true;

            // SRB1
            if (Casco.LeerSRB1DelCanal(canalAuxiliar) == DatosCasco.SRB1_CONNECT_ALL)
                cbxSRB1Si.Checked = true;
            else
                cbxSRB1No.Checked = true;

            // SRB2
            if (Casco.LeerSRB2DelCanal(canalAuxiliar) == DatosCasco.SRB2_CONNECT)
                cbxSRB2Si.Checked = true;
            else
                cbxSRB2No.Checked = true;

            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;

            // Se cambia el color del contacto en cuestion para resaltarlo
            for (int indice = 0; indice < cantidadDeCanales; indice++)
                colorearContactoApagado(indice);
            colorearContactoConfigurandose(canalAuxiliar);

        }

        // Seleccion de la ganancia
        private void cmbGanancia_SelectedIndexChanged(object sender, EventArgs e)
        {
            gananciaAuxiliar = cmbGanancia.SelectedIndex;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }

        // Seleccion del tipo de entrada
        private void cmbEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoDeEntradaAuxiliar = cmbEntrada.SelectedIndex;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }


        /* OPCION PARA EL ENCENDIDO DEL CANAL */

        // Encendido
        private void cbxEncendidoSi_CheckedChanged(object sender, EventArgs e)
        {
            cbxEncendidoNo.CheckedChanged -= cbxEncendidoNo_CheckedChanged;
            if (cbxEncendidoSi.Checked == true)
            {
                cbxEncendidoNo.Checked = false;
                encendidoAuxiliar = true;
                // Habilitacion de todas las opciones
                cmbGanancia.Enabled = true;
                cmbEntrada.Enabled = true;
                cbxBiasNo.Enabled = true;
                cbxBiasSi.Enabled = true;
                cbxSRB1No.Enabled = true;
                cbxSRB1Si.Enabled = true;
                cbxSRB2No.Enabled = true;
                cbxSRB2Si.Enabled = true;
                cbxDefault.Enabled = true;
                btnGraficarCanal.Enabled = true;
            }
            else
            {
                cbxEncendidoNo.Checked = true;
                encendidoAuxiliar = false;
                // Inhabilitacion de todas las opciones
                cmbGanancia.Enabled = false;
                cmbEntrada.Enabled = false;
                cbxBiasNo.Enabled = false;
                cbxBiasSi.Enabled = false;
                cbxSRB1No.Enabled = false;
                cbxSRB1Si.Enabled = false;
                cbxSRB2No.Enabled = false;
                cbxSRB2Si.Enabled = false;
                cbxDefault.Enabled = false;
                btnGraficarCanal.Enabled = false;
            }
            cbxEncendidoNo.CheckedChanged += cbxEncendidoNo_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }

        // Apagado
        private void cbxEncendidoNo_CheckedChanged(object sender, EventArgs e)
        {
            cbxEncendidoSi.CheckedChanged -= cbxEncendidoSi_CheckedChanged;
            if (cbxEncendidoNo.Checked == true)
            {
                cbxEncendidoSi.Checked = false;
                encendidoAuxiliar = false;
                // Inhabilitacion de todas las opciones
                cmbGanancia.Enabled = false;
                cmbEntrada.Enabled = false;
                cbxBiasNo.Enabled = false;
                cbxBiasSi.Enabled = false;
                cbxSRB1No.Enabled = false;
                cbxSRB1Si.Enabled = false;
                cbxSRB2No.Enabled = false;
                cbxSRB2Si.Enabled = false;
                cbxDefault.Enabled = false;
                btnGraficarCanal.Enabled = false;
            }
            else
            {
                cbxEncendidoSi.Checked = true;
                encendidoAuxiliar = true;
                // Habilitacion de todas las opciones
                cmbGanancia.Enabled = true;
                cmbEntrada.Enabled = true;
                cbxBiasNo.Enabled = true;
                cbxBiasSi.Enabled = true;
                cbxSRB1No.Enabled = true;
                cbxSRB1Si.Enabled = true;
                cbxSRB2No.Enabled = true;
                cbxSRB2Si.Enabled = true;
                cbxDefault.Enabled = true;
                btnGraficarCanal.Enabled = true;
            }
            cbxEncendidoSi.CheckedChanged += cbxEncendidoSi_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }


        /* OPCION PARA BIAS */

        // Incluido
        private void cbxBiasSi_CheckedChanged(object sender, EventArgs e)
        {
            cbxBiasNo.CheckedChanged -= cbxBiasNo_CheckedChanged;
            if (cbxBiasSi.Checked == true)
            {
                cbxBiasNo.Checked = false;
                BiasAuxiliar = true;
            }
            else
            {
                cbxBiasNo.Checked = true;
                BiasAuxiliar = false;
            }
            cbxBiasNo.CheckedChanged += cbxBiasNo_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }

        // No incluido
        private void cbxBiasNo_CheckedChanged(object sender, EventArgs e)
        {
            cbxBiasSi.CheckedChanged -= cbxBiasSi_CheckedChanged;
            if (cbxBiasNo.Checked == true)
            {
                cbxBiasSi.Checked = false;
                BiasAuxiliar = false;
            }
            else
            {
                cbxBiasSi.Checked = true;
                BiasAuxiliar = true;
            }
            cbxBiasSi.CheckedChanged += cbxBiasSi_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }


        /* OPCION PARA SRB1 */

        // Incluido
        private void cbxSRB1Si_CheckedChanged(object sender, EventArgs e)
        {
            cbxSRB1No.CheckedChanged -= cbxSRB1No_CheckedChanged;
            if (cbxSRB1Si.Checked == true)
            {
                cbxSRB1No.Checked = false;
                SRB1Auxiliar = true;
            }
            else
            {
                cbxSRB1No.Checked = true;
                SRB1Auxiliar = false;
            }
            cbxSRB1No.CheckedChanged += cbxSRB1No_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }

        // No incluido
        private void cbxSRB1No_CheckedChanged(object sender, EventArgs e)
        {
            cbxSRB1Si.CheckedChanged -= cbxSRB1Si_CheckedChanged;
            if (cbxSRB1No.Checked == true)
            {
                cbxSRB1Si.Checked = false;
                SRB1Auxiliar = false;
            }
            else
            {
                cbxSRB1Si.Checked = true;
                SRB1Auxiliar = true;
            }
            cbxSRB1Si.CheckedChanged += cbxSRB1Si_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }


        /* OPCION PARA SRB2 */

        // Incluido
        private void cbxSRB2Si_CheckedChanged(object sender, EventArgs e)
        {
            cbxSRB2No.CheckedChanged -= cbxSRB2No_CheckedChanged;
            if (cbxSRB2Si.Checked == true)
            {
                cbxSRB2No.Checked = false;
                SRB2Auxiliar = true;
            }
            else
            {
                cbxSRB2No.Checked = true;
                SRB2Auxiliar = false;
            }
            cbxSRB2No.CheckedChanged += cbxSRB2No_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }

        // No incluido
        private void cbxSRB2No_CheckedChanged(object sender, EventArgs e)
        {
            cbxSRB2Si.CheckedChanged -= cbxSRB2Si_CheckedChanged;
            if (cbxSRB2No.Checked == true)
            {
                cbxSRB2Si.Checked = false;
                SRB2Auxiliar = false;
            }
            else
            {
                cbxSRB2Si.Checked = true;
                SRB2Auxiliar = true;
            }
            cbxSRB2Si.CheckedChanged += cbxSRB2Si_CheckedChanged;
            // Cualquier cambio anula la configuracion por default
            cbxDefault.Checked = false;
        }


        // Configuracion por default
        private void cbxDefault_CheckedChanged(object sender, EventArgs e)
        {
            // Ganancia
            cmbGanancia.SelectedIndexChanged -= cmbGanancia_SelectedIndexChanged;
            cmbGanancia.SelectedIndex = Casco.LeerGananciaDelCanal(DatosCasco.OPCION_POR_DEFAULT_GANANCIA);
            gananciaAuxiliar = cmbGanancia.SelectedIndex;
            cmbGanancia.SelectedIndexChanged += cmbGanancia_SelectedIndexChanged;

            // Tipo de entrada
            cmbEntrada.SelectedIndexChanged -= cmbEntrada_SelectedIndexChanged;
            cmbEntrada.SelectedIndex = Casco.LeerTipoDeEntradaDelCanal(DatosCasco.OPCION_POR_DEFAULT_TIPO_DE_ENTRADA);
            tipoDeEntradaAuxiliar = cmbEntrada.SelectedIndex;
            cmbEntrada.SelectedIndexChanged += cmbEntrada_SelectedIndexChanged;

            // Encendido
            cbxEncendidoSi.Checked = true;
            encendidoAuxiliar = true;

            // Bias
            cbxBiasSi.Checked = true;
            BiasAuxiliar = true;

            // SRB1
            cbxSRB1No.Checked = true;
            SRB1Auxiliar = false;

            // SRB2
            cbxSRB2Si.Checked = true;
            SRB2Auxiliar = true;
        }


        // Boton para enviar la configuracion de comandos elegida
        private void btnConfigurarCanal_Click(object sender, EventArgs e)
        {
            // Se deben enviar los datos al casco
            Casco.ConfigurarCanal(canalAuxiliar, encendidoAuxiliar, gananciaAuxiliar, tipoDeEntradaAuxiliar, BiasAuxiliar, SRB1Auxiliar, SRB2Auxiliar);

            // Si la orden se manda correctamente, se almacenan los nuevos valores de la configuracion del canal en cuestion
            Casco.ModificarEncendidoDelCanal(canalAuxiliar, encendidoAuxiliar);
            Casco.ModificarGananciaDelCanal(canalAuxiliar, gananciaAuxiliar);
            Casco.ModificarTipoDeEntradaDelCanal(canalAuxiliar, tipoDeEntradaAuxiliar);
            Casco.ModificarBiasDelCanal(canalAuxiliar, BiasAuxiliar);
            Casco.ModificarSRB1DelCanal(canalAuxiliar, SRB1Auxiliar);
            Casco.ModificarSRB2DelCanal(canalAuxiliar, SRB2Auxiliar);
            tbxEstado.Text = "Configurado canal " + (canalAuxiliar + 1).ToString();
        }


        // Opcion para comenzar a graficar los datos del canal elegido
        private void btnGraficarCanal_Click(object sender, EventArgs e)
        {
            if (btnGraficarCanal.Text == "Graficar canal")
            {
                // Comenzar una nueva grafica
                btnGraficarCanal.Text = "Detener grafica";
                btnConfigurarCanal.Enabled = false;
                btnMedirZ.Enabled = false;
                btnGraficarZ.Enabled = false;
                tbxEstado.Text = "Graficando Senial canal " + (canalAuxiliar + 1).ToString();
                // Inhabilitacion de todas las opciones
                cmbCanalNormal.Enabled = false;
                cmbGanancia.Enabled = false;
                cmbEntrada.Enabled = false;
                cbxBiasNo.Enabled = false;
                cbxBiasSi.Enabled = false;
                cbxSRB1No.Enabled = false;
                cbxSRB1Si.Enabled = false;
                cbxSRB2No.Enabled = false;
                cbxSRB2Si.Enabled = false;
                cbxDefault.Enabled = false;
                cbxEncendidoNo.Enabled = false;
                cbxEncendidoSi.Enabled = false;
                cmbCanalZ.Enabled = false;
                cbxContactoN.Enabled = false;
                cbxContactoP.Enabled = false;
                btnAceptar.Enabled = false;

                // Resta lanzar el Thread grafico
                terminarThreadGrafico = false;
                threadGrafico = new Thread(new ThreadStart(funcionThreadGraficarSenial));
                threadGrafico.Start();
            }
            else
            {
                // Detener la grafica actual
                btnGraficarCanal.Text = "Graficar canal";
                btnConfigurarCanal.Enabled = true;
                btnMedirZ.Enabled = true;
                if (btnMedirZ.Text == "No medir impedancia")
                    btnGraficarZ.Enabled = true;
                tbxEstado.Text = "";
                // Habilitacion de todas las opciones
                cmbCanalNormal.Enabled = true;
                cmbGanancia.Enabled = true;
                cmbEntrada.Enabled = true;
                cbxBiasNo.Enabled = true;
                cbxBiasSi.Enabled = true;
                cbxSRB1No.Enabled = true;
                cbxSRB1Si.Enabled = true;
                cbxSRB2No.Enabled = true;
                cbxSRB2Si.Enabled = true;
                cbxDefault.Enabled = true;
                cbxEncendidoNo.Enabled = true;
                cbxEncendidoSi.Enabled = true;
                cmbCanalZ.Enabled = true;
                cbxContactoN.Enabled = true;
                cbxContactoP.Enabled = true;
                btnAceptar.Enabled = true;

                // Se detiene el thread grafico
                terminarThreadGrafico = true;
            }
        }



        /********** MEDICION DE IMPEDANCIA **********/


        // Seleccion del numero de canal
        private void cmbCanalZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tomar el valor del nuevo canal a mostrar
            canalImpedanciaAuxiliar = cmbCanalZ.SelectedIndex;

            // Mostrar en pantalla los valores del nuevo canal seleccionado

            // Contacto P
            if (Casco.LeerImpedanciaContactoPDelCanal(canalImpedanciaAuxiliar) == DatosCasco.NO_MEDIR_IMPEDANCIA_P)
                cbxContactoP.Checked = false;
            else
                cbxContactoP.Checked = true;

            // Contacto N
            if (Casco.LeerImpedanciaContactoNDelCanal(canalImpedanciaAuxiliar) == DatosCasco.NO_MEDIR_IMPEDANCIA_N)
                cbxContactoN.Checked = false;
            else
                cbxContactoN.Checked = true;

            // Modificacion del texto del boton para indicar si se esta midiendo o no
            if (Casco.LeerImpedanciaMidiendo(canalImpedanciaAuxiliar) == true)
            {
                btnMedirZ.Text = "No medir impedancia";
                btnGraficarZ.Enabled = true;
            }
            else
            {
                btnMedirZ.Text = "Medir impedancia";
                btnGraficarZ.Enabled = false;
            }

        }


        // Medicion del contacto P
        private void cbxContactoP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxContactoP.Checked == false)
                medirImpedanciaContactoP = DatosCasco.NO_MEDIR_IMPEDANCIA_P;
            else
                medirImpedanciaContactoP = DatosCasco.SI_MEDIR_IMPEDANCIA_P;
        }
        

        // Medicion del contacto N
        private void cbxContactoN_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxContactoN.Checked == false)
                medirImpedanciaContactoN = DatosCasco.NO_MEDIR_IMPEDANCIA_N;
            else
                medirImpedanciaContactoN = DatosCasco.SI_MEDIR_IMPEDANCIA_N;
        }


        // Boton para medir la impedancia del canal elegido
        private void btnMedirZ_Click(object sender, EventArgs e)
        {
            if (btnMedirZ.Text == "Medir impedancia")
            {
                // Se debe enviar la orden al casco para que mida la impedancia del canal seleccionado

                // Si la orden se manda correctamente, se almacenan los nuevos valores de la configuracion del canal en cuestion
                Casco.ModificarImpedanciaMidiendo(canalImpedanciaAuxiliar, true);
                Casco.ModificarImpedanciaContactoPDelCanal(canalImpedanciaAuxiliar, medirImpedanciaContactoP);
                Casco.ModificarImpedanciaContactoNDelCanal(canalImpedanciaAuxiliar, medirImpedanciaContactoN);
                btnMedirZ.Text = "No medir impedancia";
                tbxEstado.Text = "Midiendo Impedancia canal " + (canalImpedanciaAuxiliar + 1).ToString();
                btnGraficarZ.Enabled = true;
            }
            else
            {
                // Se debe enviar la orden al casco para que deje de medir la impedancia del canal seleccionado

                // Si la orden se manda correctamente, se registra en la variable indicativa
                Casco.ModificarImpedanciaMidiendo(canalImpedanciaAuxiliar, false);
                btnMedirZ.Text = "Medir impedancia";
                tbxEstado.Text = "";
                btnGraficarZ.Enabled = false;
            }
        }


        // Opcion para comenzar a graficar la impedancia del canal elegido
        private void btnGraficarZ_Click_1(object sender, EventArgs e)
        {
            if (btnGraficarZ.Text == "Graficar Impedancia")
            {
                // Comenzar una nueva grafica
                btnGraficarZ.Text = "Detener grafica";
                btnConfigurarCanal.Enabled = false;
                btnMedirZ.Enabled = false;
                btnGraficarCanal.Enabled = false;
                tbxEstado.Text = "Graficando Impedancia canal " + (canalImpedanciaAuxiliar + 1).ToString();
                // Inhabilitacion de todas las opciones
                cmbCanalNormal.Enabled = false;
                cmbGanancia.Enabled = false;
                cmbEntrada.Enabled = false;
                cbxBiasNo.Enabled = false;
                cbxBiasSi.Enabled = false;
                cbxSRB1No.Enabled = false;
                cbxSRB1Si.Enabled = false;
                cbxSRB2No.Enabled = false;
                cbxSRB2Si.Enabled = false;
                cbxDefault.Enabled = false;
                cbxEncendidoNo.Enabled = false;
                cbxEncendidoSi.Enabled = false;
                cmbCanalZ.Enabled = false;
                cbxContactoN.Enabled = false;
                cbxContactoP.Enabled = false;
                btnAceptar.Enabled = false;
            }
            else
            {
                // Detener la grafica actual
                btnGraficarZ.Text = "Graficar Impedancia";
                btnConfigurarCanal.Enabled = true;
                btnMedirZ.Enabled = true;
                btnGraficarCanal.Enabled = true;
                tbxEstado.Text = "";
                // Habilitacion de todas las opciones
                cmbCanalNormal.Enabled = true;
                cmbGanancia.Enabled = true;
                cmbEntrada.Enabled = true;
                cbxBiasNo.Enabled = true;
                cbxBiasSi.Enabled = true;
                cbxSRB1No.Enabled = true;
                cbxSRB1Si.Enabled = true;
                cbxSRB2No.Enabled = true;
                cbxSRB2Si.Enabled = true;
                cbxDefault.Enabled = true;
                cbxEncendidoNo.Enabled = true;
                cbxEncendidoSi.Enabled = true;
                cmbCanalZ.Enabled = true;
                cbxContactoN.Enabled = true;
                cbxContactoP.Enabled = true;
                btnAceptar.Enabled = true;
            }
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                         FUNCIONES DE CONTROL                                                             */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/


        /****************************************************************/
        /* dibujarCuadricula                                            */
        /*   Deja la grafica en blanco y redibuja la cuadricula         */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void dibujarCuadricula()
        {
            // Borrar el contenido anterior
            imagenSenial.Clear(Color.White);

            // Variables extra para hacer mas legible el codigo
            float xInicial;
            float xFinal;
            float yInicial;
            float yFinal;

            // Crear las cuadriculas
            int proporcionCuadriculaEnX = (LienzoSenial.Width - margenIzquierda - margenDerecha) / divisionesHorizontales;
            int proporcionCuadriculaEnY = (LienzoSenial.Height - margenInferior - margenSuperior) / divisionesVerticales;

            // Dibujar las lineas verticales
            for (int division = 0; division <= divisionesHorizontales; division++)
            {
                xInicial = proporcionCuadriculaEnX * division + margenIzquierda;
                xFinal = xInicial;
                yInicial = margenInferior;
                yFinal = LienzoSenial.Height - margenSuperior;
                imagenSenial.DrawLine(Pens.Black, xInicial, yInicial, xFinal, yFinal);
            }

            // Dibujar las lineas horizontales
            for (int division = 0; division <= divisionesVerticales; division++)
            {
                xInicial = margenIzquierda;
                xFinal = LienzoSenial.Width - margenDerecha;
                yInicial = proporcionCuadriculaEnY * division + margenInferior;
                yFinal = yInicial;
                imagenSenial.DrawLine(Pens.Black, xInicial, yInicial, xFinal, yFinal);
            }
        }



        /****************************************************************/
        /* graficarDatos                                                */
        /*   Grafica los datos contenidos en "listaDatosEjeY"           */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void graficarDatos()
        {
            // Variables extra para hacer mas legible el codigo
            float xInicial;
            float xFinal;
            float yInicial;
            float yFinal;

            // Obtener el valor maximo dentro de los datos para calcular la proporcionalidad y mejorar la escala del grafico
            float proporcionAncho = (LienzoSenial.Width - margenIzquierda - margenDerecha) / (float)listaDatosEjeY.Count;

            // Graficar los valores
            for (int division = 0; division < listaDatosEjeY.Count - 1; division++)
            {
                xInicial = division * proporcionAncho + margenIzquierda;
                xFinal = xInicial + proporcionAncho;
                yInicial = listaDatosEjeY[division];
                yFinal = listaDatosEjeY[division + 1];
                imagenSenial.DrawLine(Pens.Red, xInicial, yInicial, xFinal, yFinal);
            }

            // Escribir los indicadores del maximo y el minimo
            Font fuente = new Font("Arial", 8);
            imagenSenial.DrawString( "Maximo", fuente, Brushes.Red, new Point( 0, 10 ));
            imagenSenial.DrawString( maximoValorDeEntrada.ToString() + "uV", fuente, Brushes.Red, new Point(0, 30));
            imagenSenial.DrawString( "Minimo", fuente, Brushes.Red, new Point( 0, 110 ));
            imagenSenial.DrawString( minimoValorDeEntrada.ToString() + "uV", fuente, Brushes.Red, new Point(0, 130));
        }



        /****************************************************************/
        /* colorearContactoBien                                         */
        /*   Cambia el color del dibujo del contacto indicado a VERDE   */
        /*                                                              */
        /* Recibe: El numero del contacto a modificar                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void colorearContactoBien(int numeroDeContacto)
        {
            switch (numeroDeContacto)
            {
                case 0:
                    imagenCasco.FillEllipse(Brushes.Green, X_Fp1, Y_Fp1, RadioContacto, RadioContacto);
                    break;
                case 1:
                    imagenCasco.FillEllipse(Brushes.Green, X_Fp2, Y_Fp2, RadioContacto, RadioContacto);
                    break;
                case 6:
                    imagenCasco.FillEllipse(Brushes.Green, X_O1, Y_O1, RadioContacto, RadioContacto);
                    break;
                case 7:
                    imagenCasco.FillEllipse(Brushes.Green, X_O2, Y_O2, RadioContacto, RadioContacto);
                    break;
                case 4:
                    imagenCasco.FillEllipse(Brushes.Green, X_P7, Y_P7, RadioContacto, RadioContacto);
                    break;
                case 5:
                    imagenCasco.FillEllipse(Brushes.Green, X_P8, Y_P8, RadioContacto, RadioContacto);
                    break;
                case 2:
                    imagenCasco.FillEllipse(Brushes.Green, X_C3, Y_C3, RadioContacto, RadioContacto);
                    break;
                case 3:
                    imagenCasco.FillEllipse(Brushes.Green, X_C4, Y_C4, RadioContacto, RadioContacto);
                    break;
            }
        }



        /****************************************************************/
        /* colorearContactoRegular                                      */
        /*   Cambia el color del dibujo del contacto indicado a NARANJA */
        /*                                                              */
        /* Recibe: El numero del contacto a modificar                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void colorearContactoRegular(int numeroDeContacto)
        {
            switch (numeroDeContacto)
            {
                case 0:
                    imagenCasco.FillEllipse(Brushes.Orange, X_Fp1, Y_Fp1, RadioContacto, RadioContacto);
                    break;
                case 1:
                    imagenCasco.FillEllipse(Brushes.Orange, X_Fp2, Y_Fp2, RadioContacto, RadioContacto);
                    break;
                case 6:
                    imagenCasco.FillEllipse(Brushes.Orange, X_O1, Y_O1, RadioContacto, RadioContacto);
                    break;
                case 7:
                    imagenCasco.FillEllipse(Brushes.Orange, X_O2, Y_O2, RadioContacto, RadioContacto);
                    break;
                case 4:
                    imagenCasco.FillEllipse(Brushes.Orange, X_P7, Y_P7, RadioContacto, RadioContacto);
                    break;
                case 5:
                    imagenCasco.FillEllipse(Brushes.Orange, X_P8, Y_P8, RadioContacto, RadioContacto);
                    break;
                case 2:
                    imagenCasco.FillEllipse(Brushes.Orange, X_C3, Y_C3, RadioContacto, RadioContacto);
                    break;
                case 3:
                    imagenCasco.FillEllipse(Brushes.Orange, X_C4, Y_C4, RadioContacto, RadioContacto);
                    break;
            }
        }



        /****************************************************************/
        /* colorearContactoMal                                          */
        /*   Cambia el color del dibujo del contacto indicado a ROJO    */
        /*                                                              */
        /* Recibe: El numero del contacto a modificar                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void colorearContactoMal(int numeroDeContacto)
        {
            switch (numeroDeContacto)
            {
                case 0:
                    imagenCasco.FillEllipse(Brushes.Red, X_Fp1, Y_Fp1, RadioContacto, RadioContacto);
                    break;
                case 1:
                    imagenCasco.FillEllipse(Brushes.Red, X_Fp2, Y_Fp2, RadioContacto, RadioContacto);
                    break;
                case 6:
                    imagenCasco.FillEllipse(Brushes.Red, X_O1, Y_O1, RadioContacto, RadioContacto);
                    break;
                case 7:
                    imagenCasco.FillEllipse(Brushes.Red, X_O2, Y_O2, RadioContacto, RadioContacto);
                    break;
                case 4:
                    imagenCasco.FillEllipse(Brushes.Red, X_P7, Y_P7, RadioContacto, RadioContacto);
                    break;
                case 5:
                    imagenCasco.FillEllipse(Brushes.Red, X_P8, Y_P8, RadioContacto, RadioContacto);
                    break;
                case 2:
                    imagenCasco.FillEllipse(Brushes.Red, X_C3, Y_C3, RadioContacto, RadioContacto);
                    break;
                case 3:
                    imagenCasco.FillEllipse(Brushes.Red, X_C4, Y_C4, RadioContacto, RadioContacto);
                    break;
            }
        }



        /****************************************************************/
        /* colorearContactoApagado                                      */
        /*   Cambia el color del dibujo del contacto indicado a NEGRO   */
        /*                                                              */
        /* Recibe: El numero del contacto a modificar                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void colorearContactoApagado(int numeroDeContacto)
        {
            switch (numeroDeContacto)
            {
                case 0:
                    imagenCasco.FillEllipse(Brushes.Black, X_Fp1, Y_Fp1, RadioContacto, RadioContacto);
                    break;
                case 1:
                    imagenCasco.FillEllipse(Brushes.Black, X_Fp2, Y_Fp2, RadioContacto, RadioContacto);
                    break;
                case 6:
                    imagenCasco.FillEllipse(Brushes.Black, X_O1, Y_O1, RadioContacto, RadioContacto);
                    break;
                case 7:
                    imagenCasco.FillEllipse(Brushes.Black, X_O2, Y_O2, RadioContacto, RadioContacto);
                    break;
                case 4:
                    imagenCasco.FillEllipse(Brushes.Black, X_P7, Y_P7, RadioContacto, RadioContacto);
                    break;
                case 5:
                    imagenCasco.FillEllipse(Brushes.Black, X_P8, Y_P8, RadioContacto, RadioContacto);
                    break;
                case 2:
                    imagenCasco.FillEllipse(Brushes.Black, X_C3, Y_C3, RadioContacto, RadioContacto);
                    break;
                case 3:
                    imagenCasco.FillEllipse(Brushes.Black, X_C4, Y_C4, RadioContacto, RadioContacto);
                    break;
            }
        }



        /****************************************************************/
        /* colorearContactoConfigurandose                               */
        /*   Cambia el color del dibujo del contacto indicado a Azul    */
        /*                                                              */
        /* Recibe: El numero del contacto a modificar                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void colorearContactoConfigurandose(int numeroDeContacto)
        {
            switch (numeroDeContacto)
            {
                case 0:
                    imagenCasco.FillEllipse(Brushes.Blue, X_Fp1, Y_Fp1, RadioContacto, RadioContacto);
                    break;
                case 1:
                    imagenCasco.FillEllipse(Brushes.Blue, X_Fp2, Y_Fp2, RadioContacto, RadioContacto);
                    break;
                case 6:
                    imagenCasco.FillEllipse(Brushes.Blue, X_O1, Y_O1, RadioContacto, RadioContacto);
                    break;
                case 7:
                    imagenCasco.FillEllipse(Brushes.Blue, X_O2, Y_O2, RadioContacto, RadioContacto);
                    break;
                case 4:
                    imagenCasco.FillEllipse(Brushes.Blue, X_P7, Y_P7, RadioContacto, RadioContacto);
                    break;
                case 5:
                    imagenCasco.FillEllipse(Brushes.Blue, X_P8, Y_P8, RadioContacto, RadioContacto);
                    break;
                case 2:
                    imagenCasco.FillEllipse(Brushes.Blue, X_C3, Y_C3, RadioContacto, RadioContacto);
                    break;
                case 3:
                    imagenCasco.FillEllipse(Brushes.Blue, X_C4, Y_C4, RadioContacto, RadioContacto);
                    break;
            }
        }



        /****************************************************************/
        /* timerInicial_Tick                                            */
        /*   Inicia las graficas                                        */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void timerInicial_Tick(object sender, EventArgs e)
        {
            // Dibujar la cuadricula para el grafico de las seniales
            dibujarCuadricula();

            // Dibujar los contactos del casco
            for (int indice = 0; indice < cantidadDeCanales; indice++)
                colorearContactoApagado(indice);

            // Al no volver a utilizarse, se elimina
            timerInicial.Stop();
            timerInicial.Dispose();

        }



        /****************************************************************/
        /* graficarSenialNormal                                         */
        /*   Grafica las muestras recolectadas del canal en cuestion    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void graficarSenialNormal()
        {
            // Primero se borra lo previo y se dibuja la cuadricula
            dibujarCuadricula();

            // Luego se leen los datos
            listaDatosEjeY = Casco.LeerListaDelCanal( canalAuxiliar + 1 );

            // Y se escalan al grafico
            escalarDatosAlGrafico();

            // Por ultimo se grafican
            graficarDatos();
        }



        /****************************************************************/
        /* graficarSenialNormal                                         */
        /*   Grafica las muestras recolectadas del canal en cuestion    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void escalarDatosAlGrafico()
        {
            // Limites para el grafico
            Int32 maximoValorDeSalida;
            Int32 minimoValorDeSalida;
            Int32 diferenciaDeEntrada;
            Int32 diferenciaDeSalida;

            maximoValorDeEntrada = listaDatosEjeY.Max();
            minimoValorDeEntrada = listaDatosEjeY.Min();
            maximoValorDeSalida = margenSuperior;
            minimoValorDeSalida = LienzoSenial.Height - margenInferior;
            diferenciaDeEntrada = maximoValorDeEntrada - minimoValorDeEntrada;
            diferenciaDeSalida = maximoValorDeSalida - minimoValorDeSalida;

            if (diferenciaDeEntrada == 0)
                diferenciaDeEntrada = 100;

            for( int indice = 0; indice < listaDatosEjeY.Count; indice++ )
                listaDatosEjeY[indice] = (Int32) ( ( listaDatosEjeY[indice] - minimoValorDeEntrada ) * ( (double) diferenciaDeSalida / diferenciaDeEntrada ) ) + minimoValorDeSalida;
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                            THREADS GRAFICOS                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/


        /****************************************************************/
        /* funcionThreadGraficarSenial                                  */
        /*   Manda la orden al casco para que comienze el envio de las  */
        /*   muestras y las recolecta durante 2 segundos. Frena el      */
        /*   envio y grafica los valores del canal seleccionado.        */
        /*   Continua asi en un loop perpetuo hasta que se cancele      */
        /*   mediante el boton para "dejar de graficar"                 */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void funcionThreadGraficarSenial()
        {
            bool error = false;

            while (terminarThreadGrafico == false)
            {
                // Se borran los datos previos de las mediciones
                Casco.BorrarDatos();

                // Se da la orden al casco para que comience el envio de las muestras
                if (Casco.IniciarEnvioDeDatos() == false) { error = true; }

                // Se actualiza el limite de tramas a recibir en funcion del tiempo del estudio
                Casco.ActualizarMaximoDeTramasARecibir(dosSegundos);

                // Bucle para la recepcion de datos
                for (Int32 indice = 0; indice < Casco.MaximoTramasARecibir; indice++ )
                    { if (Casco.AdquirirDatos() == false) { error = true; break; } }

                // Luego de tomar las muestras, se da la orden al casco para que cese el envio mientras se procesan los datos
                if (Casco.DetenerEnvioDeDatos() == false) { error = true; }

                // Si no hay indicacion de error en la recepcion de los datos, se procede a procesarlos y almacenarlos
                if (error == false)
                {
                    // Luego de detener la comunicacion, pueden quedar tramas sin procesar en el buffer de recepcion
                    while (Casco.LongitudDeLaLista < Casco.MaximoTramasARecibir)
                    { if (Casco.AdquirirDatos() == false) { error = true; break; } }

                    // Sea que termino por falta de datos o porque completo la lista, resta acondicionar los datos
                    Casco.AcondicionarDatos();

                    // Finalmente se deben graficar los datos recolectados
                    graficarSenialNormal();
                }

                // Si hubo un error en la comunicacion, se informa al usuario mediante un mensaje y se reinicia la comunicacion principal
                else
                {
                    MessageBox.Show( "Surgieron problemas con la comunicacion. Debe reiniciarse." );
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }

            }
        }





    }
}
