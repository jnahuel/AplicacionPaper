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
using System.Data.SQLite;
using System.IO;

namespace AplicacionPaper
{
    public partial class Form1 : Form
    {

        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/


        DatosCasco Casco;
        EstimulacionSecuencial EstimulosSecuenciales;
        ConfiguracionEstimulacion ConfiguracionDeLosEstimulos;
        EstimulacionFrecuencial EstimulosFrecuenciales;


        MLApp.MLApp funcionMatlab = new MLApp.MLApp();  // Objeto para ejecutar el codigo de Matlab
        object[] decisionTomada;                        // Para almacenar el resultado del algoritmo


        // Variables de Threads
        Thread threadEstimulacionSecuencial;
        Thread threadEstimulacionFrecuencial;
        Thread threadCuentaRegresiva;
        Thread threadLecturaPuertoSerie;
        bool threadsIniciados = false;
        bool finalizarThreadLecturaPuertoSerie = false;
        bool finalizoThreadEstimulacionGrafica = false;


        // Datos personales
        string apellido = "Prueba";
        string nombre = "Prueba";
        int edad = 20;
        bool experiencia = false;


        // Variables para manejar la base de datos
        private String conexionBaseDatosString;
        private SQLiteConnection conexionBaseDatos;
        DateTime fechaHora = DateTime.Now;
        private string fechaInicioEstudio;
        private string horaInicioEstudio;


        // Orden para ingresar un nuevo registro a la base de datos
        private String SQLInsert = "INSERT INTO BdDAplicacionPaper(Apellido, Nombre, Edad, Experiencia, TiempoDeDescanso, TiempoDeExcitacion, TiempoDeEstudio, CantidadDeSimbolos, SimboloElegido, SimboloResultante, Dia, Hora, ColorDeFondo, ColorLetraNormal, ColorLetraResaltada, TamanioLetra, TamanioPantalla, CantidadDeFilas, Archivo, Secuencia) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
        // Orden para leer toda la base de datos
//        private String SQLSelect = "SELECT * FROM BdDAplicacionPaper";




        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        
        public Form1()
        {
            // Cogido necesario para Windows
            InitializeComponent();

            // Para poder controlar los objetos graficos del formulario desde otros Threads sin usar delegados
            CheckForIllegalCrossThreadCalls = false;

            Casco = new DatosCasco();
            EstimulosSecuenciales = new EstimulacionSecuencial();
            ConfiguracionDeLosEstimulos = new ConfiguracionEstimulacion();
            EstimulosFrecuenciales = new EstimulacionFrecuencial();

            // Por defecto, se muestran inicialmente 2 opciones
            ConfiguracionDeLosEstimulos.CantidadOpciones = 2;
            agregarBotones(ConfiguracionDeLosEstimulos.CantidadOpciones);
            redimensionarBotones();

            // Ciertas opciones no están disponibles hasta no configurar correctamente los parametros necesarios
            detenerToolStripMenuItem.Enabled = false;
            reiniciarToolStripMenuItem.Enabled = false;
            accionesToolStripMenuItem.Enabled = false;
            hardwareToolStripMenuItem.Enabled = false;

            // Inicializacion de la conexion con la base de datos
            conexionBaseDatosString = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            conexionBaseDatos = new SQLiteConnection(conexionBaseDatosString);
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                              OPCIONES DEL MENU DE CONFIGURACIÓN                                                          */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Submenu para iniciar la configuración de la comunicación con el casco
        private void comunicacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Flag para indicar si se quiere mostrar o no el mensaje inicial que envia el casco luego de establecer la comunicacion
            bool mostrarInformacion = false;

            PuertoSerie formularioConfiguracionPuertoSerie = new PuertoSerie();
            if (formularioConfiguracionPuertoSerie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Casco.EstablecerComunicacionSerie(formularioConfiguracionPuertoSerie.nombreDelPuerto, mostrarInformacion) == true)
                {
                    accionesToolStripMenuItem.Enabled = true;
                    comunicacionToolStripMenuItem.Enabled = false;
                    iniciarToolStripMenuItem.Enabled = true;
                    detenerToolStripMenuItem.Enabled = false;
                    reiniciarToolStripMenuItem.Enabled = true;
                    hardwareToolStripMenuItem.Enabled = true;
                    MessageBox.Show("Se establecio la comunicacion correctamente.");
                }
                else { MessageBox.Show(Casco.Error); }
            }
        }

