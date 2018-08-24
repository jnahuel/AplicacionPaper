using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace AplicacionPaper
{
    public class DatosCasco
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                      DEFINICIONES DE LA TRAMA                                                            */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        /*  Descripcion de la trama: Consta de 33 bytes, que se ordenan de la siguiente forma
         *      Byte 0:         Header inicial  =>  0xA0
         *      Byte 1:         Contador de trama
         *      Bytes 2-4:      Canal 1
         *      Bytes 5-7:      Canal 2
         *      Bytes 8-10:     Canal 3
         *      Bytes 11-13:    Canal 4
         *      Bytes 14-16:    Canal 5
         *      Bytes 17-19:    Canal 6
         *      Bytes 20-22:    Canal 7
         *      Bytes 23-25:    Canal 8
         *      Bytes 26-27:    Acelecrómetro eje X
         *      Bytes 28-29:    Acelecrómetro eje Y
         *      Bytes 30-31:    Acelecrómetro eje Z
         *      Byte 32:        Header final  =>  0xC0
        */

        // Tamanio de la trama de datos: Siempre son 33 bytes
        private const int LongitudDeTrama = 33;

        // La comunicacion Bluetooth solo permite una tasa de envio de 250 tramas por segundo
        private const int TramasPorSegundo = 250;
        private const int TiempoFijoEntreTramas = 1000 / TramasPorSegundo;    // Cuantos mili segundos pasan entre tramas sucesivas

        // Constantes para indicar la posicion de cada byte dentro de la trama recepcionada
        // Header inicial
        private const int HeaderInicialPosicion = 0;
        private const byte HeaderInicial = 0xA0;
        // Contador de tramas
        private const int ContadorDeTramasPosicion = 1;
        // Canal 1
        private const int CanalUno_ByteAlto = 2;
        private const int CanalUno_ByteMedio = 3;
        private const int CanalUno_ByteBajo = 4;
        // Canal 2
        private const int CanalDos_ByteAlto = 5;
        private const int CanalDos_ByteMedio = 6;
        private const int CanalDos_ByteBajo = 7;
        // Canal 3
        private const int CanalTres_ByteAlto = 8;
        private const int CanalTres_ByteMedio = 9;
        private const int CanalTres_ByteBajo = 10;
        // Canal 4
        private const int CanalCuatro_ByteAlto = 11;
        private const int CanalCuatro_ByteMedio = 12;
        private const int CanalCuatro_ByteBajo = 13;
        // Canal 5
        private const int CanalCinco_ByteAlto = 14;
        private const int CanalCinco_ByteMedio = 15;
        private const int CanalCinco_ByteBajo = 16;
        // Canal 6
        private const int CanalSeis_ByteAlto = 17;
        private const int CanalSeis_ByteMedio = 18;
        private const int CanalSeis_ByteBajo = 19;
        // Canal 7
        private const int CanalSiete_ByteAlto = 20;
        private const int CanalSiete_ByteMedio = 21;
        private const int CanalSiete_ByteBajo = 22;
        // Canal 8
        private const int CanalOcho_ByteAlto = 23;
        private const int CanalOcho_ByteMedio = 24;
        private const int CanalOcho_ByteBajo = 25;
        // Header final
        private const int HeaderFinalPosicion = 32;
        private const byte HeaderFinal = 0xC0;



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                    COMANDOS Y CARACTERES                                                                 */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/


        // Total de canales
        public const int TOTAL_DE_CANALES = 8;


        // Nombres de los canales en funcion del contacto que representan
        public const int CANAL_FP1 = CANAL_1;
        public const int CANAL_FP2 = CANAL_2;
        public const int CANAL_C3  = CANAL_3;
        public const int CANAL_C4  = CANAL_4;
        public const int CANAL_P7  = CANAL_5;
        public const int CANAL_P8  = CANAL_6;
        public const int CANAL_O1  = CANAL_7;
        public const int CANAL_O2  = CANAL_8;


        /*******************************************/
        /* Opciones de configuracion implementadas */
        /*******************************************/


        /* Comando: APAGAR-ENCENDER CANAL */

        // CHANNEL: Definiciones de los numeros de los canales
        public const int CANAL_1 = 1;
        public const int CANAL_2 = 2;
        public const int CANAL_3 = 3;
        public const int CANAL_4 = 4;
        public const int CANAL_5 = 5;
        public const int CANAL_6 = 6;
        public const int CANAL_7 = 7;
        public const int CANAL_8 = 8;


        /* Comando: ACTIVAR SENIAL DE CONTROL */

        public const int CONNECT_TO_INTERNAL_GND = 0;
        public const int CONNECT_TO_TEST_SIGNAL_1X_SLOW_PULSE = 1;
        public const int CONNECT_TO_TEST_SIGNAL_1X_FAST_PULSE = 2;
        public const int CONNECT_TO_DC_SIGNAL = 3;
        public const int CONNECT_TO_TEST_SIGNAL_2X_SLOW_PULSE = 4;
        public const int CONNECT_TO_TEST_SIGNAL_2X_FAST_PULSE = 5;


        /* Comando: CONFIGURAR CANAL */

        // POWER_DOWN: Definicion del estado prendido o apagado
        public const bool CANAL_PRENDIDO = true;            // Opcion tomada por default en el inicio del casco - Interna de su micro
        public const bool CANAL_APAGADO = false;

        // GAIN_SET: Definicion de las posibles ganancias para el amplificador del conversor
        public const int GANANCIA_1 = 0;
        public const int GANANCIA_2 = 1;
        public const int GANANCIA_4 = 2;
        public const int GANANCIA_6 = 3;
        public const int GANANCIA_8 = 4;
        public const int GANANCIA_12 = 5;
        public const int GANANCIA_24 = 6;               // Opcion tomada por default en el inicio del casco - Interna de su micro

        // INPUT_TYPE_SET: Definicion del tipo de entrada al adc
        public const int ADS_INPUT_NORMAL = 0;          // Opcion tomada por default en el inicio del casco - Interna de su micro
        public const int ADS_INPUT_SHORTED = 1;
        public const int ADS_INPUT_BIAS_MEAS = 2;
        public const int ADS_INPUT_MVDD = 3;
        public const int ADS_INPUT_TEMP = 4;
        public const int ADS_INPUT_TESTING = 5;
        public const int ADS_INPUT_BIAS_DRP = 6;
        public const int ADS_INPUT_BIAS_DRN = 7;

        // BIAS_SET: Definicion de la opcion para incluir o no al canal en la generacion de la senial de BIAS
        public const bool BIAS_REMOVE = false;
        public const bool BIAS_INCLUDE = true;              // Opcion tomada por default en el inicio del casco - Interna de su micro

        // SRB2_SET: Definicion de la opcion para conectar la entrada P del canal al pin SRB2
        public const bool SRB2_DISCONNECT = false;
        public const bool SRB2_CONNECT = true;              // Opcion tomada por default en el inicio del casco - Interna de su micro

        // SRB1_SET: Definicion de la opcion para desconectar todas las entradas N de los canales al pin SBR1
        public const bool SRB1_DISCONNECT_ALL = false;       // Opcion tomada por default en el inicio del casco - Interna de su micro
        public const bool SRB1_CONNECT_ALL = true;


        /* Comando: MEDIR IMPEDANCIA */

        // PCHAN: Se usa para indicar si se inyecta la senial de prueba en la entrada P del canal
        public const bool NO_MEDIR_IMPEDANCIA_P = false;        // Opcion tomada por default en el inicio del casco - Interna de su micro
        public const bool SI_MEDIR_IMPEDANCIA_P = true;

        // NCHAN: Se usa para indicar si se inyecta la senial de prueba en la entrada N del canal
        public const bool NO_MEDIR_IMPEDANCIA_N = false;        // Opcion tomada por default en el inicio del casco - Interna de su micro
        public const bool SI_MEDIR_IMPEDANCIA_N = true;


        /* Opciones tomadas por default para hacer mas legible el codigo */
        public const bool OPCION_POR_DEFAULT_ENCENDIDO = CANAL_PRENDIDO;
        public const Int32 OPCION_POR_DEFAULT_GANANCIA = GANANCIA_24;
        public const Int32 OPCION_POR_DEFAULT_TIPO_DE_ENTRADA = ADS_INPUT_NORMAL;
        public const bool OPCION_POR_DEFAULT_BIAS = BIAS_INCLUDE;
        public const bool OPCION_POR_DEFAULT_SRB2 = SRB2_CONNECT;
        public const bool OPCION_POR_DEFAULT_SRB1 = SRB1_DISCONNECT_ALL;
        public const bool OPCION_POR_DEFAULT_MEDIENDO_IMPEDANCIA = false;
        public const bool OPCION_POR_DEFAULT_MEDIR_IMPEDANCIA_P = NO_MEDIR_IMPEDANCIA_P;
        public const bool OPCION_POR_DEFAULT_MEDIR_IMPEDANCIA_N = NO_MEDIR_IMPEDANCIA_N;





        /***********************************************/
        /* Caracteres de configuraciones implementadas */
        /***********************************************/

        /* Comando: APAGAR CANAL */

        private const string CARACTER_CANAL_1_APAGAR = "1";
        private const string CARACTER_CANAL_2_APAGAR = "2";
        private const string CARACTER_CANAL_3_APAGAR = "3";
        private const string CARACTER_CANAL_4_APAGAR = "4";
        private const string CARACTER_CANAL_5_APAGAR = "5";
        private const string CARACTER_CANAL_6_APAGAR = "6";
        private const string CARACTER_CANAL_7_APAGAR = "7";
        private const string CARACTER_CANAL_8_APAGAR = "8";


        /* Comando: ENCENDER CANAL */

        private const string CARACTER_CANAL_1_ENCENDER = "!";
        private const string CARACTER_CANAL_2_ENCENDER = "@";
        private const string CARACTER_CANAL_3_ENCENDER = "#";
        private const string CARACTER_CANAL_4_ENCENDER = "$";
        private const string CARACTER_CANAL_5_ENCENDER = "%";
        private const string CARACTER_CANAL_6_ENCENDER = "^";
        private const string CARACTER_CANAL_7_ENCENDER = "&";
        private const string CARACTER_CANAL_8_ENCENDER = "*";


        /* Comando: ACTIVAR SENIAL DE CONTROL */
        
        private const string CARACTER_CONNECT_TO_INTERNAL_GND = "0";
        private const string CARACTER_CONNECT_TO_TEST_SIGNAL_1X_SLOW_PULSE = "-";
        private const string CARACTER_CONNECT_TO_TEST_SIGNAL_1X_FAST_PULSE = "=";
        private const string CARACTER_CONNECT_TO_DC_SIGNAL = "p";
        private const string CARACTER_CONNECT_TO_TEST_SIGNAL_2X_SLOW_PULSE = "[";
        private const string CARACTER_CONNECT_TO_TEST_SIGNAL_2X_FAST_PULSE = "]";


        /* Comando: CONFIGURAR CANAL */

        private const string CARACTER_CANAL_1 = "1";
        private const string CARACTER_CANAL_2 = "2";
        private const string CARACTER_CANAL_3 = "3";
        private const string CARACTER_CANAL_4 = "4";
        private const string CARACTER_CANAL_5 = "5";
        private const string CARACTER_CANAL_6 = "6";
        private const string CARACTER_CANAL_7 = "7";
        private const string CARACTER_CANAL_8 = "8";

        private const string CARACTER_CANAL_PRENDIDO = "0";
        private const string CARACTER_CANAL_APAGADO = "1";

        private const string CARACTER_GANANCIA_1 = "0";
        private const string CARACTER_GANANCIA_2 = "1";
        private const string CARACTER_GANANCIA_4 = "2";
        private const string CARACTER_GANANCIA_6 = "3";
        private const string CARACTER_GANANCIA_8 = "4";
        private const string CARACTER_GANANCIA_12 = "5";
        private const string CARACTER_GANANCIA_24 = "6";

        private const string CARACTER_ADS_INPUT_NORMAL = "0";
        private const string CARACTER_ADS_INPUT_SHORTED = "1";
        private const string CARACTER_ADS_INPUT_BIAS_MEAS = "2";
        private const string CARACTER_ADS_INPUT_MVDD = "3";
        private const string CARACTER_ADS_INPUT_TEMP = "4";
        private const string CARACTER_ADS_INPUT_TESTING = "5";
        private const string CARACTER_ADS_INPUT_BIAS_DRP = "6";
        private const string CARACTER_ADS_INPUT_BIAS_DRN = "7";

        private const string CARACTER_BIAS_REMOVE = "0";
        private const string CARACTER_BIAS_INCLUDE = "1";

        private const string CARACTER_SRB2_DISCONNECT = "0";
        private const string CARACTER_SRB2_CONNECT = "1";

        private const string CARACTER_SRB1_DISCONNECT_ALL = "0";
        private const string CARACTER_SRB1_CONNECT_ALL = "1";


        /* Comando: MEDIR IMPEDANCIA */

        private const string CARACTER_NO_MEDIR_IMPEDANCIA_P = "0";
        private const string CARACTER_SI_MEDIR_IMPEDANCIA_P = "1";

        private const string CARACTER_NO_MEDIR_IMPEDANCIA_N = "0";
        private const string CARACTER_SI_MEDIR_IMPEDANCIA_N = "1";


        /* Comando: REESTABLECER OPCIONES POR DEFAULT */
        private const string CARACTER_OPCIONES_POR_DEFAULT = "d";

        /* Comando: INICIAR ENVIO DE DATOS */
        private const string CARACTER_INICIAR_ENVIO_DE_DATOS = "b";

        /* Comando: DETENER ENVIO DE DATOS */
        private const string CARACTER_DETENER_ENVIO_DE_DATOS = "s";

        /* Comando: REINICIAR CASCO */
        private const string CARACTER_REINICIAR_CASCO = "v";



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                    VARIABLES Y CONSTANTES                                                                */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Listas para almacenar los datos
        private List<Int32> CanalUno;
        private List<Int32> CanalDos;
        private List<Int32> CanalTres;
        private List<Int32> CanalCuatro;
        private List<Int32> CanalCinco;
        private List<Int32> CanalSeis;
        private List<Int32> CanalSiete;
        private List<Int32> CanalOcho;

        // Listas para almacenar la configuracion de los canales
        private List<Int32> configuracionGanancia;
        private List<Int32> configuracionTipoDeEntrada;
        private List<bool> configuracionEncendido;
        private List<bool> configuracionBIAS;
        private List<bool> configuracionSRB1;
        private List<bool> configuracionSRB2;
        private List<bool> configuracionImpendanciaMidiendo;
        private List<bool> configuracionImpendanciaContactoP;
        private List<bool> configuracionImpendanciaContactoN;

        // Lista auxiliar para almacenar el contador de tramas
        private List<Int32> NumerosDeTramas;

        // Contadores auxiliares para agilizar los cálculos
        private int CantidadDeTramasFalladas;
        private int CantidadDeTramasValidas;
        private int CantidadDeTramasIncompletas;
        private int CantidadDeTramasDesfasadas;
        private byte NumeroTramaAnterior;

        // Constante de conversión de cuentas a uV
        //private const double ConstanteDeEscala = 0.00093215;        // Ganancia unitaria
//        private const double ConstanteDeEscala = 0.02235;        // Si no se modifica nada, por defecto esta es la constante de conversión.
        private double ConstanteDeEscala;
        // La fórmula es => K = ( 4.5 volts / ganacia ) / ( 2^23 - 1 );
        // Por default, la ganancia del conversor es de 24


        /* VARIABLES PARA MANEJAR LA COMUNICACION SERIE */

        // Objeto para manejar el puerto serie
        private const int tasaPuertoSerie = 115200;
        private const int bitsDeDatos = 8;
        private SerialPort puertoSerie = new SerialPort("COM1", tasaPuertoSerie, Parity.None, bitsDeDatos, StopBits.One);

        // Se considera una tiempo maximo a la recepcion de "maximoTramasNoRecibidas" para que se considere como caida la comunicacion
        private const int maximoTramasNoRecibidas = 500;
        private const int maximoTiempoReadSerie = maximoTramasNoRecibidas * TiempoFijoEntreTramas;      // Timeout normal durante la recepcion de tramas
        private const int maximoTiempoReadSerieInicio = 1000 * TiempoFijoEntreTramas;                   // Timeout para el reinicio del casco, de 1 segundo

        // Variable para corregir la lectura de las tramas cuando se desincronicen
        private int desplazamientoTrama;

        private int maximoTramasRecibidas;

        // Variables internas para el control del estado
        private int bytesLeidos;
        private int totalBytesLeidos;
        private byte[] tramaAuxiliar;
        private byte[] trama;

        // Variable para indicar el error que surja
        private string error;

        // Varaibles para hacer mas legible el envio de datos hacia el casco
        private string parametro1, parametro2, parametro3, parametro4, parametro5, parametro6, parametro7;


        // Objeto para ejecutar el codigo de Matlab
        private MLApp.MLApp funcionMatlab = new MLApp.MLApp();
        private object[] impedanciaCalculada;

        // Variables privadas para almacenar los valores calculados de las impedancias de los contactos
        private double impedanciaContactoFp1;
        private double impedanciaContactoFp2;
        private double impedanciaContactoC3;
        private double impedanciaContactoC4;
        private double impedanciaContactoP7;
        private double impedanciaContactoP8;
        private double impedanciaContactoO1;
        private double impedanciaContactoO2;

        // Constantes para medir la calidad de los contactos basados en sus impedancias
        private const double LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO = 100000;      // 100k
        private const double LIMITE_IMPEDANCIA_CONTACTO_BIEN = 10000;               // 10k

        public enum CalidadDelContacto { CONTACTO_BUENO, CONTACTO_REGULAR, CONTACTO_MALO };
        CalidadDelContacto calidadDelContactoFp1;
        CalidadDelContacto calidadDelContactoFp2;
        CalidadDelContacto calidadDelContactoC3;
        CalidadDelContacto calidadDelContactoC4;
        CalidadDelContacto calidadDelContactoP7;
        CalidadDelContacto calidadDelContactoP8;
        CalidadDelContacto calidadDelContactoO1;
        CalidadDelContacto calidadDelContactoO2;



        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        public DatosCasco()
        {
            BorrarDatos();

            configuracionEncendido = new List<bool>();
            configuracionBIAS = new List<bool>();
            configuracionGanancia = new List<int>();
            configuracionImpendanciaMidiendo = new List<bool>();
            configuracionImpendanciaContactoN = new List<bool>();
            configuracionImpendanciaContactoP = new List<bool>();
            configuracionSRB1 = new List<bool>();
            configuracionSRB2 = new List<bool>();
            configuracionTipoDeEntrada = new List<int>();

            // Almacenado de las configuraciones por default
            for (int indice = 0; indice < TOTAL_DE_CANALES; indice++)
            {
                configuracionEncendido.Add(OPCION_POR_DEFAULT_ENCENDIDO);
                configuracionGanancia.Add(OPCION_POR_DEFAULT_GANANCIA);
                configuracionTipoDeEntrada.Add(OPCION_POR_DEFAULT_TIPO_DE_ENTRADA);
                configuracionBIAS.Add(OPCION_POR_DEFAULT_BIAS);
                configuracionSRB2.Add(OPCION_POR_DEFAULT_SRB2);
                configuracionSRB1.Add(OPCION_POR_DEFAULT_SRB1);
                configuracionImpendanciaMidiendo.Add(OPCION_POR_DEFAULT_MEDIENDO_IMPEDANCIA);
                configuracionImpendanciaContactoN.Add(OPCION_POR_DEFAULT_MEDIR_IMPEDANCIA_N);
                configuracionImpendanciaContactoP.Add(OPCION_POR_DEFAULT_MEDIR_IMPEDANCIA_P);
            }

        }





        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                           MÉTODOS Y PROPIEDADES                                                          */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/



        /********************************************************************************************************************************************/
        /*                                                                 PUBLICOS                                                                 */
        /********************************************************************************************************************************************/



        /***    CONTROL SOBRE EL ESTABLECIMIENTO O CORTE DEL ENLACE    ***/


        /****************************************************************/
        /* EstablecerComunicacion                                       */
        /*   Configura el puerto serie, lo abre y recibe el mensaje     */
        /*   inicial del casco                                          */
        /*                                                              */
        /* Recibe:                                                      */
        /*   nombrePuerto - string con el nombre del puerto a usar      */
        /*   mostrarInformacion - Un indicador para mostrar o no el     */
        /*      mensaje inicial                                         */
        /* Devuelve:                                                    */
        /*   True - Si se configuro y abrio correctamente               */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        public bool EstablecerComunicacionSerie(string nombrePuerto, bool mostrarInformacion)
        {
            try
            {
                puertoSerie.PortName = nombrePuerto;                        // Se configura el nombre del puerto
                puertoSerie.ReadTimeout = maximoTiempoReadSerieInicio;      // Se establece el timeout inicial, mas alto que el resto
                puertoSerie.Open();
                puertoSerie.Write(CARACTER_DETENER_ENVIO_DE_DATOS);       // Se da la orden de detener un posible streaming previo, ante la duda
                System.Threading.Thread.Sleep(100);                         // Se le da tiempo al casco para procesar el comando
                puertoSerie.DiscardInBuffer();                              // Se limpia el buffer de recepcion por las dudas
                puertoSerie.Write(CARACTER_REINICIAR_CASCO);              // Se manda la orden de reinicio al casco
            }

            catch (UnauthorizedAccessException)     // Open
            {
                Error = "No se tienen permisos para abrir el puerto serie - EstablecerComunicacion";
                return (false);
            }

            catch (System.IO.IOException)
            {
                Error = "Error al intentar abrir el puerto serie - EstablecerComunicacion";
                return (false);
            }

            catch (ArgumentException)               // PortName
            {
                Error = "El nombre suministrado para el puerto no es correcto - EstablecerComunicacion";
                return (false);
            }

            catch (InvalidOperationException)       // PortName
            {
                if (puertoSerie.IsOpen)
                {
                    Error = "El puerto ya esta en uso - EstablecerComunicacion";
                    return (false);
                }
                Error = "No se puede asignar el nombre al puerto - EstablecerComunicacion";
                return (false);
            }


            // Se completo el bloque try-catch sin excepciones. Se procede a verificar si se envio el mensaje inicial de forma correcta
            if (LeerMensajeInicial(mostrarInformacion) == false)       // False: No se leyo correctamente el mensaje. Se cierra el puerto serie
            {
                // La funcion "LeerMensajeInicial" se encarga de informar el error
                TerminarComunicacionSerie();                           // Se abrio un puerto pero la comunicacion no es correcta. Se debe cerrar ese enlace
                return (false);
            }

            // En este punto, el puerto se configuro correctamente y el casco envio el mensaje inicial sin fallas
            return (true);
        }



        /****************************************************************/
        /* TerminarComunicacionSerie                                    */
        /*   Si el puerto esta abierto, manda la senial de detener un   */
        /*   posible streaming de datos, para que el casco se quede en  */
        /*   estado de reposo. Luego cierra el puerto                   */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si se puede cerrar bien                             */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        public bool TerminarComunicacionSerie()
        {
            if (puertoSerie.IsOpen)
            {
                try
                {
                    puertoSerie.Write(CARACTER_DETENER_ENVIO_DE_DATOS);         // Se da la orden de detener un posible streaming previo, ante la duda
                }
                catch (TimeoutException)
                {
                    Error = "Se excedio el tiempo limite para el envio de datos - TerminarComunicacionSerie";
                    return (false);
                }
                System.Threading.Thread.Sleep(100);                         // Se le da tiempo al casco para procesar el comando
                try
                {
                    puertoSerie.Close();
                }
                catch (System.IO.IOException)
                {
                    Error = "Ocurrio un error al cerrar el puerto serie - TerminarComunicacionSerie";
                    return (false);
                }
            }

            // Todo salio bien
            return (true);
        }



        /***    CONTROL SOBRE EL INICIO O FIN DEL ENVIO DE DATOS    ***/


        /****************************************************************/
        /* IniciarComunicacion                                          */
        /*   Si el puerto esta abierto, manda la senial de iniciar el   */
        /*   streaming de datos y realiza una demora para que el casco  */
        /*   procese el comando                                         */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si se puede mandar la senial correctamente          */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        public bool IniciarEnvioDeDatos()
        {
            if (puertoSerie.IsOpen)
            {
                puertoSerie.ReadTimeout = maximoTiempoReadSerie;
                try
                {
                    puertoSerie.Write(CARACTER_INICIAR_ENVIO_DE_DATOS);
                }
                catch (TimeoutException)
                {
                    Error = "Se excedio el tiempo limite para el envio de datos - IniciarComunicacion";
                    return (false);
                }
                puertoSerie.DiscardInBuffer();
                System.Threading.Thread.Sleep(50);         // Se le da tiempo al casco para procesar el comando
                return (true);
            }

            Error = "El puerto serie no se encuentra abierto al intentar iniciar la comunicacion - IniciarComunicacion";
            return (false);
        }



        /****************************************************************/
        /* DetenerComunicacion                                          */
        /*   Si el puerto esta abierto, manda la senial de detener el   */
        /*   streaming de datos y realiza una demora para que el casco  */
        /*   procese el comando                                         */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si se puede mandar la senial correctamente          */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        public bool DetenerEnvioDeDatos()
        {
            if (puertoSerie.IsOpen)
            {
                try
                {
                    puertoSerie.Write(CARACTER_DETENER_ENVIO_DE_DATOS);
                }
                catch (TimeoutException)
                {
                    Error = "Se excedio el tiempo limite para el envio de datos - DetenerComunicacion";
                    return (false);
                }
                System.Threading.Thread.Sleep(100);         // Se le da tiempo al casco para procesar el comando
                return (true);
            }

            Error = "El puerto serie no se encuentra abierto al intentar detener la comunicacion - DetenerComunicacion";
            return (false);
        }



        /***    CONTROL SOBRE LA LECTURA Y ALMACENAMIENTO / PEDIDO DE DATOS    ***/


        /****************************************************************/
        /* AdquirirDatos                                                */
        /*   Realiza la lectura y verificacion de una trama de datos    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si se puede leer bien                               */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        public bool AdquirirDatos()
        {
            try
            {
                bytesLeidos = puertoSerie.Read(trama, desplazamientoTrama, LongitudDeTrama);
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo limite para la lectura de datos - AdquirirDatos";
                return (false);
            }
            catch (ArgumentException)
            {
                Error = "Valores fuera de los limites admisibles - AdquirirDatos";
                return (false);
            }

            if (VerificarLongitudTrama() == true)   // Ante una trama incompleta, se realizan 5 intentos mas de lectura para completarla
            {
                // Se leyo una trama completa. Resta verificar su alineacion. Si esta desfasada, se la corrije
                if (VerificarTrama() == true)
                {
                    // La alineacion es correcta o solo esta desfasado y se contempla en la variable "desplazamientoTrama" (Ver el bloque try anterior)
                    CantidadDeTramasValidas++;
                    AlmacenarTrama();
                    System.Threading.Thread.Sleep(TiempoFijoEntreTramas);
                    return (true);
                }

                // La trama esta corrupta o no esta alineada (Falla al menos un header). La funcion "VerificarTrama()" indica el error
                return (false);
            }

            // No se pudo completar la lectura de una trama en los 5 intentos que realiza "VerificarLongitudTrama()", la cual controla la variable de error
            CantidadDeTramasFalladas++;
            return (false);
        }



        /****************************************************************/
        /* BorrarDatos                                                  */
        /*   Reinicia todas las variables internas                      */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void BorrarDatos()
        {
            // Inicialización de las listas
            CanalUno = new List<Int32>();
            CanalDos = new List<Int32>();
            CanalTres = new List<Int32>();
            CanalCuatro = new List<Int32>();
            CanalCinco = new List<Int32>();
            CanalSeis = new List<Int32>();
            CanalSiete = new List<Int32>();
            CanalOcho = new List<Int32>();
            NumerosDeTramas = new List<Int32>();
            CantidadDeTramasValidas = 0;
            CantidadDeTramasFalladas = 0;
            CantidadDeTramasIncompletas = 0;
            CantidadDeTramasDesfasadas = 0;
            NumeroTramaAnterior = 0;
            desplazamientoTrama = 0;
            tramaAuxiliar = new byte[LongitudDeTrama];
            trama = new byte[LongitudDeTrama];
            bytesLeidos = 0;
            error = "";
        }



        /****************************************************************/
        /* LeerCanal                                                    */
        /*   Convierte la lista de datos en un vector                   */
        /*                                                              */
        /* Recibe: El numero del canal                                  */
        /* Devuelve: Los datos en formato de vector                     */
        /****************************************************************/
        public Int32[] LeerCanal(int canal)
        {
            switch (canal)
            {
                case CANAL_1:
                    return CanalUno.ToArray();
                case CANAL_2:
                    return CanalDos.ToArray();
                case CANAL_3:
                    return CanalTres.ToArray();
                case CANAL_4:
                    return CanalCuatro.ToArray();
                case CANAL_5:
                    return CanalCinco.ToArray();
                case CANAL_6:
                    return CanalSeis.ToArray();
                case CANAL_7:
                    return CanalSiete.ToArray();
                default:
                    return CanalOcho.ToArray();
            }
        }



        /****************************************************************/
        /* LeerListaDelCanal                                            */
        /*   Devuelve los datos en forma de lista                       */
        /*                                                              */
        /* Recibe: El numero del canal                                  */
        /* Devuelve: Los datos en formato de lista                      */
        /****************************************************************/
        public List<Int32> LeerListaDelCanal(int canal)
        {
            switch (canal)
            {
                case CANAL_1:
                    return CanalUno;
                case CANAL_2:
                    return CanalDos;
                case CANAL_3:
                    return CanalTres;
                case CANAL_4:
                    return CanalCuatro;
                case CANAL_5:
                    return CanalCinco;
                case CANAL_6:
                    return CanalSeis;
                case CANAL_7:
                    return CanalSiete;
                default:
                    return CanalOcho;
            }
        }



        /****************************************************************/
        /* LeerDatoDelCanal                                             */
        /*   Lee una posicion especifica de la lista de datos           */
        /*                                                              */
        /* Recibe:                                                      */
        /*   canal - El numero del canal                                */
        /*   muestra - La posicion del dato                             */
        /* Devuelve: El datos solicitado                                */
        /****************************************************************/
        public Int32 LeerDatoDelCanal(int canal, int muestra)
        {
            switch (canal)
            {
                case CANAL_1:
                    return CanalUno[muestra];
                case CANAL_2:
                    return CanalDos[muestra];
                case CANAL_3:
                    return CanalTres[muestra];
                case CANAL_4:
                    return CanalCuatro[muestra];
                case CANAL_5:
                    return CanalCinco[muestra];
                case CANAL_6:
                    return CanalSeis[muestra];
                case CANAL_7:
                    return CanalSiete[muestra];
                default:
                    return CanalOcho[muestra];
            }
        }



        /****************************************************************/
        /* LeerNumeroDeMuestra                                          */
        /*   Lee una posicion especifica de la lista del contador       */
        /*                                                              */
        /* Recibe:                                                      */
        /*   muestra - La posicion del dato                             */
        /* Devuelve: El datos solicitado                                */
        /****************************************************************/
        public Int32 LeerNumeroDeMuestra(int muestra)
        {
            return( NumerosDeTramas[muestra] );
        }



        /***    CONTROL SOBRE LAS CONFIGURACIONES DEL HARDWARE DEL CASCO    ***/


        /****************************************************************/
        /* ApagarCanal                                                  */
        /*   Manda el comando para apagar el canal solicitado           */
        /*                                                              */
        /* Recibe: El numero del canal                                  */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si esta mal el numero del canal o no puede mandar  */
        /*      la orden correspondiente                                */
        /****************************************************************/
        public bool ApagarCanal(int canal)
        {
            try
            {
                switch (canal)
                {
                    case CANAL_1:
                        puertoSerie.Write(CARACTER_CANAL_1_APAGAR);
                        break;
                    case CANAL_2:
                        puertoSerie.Write(CARACTER_CANAL_2_APAGAR);
                        break;
                    case CANAL_3:
                        puertoSerie.Write(CARACTER_CANAL_3_APAGAR);
                        break;
                    case CANAL_4:
                        puertoSerie.Write(CARACTER_CANAL_4_APAGAR);
                        break;
                    case CANAL_5:
                        puertoSerie.Write(CARACTER_CANAL_5_APAGAR);
                        break;
                    case CANAL_6:
                        puertoSerie.Write(CARACTER_CANAL_6_APAGAR);
                        break;
                    case CANAL_7:
                        puertoSerie.Write(CARACTER_CANAL_7_APAGAR);
                        break;
                    case CANAL_8:
                        puertoSerie.Write(CARACTER_CANAL_8_APAGAR);
                        break;
                    default:
                        Error = "El numero del canal no es correcto - ApagarCanal";
                        return (false);
                }
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo limite para el envio de datos - ApagarCanal";
                return (false);
            }

            IndicarCanalApagado(canal);
            return (true);
        }



        /****************************************************************/
        /* PrenderCanal                                                 */
        /*   Manda el comando para prender el canal solicitado          */
        /*                                                              */
        /* Recibe: El numero del canal                                  */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si esta mal el numero del canal o no puede mandar  */
        /*      la orden correspondiente                                */
        /****************************************************************/
        public bool PrenderCanal(int canal)
        {
            try
            {
                switch (canal)
                {
                    case CANAL_1:
                        puertoSerie.Write(CARACTER_CANAL_1_ENCENDER);
                        break;
                    case CANAL_2:
                        puertoSerie.Write(CARACTER_CANAL_2_ENCENDER);
                        break;
                    case CANAL_3:
                        puertoSerie.Write(CARACTER_CANAL_3_ENCENDER);
                        break;
                    case CANAL_4:
                        puertoSerie.Write(CARACTER_CANAL_4_ENCENDER);
                        break;
                    case CANAL_5:
                        puertoSerie.Write(CARACTER_CANAL_5_ENCENDER);
                        break;
                    case CANAL_6:
                        puertoSerie.Write(CARACTER_CANAL_6_ENCENDER);
                        break;
                    case CANAL_7:
                        puertoSerie.Write(CARACTER_CANAL_7_ENCENDER);
                        break;
                    case CANAL_8:
                        puertoSerie.Write(CARACTER_CANAL_8_ENCENDER);
                        break;
                    default:
                        Error = "El numero del canal no es correcto - PrenderCanal";
                        return (false);
                }
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo limite para el envio de datos - PrenderCanal";
                return (false);
            }

            IndicarCanalEncendido(canal);
            return (true);
        }



        /****************************************************************/
        /* ConfigurarCanal                                              */
        /*   Configura el canal segun las opciones deseadas             */
        /*                                                              */
        /* Recibe:                                                      */
        /*      canal - El numero de canal a configurar                 */
        /*      encendido - Si el canal se debe prender o no            */
        /*      ganancia - La ganancia del canal                        */
        /*      entrada - El tipo de entrada (ADS_INPUT)                */
        /*      bias - Si el canal se incluye o no en esta senial       */
        /*      srb2 - Si la entrada P del canal se conecta a este pin  */
        /*      srb1 - Si las entradas N se conectan a este pin         */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si algun parametro esta mal                        */
        /****************************************************************/
        public bool ConfigurarCanal(int canal, bool encendido, int ganancia, int entrada, bool bias, bool srb2, bool srb1)
        {
            // Primero se realiza una verificacion de que todos los parametros sean correctos

            switch (canal)
            {
                case CANAL_1:
                    parametro1 = CARACTER_CANAL_1;
                    break;
                case CANAL_2:
                    parametro1 = CARACTER_CANAL_2;
                    break;
                case CANAL_3:
                    parametro1 = CARACTER_CANAL_3;
                    break;
                case CANAL_4:
                    parametro1 = CARACTER_CANAL_4;
                    break;
                case CANAL_5:
                    parametro1 = CARACTER_CANAL_5;
                    break;
                case CANAL_6:
                    parametro1 = CARACTER_CANAL_6;
                    break;
                case CANAL_7:
                    parametro1 = CARACTER_CANAL_7;
                    break;
                case CANAL_8:
                    parametro1 = CARACTER_CANAL_8;
                    break;
                default:
                    Error = "El numero del canal no es correcto - ConfigurarCanal";
                    return (false);
            }

            switch (encendido)
            {
                case CANAL_PRENDIDO:
                    parametro2 = CARACTER_CANAL_PRENDIDO;
                    break;
                case CANAL_APAGADO:
                    parametro2 = CARACTER_CANAL_APAGADO;
                    break;
                default:
                    Error = "La opcion de encendido no es correcta - ConfigurarCanal";
                    return (false);
            }

            switch (ganancia)
            {
                case GANANCIA_1:
                    parametro3 = CARACTER_GANANCIA_1;
                    break;
                case GANANCIA_2:
                    parametro3 = CARACTER_GANANCIA_2;
                    break;
                case GANANCIA_4:
                    parametro3 = CARACTER_GANANCIA_4;
                    break;
                case GANANCIA_6:
                    parametro3 = CARACTER_GANANCIA_6;
                    break;
                case GANANCIA_8:
                    parametro3 = CARACTER_GANANCIA_8;
                    break;
                case GANANCIA_12:
                    parametro3 = CARACTER_GANANCIA_12;
                    break;
                case GANANCIA_24:
                    parametro3 = CARACTER_GANANCIA_24;
                    break;
                default:
                    Error = "La ganancia no es correcta - ConfigurarCanal";
                    return (false);
            }

            switch (entrada)
            {
                case ADS_INPUT_NORMAL:
                    parametro4 = CARACTER_ADS_INPUT_NORMAL;
                    break;
                case ADS_INPUT_SHORTED:
                    parametro4 = CARACTER_ADS_INPUT_SHORTED;
                    break;
                case ADS_INPUT_BIAS_MEAS:
                    parametro4 = CARACTER_ADS_INPUT_BIAS_MEAS;
                    break;
                case ADS_INPUT_MVDD:
                    parametro4 = CARACTER_ADS_INPUT_MVDD;
                    break;
                case ADS_INPUT_TEMP:
                    parametro4 = CARACTER_ADS_INPUT_TEMP;
                    break;
                case ADS_INPUT_TESTING:
                    parametro4 = CARACTER_ADS_INPUT_TESTING;
                    break;
                case ADS_INPUT_BIAS_DRP:
                    parametro4 = CARACTER_ADS_INPUT_BIAS_DRP;
                    break;
                case ADS_INPUT_BIAS_DRN:
                    parametro4 = CARACTER_ADS_INPUT_BIAS_DRN;
                    break;
                default:
                    Error = "La opcion de entrada no es correcta - ConfigurarCanal";
                    return (false);
            }

            switch (bias)
            {
                case BIAS_REMOVE:
                    parametro5 = CARACTER_BIAS_REMOVE;
                    break;
                case BIAS_INCLUDE:
                    parametro5 = CARACTER_BIAS_INCLUDE;
                    break;
                default:
                    Error = "La opcion de BIAS no es correcta - ConfigurarCanal";
                    return (false);
            }

            switch (srb2)
            {
                case SRB2_DISCONNECT:
                    parametro6 = CARACTER_SRB2_DISCONNECT;
                    break;
                case SRB2_CONNECT:
                    parametro6 = CARACTER_SRB2_CONNECT;
                    break;
                default:
                    Error = "La opcion de SRB2 no es correcta - ConfigurarCanal";
                    return (false);
            }

            switch (srb1)
            {
                case SRB1_DISCONNECT_ALL:
                    parametro7 = CARACTER_SRB1_DISCONNECT_ALL;
                    break;
                case SRB1_CONNECT_ALL:
                    parametro7 = CARACTER_SRB1_CONNECT_ALL;
                    break;
                default:
                    Error = "La opcion de SRB1 no es correcta - ConfigurarCanal";
                    return (false);
            }

            // En este punto, todos los parametros fueron verificados y son correctos. Se envia la orden de modificacion al casco
            try
            {
                puertoSerie.Write("x");           // Inicio del comando
                puertoSerie.Write(parametro1);    // Numero del canal
                puertoSerie.Write(parametro2);    // Opcion para el encendido
                puertoSerie.Write(parametro3);    // Ganancia
                puertoSerie.Write(parametro4);    // Tipo de entrada
                puertoSerie.Write(parametro5);    // BIAS
                puertoSerie.Write(parametro6);    // SRB2
                puertoSerie.Write(parametro7);    // SRB1
                puertoSerie.Write("X");           // Fin del comando
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo maximo para el envio de los comandos - ConfigurarCanal";
                return (false);
            }

            // Luego de configurar correctamente el canal, se modifican sus valores en la lista de configuraciones
            canal--;        // Esto es porque las listas arrancan desde cero y el numero del canal arranca en 1
            ModificarEncendidoDelCanal(canal, encendido);
            ModificarGananciaDelCanal(canal, ganancia);
            ModificarTipoDeEntradaDelCanal(canal, entrada);
            ModificarBiasDelCanal(canal, bias);
            ModificarSRB1DelCanal(canal, srb1);
            ModificarSRB2DelCanal(canal, srb2);

            if(encendido == CANAL_PRENDIDO)
                IndicarCanalEncendido(canal);
            else
                IndicarCanalApagado(canal);

            return (true);
        }



        /****************************************************************/
        /* ActivarMedicionDeImpedancia                                  */
        /*   Inyecta las seniales de medicion en las entradas           */
        /*                                                              */
        /* Recibe:                                                      */
        /*      canal - El numero de canal a configurar                 */
        /*      opcionEntradaP - Indica si se inyecta o no la senial    */
        /*      opcionEntradaN - Indica si se inyecta o no la senial    */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si algun parametro esta mal                        */
        /****************************************************************/
        public bool ActivarMedicionDeImpedancia(int canal, bool opcionEntradaP, bool opcionEntradaN)
        {
            // Primero se realiza una verificacion de que todos los parametros sean correctos

            switch (canal)
            {
                case CANAL_1:
                    parametro1 = CARACTER_CANAL_1;
                    break;
                case CANAL_2:
                    parametro1 = CARACTER_CANAL_2;
                    break;
                case CANAL_3:
                    parametro1 = CARACTER_CANAL_3;
                    break;
                case CANAL_4:
                    parametro1 = CARACTER_CANAL_4;
                    break;
                case CANAL_5:
                    parametro1 = CARACTER_CANAL_5;
                    break;
                case CANAL_6:
                    parametro1 = CARACTER_CANAL_6;
                    break;
                case CANAL_7:
                    parametro1 = CARACTER_CANAL_7;
                    break;
                case CANAL_8:
                    parametro1 = CARACTER_CANAL_8;
                    break;
                default:
                    Error = "El numero del canal no es correcto - ActivarMedicionDeImpedancia";
                    return (false);
            }

            if (opcionEntradaP == NO_MEDIR_IMPEDANCIA_P)
                parametro2 = CARACTER_NO_MEDIR_IMPEDANCIA_P;
            else
                parametro2 = CARACTER_SI_MEDIR_IMPEDANCIA_P;

            if (opcionEntradaN == NO_MEDIR_IMPEDANCIA_N)
                parametro3 = CARACTER_NO_MEDIR_IMPEDANCIA_N;
            else
                parametro3 = CARACTER_SI_MEDIR_IMPEDANCIA_N;


            // En este punto, todos los parametros fueron verificados y son correctos. Se envia la orden de modificacion al casco
            try
            {
                puertoSerie.Write("z");           // Inicio del comando
                puertoSerie.Write(parametro1);    // Numero del canal
                puertoSerie.Write(parametro2);    // Opcion para la entrada P
                puertoSerie.Write(parametro3);    // Opcion para la entrada N
                puertoSerie.Write("Z");           // Fin del comando
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo maximo para el envio de los comandos - ActivarMedicionDeImpedancia";
                return (false);
            }

            return (true);
        }



        /****************************************************************/
        /* DesactivarMedicionDeImpedancia                               */
        /*   Quita las seniales de medicion en las entradas del canal   */
        /*                                                              */
        /* Recibe: El numero del canal                                  */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si el parametro esta mal                           */
        /****************************************************************/
        public bool DesactivarMedicionDeImpedancia(int canal)
        {
            // Primero se realiza una verificacion de que el parametro sea correcto

            switch (canal)
            {
                case CANAL_1:
                case CANAL_2:
                case CANAL_3:
                case CANAL_4:
                case CANAL_5:
                case CANAL_6:
                case CANAL_7:
                case CANAL_8:
                    break;
                default:
                    Error = "El numero del canal no es correcto - DesactivarMedicionDeImpedancia";
                    return (false);
            }

            // No se tiene en cuenta la devolucion de "ActivarMedicionDeImpedancia" porque sera TRUE, ya que los parametros estan verificados
            ActivarMedicionDeImpedancia(canal, NO_MEDIR_IMPEDANCIA_P, NO_MEDIR_IMPEDANCIA_N);
            return (true);

        }



        /****************************************************************/
        /* ConectarSenialDeControl                                      */
        /*   Conecta todos los canales activos a la senial de control   */
        /*                                                              */
        /* Recibe: El tipo de senial a inyectar                         */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si el parametro esta mal                           */
        /****************************************************************/
        public bool ConectarSenialDeControl(int opcionElegida)
        {
            try
            {
                switch (opcionElegida)
                {
                    case CONNECT_TO_INTERNAL_GND:
                         puertoSerie.Write(CARACTER_CONNECT_TO_INTERNAL_GND);
                         break;
                    case CONNECT_TO_TEST_SIGNAL_1X_SLOW_PULSE:
                         puertoSerie.Write(CARACTER_CONNECT_TO_TEST_SIGNAL_1X_SLOW_PULSE);
                         break;
                    case CONNECT_TO_TEST_SIGNAL_1X_FAST_PULSE:
                         puertoSerie.Write(CARACTER_CONNECT_TO_TEST_SIGNAL_1X_FAST_PULSE);
                         break;
                    case CONNECT_TO_DC_SIGNAL:
                         puertoSerie.Write(CARACTER_CONNECT_TO_DC_SIGNAL);
                         break;
                    case CONNECT_TO_TEST_SIGNAL_2X_SLOW_PULSE:
                         puertoSerie.Write(CARACTER_CONNECT_TO_TEST_SIGNAL_2X_SLOW_PULSE);
                         break;
                    case CONNECT_TO_TEST_SIGNAL_2X_FAST_PULSE:
                         puertoSerie.Write(CARACTER_CONNECT_TO_TEST_SIGNAL_2X_FAST_PULSE);
                         break;
                    default:
                        Error = "La opcion de la senial de control no es correcta - ConectarSenialDeControl";
                        return (false);
                }
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo maximo para el envio de los comandos - ConectarSenialDeControl";
                return (false);
            }

            return (true);

        }



        /****************************************************************/
        /* ConfigurarCanalesPorDefault                                  */
        /*   Conecta todos los canales con las opciones por default     */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si se pudo enviar el comando                        */
        /*   False - Si hay una falla (SIN IMPLEMENTAR TODAVIA)         */
        /****************************************************************/
        public bool ConfigurarCanalesPorDefault()
        {
            try
            {
                puertoSerie.Write(CARACTER_OPCIONES_POR_DEFAULT);
            }
            catch (TimeoutException)
            {
                Error = "Se excedio el tiempo maximo para el envio de los comandos - ConfigurarCanalesPorDefault";
                return (false);
            }
            return (true);
        }



        /***    CONTROL SOBRE EL PROCESAMIENTO DE LOS DATOS POST RECOLECCION    ***/


        /****************************************************************/
        /* CalcularImpedancias                                          */
        /*   Realiza el calculo de las impedancias luego de que se      */
        /*   hayan recabado los datos                                   */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void CalcularImpedancias()
        {
            // Procesamiento de los datos
            funcionMatlab.Execute(@"cd " + Directory.GetCurrentDirectory());
            object resultadoMatlab = null;

            // Impedancia 1 - Contacto Fp1
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_1));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoFp1 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 2 - Contacto Fp2
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_2));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoFp2 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 3 - Contacto C3
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_3));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoC3 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 4 - Contacto C4
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_4));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoC4 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 5 - Contacto P7
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_5));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoP7 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 6 - Contacto P8
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_6));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoP8 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 7 - Contacto O1
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_7));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoO1 = Math.Round((double)(impedanciaCalculada[0]));

            // Impedancia 8 - Contacto O2
            resultadoMatlab = null;
            funcionMatlab.Feval("obtenerImpedancia", 1, out resultadoMatlab, LeerCanal(CANAL_8));
            impedanciaCalculada = resultadoMatlab as object[];
            impedanciaContactoO2 = Math.Round((double)(impedanciaCalculada[0]));

            // Luego de obtener todas las impedancias, se obtiene la calidad del contacto
            VerificarLaCalidadDeLosContactos();
        }



        /****************************************************************/
        /* AcondicionarDatos                                            */
        /*   Verifica que no haya muestras nulas, copiando el valor mas */
        /*   proximo distinto de cero y verifica que la longitud de la  */
        /*   lista de datos sea la correcta                             */
        /*                                                              */
        /* Recibe: El numero maximo de datos validos                    */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public bool AcondicionarDatos()
        {
            // Al agregar un marker inicial y otro final, la lista debe contener 2 elementos mas que el total de tramas recibidas
            Int32 limite = maximoTramasRecibidas + 2;

            // Primero se verifica que la cantidad de datos no exceda el maximo previsto
            Int32 diferencia;

            if (LongitudDeLaLista > limite)
            {
                try
                {
                    diferencia = LongitudDeLaLista - limite;    // Se excedieron los datos y hay que eliminar
                    CanalUno.RemoveRange(limite, diferencia);
                    CanalDos.RemoveRange(limite, diferencia);
                    CanalTres.RemoveRange(limite, diferencia);
                    CanalCuatro.RemoveRange(limite, diferencia);
                    CanalCinco.RemoveRange(limite, diferencia);
                    CanalSeis.RemoveRange(limite, diferencia);
                    CanalSiete.RemoveRange(limite, diferencia);
                    CanalOcho.RemoveRange(limite, diferencia);
                    NumerosDeTramas.RemoveRange(limite, diferencia);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Error = "Los valores estan fuera del rango permitido - AcondicionarDatos";
                    return (false);
                }
                catch (ArgumentException)
                {
                    Error = "Los valores suministrados nos estan permitidos - AcondicionarDatos";
                    return (false);
                }
            }
            else
            {
                // No se completaron los datos y hay que rellenar. Primero conviene revisar que las ultimas muestras nulas
                diferencia = limite - LongitudDeLaLista;
                for (int indice = 0; indice < diferencia; indice++) {   DuplicarUltimoValor();  }
            }

            // En este punto la lista tiene la dimension exacta. Resta eliminar las muestras nulas al inicio
            EliminarPrimerMuestraNula();

            return true;
        }



        /****************************************************************/
        /* ActualizarMaximoDeTramasARecibir                             */
        /*   Calcula el maximo de tramas a recibir en funcion del       */
        /*   de duracion total del estudio                              */
        /*                                                              */
        /* Recibe: El tiempo del estudio en segundos                    */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public bool ActualizarMaximoDeTramasARecibir(Int32 tiempoDelEstudioEnSeg)
        {
            if (tiempoDelEstudioEnSeg > 0)
            {
                maximoTramasRecibidas = (tiempoDelEstudioEnSeg * 1000) / TiempoEntreTramas;
                return( true );
            }

            return(false);
        }



        // Funciones para leer los datos de las configuraciones
        public bool LeerEncendidoDelCanal(Int32 canal)      { return (configuracionEncendido[canal]); }
        public Int32 LeerGananciaDelCanal(Int32 canal)      { return (configuracionGanancia[canal]); }
        public Int32 LeerTipoDeEntradaDelCanal(Int32 canal) { return (configuracionTipoDeEntrada[canal]); }
        public bool LeerBiasDelCanal(Int32 canal)           { return (configuracionBIAS[canal]); }
        public bool LeerSRB1DelCanal(Int32 canal)           { return (configuracionSRB1[canal]); }
        public bool LeerSRB2DelCanal(Int32 canal)           { return (configuracionSRB2[canal]); }

        // Funciones para escribir los datos de las configuraciones
        public void ModificarEncendidoDelCanal(Int32 canal, bool encendido)     { configuracionEncendido[canal] = encendido; }
        public void ModificarGananciaDelCanal(Int32 canal, Int32 ganancia)      { configuracionGanancia[canal] = ganancia; }
        public void ModificarTipoDeEntradaDelCanal(Int32 canal, Int32 entrada)  { configuracionTipoDeEntrada[canal] = entrada; }
        public void ModificarBiasDelCanal(Int32 canal, bool bias)               { configuracionBIAS[canal] = bias; }
        public void ModificarSRB1DelCanal(Int32 canal, bool SRB1)               { configuracionSRB1[canal] = SRB1; }
        public void ModificarSRB2DelCanal(Int32 canal, bool SRB2)               { configuracionSRB2[canal] = SRB2; }
        

        // Funciones para leer los datos de las configuraciones de medicion de la impedancia
        public bool LeerImpedanciaMidiendo(Int32 canal) { return (configuracionImpendanciaMidiendo[canal]); }
        public bool LeerImpedanciaContactoPDelCanal(Int32 canal) { return (configuracionImpendanciaContactoP[canal]); }
        public bool LeerImpedanciaContactoNDelCanal(Int32 canal) { return (configuracionImpendanciaContactoN[canal]); }

        // Funciones para escribir los datos de las impedancias
        public void ModificarImpedanciaMidiendo(Int32 canal, bool opcion) { configuracionImpendanciaMidiendo[canal] = opcion; }
        public void ModificarImpedanciaContactoPDelCanal(Int32 canal, bool opcion) { configuracionImpendanciaContactoP[canal] = opcion; }
        public void ModificarImpedanciaContactoNDelCanal(Int32 canal, bool opcion) { configuracionImpendanciaContactoN[canal] = opcion; }






        /***    METODOS "SET" Y "GET" PARA ACCEDER LAS PROPIEDADES DEL OBJETO    ***/


        // Impendancia de los contactos
        public double ImpedanciaContactoFp1 { get { return impedanciaContactoFp1; } }
        public double ImpedanciaContactoFp2 { get { return impedanciaContactoFp2; } }
        public double ImpedanciaContactoC3 { get { return impedanciaContactoC3; } }
        public double ImpedanciaContactoC4 { get { return impedanciaContactoC4; } }
        public double ImpedanciaContactoP7 { get { return impedanciaContactoP7; } }
        public double ImpedanciaContactoP8 { get { return impedanciaContactoP8; } }
        public double ImpedanciaContactoO1 { get { return impedanciaContactoO1; } }
        public double ImpedanciaContactoO2 { get { return impedanciaContactoO2; } }


        // Calidad de los contactos
        public CalidadDelContacto CalidadDelContactoFp1 { get { return calidadDelContactoFp1; } }
        public CalidadDelContacto CalidadDelContactoFp2 { get { return calidadDelContactoFp2; } }
        public CalidadDelContacto CalidadDelContactoC3 { get { return calidadDelContactoC3; } }
        public CalidadDelContacto CalidadDelContactoC4 { get { return calidadDelContactoC4; } }
        public CalidadDelContacto CalidadDelContactoP7 { get { return calidadDelContactoP7; } }
        public CalidadDelContacto CalidadDelContactoP8 { get { return calidadDelContactoP8; } }
        public CalidadDelContacto CalidadDelContactoO1 { get { return calidadDelContactoO1; } }
        public CalidadDelContacto CalidadDelContactoO2 { get { return calidadDelContactoO2; } }


        // Dimension de la lista
        public Int32 LongitudDeLaLista      {   get { return NumerosDeTramas.Count; }   }


        // Indicador del error
        public string Error
        {
            get { return error; }
            set { error = value; }
        }


        // Limite de tramas a recibir, que se debe actualizar en funcion del tiempo del estudio mediante el metodo "ActualizarMaximoDeTramasARecibir"
        public int MaximoTramasARecibir    { get { return maximoTramasRecibidas; } }


        // Constante del tiempo entre tramas
        static public int TiempoEntreTramas {   get { return TiempoFijoEntreTramas; } }


        // Solo para pruebas
        public int BytesEnElBuffer()        {   return (puertoSerie.BytesToRead);   }





        /********************************************************************************************************************************************/
        /*                                                                 PRIVADOS                                                                 */
        /********************************************************************************************************************************************/



        /****************************************************************/
        /* LeerMensajeInicial                                           */
        /*   Se encarga de corroborar que llegue correctamente el       */
        /*   mensaje inicial luego de resetar el casco                  */
        /*                                                              */
        /* Recibe: Un indicador para mostrar o no el mensaje inicial    */
        /* Devuelve:                                                    */
        /*   True - Si se leyo correctamente                            */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        private bool LeerMensajeInicial(bool mostrarInformacion)
        {
            const int longitudMensajeInicial = 104;                     // Tiene un maximo de 104 caracteres
            byte[] mensajeInicial = new byte[longitudMensajeInicial];   // Variable para almacenar diche mensaje
            const byte largoCadenaAuxiliar = 3;                         // Se utiliza una variable auxiliar para ir leyendolo por partes
            byte[] cadenaAuxiliar = new byte[largoCadenaAuxiliar];      // Los bytes leidos correctamente en la cadena auxiliar luego se concatenan
            int bytesLeidos = 0, bytesTotalesLeidos = 0;
            byte letraF = 0x46;

            do          // Se debe leer el puerto hasta encontrar "$$$", que es la indicacion de fin de trama
            {
                try
                {
                    bytesLeidos = puertoSerie.Read(cadenaAuxiliar, 0, largoCadenaAuxiliar);           // Se lee una fraccion del mensaje
                    Array.Copy(cadenaAuxiliar, 0, mensajeInicial, bytesTotalesLeidos, bytesLeidos);   // Se agregan los caracteres leidos al mensaje total
                    bytesTotalesLeidos += bytesLeidos;
                    System.Threading.Thread.Sleep(1);
                }
                catch (TimeoutException)
                {
                    Error = "Se excedio el tiempo limite para la lectura de datos - LeerMensajeInicial";
                    return (false);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Error = "LLego un mensaje erroneo (IndexOutOfRangeException) - LeerMensajeInicial";
                    return (false);
                }

            } while (!(mensajeInicial[bytesTotalesLeidos - 1] == 0x24 && mensajeInicial[bytesTotalesLeidos - 2] == 0x24 && mensajeInicial[bytesTotalesLeidos - 3] == 0x24));

            if (mensajeInicial[0] == letraF)        // Mensaje = Failure: ...; Esta conectado el adaptador pero no se prendio el casco
            {
                Error = "El casco no esta encendido - LeerMensajeInicial";
                return (false);
            }

            if (mostrarInformacion == true)
            {
                System.Windows.Forms.MessageBox.Show(System.Text.Encoding.ASCII.GetString(mensajeInicial));
            }

            return (true);
        }



        /****************************************************************/
        /* VerificarLongitudTrama                                       */
        /*   Revisa la cantidad de bytes leidos. Si no se completo una  */
        /*   trama, realiza 5 intentos de hacerlo. Si no se puede, se   */
        /*   considera perdida la comunicacion                          */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si se completa la trama en menos de 5 intentos      */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        private bool VerificarLongitudTrama()
        {
            if (bytesLeidos == LongitudDeTrama)
            {
                // La trama se leyo correctamente
                return (true);
            }
            else
            {
                CantidadDeTramasIncompletas++;

                totalBytesLeidos = bytesLeidos;

                for (int indice = 0; indice < 5; indice++)
                {
                    try
                    {
                        bytesLeidos = puertoSerie.Read(tramaAuxiliar, 0, LongitudDeTrama - totalBytesLeidos);
                        Array.Copy(tramaAuxiliar, 0, trama, totalBytesLeidos, bytesLeidos);
                        totalBytesLeidos += bytesLeidos;
                        if (totalBytesLeidos == LongitudDeTrama)
                            return (true);
                    }
                    catch (TimeoutException)
                    {
                        Error = "Se excedio el tiempo limite para la lectura de datos - VerificarLongitudTrama";
                        return (false);
                    }
                }

                Error = "No se pudo completar la lectura de una trama - VerificarLongitudTrama";
                return (false);
            }
        }



        /****************************************************************/
        /* VerificarTrama                                               */
        /*   Corrobora la alineacion de la trama recibida               */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve:                                                    */
        /*   True - Si la trama es correcta o solo esta desfasada       */
        /*          Si esta desfasada, la variable desplazamientoTrama  */
        /*          se modifica para corregir la siguiente lectura      */
        /*   False - Si hay algun problema                              */
        /****************************************************************/
        private bool VerificarTrama()
        {
            desplazamientoTrama = 0;

            if (trama[HeaderInicialPosicion] != HeaderInicial || trama[HeaderFinalPosicion] != HeaderFinal)
            {
                CantidadDeTramasDesfasadas++;

                // Si los Headers no estan en su posicion original, la trama puede estar desfasada. Se debe buscar la secuencia: " HeaderFinal - HeaderInicial "
                for (int indice = 1; indice < LongitudDeTrama; indice++)
                    if (trama[indice] == HeaderInicial && trama[indice - 1] == HeaderFinal)
                    {
                        desplazamientoTrama = indice;       // Se encontro el desplazamiento que tuvo la trama y se usara en la proxima lectura
                        break;
                    }
                if (desplazamientoTrama == 0)
                {
                    // No se encontraron los Headers de forma correcta y la trama es invalida
                    Error = "No se encontraron los Headers en la trama - VerificarTrama";
                    return (false);
                }
            }

            // En este punto se tiene la trama alineada correctamente o se sabe el desplazamiento
            if ((trama[ContadorDeTramasPosicion + desplazamientoTrama] - NumeroTramaAnterior) != 1)
            {
                // El contador de trama difiere en mas de una unidad respecto de la anterior
                NumeroTramaAnterior = trama[ContadorDeTramasPosicion + desplazamientoTrama];

                if (NumeroTramaAnterior == 0)
                {
                    // Si el numero de la trama es cero, el contador simplemente se reinicio, no es un error y no estaba desfasada
                    return (true);
                }

                // Si llega aca, es porque el contador difiere y se perdieron tramas intermedias
                Error = "El contador de tramas sucesivas difiere en mas de una unidad. Se perdieron tramas - VerificarTrama";
                return (false);
            }

            // En este punto, tanto los Headers como el contador de tramas estan correctos y la trama es valida
            NumeroTramaAnterior = trama[ContadorDeTramasPosicion + desplazamientoTrama];
            return (true);

        }



        /****************************************************************/
        /* AlmacenarTrama                                               */
        /*   Agrega los datos recibidos a las listas                    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void AlmacenarTrama()
        {
            CanalUno.Add(ConvertirBytesEnInt(trama[CanalUno_ByteAlto], trama[CanalUno_ByteMedio], trama[CanalUno_ByteBajo], CANAL_1));
            CanalDos.Add(ConvertirBytesEnInt(trama[CanalDos_ByteAlto], trama[CanalDos_ByteMedio], trama[CanalDos_ByteBajo], CANAL_2));
            CanalTres.Add(ConvertirBytesEnInt(trama[CanalTres_ByteAlto], trama[CanalTres_ByteMedio], trama[CanalTres_ByteBajo], CANAL_3));
            CanalCuatro.Add(ConvertirBytesEnInt(trama[CanalCuatro_ByteAlto], trama[CanalCuatro_ByteMedio], trama[CanalCuatro_ByteBajo], CANAL_4));
            CanalCinco.Add(ConvertirBytesEnInt(trama[CanalCinco_ByteAlto], trama[CanalCinco_ByteMedio], trama[CanalCinco_ByteBajo], CANAL_5));
            CanalSeis.Add(ConvertirBytesEnInt(trama[CanalSeis_ByteAlto], trama[CanalSeis_ByteMedio], trama[CanalSeis_ByteBajo], CANAL_6));
            CanalSiete.Add(ConvertirBytesEnInt(trama[CanalSiete_ByteAlto], trama[CanalSiete_ByteMedio], trama[CanalSiete_ByteBajo], CANAL_7));
            CanalOcho.Add(ConvertirBytesEnInt(trama[CanalOcho_ByteAlto], trama[CanalOcho_ByteMedio], trama[CanalOcho_ByteBajo], CANAL_8));
            NumerosDeTramas.Add(trama[ContadorDeTramasPosicion]);
        }



        /****************************************************************/
        /* ConvertirBytesEnInt                                          */
        /*   Desempaqueta los 3 bytes de informacion de la trama        */
        /*                                                              */
        /* Recibe: Los 3 bytes de informacion                           */
        /* Devuelve: Un Int32 con el dato desempaquetado                */
        /****************************************************************/
        private Int32 ConvertirBytesEnInt(byte ParteAlta, byte ParteMedia, byte ParteBaja, Int32 canal)
        {
            Int32 Dato, Auxiliar;
            Int32 ganancia;

            Dato = ParteAlta << 16;
            Auxiliar = ParteMedia << 8;
            Dato += Auxiliar;
            Auxiliar = ParteBaja;
            Dato += Auxiliar;

            if ((Dato & 0x00800000) > 0)
            {
                Auxiliar = 0xFF << 24;
                Dato = Dato | Auxiliar;
            }

            else
            {
                Auxiliar = Convert.ToInt32(0x00FFFFFF);
                Dato = Dato & Auxiliar;
            }

            canal--;        // Esto es porque las listas arrancan desde cero y el numero del canal arranca en 1
            switch (configuracionGanancia[canal])
            {
                case GANANCIA_1:
                    ganancia = 1;
                    break;
                case GANANCIA_2:
                    ganancia = 2;
                    break;
                case GANANCIA_4:
                    ganancia = 4;
                    break;
                case GANANCIA_6:
                    ganancia = 6;
                    break;
                case GANANCIA_8:
                    ganancia = 8;
                    break;
                case GANANCIA_12:
                    ganancia = 12;
                    break;
                case GANANCIA_24:
                default:
                    ganancia = 24;
                    break;
            }
            ConstanteDeEscala = (1000000 * 4.5) / ( ganancia * ( Math.Pow(2, 23) - 1 ) );
            Dato = Convert.ToInt32(Dato * ConstanteDeEscala);

            return (Dato);
        }



        /****************************************************************/
        /* VerificarLaCalidadDeLosContactos                             */
        /*   Verifica la calidad de los contactos segun su impedancia   */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void VerificarLaCalidadDeLosContactos()
        {
            // Contacto 1: Fp1
            if (impedanciaContactoFp1 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoFp1 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoFp1 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoFp1 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoFp1 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 2: Fp2
            if (impedanciaContactoFp2 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoFp2 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoFp2 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoFp2 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoFp2 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 3: C3
            if (impedanciaContactoC3 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoC3 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoC3 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoC3 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoC3 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 4: C4
            if (impedanciaContactoC4 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoC4 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoC4 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoC4 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoC4 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 5: P7
            if (impedanciaContactoP7 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoP7 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoP7 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoP7 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoP7 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 6: P8
            if (impedanciaContactoP8 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoP8 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoP8 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoP8 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoP8 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 7: O1
            if (impedanciaContactoO1 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoO1 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoO1 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoO1 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoO1 = CalidadDelContacto.CONTACTO_REGULAR;

            // Contacto 8: O2
            if (impedanciaContactoO2 > LIMITE_IMPEDANCIA_CONTACTO_DESCONECTADO)
                calidadDelContactoO2 = CalidadDelContacto.CONTACTO_MALO;
            else
                if (impedanciaContactoO2 < LIMITE_IMPEDANCIA_CONTACTO_BIEN)
                    calidadDelContactoO2 = CalidadDelContacto.CONTACTO_BUENO;
                else
                    calidadDelContactoO2 = CalidadDelContacto.CONTACTO_REGULAR;

        }



        /****************************************************************/
        /* DuplicarUltimoValor                                          */
        /*   Replica el ultimo valor de todas las listas                */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void DuplicarUltimoValor()
        {
            // Se copian los datos en los canales activos
            if (configuracionEncendido[CANAL_1 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_1);
            if (configuracionEncendido[CANAL_2 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_2);
            if (configuracionEncendido[CANAL_3 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_3);
            if (configuracionEncendido[CANAL_4 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_4);
            if (configuracionEncendido[CANAL_5 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_5);
            if (configuracionEncendido[CANAL_6 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_6);
            if (configuracionEncendido[CANAL_7 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_7);
            if (configuracionEncendido[CANAL_8 - 1] == CANAL_PRENDIDO)
                DuplicarUltimoValorEnCanal(CANAL_8);

            // Luego se actualiza tambien el contador de tramas. Solo se deberia hacer si hay almenos un canal activo, pero esto no va a pasar
            NumerosDeTramas.Add(NumerosDeTramas[NumerosDeTramas.Count - 1]);
        }



        /****************************************************************/
        /* DuplicarUltimoValorEnCanal                                   */
        /*   Agrega un nuevo elemento a la lista de datos, copiando el  */
        /*   valor del ultimo elemento                                  */
        /*                                                              */
        /* Recibe: El numero de canal                                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void DuplicarUltimoValorEnCanal(int canal)
        {
            switch (canal)
            {
                case CANAL_1:
                    CanalUno.Add( CanalUno[CanalUno.Count-1] );
                    return;
                case CANAL_2:
                    CanalDos.Add( CanalDos[CanalDos.Count-1] );
                    return;
                case CANAL_3:
                    CanalTres.Add( CanalTres[CanalTres.Count-1] );
                    return;
                case CANAL_4:
                    CanalCuatro.Add( CanalCuatro[CanalCuatro.Count-1]);
                    return;
                case CANAL_5:
                    CanalCinco.Add( CanalCinco[CanalCinco.Count-1] );
                    return;
                case CANAL_6:
                    CanalSeis.Add( CanalSeis[CanalSeis.Count-1] );
                    return;
                case CANAL_7:
                    CanalSiete.Add( CanalSiete[CanalSiete.Count-1] );
                    return;
                case CANAL_8:
                    CanalOcho.Add( CanalOcho[CanalOcho.Count-1] );
                    return;
            }
        }



        /****************************************************************/
        /* IndicarCanalEncendido                                        */
        /*   Activa el estado del indicador de encendido del canal      */
        /*                                                              */
        /* Recibe: El numero de canal que se encendio                   */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void IndicarCanalEncendido(int canal)
        {
            switch (canal)
            {
                case CANAL_1:
                    configuracionEncendido[CANAL_1 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_2:
                    configuracionEncendido[CANAL_2 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_3:
                    configuracionEncendido[CANAL_3 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_4:
                    configuracionEncendido[CANAL_4 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_5:
                    configuracionEncendido[CANAL_5 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_6:
                    configuracionEncendido[CANAL_6 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_7:
                    configuracionEncendido[CANAL_7 - 1] = CANAL_PRENDIDO;
                    break;
                case CANAL_8:
                    configuracionEncendido[CANAL_8 - 1] = CANAL_PRENDIDO;
                    break;
            }
        }



        /****************************************************************/
        /* IndicarCanalApagado                                          */
        /*   Desactiva el estado del indicador de encendido del canal   */
        /*                                                              */
        /* Recibe: El numero de canal que se apago                      */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void IndicarCanalApagado(int canal)
        {
            switch (canal)
            {
                case CANAL_1:
                    configuracionEncendido[CANAL_1 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_2:
                    configuracionEncendido[CANAL_2 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_3:
                    configuracionEncendido[CANAL_3 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_4:
                    configuracionEncendido[CANAL_4 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_5:
                    configuracionEncendido[CANAL_5 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_6:
                    configuracionEncendido[CANAL_6 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_7:
                    configuracionEncendido[CANAL_7 - 1] = CANAL_APAGADO;
                    break;
                case CANAL_8:
                    configuracionEncendido[CANAL_8 - 1] = CANAL_APAGADO;
                    break;
            }
        }



        /****************************************************************/
        /* EliminarPrimerMuestraNula                                    */
        /*   Si el canal esta encendido, elimina las primeras muestras  */
        /*   nulas                                                      */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void EliminarPrimerMuestraNula()
        {
            // Si el canal esta encendido, se elimina la primer muestra nula

            if (configuracionEncendido[CANAL_1 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalUno );
            if (configuracionEncendido[CANAL_2 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalDos );
            if (configuracionEncendido[CANAL_3 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalTres );
            if (configuracionEncendido[CANAL_4 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalCuatro );
            if (configuracionEncendido[CANAL_5 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalCinco );
            if (configuracionEncendido[CANAL_6 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalSeis );
            if (configuracionEncendido[CANAL_7 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalSiete );
            if (configuracionEncendido[CANAL_8 - 1] == CANAL_PRENDIDO)
                EliminarPrimerMuestraNulaEnCanal( CanalOcho );
        }



        /****************************************************************/
        /* EliminarPrimerMuestraNulaEnCanal                             */
        /*   Verifica cual es la primer muestra no nula y la replica en */
        /*   las anteriores                                             */
        /*                                                              */
        /* Recibe: La lista de datos del canal a verificar              */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        private void EliminarPrimerMuestraNulaEnCanal(List<Int32> ListaFuente)
        {
            Int32 indice;

            // Se recorre la lista desde el inicio hasta la primer muestra no nula
            for (indice = 0; indice < ListaFuente.Count; indice++)
                if (ListaFuente[indice] != 0)
                    break;

            // Si el indice es 0, entonces no hay muestras nulas y no hay que hacer nada
            if (indice == 0)
                return;

            // Para copiar los valores, se usa el mismo valor del indice con un recorrido en el sentido inverso
            for (; indice > 0; indice--)
                ListaFuente[indice - 1] = ListaFuente[indice];

        }




    }
}