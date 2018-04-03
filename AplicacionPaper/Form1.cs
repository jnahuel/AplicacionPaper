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
using System.IO.Ports;
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

        // Objeto para ejecutar el codigo de Matlab
        MLApp.MLApp funcionMatlab = new MLApp.MLApp();
        object[] decisionTomada;                        // Para almacenar el resultado del algoritmo

        // Unicos parametros que son fijos y solo dependen del caso:
        // La cantidad de bytes por trama (33) y la cantidad de tramas por segundo (250)
        private const int bytesALeer = 33;
        private const int tramasPorSegundo = 250;
        private const int tiempoEntreTramas = 1000 / tramasPorSegundo;    // Cuantos mili segundos pasan entre tramas sucesivas

        // Se considera una tiempo maximo a la recepcion de 10 tramas para que se considere como caida la comunicacion
        private const int maximoTiempoReadSerie = 100 * tiempoEntreTramas;

        // Se almacenará el dato de 2 o 6 opciones en uso para agilizar las comparaciones
        int cantidadOpcionesActuales;

        // Posibes estados que tendra la pantalla
        enum estadosPantalla : byte { Minimizada, Normal, Maximizada };
        estadosPantalla pantallaActual = estadosPantalla.Normal;            // Arranca en estado normal
        estadosPantalla pantallaAnterior = estadosPantalla.Normal;
        string tamanioPantalla = "Normal";
        
        // Arrays de caracteres que presentarán los botones
        string[] textosDosOpciones = new string[] { "SI", "NO" };
        string[] textosSeisOpciones = new string[] { "A", "B", "C", "D", "E", "F" };
        string textoOpcionElegida;

        // Marcas para seccionar los datos
        byte marcaInicialEstudio = 0x40;                    // Marca de inicio del estudio      =>  @ = 0x40 = 64
        byte marcaFinalEstudio = 0x24;                      // Marca de fin del estudio         =>  $ = 0x24 = 64
        byte[] marcasFinalesDosOpciones = { 0x26, 0x21 };   // Marca de fin del estimulo 1      =>  & = 0x26 = 38 // Marca de fin del estimulo 2      =>  ! = 0x21 = 33
        byte[] marcasInicialesDosOpciones = { 0x25, 0x23 }; // Marca de inicio del estimulo 1   =>  % = 0x25 = 37 // Marca de inicio del estimulo 2   =>  # = 0x23 = 35
        byte[] marcasFinalesSeisOpciones = { 0, 0, 0, 0, 0, 0 };
        byte[] marcasInicialesSeisOpciones = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46 };   // Son los caracteres desde A hasta F

        // Se comandarán desde el contro de configuración "Visual"
        byte filasTotales = 1;
        string colorDeFondo = "Black";
        string colorDeLetraResaltada = "White";
        string colorDeLetraNormal = "Gray";
        string tamanioLetra = "Grande";

        // Variables de Threads
        static Thread threadGrafico;
        static Thread threadCuentaRegresiva;
        static Thread threadLecturaPuertoSerie;
        static Thread threadGuardarExcel;
        static bool threadsIniciados = false;
        static bool finalizarThreadLecturaPuertoSerie = false;

        // Cadenas de almacenamiento de los tamaños de letras disponibles disponibles
        string[] tamaniosDeLetra = { "Muy grande", "Grande", "Media", "Chica", "Muy chica" };

        // Variables para almacenar los tiempos del estudio
        static int tiempoDeDescanso = 500;         // En mSeg
        static int tiempoDeExcitacion = 500;       // En mSeg
        static int tiempoEstudioSegundos = 10;        // En Seg
        static int tiempoEstudioMiliSegundos = tiempoEstudioSegundos * 1000;
        static int maximoDeTramasRecibidas = tramasPorSegundo * tiempoEstudioSegundos;

        // Variables globales de la comunicacion serie
        private byte[] markersPosiciones = new byte[maximoDeTramasRecibidas + 2];
        private int posicionDelMarker = 0;
        byte[] trama = new byte[bytesALeer];                  // Almacena cada trama recibida desde el casco
        const int bitsDeDatos = 8;
        const int offsetPuertoSerie = 0;
        private bool redimensionarBufferSerie = true;          // Para indicar que se modificó el tiempo total del estudio y puede afectar al buffer serie
        DatosCasco DatosDelCasco;                               // Objeto que contendrá los datos recibidos

        // Objeto para manejar el puerto serie
        static SerialPort puertoSerie = new SerialPort("COM1", 115200, Parity.None, bitsDeDatos, StopBits.One);

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
        //private String SQLSelect = "SELECT * FROM BdDAplicacionPaper";

        // Variables strings para almacenar textos. Es para una mayor comodidad de lectura en la base de datos
        public string simboloElegido;
        public string secuenciaElegida;

        // Variables para indicar y generar un vector aleatorio
        List<int> secuenciaFinal = new List<int>();
        bool desordenada;
        bool rueda;

        string nombreArchivoCompleto;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                  CONSTRUCTOR DEL FORMULARIO                                                              */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        
        public Form1()
        {
            // Cogido necesario para Windows
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            // Por defecto, se muestran inicialmente 2 opciones
            cantidadOpcionesActuales = 2;
            agregarBotones(cantidadOpcionesActuales);
            redimensionarBotones();

            // Ciertas opciones no están disponibles hasta no configurar correctamente los parametros necesarios
            detenerToolStripMenuItem.Enabled = false;
            reiniciarToolStripMenuItem.Enabled = false;
            accionesToolStripMenuItem.Enabled = false;

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
            PuertoSerie formularioConfiguracionPuertoSerie = new PuertoSerie();
            if (formularioConfiguracionPuertoSerie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                puertoSerie.PortName = formularioConfiguracionPuertoSerie.nombreDelPuerto;
                accionesToolStripMenuItem.Enabled = true;
                comunicacionToolStripMenuItem.Enabled = false;
                reiniciarToolStripMenuItem.Enabled = true;
            }
        }

        // Submenu para realizar la configuración de tiempos del estudio
        private void tiemposDelEstudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formularioConfiguracionTiempos = new Tiempos( tiempoDeDescanso, tiempoDeExcitacion, tiempoEstudioSegundos ) )
            {
                var resultado = formularioConfiguracionTiempos.ShowDialog();
                if (resultado == DialogResult.OK)   // Se eligieron nuevos valores y se deben cambiar los actuales
                {
                    tiempoDeDescanso   = formularioConfiguracionTiempos.tiempoDeDescanso;
                    tiempoDeExcitacion = formularioConfiguracionTiempos.tiempoDeExcitacion;
                    tiempoEstudioSegundos = formularioConfiguracionTiempos.tiempoTotal;
                    redimensionarBufferSerie = true;
                }
            }
        }

        // Opción para realizar la configuración visual de los objetos
        private void menuConfiguracionVisual_Click(object sender, EventArgs e)
        {
            using (var formularioConfiguracionVisual = new Visual(colorDeFondo, colorDeLetraResaltada, colorDeLetraNormal, tamanioLetra, filasTotales))
            {
                var resultado = formularioConfiguracionVisual.ShowDialog();
                if (resultado == DialogResult.OK)   // Se eligieron nuevos valores y se deben cambiar los actuales
                {
                    // Almacenado de los nuevos datos
                    colorDeFondo = formularioConfiguracionVisual.colorFondo;
                    colorDeLetraNormal = formularioConfiguracionVisual.colorLetraNormal;
                    colorDeLetraResaltada = formularioConfiguracionVisual.colorLetraResaltada;
                    filasTotales = formularioConfiguracionVisual.cantidadFilas;
                    tamanioLetra = formularioConfiguracionVisual.tamanioLetra;

                    // Se rehacen los botones con los nuevos parametros
                    if (borrarBotones() == false) { MessageBox.Show("Hubo un error al intentar borrar los botones"); }
                    agregarBotones(cantidadOpcionesActuales);
                    redimensionarBotones();
                }
            }
        }
        
        // Submenu para indicar que se deben presentar 2 estimulos
        private void subMenuDosOpciones_Click(object sender, EventArgs e)
        {
            cantidadOpcionesActuales = 2;

            if (borrarBotones() == false) { MessageBox.Show("Hubo un error al intentar borrar los botones"); }

            agregarBotones(cantidadOpcionesActuales);
            redimensionarBotones();

            secuencialToolStripMenuItem.Enabled = true;         // Se habilita la opción "6 estímulos secuenciales"
            ruedaToolStripMenuItem.Enabled = true;              // Se habilita la opción "6 estímulos por rueda"
            aleatorioToolStripMenuItem.Enabled = true;          // Se habilita la opción "6 estímulos aleatorios"
            subMenuDosOpciones.Enabled = false;                 // Se deshabilita la opción "2 estímulos"

            desordenada = false;
            secuenciaElegida = "Secuencial";
        }

        // Submenu para indicar que se deben presentar 6 estimulos
        private void subMenuSeisOpciones_Click(object sender, EventArgs e)
        {
            cantidadOpcionesActuales = 6;

            if (borrarBotones() == false) { MessageBox.Show("Hubo un error al intentar borrar los botones"); }

            agregarBotones(cantidadOpcionesActuales);
            redimensionarBotones();

            secuencialToolStripMenuItem.Enabled = false;        // Se deshabilita la opción "6 estímulos secuenciales"
            ruedaToolStripMenuItem.Enabled = true;              // Se habilita la opción "6 estímulos por rueda"
            aleatorioToolStripMenuItem.Enabled = true;          // Se habilita la opción "6 estímulos aleatorios"
            subMenuDosOpciones.Enabled = true;                  // Se habilita la opción "2 estímulos"

            redimensionarBufferSerie = true;

            desordenada = false;
            secuenciaElegida = "Secuencial";
        }

        // Submenú para indicar que se deben presentar 6 estímulos y ordenarlos aleatoriamente por rueda
        private void ruedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subMenuSeisOpciones_Click(sender, e);
            secuencialToolStripMenuItem.Enabled = true;         // Se habilita la opción "6 estímulos secuenciales"
            ruedaToolStripMenuItem.Enabled = false;             // Se deshabilita la opción "6 estímulos por rueda"
            aleatorioToolStripMenuItem.Enabled = true;          // Se habilita la opción "6 estímulos aleatorios"
            subMenuDosOpciones.Enabled = true;                  // Se habilita la opción "2 estímulos"
            desordenada = true;
            rueda = true;
            secuenciaElegida = "Rueda";
        }

        // Submenú para indicar que se deben presentar 6 estímulos y ordenarlos aleatoriamente en general
        private void aleatorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subMenuSeisOpciones_Click(sender, e);
            secuencialToolStripMenuItem.Enabled = true;         // Se habilita la opción "6 estímulos secuenciales"
            ruedaToolStripMenuItem.Enabled = true;              // Se habilita la opción "6 estímulos por rueda"
            aleatorioToolStripMenuItem.Enabled = false;         // Se deshabilita la opción "6 estímulos aleatorios"
            subMenuDosOpciones.Enabled = true;                  // Se habilita la opción "2 estímulos"
            desordenada = true;
            rueda = false;
            secuenciaElegida = "Aleatorio";
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



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                 OPCIONES DEL MENU DE ACCIONES                                                            */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Primero se reconfigura el intervalo del timer por las dudas
            double numeroTemporal;
            double segundosPorSimbolo;
            double segundosPorRueda;

            // Se debe calcular el múltiplo superior al que se solicita tal que se de una ronda completa
            segundosPorSimbolo = (double)(tiempoDeDescanso + tiempoDeExcitacion) / 1000;
            segundosPorRueda = (double)segundosPorSimbolo * cantidadOpcionesActuales;
            numeroTemporal = Math.Ceiling((double)tiempoEstudioSegundos / segundosPorRueda);
            tiempoEstudioSegundos = (int)(numeroTemporal * segundosPorRueda);
            tiempoEstudioMiliSegundos = tiempoEstudioSegundos * 1000;
            timerTiempoEstudio.Interval = tiempoEstudioSegundos;

            MostrarInformacionPersonal formularioMostrarInformacionPersonal = new MostrarInformacionPersonal( apellido, nombre, edad, experiencia, 
                                                                                    tiempoDeDescanso, tiempoDeExcitacion, tiempoEstudioSegundos,
                                                                                    cantidadOpcionesActuales, colorDeLetraNormal, colorDeLetraResaltada, 
                                                                                    colorDeFondo, tamanioLetra, tamanioPantalla, filasTotales );

            if( formularioMostrarInformacionPersonal.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                // Se almacena el dato de la opción elegida
                textoOpcionElegida = formularioMostrarInformacionPersonal.opcionElegida;

                // Se quitan los botones y las opciones de maximizar y minimizar la ventana
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

                // Se muestra el conteo inicial para que la persona se prepare y para terminar de configurar todo
                threadCuentaRegresiva = new Thread(new ThreadStart(funcionThreadCuentaRegresiva));
                threadCuentaRegresiva.Start();      // Se lanza el thread para que muestre la cuenta regresiva

                    // Mientras se muestra la cuenta regresiva, se cierra el puerto serie para reconfigurar el buffer de recepción (si cambio el tiempo total)
                    if (redimensionarBufferSerie == true)
                    {
                        redimensionarBufferSerie = false;

                        // Antes de cambiar el parametro, se debe cerrar el puerto serie
                        try { puertoSerie.Close(); }        // No debería suceder ninguna excepción, pero por las dudas se hace esto
                        catch( Exception ) {  }             // para que no se propague

                        // Se redimensiona el buffer del puerto serie en función del tiempo total del estudio
                        maximoDeTramasRecibidas = tramasPorSegundo * tiempoEstudioSegundos;
                        markersPosiciones = new byte[maximoDeTramasRecibidas + 2];
                        puertoSerie.ReadBufferSize = maximoDeTramasRecibidas * bytesALeer;

                        // Luego de redimensionar el buffer, se vuelve a iniciar el puerto serie
                        puertoSerie.Open();
                        puertoSerie.Write("v");         // Se da la orden para reiniciar al BCI
                    }

                // Esta accion sincroniza todo, ya que espera a que termine la cuenta regresiva para continuar
                threadCuentaRegresiva.Join();

                puertoSerie.Write("b");       // Se da la orden para que comienze a enviar las muestras
                puertoSerie.DiscardInBuffer();                          // Se limpia el buffer de los datos previos
                Thread.Sleep(200);            // Se genera una pequeña demora para que procese correctamente la instrucción

                // Se toma la fecha y hora de comienzo
                fechaHora = DateTime.Now;
                fechaInicioEstudio = fechaHora.ToShortDateString();
                horaInicioEstudio = fechaHora.ToLongTimeString();

                // Se habilita el botón para detener el estudio en curso y se quita la posibilidad de iniciar uno nuevo
                detenerToolStripMenuItem.Enabled = true;
                iniciarToolStripMenuItem.Enabled = false;

                // A su vez, mientras esté en curso, no pueden modificar los parametros
                configuracionToolStripMenuItem.Enabled = false;

                // Se genera el objeto para almacenar los datos del estudio
                DatosDelCasco = new DatosCasco();

                // Se asignan las funciones de los threads
                threadLecturaPuertoSerie = new Thread(new ThreadStart(funcionThreadLecturaPuertoSerie));
                threadGrafico = new Thread(new ThreadStart(funcionThreadGrafico));

                // En lugar de usar Thread.Abort, se controla la finalización del thread mediante una variable
                finalizarThreadLecturaPuertoSerie = false;

                // Se configura el timer del estudio
                timerTiempoEstudio.Enabled = false;
                timerTiempoEstudio.Interval = tiempoEstudioMiliSegundos;
                timerTiempoEstudio.Enabled = true;

                // Se lanzan los threads
                threadLecturaPuertoSerie.Start();
                threadGrafico.Start();

                // Se genera una demora ficticia sólo para sincronizar el inicio de los threads con el timer principal
                Thread.Sleep(0);
                threadsIniciados = true;
//                timerTiempoEstudio.Start();
            }
        }

        private void detenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerTiempoEstudio.Stop();

            // Primero se cancela el thread gráfico
            try { threadGrafico.Abort(); }
            catch (ThreadAbortException) { };

            // Se cancela el thread de lectura del puerto serie
            try { threadLecturaPuertoSerie.Abort(); }
            catch (ThreadAbortException) { };

            // Se da la orden al casco para que deje de enviar muestras
            puertoSerie.Write( "s" );

            // Detenido el estudio, se puede iniciar otro junto con la configuración
            iniciarToolStripMenuItem.Enabled = true;
            detenerToolStripMenuItem.Enabled = false;
            configuracionToolStripMenuItem.Enabled = true;

            // Por último, se repasan todos los botones para que ninguno quede en estado de excitación
            foreach (Control boton in Controls)
            {
                if (boton is Button) { boton.ForeColor = Color.FromName(colorDeLetraNormal); }
            }

            // Se reestablecen las opciones para modificar el tamaño de la pantalla
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        }

        private void reiniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comunicacionToolStripMenuItem.Enabled = true;
            reiniciarToolStripMenuItem.Enabled = false;
            accionesToolStripMenuItem.Enabled = false;

            // Se cierra el puerto serie
            try { puertoSerie.Close(); }
            catch (Exception) { }
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
                    pantallaActual = estadosPantalla.Maximizada;
                    tamanioPantalla = "Maximizada";
                    break;
                case FormWindowState.Minimized:
                    pantallaActual = estadosPantalla.Minimizada;
                    tamanioPantalla = "Minimizada";
                    break;
                case FormWindowState.Normal:
                    pantallaActual = estadosPantalla.Normal;
                    tamanioPantalla = "Normal";
                    break;
            }

            if (!(pantallaActual == estadosPantalla.Minimizada || pantallaAnterior == estadosPantalla.Minimizada)) { redimensionarBotones(); }

            pantallaAnterior = pantallaActual;
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

            int anchoBoton = anchoDisponible / (cantidadOpcionesActuales / filasTotales);
            int altoBoton = altoDisponible / filasTotales;

            int indiceAuxiliar = 0, segundaFila = 0;
            foreach (Control boton in Controls)
            {
                if (boton is Button)
                {
                    boton.Size = new Size(anchoBoton, altoBoton);
                    boton.Location = new Point(indiceAuxiliar * anchoBoton, altoMenus + altoBoton * segundaFila);
                    if (++indiceAuxiliar > (cantidadOpcionesActuales / 2 - 1) && filasTotales > 1)
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
            int escalaLetra = (Array.IndexOf(tamaniosDeLetra, tamanioLetra) + 1) * 3;

            foreach (Control boton in Controls)
            {
                if (boton is Button)
                {
                    if (boton.Height > boton.Width) { boton.Font = new Font("Times new roman", boton.Width / escalaLetra); }
                    else { boton.Font = new Font("Times new roman", boton.Height / escalaLetra); }
                }
            }
        }

        // "borrarBotones" hace un ciclo y borra todos los botones presentes en el formulario.
        //  Si en "maximosIntentos" de pasadas no logra borrarlos, lo indica devolviendo False
        private bool borrarBotones()
        {
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
                botonAgregado.FlatAppearance.BorderColor = Color.FromName(colorDeFondo);
                botonAgregado.FlatAppearance.MouseDownBackColor = Color.FromName(colorDeFondo);
                botonAgregado.FlatAppearance.MouseOverBackColor = Color.FromName(colorDeFondo);
                botonAgregado.BackColor = Color.FromName(colorDeFondo);
                botonAgregado.ForeColor = Color.FromName(colorDeLetraNormal);
                switch (cantidadDeBotonesAgregar)
                {
                    case 2:
                        botonAgregado.Text = textosDosOpciones[indiceBotonesNuevos];    // Texto a mostrar
                        break;
                    case 6:
                        botonAgregado.Text = textosSeisOpciones[indiceBotonesNuevos];    // Texto a mostrar
                        break;
                }
            }
        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          THREAD GRÁFICO                                                                  */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Este es el thread que controla los botones
        private void funcionThreadGrafico()
        {
            // Declaracion de variables
            int tiempoAcumulado = 0;                                        // En funcion del tiempo que lleva el estudio, se determinan las posiciones de los markers
            byte[] marcasSeccionInicio, marcasSeccionFinal;
            int numeroBoton;
            int botonActual;
            int[] contadorRepeticiones = new int[6];                        // Para llevar la cuenta de la cantidad de veces que se enciende cada boton
            Random aleatorio = new Random();                                // Objeto de la clase Random para usar los métodos aleatorios
            List<int> lista = new List<int>();                              // En la lista se irán almacenando los valores generados
            int longitudDeSecuencia = (tiempoEstudioMiliSegundos) / (tiempoDeDescanso + tiempoDeExcitacion);                    // Total de detellos
            int repeticionesMaximasPorSimbolo = longitudDeSecuencia / cantidadOpcionesActuales;                                // Destellos totales por símbolo

            // En función de la cantidad de símbolos se elijen las marcas de seccionamiento
            if (cantidadOpcionesActuales == 2)
            {
                marcasSeccionInicio = marcasInicialesDosOpciones;               // Las marcas de inicio de sección son las que corresponden a 2 símbolos
                marcasSeccionFinal = marcasFinalesDosOpciones;                  // Lo mismo para las de fin de sección
            }
            else
            {
                marcasSeccionInicio = marcasInicialesSeisOpciones;               // Las marcas de seis opciones son las mismas para inicio y para fin
                marcasSeccionFinal = marcasFinalesSeisOpciones;
            }

            // Marcas fijas de inicio y fin de estudio
            markersPosiciones[0] = marcaInicialEstudio;
            markersPosiciones[maximoDeTramasRecibidas + 1] = marcaFinalEstudio;

            
            // Se genera un vector con la secuencia total 1 vez y después se lo va seccionando
            for (int indiceAuxiliar = 0; indiceAuxiliar < longitudDeSecuencia; indiceAuxiliar++)
            {
                // Aca se dividen los casos, si es secuencial, por rueda o aleatorio
                if (desordenada == false)
                    secuenciaFinal.Add(indiceAuxiliar % cantidadOpcionesActuales);        // Secuencial
                else
                {
                    if (rueda == false)             // Aleatorio total
                    {
                        while (true)
                        {
                            int valorAleatorio = aleatorio.Next(0, 6);                  // Se genera un número entre 0 (inclusivo) y 6 (no inclusivo)
                            if (contadorRepeticiones[valorAleatorio] < repeticionesMaximasPorSimbolo)      // Si no llegó al límite de repeticiones por símbolo
                            {
                                secuenciaFinal.Add(valorAleatorio);                                         // Se lo agrega a la lista. Equivale a secuenciaFinal[ indiceAuxiliar ] = valorAleatorio
                                contadorRepeticiones[valorAleatorio]++;                                     // Se incrementa el contador de repeticiones por símbolo
                                break;
                            }
                        }
                    }
                    else                            // Por rueda
                    {
                        if ((indiceAuxiliar % cantidadOpcionesActuales) == 0)           // Cada vez que se completa una rueda, se borra la lista
                            lista.Clear();
                        while (true)
                        {
                            int valorAleatorio = aleatorio.Next(0, 6);
                            if (!lista.Contains(valorAleatorio))
                            {
                                lista.Add(valorAleatorio);
                                secuenciaFinal.Add(valorAleatorio);
                                break;
                            }
                        }
                    }   // Fin del "elseif( rueda )"
                }   // Fin del "elseif( desordenada )"
            }   // Fin del "for"

            timerTiempoEstudio.Start();

            // Luego de generar todo el vector de secuencias, se procede a iluminar los símbolos en dicho orden
            while (true)        // Bloque perpetuo
            {
                for (int botonesRepasados = 0; botonesRepasados < longitudDeSecuencia; botonesRepasados++)     // Realiza una rueda de giro por todos los botones
                {
                    botonActual = 0;
                    numeroBoton = secuenciaFinal[botonesRepasados];           // Almacena el número del boton que se deba buscar ahora
                    foreach (Control boton in Controls)
                    {   // Bucle por los botones hasta encontrar el solicitado
                        if (boton is Button)
                        {   // Se encontró un botón
                            if (botonActual++ == numeroBoton)
                            {   // Se encontró el número de botón solicitado

                                // Primero se coloca la marca de inicio de sección del símbolo
                                posicionDelMarker = (tiempoAcumulado / tiempoEntreTramas) + 1;
                                markersPosiciones[posicionDelMarker] = marcasSeccionInicio[numeroBoton];
                                tiempoAcumulado += tiempoDeExcitacion;

                                // Luego se resalta el símbolo
                                boton.ForeColor = Color.FromName(colorDeLetraResaltada);
                                Thread.Sleep(tiempoDeExcitacion);

                                // Se coloca la marca de fin de la seccion del símbolo
                                posicionDelMarker = (tiempoAcumulado / tiempoEntreTramas) + 1;
                                markersPosiciones[posicionDelMarker] = marcasSeccionFinal[numeroBoton];
                                tiempoAcumulado += tiempoDeDescanso;

                                // Se deja el símbolo en estado normal
                                boton.ForeColor = Color.FromName(colorDeLetraNormal);
                                Thread.Sleep(tiempoDeDescanso);

                                break;
                            }   // Fin del "if( Se encontró el boton buscado )"
                        }   // Fin del "if( Se encontró un boton )"
                    }   // Fin del "foreach( Recorrer todos los controlas gráficos )"
                }   // Fin del "for( Recorrer la secuencia )"
            }   // Fin del "while( true )". Nunca sale de este bucle

        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                           THREAD SERIE                                                                   */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Este es el thread que se encarga de la recepción de datos por el puerto serie
        private void funcionThreadLecturaPuertoSerie()
        {
            // Declaracion de variables
            int[] contadorBytesRecibidos = new int[maximoDeTramasRecibidas];        // Solo para control interno en el debug
            int numeroTramaRecibidas = 0;
            bool error = false;

            byte[] tramaAuxiliar = new byte[bytesALeer];
            int bytesFaltantes, bytesLeidos, bytesAuxiliares;

            // Acciones iniciales de una sola ejecucion
            puertoSerie.ReadTimeout = maximoTiempoReadSerie;
            numeroTramaRecibidas = 1;                               // Se resetea el contador de tramas recibidas

            while (!finalizarThreadLecturaPuertoSerie && numeroTramaRecibidas < maximoDeTramasRecibidas)           // Bucle para la recepcion de datos
            {
                try
                { 
                    contadorBytesRecibidos[numeroTramaRecibidas] = puertoSerie.Read(trama, offsetPuertoSerie, bytesALeer);
                    bytesLeidos = contadorBytesRecibidos[numeroTramaRecibidas];

                    if ( bytesLeidos < bytesALeer)
                    {
                        // Hacer un loop para llegar a 33 bytes leidos
                        do
                        {
                            bytesFaltantes = bytesALeer - bytesLeidos;
                            bytesAuxiliares = puertoSerie.Read(tramaAuxiliar, bytesLeidos, bytesFaltantes);
                            bytesLeidos += bytesAuxiliares;

                        } while (bytesLeidos < bytesALeer);

                        //bytesFaltantes = bytesALeer - bytesLeidos;
                    }
                }
                catch (TimeoutException)
                {
                    // Se supero el tiempo limite de espera de datos. Se considera que se perdio la comunicacion
                    error = true;
                    redimensionarBufferSerie = true;
                    break;      // Termina el bloque "while"
                }

                DatosDelCasco.AlmacenarTrama( trama );
                numeroTramaRecibidas++;
                Thread.Sleep(5);
            }

            if (error == false)     // Si no hay problemas, se continua con las tramas faltantes
            {
                // Finalizado el loop, pueden faltar datos por leer correspondientes a los ultimos momentos del estudio
                while (numeroTramaRecibidas < maximoDeTramasRecibidas)
                {
                    contadorBytesRecibidos[numeroTramaRecibidas] = puertoSerie.Read(trama, offsetPuertoSerie, bytesALeer);
                    DatosDelCasco.AlmacenarTrama(trama);
                    numeroTramaRecibidas++;
                    Thread.Sleep(0);
                }

                while( DatosDelCasco.LongitudDeLaLista() <= ( maximoDeTramasRecibidas + 1 ) )
                        DatosDelCasco.AgregarMuestraNula();

                // Luego de completar la recepcion de los datos, se le envia la orden al casco de detener el streaming de datos
                puertoSerie.Write("s");
            }
            else
            {
                // Se detienen el timer principal y se cancela el thread grafico
                timerTiempoEstudio.Stop();
                try { threadGrafico.Abort(); }
                catch ( ThreadAbortException ) {}

                // Se deshabilitan las opciones para iniciar un nuevo estudio o para detenerlo
                iniciarToolStripMenuItem.Enabled = false;
                detenerToolStripMenuItem.Enabled = false;
                reiniciarToolStripMenuItem.Enabled = true;
                configuracionToolStripMenuItem.Enabled = true;

                // Se indica al usuario la condicion para que reinicie el estudio
                MessageBox.Show("Se perdio la comunicacion con el casco. Por favor, reinicie el puerto");
            }

        }



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                     TIMER PRINCIPAL DEL ESTUDIO                                                          */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Si se ejecuta el handler del timer, el estudio finalizo correctamente
        private void timerTiempoEstudio_Tick(object sender, EventArgs e)
        {
            timerTiempoEstudio.Stop();

            // Cancela el thread grafico
            try { threadGrafico.Abort(); }
            catch (ThreadAbortException) { };

            // Tiene que dejar las condiciones necesarias para iniciar un nuevo estudio
            iniciarToolStripMenuItem.Enabled = true;
            detenerToolStripMenuItem.Enabled = false;
            configuracionToolStripMenuItem.Enabled = true;

            // Se reestablecen las opciones para modificar el tamaño de la pantalla
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            // Se termina el thread de lectura del puerto serie
            finalizarThreadLecturaPuertoSerie = true;
            threadLecturaPuertoSerie.Join();                // Se sincroniza cuando termine

            // Parte del codigo para emplear el llamado a Matlab
            if( cantidadOpcionesActuales == 2 )
                funcionMatlab.Execute(@"cd " + Directory.GetCurrentDirectory() + @"\MatlabScripts\DosOpciones");
            else
                funcionMatlab.Execute(@"cd " + Directory.GetCurrentDirectory() + @"\MatlabScripts\SeisOpciones");

            object resultadoMatlab = null;
            funcionMatlab.Feval("script_Principal", 1, out resultadoMatlab, DatosDelCasco.LeerCanal(6), markersPosiciones);
            decisionTomada = resultadoMatlab as object[];

            // Se muestra un mensaje diciendo que el estudio termino correctamente
            //MessageBox.Show("Estudio finalizado correctamente. La opcion es " + decisionTomada[0] );

            // Finalizado correctamente el estudio, se almacenan los valores en la base de datos
            guardarDatos();

            guardarDatosEnExcel();

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
                try { threadGrafico.Abort(); }
                catch (ThreadAbortException) { };

                try { threadLecturaPuertoSerie.Abort(); }
                catch (ThreadAbortException) { };
            }

            // Se cierra el puerto serie
            try { puertoSerie.Close(); }
            catch (Exception) { }

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

            comandoBaseDatos.Parameters.AddWithValue("TiempoDeDescanso", tiempoDeDescanso);
            comandoBaseDatos.Parameters.AddWithValue("TiempoDeExcitacion", tiempoDeExcitacion);
            comandoBaseDatos.Parameters.AddWithValue("TiempoDeEstudio", tiempoEstudioSegundos);
            comandoBaseDatos.Parameters.AddWithValue("CantidadDeSimbolos", cantidadOpcionesActuales);
            comandoBaseDatos.Parameters.AddWithValue("SimboloElegido", textoOpcionElegida);
            comandoBaseDatos.Parameters.AddWithValue("SimboloResultante", decisionTomada[0] );
            comandoBaseDatos.Parameters.AddWithValue("Dia", fechaInicioEstudio);
            comandoBaseDatos.Parameters.AddWithValue("Hora", horaInicioEstudio);
            comandoBaseDatos.Parameters.AddWithValue("ColorDeFondo", colorDeFondo);
            comandoBaseDatos.Parameters.AddWithValue("ColorLetraNormal", colorDeLetraNormal);
            comandoBaseDatos.Parameters.AddWithValue("ColorLetraResaltada", colorDeLetraResaltada);
            comandoBaseDatos.Parameters.AddWithValue("TamanioLetra", tamanioLetra);
            comandoBaseDatos.Parameters.AddWithValue("TamanioPantalla", tamanioPantalla);
            comandoBaseDatos.Parameters.AddWithValue("CantidadDeFilas", filasTotales);
            comandoBaseDatos.Parameters.AddWithValue("Archivo", nombreArchivoExcel);
            comandoBaseDatos.Parameters.AddWithValue("Secuencia", secuenciaElegida);

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

            // Luego se escriben los datos
            for (int filaExcel = 0; filaExcel < DatosDelCasco.LongitudDeLaLista(); filaExcel++)
            {
                hoja_trabajo.Cells[filaExcel + 2, 7] = DatosDelCasco.LeerDatosDelCanal(6, filaExcel);
                hoja_trabajo.Cells[filaExcel + 2, 8] = DatosDelCasco.LeerDatosDelCanal(7, filaExcel);
                hoja_trabajo.Cells[filaExcel + 2, 9] = markersPosiciones[filaExcel];
                formularioMostrarPorcentaje.actualizarPorcentaje( (int) ( 100 * (float) filaExcel / DatosDelCasco.LongitudDeLaLista() ) );
            }

            // Finalmente se agregan detalles de configuración
            hoja_trabajo.Cells[1, 11] = "Apellido";
            hoja_trabajo.Cells[1, 12] = apellido;
            hoja_trabajo.Cells[2, 11] = "Edad";
            hoja_trabajo.Cells[2, 12] = edad;
            hoja_trabajo.Cells[3, 11] = "Opcion elegida";
            hoja_trabajo.Cells[3, 12] = textoOpcionElegida;
            hoja_trabajo.Cells[4, 11] = "Opcion resultante";
            hoja_trabajo.Cells[4, 12] = decisionTomada[0];
            hoja_trabajo.Cells[5, 11] = "Tiempo de descanso";
            hoja_trabajo.Cells[5, 12] = tiempoDeDescanso;
            hoja_trabajo.Cells[6, 11] = "Tiempo de excitacion";
            hoja_trabajo.Cells[6, 12] = tiempoDeExcitacion;
            hoja_trabajo.Cells[7, 11] = "Tiempo del estudio";
            hoja_trabajo.Cells[7, 12] = tiempoEstudioSegundos;

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