        // Opción para realizar la configuración visual de los objetos
        private void menuConfiguracionVisual_Click(object sender, EventArgs e)
        {
            using (var formularioConfiguracionVisual = new Visual(  ConfiguracionDeLosEstimulos.ColorDeFondo,
                                                                    ConfiguracionDeLosEstimulos.ColorExcitacion,
                                                                    ConfiguracionDeLosEstimulos.ColorDescanso,
                                                                    ConfiguracionDeLosEstimulos.TamanioLetra,
                                                                    ConfiguracionDeLosEstimulos.Filas))
            {
                var resultado = formularioConfiguracionVisual.ShowDialog();
                if (resultado == DialogResult.OK)   // Se eligieron nuevos valores y se deben cambiar los actuales
                {
                    // Almacenado de los nuevos datos
                    ConfiguracionDeLosEstimulos.ColorDeFondo = formularioConfiguracionVisual.colorFondo;
                    ConfiguracionDeLosEstimulos.ColorDescanso = formularioConfiguracionVisual.colorLetraNormal;
                    ConfiguracionDeLosEstimulos.ColorExcitacion = formularioConfiguracionVisual.colorLetraResaltada;
                    ConfiguracionDeLosEstimulos.Filas = formularioConfiguracionVisual.cantidadFilas;
                    ConfiguracionDeLosEstimulos.TamanioLetra = formularioConfiguracionVisual.tamanioLetra;

                    // Se rehacen los botones con los nuevos parametros
                    if (borrarBotones() == false) { MessageBox.Show("Hubo un error al intentar borrar los botones"); }
                    agregarBotones(ConfiguracionDeLosEstimulos.CantidadOpciones);
                    redimensionarBotones();
                }
            }
        }

        // Submenú para ingresar los datos personales
        private void datosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formularioConfiguracionDatosPersonales = new TomarDatosPersonales( apellido, nombre, edad, experiencia ))
            {
                var resultado = formularioConfiguracionDatosPersonales.ShowDialog();
                if (resultado == DialogResult.OK)   // Se eligieron nuevos valores y se deben cambiar los actuales
                {
                    apellido = formularioConfiguracionDatosPersonales.apellido;
                    nombre = formularioConfiguracionDatosPersonales.nombre;
                    edad = formularioConfiguracionDatosPersonales.edad;
                    experiencia = formularioConfiguracionDatosPersonales.experiencia;
                }
            }
        }

        // Submenu para realizar las configuraciones del estudio secuencial
        private void secuencialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formularioConfiguracionSecuencial = new OpcionesSecuenciales(EstimulosSecuenciales.TiempoDeDescanso,
                                                                            EstimulosSecuenciales.TiempoDeExcitacion,
                                                                            EstimulosSecuenciales.TiempoEstudioSegundos,
                                                                            ConfiguracionDeLosEstimulos.CantidadOpciones,
                                                                            EstimulosSecuenciales.TextoTipoDeSecuenciaActual,
                                                                            ConfiguracionDeLosEstimulos.TextosBotones
                                                                        ))
            {
                var resultado = formularioConfiguracionSecuencial.ShowDialog();
                if (resultado == DialogResult.OK)   // Se eligieron nuevos valores y se deben cambiar los actuales
                {
                    ConfiguracionDeLosEstimulos.CantidadOpciones = formularioConfiguracionSecuencial.totalDeOpciones;
                    EstimulosSecuenciales.CantidadOpciones = ConfiguracionDeLosEstimulos.CantidadOpciones;
                    EstimulosSecuenciales.TiempoDeDescanso = formularioConfiguracionSecuencial.tiempoDeDescanso;
                    EstimulosSecuenciales.TiempoDeExcitacion = formularioConfiguracionSecuencial.tiempoDeExcitacion;
                    EstimulosSecuenciales.TiempoEstudioSegundos = formularioConfiguracionSecuencial.tiempoTotal;
                    ConfiguracionDeLosEstimulos.TiempoEstudioSegundos = EstimulosSecuenciales.TiempoEstudioSegundos;
                    EstimulosSecuenciales.TextoTipoDeSecuenciaActual = formularioConfiguracionSecuencial.tipoDeSecuencia;
                    ConfiguracionDeLosEstimulos.TipoDeEstudio = ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL;
                    ConfiguracionDeLosEstimulos.TextosBotones = formularioConfiguracionSecuencial.listaSimbolos;

                    if (borrarBotones() == false) { MessageBox.Show("Hubo un error al intentar borrar los botones"); }
                    agregarBotones(ConfiguracionDeLosEstimulos.CantidadOpciones);
                    redimensionarBotones();

                }
            }
        }

        // Submenu para realizar las configuraciones del estudio frecuencial
        private void frecuencialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formularioConfiguracionFrecuencial = new OpcionesFrecuenciales(  ConfiguracionDeLosEstimulos.CantidadOpciones,
                                                                                        EstimulosFrecuenciales.TiempoDelEstudioEnSegundos,
                                                                                        ConfiguracionDeLosEstimulos.TextosBotones))
            {
                var resultado = formularioConfiguracionFrecuencial.ShowDialog();
                if (resultado == DialogResult.OK)   // Se eligieron nuevos valores y se deben cambiar los actuales
                {
                    ConfiguracionDeLosEstimulos.TipoDeEstudio = ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_FRECUENCIAL;
                    ConfiguracionDeLosEstimulos.CantidadOpciones = formularioConfiguracionFrecuencial.cantidadDeOpcionesFrecuenciales;
                    ConfiguracionDeLosEstimulos.TextosBotones = formularioConfiguracionFrecuencial.listaSimbolos;
                    EstimulosFrecuenciales.TiempoDelEstudioEnSegundos = formularioConfiguracionFrecuencial.tiempoDelEstudio;
                    EstimulosFrecuenciales.CantidadDeOpciones = ConfiguracionDeLosEstimulos.CantidadOpciones;
                    EstimulosFrecuenciales.ListaDePeriodos = formularioConfiguracionFrecuencial.listaPeriodos;

                    if (borrarBotones() == false) { MessageBox.Show("Hubo un error al intentar borrar los botones"); }
                    agregarBotones(ConfiguracionDeLosEstimulos.CantidadOpciones);
                    redimensionarBotones();
                }

            }
        }

        // Submenu para realizar las configuraciones de funcionamiento del hardware del casco
        private void hardwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarHardware formularioConfiguracionHardware = new ConfigurarHardware( Casco );
            if (formularioConfiguracionHardware.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                // Se corto la comunicacion en el formulario y se debe reiniciar
                comunicacionToolStripMenuItem.Enabled = true;
                reiniciarToolStripMenuItem.Enabled = false;
                accionesToolStripMenuItem.Enabled = false;

                // Se cierra el puerto serie
                Casco.TerminarComunicacionSerie();
            }
            else
            {
                // Salio bien y se reconfiguraron los canales
                Casco = formularioConfiguracionHardware.Casco;
            }
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                 OPCIONES DEL MENU DE ACCIONES                                                            */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Iniciar un nuevo estudio
        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarInformacionPersonal formularioMostrarInformacionPersonal = new MostrarInformacionPersonal( apellido, nombre, edad, experiencia,
                                                                                    EstimulosSecuenciales.TiempoDeDescanso,
                                                                                    EstimulosSecuenciales.TiempoDeExcitacion,
                                                                                    ConfiguracionDeLosEstimulos.TiempoEstudioSegundos,
                                                                                    ConfiguracionDeLosEstimulos.CantidadOpciones,
                                                                                    ConfiguracionDeLosEstimulos.ColorDescanso,
                                                                                    ConfiguracionDeLosEstimulos.ColorExcitacion,
                                                                                    ConfiguracionDeLosEstimulos.ColorDeFondo,
                                                                                    ConfiguracionDeLosEstimulos.TamanioLetra,
                                                                                    EstimulosSecuenciales.TextoTipoDeSecuenciaActual,
                                                                                    ConfiguracionDeLosEstimulos.Filas,
                                                                                    ConfiguracionDeLosEstimulos.TextosBotones,
                                                                                    ConfiguracionDeLosEstimulos.TipoDeEstudioTexto,
                                                                                    EstimulosFrecuenciales.ListaDePeriodos);

            if( formularioMostrarInformacionPersonal.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                // Se almacena el dato de la opción elegida
                ConfiguracionDeLosEstimulos.TextoOpcionElegida = formularioMostrarInformacionPersonal.opcionElegida;

                // Se quitan los botones y las opciones de maximizar y minimizar la ventana
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

                // Se muestra el conteo inicial para que la persona se prepare
                threadCuentaRegresiva = new Thread(new ThreadStart(funcionThreadCuentaRegresiva));
                threadCuentaRegresiva.Start();

                // Se toma la fecha y hora de comienzo
                fechaHora = DateTime.Now;
                fechaInicioEstudio = fechaHora.ToShortDateString();
                horaInicioEstudio = fechaHora.ToLongTimeString();

                // Se habilita el botón para detener el estudio en curso y se quita la posibilidad de iniciar uno nuevo
                detenerToolStripMenuItem.Enabled = true;
                iniciarToolStripMenuItem.Enabled = false;
                reiniciarToolStripMenuItem.Enabled = false;
                hardwareToolStripMenuItem.Enabled = false;

                // A su vez, mientras esté en curso, no pueden modificar los parametros
                configuracionToolStripMenuItem.Enabled = false;

                // Se sincroniza con la finalizacion del Thread de la cuenta regresiva
                threadCuentaRegresiva.Join();

                // En lugar de usar Thread.Abort, se controla la finalización del Thread mediante una variable
                finalizarThreadLecturaPuertoSerie = false;

                // Se asigna la funcion del Thread para la recepcion de datos y se lo lanza a ejecucion
                threadLecturaPuertoSerie = new Thread(new ThreadStart(funcionThreadLecturaPuertoSerie));
                threadLecturaPuertoSerie.Start();
                threadsIniciados = true;
            }
        }

        // Detener el estudio en ejecucion
        private void detenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Primero se cancela el thread gráfico
            switch (ConfiguracionDeLosEstimulos.TipoDeEstudio)
            {
                case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL:
                    try { threadEstimulacionSecuencial.Abort(); }
                    catch (ThreadAbortException) { };
                    break;

                case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_FRECUENCIAL:
                    EstimulosFrecuenciales.TerminarTodosLosSubThreads();
                    try { threadEstimulacionFrecuencial.Abort(); }
                    catch (ThreadAbortException) { }
                    break;
            }

            // Se cancela el thread de lectura del puerto serie
            try { threadLecturaPuertoSerie.Abort(); }
            catch (ThreadAbortException) { };

            // Se da la orden al casco para que deje de enviar muestras
            Casco.DetenerEnvioDeDatos();

            // Detenido el estudio, se puede iniciar otro junto con la configuración
            iniciarToolStripMenuItem.Enabled = true;
            detenerToolStripMenuItem.Enabled = false;
            reiniciarToolStripMenuItem.Enabled = true;
            configuracionToolStripMenuItem.Enabled = true;
            hardwareToolStripMenuItem.Enabled = true;

            // Por último, se repasan todos los botones para que ninguno quede en estado de excitación
            foreach (Control boton in Controls) { if (boton is Button) { boton.ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDescanso); } }

            // Se reestablecen las opciones para modificar el tamaño de la pantalla
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        }

        // Reiniciar la comunicacion con el casco
        private void reiniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comunicacionToolStripMenuItem.Enabled = true;
            reiniciarToolStripMenuItem.Enabled = false;
            accionesToolStripMenuItem.Enabled = false;
            hardwareToolStripMenuItem.Enabled = false;

            // Se cierra el puerto serie
            Casco.TerminarComunicacionSerie();
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                        FUNCIONES PARA EL CONTROL GRÁFICO DE LOS BOTONES                                                  */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Esta rutina se encarga de reacomodar los botones cuando la pantalla cambie de normal a maximizada o a la inversa
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    ConfiguracionDeLosEstimulos.PantallaActual = ConfiguracionEstimulacion.estadosPantalla.Maximizada;
                    ConfiguracionDeLosEstimulos.TamanioPantalla = "Maximizada";
                    break;
                case FormWindowState.Minimized:
                    ConfiguracionDeLosEstimulos.PantallaActual = ConfiguracionEstimulacion.estadosPantalla.Minimizada;
                    ConfiguracionDeLosEstimulos.TamanioPantalla = "Minimizada";
                    break;
                case FormWindowState.Normal:
                    ConfiguracionDeLosEstimulos.PantallaActual = ConfiguracionEstimulacion.estadosPantalla.Normal;
                    ConfiguracionDeLosEstimulos.TamanioPantalla = "Normal";
                    break;
            }

            if ( ! ( ConfiguracionDeLosEstimulos.PantallaActual == ConfiguracionEstimulacion.estadosPantalla.Minimizada
                 ||  ConfiguracionDeLosEstimulos.PantallaAnterior == ConfiguracionEstimulacion.estadosPantalla.Minimizada) ) { redimensionarBotones(); }

            ConfiguracionDeLosEstimulos.PantallaAnterior = ConfiguracionDeLosEstimulos.PantallaActual;
        }

        // "redimensionarBotones" se encarga de reasignar los tamaños y posiciones para que ocupen toda la pantalla. No devuelve ni recibe nada
        private void redimensionarBotones()
        {
            // Variables auxiliares para los cálculos
            int anchoPantalla = this.Width;                                     // Es la distancia horizontal en píxeles disponible en la aplicación
            int altoPantalla = this.Height;                                     // Idem superior
            int margenHorizontal = 15;                                          // Se toma una pequeña guarda horizontal, por las dudas. De no ser necesaria, no se usará
            int margenVertical = 40;                                            // Idem superior
            int altoMenus = this.menuStrip1.Height;                             // Alto del menu
            int anchoDisponible = anchoPantalla - margenHorizontal;             // Es el ancho de la gráfica disponible para los botones
            int altoDisponible = altoPantalla - altoMenus - margenVertical;     // Idem superior

            int anchoBoton = anchoDisponible / (ConfiguracionDeLosEstimulos.CantidadOpciones / ConfiguracionDeLosEstimulos.Filas);
            int altoBoton = altoDisponible / ConfiguracionDeLosEstimulos.Filas;

            int indiceAuxiliar = 0, segundaFila = 0;
            foreach (Control boton in Controls)
            {
                if (boton is Button)
                {
                    boton.Size = new Size(anchoBoton, altoBoton);
                    boton.Location = new Point(indiceAuxiliar * anchoBoton, altoMenus + altoBoton * segundaFila);
                    if (++indiceAuxiliar > (ConfiguracionDeLosEstimulos.CantidadOpciones / 2 - 1) && ConfiguracionDeLosEstimulos.Filas > 1)
                    {
                        indiceAuxiliar = 0;
                        segundaFila = 1;
                    }
                }
            }
            actualizarLetras();
        }

        // Funcion para ajustar las letras de todos los botones
        private void actualizarLetras()
        {
            ConfiguracionDeLosEstimulos.ActualizarEscalaDeLetra();

            foreach (Control boton in Controls)
            {
                if (boton is Button)
                {
                    if (boton.Height > boton.Width) { boton.Font = new Font("Times new roman", boton.Width / ConfiguracionDeLosEstimulos.EscalaDeLetra); }
                    else { boton.Font = new Font("Times new roman", boton.Height / ConfiguracionDeLosEstimulos.EscalaDeLetra); }
                }
            }
        }

        // "borrarBotones" hace un ciclo y borra todos los botones presentes en el formulario.
        private bool borrarBotones()
        {
            //  Si en "maximosIntentos" de pasadas no logra borrarlos, lo indica devolviendo False
            const int maximosIntentos = 10;

            for (int veces = 0, botonesBorrados; veces < maximosIntentos; veces++)
            {
                botonesBorrados = 0;
                foreach (Control boton in this.Controls)
                {
                    // Este ciclo recorre todos los controles gráficos dentro del formulario
                    if (boton is Button)
                    {
                        // Esto se ejecuta cada vez que encuentre un elemento gráfico del tipo "Button", los cuales debe eliminar
                        boton.Dispose();
                        botonesBorrados++;
                    }
                }
                if (botonesBorrados == 0) { return (true); }
            } return (false);
        }

        // Funcion para agregar botones
        private void agregarBotones(int cantidadDeBotonesAgregar)
        {
            for (int indiceBotonesNuevos = 0; indiceBotonesNuevos < cantidadDeBotonesAgregar; indiceBotonesNuevos++)
            {
                Button botonAgregado = new Button();                            // Creación del objeto tipo "Button"
                this.Controls.Add(botonAgregado);                               // Se agrega al resto de los controles gráficos
                botonAgregado.FlatStyle = FlatStyle.Flat;                       // Al ser "FlatStyle.Flat", permite cambiar el color de los bordes
                botonAgregado.FlatAppearance.BorderColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDeFondo);
                botonAgregado.FlatAppearance.MouseDownBackColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDeFondo);
                botonAgregado.FlatAppearance.MouseOverBackColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDeFondo);
                botonAgregado.BackColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDeFondo);
                botonAgregado.ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDescanso);
                botonAgregado.Text = ConfiguracionDeLosEstimulos.TextoBoton(indiceBotonesNuevos);
                botonAgregado.Name = "button" + indiceBotonesNuevos.ToString();
            }
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                         THREADS GRAFICOS                                                                 */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        /***************** ESTIMULACION SECUENCIAL *****************/

        // Este es el thread que controla los botones mediante la excitacion secuencial
        private void funcionThreadEstimulacionSecuencial()
        {
            // Se resetean los datos para iniciar un nuevo estudio
            EstimulosSecuenciales.IniciarNuevaSegmentacion();

            for (int secuencia = 0, botonSolicitado; secuencia < EstimulosSecuenciales.TotalDeSecuencias; secuencia++)
            {
                for (int botonesRepasados = 0; botonesRepasados < EstimulosSecuenciales.CantidadOpciones; botonesRepasados++)
                {
                    botonSolicitado = EstimulosSecuenciales.EstimuloActual;

                    // Se resalta el símbolo
                    this.Controls["button" + botonSolicitado.ToString()].ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorExcitacion);
                    EstimulosSecuenciales.ColocarMarcaInicialDeEstimulo();              // Se coloca la marca de inicio de sección del símbolo
                    Thread.Sleep(EstimulosSecuenciales.TiempoDeExcitacionThread);

                    // Se deja el símbolo en estado normal
                    this.Controls["button" + botonSolicitado.ToString()].ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDescanso);
                    EstimulosSecuenciales.ColocarMarcaFinalDeEstimulo();                // Se coloca la marca de fin de la seccion del símbolo
                    Thread.Sleep(EstimulosSecuenciales.TiempoDeDescansoThread);

                    EstimulosSecuenciales.SiguienteEstimulo();
                }
            }   // Fin del "for( Recorrer la secuencia )"

            // Aqui termino de ejecutar todas las secuencias. Solo resta colocar la marca final del estudio y termina el Thread
            EstimulosSecuenciales.FinalizarSegmentacion();

            // Se indica mediante la variable de flag que el Thread grafico finalizo su ejecucion
            finalizoThreadEstimulacionGrafica = true;
        }



        /***************** ESTIMULACION FRECUENCIAL *****************/

        // Encargado de organizar los subThreads, lanzarlos y cancelarlos cuando se complete el tiempo del estudio
        private void funcionThreadEstimulacionFrecuencial()
        {
            // Reiniciar el objeto encargado de controlar los subthreads
            EstimulosFrecuenciales.BorrarDatos();

            // Organizar los threads
            for (Int32 indice = 0; indice < EstimulosFrecuenciales.CantidadDeOpciones; indice++)
            {
                EstimulosFrecuenciales.AgregarSubThread( new Thread( funcionSubThreadFrecuencial ) );
                EstimulosFrecuenciales.AgregarNombre( "button" + indice.ToString() );
            }

            // Ponerlos en ejecucion
            if (EstimulosFrecuenciales.LanzarTodosLosSubThreads() == false)
            {
                MessageBox.Show("No hay sub threads listos para ejecutar");
                try { threadEstimulacionFrecuencial.Abort(); }
                catch (ThreadAbortException) { }
            }

            // Demorar el tiempo necesario para el estudio
            try { Thread.Sleep( EstimulosFrecuenciales.TiempoDelEstudioEnMiliSegundos ); }
            catch (ThreadInterruptedException) { }

            // Luego del tiempo del estudio se deben terminar todos los subthreads
            EstimulosFrecuenciales.TerminarTodosLosSubThreads();

            // Por ultimo, se deja la interfaz grafica en condiciones neutras
            for (Int32 indice = 0; indice < EstimulosFrecuenciales.CantidadDeOpciones; indice++)
            {
                this.Controls[EstimulosFrecuenciales.NombreDelBoton(indice)].ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDescanso);
            }

            // Se indica mediante la variable de flag que el Thread grafico finalizo su ejecucion
            finalizoThreadEstimulacionGrafica = true;
        }


        // SubThread encargado de controlar solamente un objeto grafico, el cual reconoce mediante la indicacion de su numero de SubThread
        public void funcionSubThreadFrecuencial(object numeroDeSubThread)
        {
            while (true)
            {
                this.Controls[ EstimulosFrecuenciales.NombreDelBoton(  (Int32) numeroDeSubThread)  ].ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorExcitacion);
                Thread.Sleep(  EstimulosFrecuenciales.PeriodoDelBoton( (Int32) numeroDeSubThread ) );
                this.Controls[ EstimulosFrecuenciales.NombreDelBoton(  (Int32) numeroDeSubThread)  ].ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDescanso);
                Thread.Sleep(  EstimulosFrecuenciales.PeriodoDelBoton( (Int32) numeroDeSubThread ) );
            }
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                           THREAD SERIE                                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Este es el thread que se encarga de la recepción de datos por el puerto serie
        private void funcionThreadLecturaPuertoSerie()
        {
            // Variable para indicar si algo falla en el proceso de recepcion de los datos
            bool error = false;

            // Se borran los datos previos de las mediciones
            Casco.BorrarDatos();

            // Se da la orden al casco para que comience el envio de las muestras
            if (Casco.IniciarEnvioDeDatos() == false) { error = true; }

            // En funcion del tipo de estudio a realizar, se lanza el Thread grafico correspondiente
            switch( ConfiguracionDeLosEstimulos.TipoDeEstudio )
            {
                case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL:
                    threadEstimulacionSecuencial = new Thread(new ThreadStart(funcionThreadEstimulacionSecuencial));
                    threadEstimulacionSecuencial.Start();
                    break;

                case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_FRECUENCIAL:
                    threadEstimulacionFrecuencial = new Thread(new ThreadStart(funcionThreadEstimulacionFrecuencial));
                    threadEstimulacionFrecuencial.Start();
                    break;
            }

            // Se actualiza el limite de tramas a recibir en funcion del tiempo del estudio
            Casco.ActualizarMaximoDeTramasARecibir(ConfiguracionDeLosEstimulos.TiempoEstudioSegundos);

            // Se resetea el flag indicador de finalizacion del Thread grafico
            finalizoThreadEstimulacionGrafica = false;

            // Bucle para la recepcion de datos
            while ( !finalizarThreadLecturaPuertoSerie && finalizoThreadEstimulacionGrafica == false)
                { if ( Casco.AdquirirDatos() == false ) { error = true; break; } }

            // Luego de tomar las muestras, se da la orden al casco para que cese el envio mientras se procesan los datos
            if (Casco.DetenerEnvioDeDatos() == false) { error = true; }

            // Si no hay indicacion de error en la recepcion de los datos, se procede a procesarlos y almacenarlos
            if (error == false)
            {
                // Luego de detener la comunicacion, pueden quedar tramas sin procesar en el buffer de recepcion
                while (Casco.LongitudDeLaLista < Casco.MaximoTramasARecibir)
                    { if ( Casco.AdquirirDatos() == false ) { error = true; break; } }

                // Sea que termino por falta de datos o porque completo la lista, resta acondicionar los datos
                Casco.AcondicionarDatos();

                // Se verifica por las dudas que el Thread grafico haya finalizado. Si no es asi, se lo cancela
                if (finalizoThreadEstimulacionGrafica == false)
                {
                    switch( ConfiguracionDeLosEstimulos.TipoDeEstudio )
                    {
                        case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL:
                            try   { threadEstimulacionSecuencial.Abort(); }
                            catch (ThreadAbortException) { }
                            break;

                        case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_FRECUENCIAL:
                            EstimulosFrecuenciales.TerminarTodosLosSubThreads();
                            try { threadEstimulacionFrecuencial.Abort(); }
                            catch (ThreadAbortException) { }
                            break;
                    }
                }

                // En este punto, se termino el Thread grafico por cumplirse el tiempo del estudio y se acondicionaron los datos. Resta guardarlos
                AlmacenarEstudioFinalizadoCorrectamente();
            }
            
            // Ante algun error en la comunicacion, se cancela el Thread grafico y de descartan los datos
            else
            {
                // Se verifica por las dudas que el Thread grafico haya finalizado. Si no es asi, se lo cancela
                if (finalizoThreadEstimulacionGrafica == false)
                {
                    switch (ConfiguracionDeLosEstimulos.TipoDeEstudio)
                    {
                        case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL:
                            try { threadEstimulacionSecuencial.Abort(); }
                            catch (ThreadAbortException) { }
                            break;

                        case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_FRECUENCIAL:
                            EstimulosFrecuenciales.TerminarTodosLosSubThreads();
                            try { threadEstimulacionFrecuencial.Abort(); }
                            catch (ThreadAbortException) { }
                            break;
                    }
                }

                // Se deshabilitan las opciones para iniciar un nuevo estudio o para detenerlo, ya que se debe reiniciar la comunicacion
                iniciarToolStripMenuItem.Enabled = false;
                detenerToolStripMenuItem.Enabled = false;
                reiniciarToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;
                hardwareToolStripMenuItem.Enabled = false;

                // Se dejan todos los botones en estado de descanso
                foreach (Control boton in Controls)
                {
                    if (boton is Button)    { boton.ForeColor = Color.FromName(ConfiguracionDeLosEstimulos.ColorDescanso); }
                }

                // Se indica al usuario la condicion para que reinicie el estudio
                MessageBox.Show(Casco.Error);
            }

        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                       FUNCION PARA GUARDAR LOS DATOS CUANDO EL ESTUDIO TERMINE BIEN                                      */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Si se ejecuta, es que el estudio finalizo correctamente
        private void AlmacenarEstudioFinalizadoCorrectamente()
        {
            // Tiene que dejar las condiciones necesarias para iniciar un nuevo estudio
            iniciarToolStripMenuItem.Enabled = true;
            detenerToolStripMenuItem.Enabled = false;
            reiniciarToolStripMenuItem.Enabled = true;
            configuracionToolStripMenuItem.Enabled = true;
            hardwareToolStripMenuItem.Enabled = true;

            // Se reestablecen las opciones para modificar el tamaño de la pantalla
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            // En funcion del tipo de estudio, es el script de Matlab encargado de procesarlo
            switch (ConfiguracionDeLosEstimulos.TipoDeEstudio)
            {
                case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL:
                    if (ConfiguracionDeLosEstimulos.CantidadOpciones == 2)
                        funcionMatlab.Execute(@"cd " + Directory.GetCurrentDirectory() + @"\MatlabScripts\DosOpciones");
                    else
                        funcionMatlab.Execute(@"cd " + Directory.GetCurrentDirectory() + @"\MatlabScripts\SeisOpciones");
                    object resultadoMatlab = null;
                    funcionMatlab.Feval("script_Principal", 1, out resultadoMatlab, Casco.LeerCanal( DatosCasco.CANAL_O1 ), EstimulosSecuenciales.MarkersPosiciones);
                    decisionTomada = resultadoMatlab as object[];
                    break;
            }

            // Finalizado correctamente el estudio, se almacenan los valores en la base de datos
//            guardarDatos();

//            guardarDatosEnExcel();
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                            DEMÁS COSAS                                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Thread que muestra la cuenta regresiva para realizar configuraciones mientras tanto
        private void funcionThreadCuentaRegresiva()
        {
            CuentaRegresiva formularioCuentaRegresiva = new CuentaRegresiva(3);   // Se le asigna un tiempo de conteo de 3 segundos
            formularioCuentaRegresiva.ShowDialog();
        }

        // Funcion encargada de revisar que se devuelvan todos los recursos cuando se cierra la aplicación
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (threadsIniciados == true)
            {
                switch (ConfiguracionDeLosEstimulos.TipoDeEstudio)
                {
                    case ConfiguracionEstimulacion.tiposDeEstudios.PARPADEO_SECUENCIAL:
                        if (threadEstimulacionSecuencial.ThreadState != ThreadState.Unstarted)
                            try { threadEstimulacionSecuencial.Abort(); }
                            catch (ThreadAbortException) { };
                        break;
                }

                if (threadLecturaPuertoSerie.ThreadState != ThreadState.Unstarted)
                    try { threadLecturaPuertoSerie.Abort(); }
                    catch (ThreadAbortException) { };
            }

            Casco.TerminarComunicacionSerie();

            // Se cierra la conexion con la base de datos
            if (conexionBaseDatos.State == ConnectionState.Open)
                conexionBaseDatos.Close();
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          BASES DE DATOS                                                                  */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Funcion para guardar los datos en la base de datos luego de terminar correctamente el estudio
        private void guardarDatos()
        {
            string nombreArchivoExcel = fechaHora.ToString().Replace(" ", "-").Replace(":", "-").Replace("/", "-");

            // Comprobacion de que este abierto el lazo con la base de datos
            if (conexionBaseDatos.State != ConnectionState.Open)
                conexionBaseDatos.Open();

            // Creacion de la orden para insertar nuevos datos
            SQLiteCommand comandoBaseDatos = conexionBaseDatos.CreateCommand();
            comandoBaseDatos.CommandText = SQLInsert;

            // Configuracion de los valores a almacenar
            comandoBaseDatos.Parameters.AddWithValue("Apellido", apellido);
            comandoBaseDatos.Parameters.AddWithValue("Nombre", nombre);
            comandoBaseDatos.Parameters.AddWithValue("Edad", edad);

            if (experiencia == true) { comandoBaseDatos.Parameters.AddWithValue("Experiencia", "Si"); }
            else { comandoBaseDatos.Parameters.AddWithValue("Experiencia", "No"); }

            comandoBaseDatos.Parameters.AddWithValue("TiempoDeDescanso", EstimulosSecuenciales.TiempoDeDescanso);
            comandoBaseDatos.Parameters.AddWithValue("TiempoDeExcitacion", EstimulosSecuenciales.TiempoDeExcitacion);
            comandoBaseDatos.Parameters.AddWithValue("TiempoDeEstudio", ConfiguracionDeLosEstimulos.TiempoEstudioSegundos);
            comandoBaseDatos.Parameters.AddWithValue("CantidadDeSimbolos", ConfiguracionDeLosEstimulos.CantidadOpciones);
            comandoBaseDatos.Parameters.AddWithValue("SimboloElegido", ConfiguracionDeLosEstimulos.TextoOpcionElegida);
//            comandoBaseDatos.Parameters.AddWithValue("SimboloResultante", decisionTomada[0] );
            comandoBaseDatos.Parameters.AddWithValue("Dia", fechaInicioEstudio);
            comandoBaseDatos.Parameters.AddWithValue("Hora", horaInicioEstudio);
            comandoBaseDatos.Parameters.AddWithValue("ColorDeFondo", ConfiguracionDeLosEstimulos.ColorDeFondo);
            comandoBaseDatos.Parameters.AddWithValue("ColorLetraNormal", ConfiguracionDeLosEstimulos.ColorDescanso);
            comandoBaseDatos.Parameters.AddWithValue("ColorLetraResaltada", ConfiguracionDeLosEstimulos.ColorExcitacion);
            comandoBaseDatos.Parameters.AddWithValue("TamanioLetra", ConfiguracionDeLosEstimulos.TamanioLetra);
            comandoBaseDatos.Parameters.AddWithValue("TamanioPantalla", ConfiguracionDeLosEstimulos.TamanioPantalla);
            comandoBaseDatos.Parameters.AddWithValue("CantidadDeFilas", ConfiguracionDeLosEstimulos.Filas);
            comandoBaseDatos.Parameters.AddWithValue("Archivo", nombreArchivoExcel);
            comandoBaseDatos.Parameters.AddWithValue("Secuencia", EstimulosSecuenciales.TextoTipoDeSecuenciaActual);

            // Ejecucion de la orden para ingresar los nuevos datos a la base
            comandoBaseDatos.ExecuteNonQuery();
            conexionBaseDatos.Close();
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                               EXCEL                                                                      */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        private void guardarDatosEnExcel()
        {
            MostrarPorcentaje formularioMostrarPorcentaje = new MostrarPorcentaje();
            formularioMostrarPorcentaje.Show();
            formularioMostrarPorcentaje.actualizarPorcentaje(0);

            Microsoft.Office.Interop.Excel.Application aplicacion;
            Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
            Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
            aplicacion = new Microsoft.Office.Interop.Excel.Application();
            libros_trabajo = aplicacion.Workbooks.Add();
            hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

            // Primero se escriben los encabezados
            hoja_trabajo.Cells[1, 1] = "Canal 1 - Fp1";
            hoja_trabajo.Cells[1, 2] = "Canal 2 - Fp2";
            hoja_trabajo.Cells[1, 3] = "Canal 3 - C3";
            hoja_trabajo.Cells[1, 4] = "Canal 4 - C4";
            hoja_trabajo.Cells[1, 5] = "Canal 5 - P7";
            hoja_trabajo.Cells[1, 6] = "Canal 6 - P8";
            hoja_trabajo.Cells[1, 7] = "Canal 7 - O1";
            hoja_trabajo.Cells[1, 8] = "Canal 8 - O2";
            hoja_trabajo.Cells[1, 9] = "Markers";
            hoja_trabajo.Cells[1, 10] = "N Muestra";

            // Luego se escriben los datos
            for (int filaExcel = 0; filaExcel < Casco.LongitudDeLaLista; filaExcel++)
            {
                hoja_trabajo.Cells[filaExcel + 2, 7] = Casco.LeerDatoDelCanal(DatosCasco.CANAL_O1, filaExcel);
                hoja_trabajo.Cells[filaExcel + 2, 8] = Casco.LeerDatoDelCanal(DatosCasco.CANAL_O2, filaExcel);
                hoja_trabajo.Cells[filaExcel + 2, 9] = EstimulosSecuenciales.MarkersPosiciones[filaExcel];
                hoja_trabajo.Cells[filaExcel + 2, 10] = Casco.LeerNumeroDeMuestra(filaExcel);
                formularioMostrarPorcentaje.actualizarPorcentaje( (int) ( 100 * (float) filaExcel / Casco.LongitudDeLaLista ) );
            }

            // Finalmente se agregan detalles de configuración
            hoja_trabajo.Cells[1, 11] = "Apellido";
            hoja_trabajo.Cells[1, 12] = apellido;
            hoja_trabajo.Cells[2, 11] = "Edad";
            hoja_trabajo.Cells[2, 12] = edad;
            hoja_trabajo.Cells[3, 11] = "Opcion elegida";
            hoja_trabajo.Cells[3, 12] = ConfiguracionDeLosEstimulos.TextoOpcionElegida;
            hoja_trabajo.Cells[4, 11] = "Opcion resultante";
//            hoja_trabajo.Cells[4, 12] = decisionTomada[0];
            hoja_trabajo.Cells[5, 11] = "Tiempo de descanso";
            hoja_trabajo.Cells[5, 12] = EstimulosSecuenciales.TiempoDeDescanso;
            hoja_trabajo.Cells[6, 11] = "Tiempo de excitacion";
            hoja_trabajo.Cells[6, 12] = EstimulosSecuenciales.TiempoDeExcitacion;
            hoja_trabajo.Cells[7, 11] = "Tiempo del estudio";
            hoja_trabajo.Cells[7, 12] = ConfiguracionDeLosEstimulos.TiempoEstudioSegundos;

            // Definiciones para hacer más legible el nombre del archivo en excel
            string extension = @".xls";
            string nombre = fechaHora.ToString().Replace(" ", "-").Replace(":", "-").Replace("/", "-");
            string carpetaContenedora = @"\Seniales\";
            string ubicacionArchivo = carpetaContenedora + nombre + extension;

            libros_trabajo.SaveAs(Directory.GetCurrentDirectory() + ubicacionArchivo, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
            libros_trabajo.Close(true);
            aplicacion.Quit();

            formularioMostrarPorcentaje.Close();
        }




    }
}