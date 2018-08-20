using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace AplicacionPaper
{
    class EstimulacionSecuencial
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                    VARIABLES Y CONSTANTES                                                                */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        // Vector para almacenar las posiciones de los marcadores
        private byte[] markersPosiciones;

        // Marcas para seccionar los datos
        private byte marcaInicialEstudio = 0x40;                    // Marca de inicio del estudio      =>  @ = 0x40 = 64
        private byte marcaFinalEstudio = 0x24;                      // Marca de fin del estudio         =>  $ = 0x24 = 64
        private byte[] marcasFinalesDosOpciones = { 0x26, 0x21 };   // Marca de fin del estimulo 1      =>  & = 0x26 = 38 // Marca de fin del estimulo 2      =>  ! = 0x21 = 33
        private byte[] marcasInicialesDosOpciones = { 0x25, 0x23 }; // Marca de inicio del estimulo 1   =>  % = 0x25 = 37 // Marca de inicio del estimulo 2   =>  # = 0x23 = 35
        private byte[] marcasFinalesSeisOpciones = { 0, 0, 0, 0, 0, 0 };
        private byte[] marcasInicialesSeisOpciones = { 0x41, 0x42, 0x43, 0x44, 0x45, 0x46 };   // Son los caracteres desde A hasta F

        // Indicador de la cantidad de opciones en uso
        private Int32 cantidadOpcionesActuales;

        // Indicador del estimulo actual en ejecucion
        private Int32 estimuloActual;

        // Variables para almacenar los tiempos del estudio
        private Int32 tiempoDeDescanso;
        private Int32 tiempoDeExcitacion;
        private Int32 tiempoEstudioSegundos;
        private Int32 tiempoEstudioMiliSegundos;
        private Int32 tiempoAcumulado;

        // Variable para indicar si se modifico alguno de los parametros temporales o la cantidad de opciones
        private bool cambioTemporalUOpciones;
        
        // Variables para reordenar la secuencia de estimulaciones
        public enum tiposDeSecuencia { SECUENCIAL, ALEATORIA, CAOS }
        private tiposDeSecuencia secuenciaActual;
        private Int32 longitudDeSecuencia;
        private Int32 repeticionesMaximasPorSimbolo;
        private Int32[] contadorRepeticiones;
        private List<Int32> lista;
        private List<Int32> secuenciaFinal;
        private string textoSecuenciaElegida;
        private Random aleatorio;

        // Objeto "stopwatch" para medir los tiempos reales de ejecucion
        private Stopwatch reloj = new Stopwatch();
        private List<Int32> milisegundosConsumidos;



        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        // Constructor por default, sin parametros
        public EstimulacionSecuencial()
        {
            tiempoEstudioSegundos = 10;
            tiempoEstudioMiliSegundos = tiempoEstudioSegundos * 1000;
            tiempoDeDescanso = 800;
            tiempoDeExcitacion = 200;
            cantidadOpcionesActuales = 2;
            cambioTemporalUOpciones = true;
            VerificarCambiosEnParametros();
            aleatorio = new Random();
            TextoTipoDeSecuenciaActual = "Aleatoria";
        }

        



        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                           MÉTODOS Y PROPIEDADES                                                          */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/



        /********************************************************************************************************************************************/
        /*                                                                 PUBLICOS                                                                 */
        /********************************************************************************************************************************************/

        /****************************************************************/
        /* ReordenarSecuencia                                           */
        /*   Genera una secuencia aleatoria en funcion de la cual luego */
        /*   se iran ilumianando los simbolos                           */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void ReordenarSecuencia()
        {
            // Se genera un vector con la secuencia total 1 vez y después se lo va seccionando
            for (int indiceAuxiliar = 0; indiceAuxiliar < longitudDeSecuencia; indiceAuxiliar++)
            {
                switch (secuenciaActual)
                {
                    case tiposDeSecuencia.SECUENCIAL:

                        secuenciaFinal.Add(indiceAuxiliar % cantidadOpcionesActuales);
                        break;

                    case tiposDeSecuencia.CAOS:
                        while (true)
                        {
                            int valorAleatorio = aleatorio.Next(0, cantidadOpcionesActuales);                  // Se genera un número entre 0 (inclusivo) y 6 (no inclusivo)
                            if (contadorRepeticiones[valorAleatorio] < repeticionesMaximasPorSimbolo)      // Si no llegó al límite de repeticiones por símbolo
                            {
                                secuenciaFinal.Add(valorAleatorio);                                         // Se lo agrega a la lista. Equivale a secuenciaFinal[ indiceAuxiliar ] = valorAleatorio
                                contadorRepeticiones[valorAleatorio]++;                                     // Se incrementa el contador de repeticiones por símbolo
                                break;
                            }
                        }
                        break;

                    case tiposDeSecuencia.ALEATORIA:
                        if ((indiceAuxiliar % cantidadOpcionesActuales) == 0)           // Cada vez que se completa una rueda, se borra la lista
                            lista.Clear();
                        while (true)
                        {
                            int valorAleatorio = aleatorio.Next(0, cantidadOpcionesActuales);
                            if (!lista.Contains(valorAleatorio))
                            {
                                lista.Add(valorAleatorio);
                                secuenciaFinal.Add(valorAleatorio);
                                break;
                            }
                        }
                        break;
                }
            }
        }



        /****************************************************************/
        /* SiguienteEstimulo                                            */
        /*   Actualiza la variable que representa el estimulo actual    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void SiguienteEstimulo()
        {
            estimuloActual++;
        }



        /****************************************************************/
        /* IniciarNuevaSegmentacion                                     */
        /*   Coloca la marca de inicio del estudio, reinicia las        */
        /*   variables de control interno y reordena la secuencia       */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void IniciarNuevaSegmentacion()
        {
            // Se revisa si se cambio algun parametro importante y hay que reconfigurar algunas variables
            VerificarCambiosEnParametros();

            // Colocar las marcas de inicio y fin de estudio
            markersPosiciones[0] = marcaInicialEstudio;
            tiempoAcumulado = 0;
            estimuloActual = 0;
            secuenciaFinal = new List<Int32>();

            milisegundosConsumidos = new List<Int32>();
            reloj.Reset();
            reloj.Start();

            ReordenarSecuencia();
        }



        /****************************************************************/
        /* FinalizarSegmentacion                                        */
        /*   Coloca la marca de fin del estudio                         */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void FinalizarSegmentacion()
        {
            markersPosiciones[markersPosiciones.Length - 1] = marcaFinalEstudio;
            reloj.Stop();
        }



        /****************************************************************/
        /* ColocarMarcaInicialDeEstimulo                                */
        /*   Calcula la posicion en el vector de marcas en funcion del  */
        /*   tiempo acumulado, inserta la correspondiente y actualiza   */
        /*   el tiempo acumulado, ya que se da por sentado que lo       */
        /*   proximo que haga la aplicacion sera un sleep(tiempoExc)    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void ColocarMarcaInicialDeEstimulo()
        {
            switch (cantidadOpcionesActuales)
            {
                case 2:
                    markersPosiciones[(tiempoAcumulado / DatosCasco.TiempoEntreTramas) + 1] = marcasInicialesDosOpciones[EstimuloActual];
                    break;
                case 6:
                    markersPosiciones[(tiempoAcumulado / DatosCasco.TiempoEntreTramas) + 1] = marcasInicialesSeisOpciones[EstimuloActual];
                    break;
            }
            tiempoAcumulado += tiempoDeExcitacion;
            milisegundosConsumidos.Add((Int32)reloj.ElapsedMilliseconds);
        }



        /****************************************************************/
        /* ColocarMarcaFinalDeEstimulo                                  */
        /*   Calcula la posicion en el vector de marcas en funcion del  */
        /*   tiempo acumulado, inserta la correspondiente y actualiza   */
        /*   el tiempo acumulado, ya que se da por sentado que lo       */
        /*   proximo que haga la aplicacion sera un sleep(tiempoDes)    */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void ColocarMarcaFinalDeEstimulo()
        {
            switch (cantidadOpcionesActuales)
            {
                case 2:
                    markersPosiciones[(tiempoAcumulado / DatosCasco.TiempoEntreTramas) + 1] = marcasFinalesDosOpciones[EstimuloActual];
                    break;
                case 6:
                    markersPosiciones[(tiempoAcumulado / DatosCasco.TiempoEntreTramas) + 1] = marcasFinalesSeisOpciones[EstimuloActual];
                    break;
            }
            tiempoAcumulado += tiempoDeDescanso;
            milisegundosConsumidos.Add((Int32)reloj.ElapsedMilliseconds);
        }





        /***    METODOS "SET" Y "GET" PARA UTILIZAR LAS VARIABLES INTERNAS DEL OBJETO    ***/

        public Int32 CantidadOpciones
        {
            get { return cantidadOpcionesActuales; }
            set { cantidadOpcionesActuales = value; cambioTemporalUOpciones = true; VerificarCambiosEnParametros(); }
        }

        public Int32 EstimuloActual                 { get { return secuenciaFinal[ estimuloActual ]; } }

        public Int32 TiempoDeDescansoThread         { get { return( tiempoAcumulado - (Int32) reloj.ElapsedMilliseconds); } }

        public Int32 TiempoDeExcitacionThread       { get { return( tiempoAcumulado - (Int32) reloj.ElapsedMilliseconds); } }

        public Int32 TiempoDeDescanso
        {
            get { return tiempoDeDescanso; }
            set { tiempoDeDescanso = value; cambioTemporalUOpciones = true; }
        }

        public Int32 TiempoDeExcitacion
        {
            get { return tiempoDeExcitacion; }
            set { tiempoDeExcitacion = value; cambioTemporalUOpciones = true; }
        }

        public Int32 TiempoEstudioSegundos
        {
            get { return tiempoEstudioSegundos; }
            set { tiempoEstudioSegundos = value; cambioTemporalUOpciones = true; VerificarCambiosEnParametros(); }
        }

        public byte[] MarkersPosiciones             { get { return markersPosiciones; } }

        public Int32 TotalDeSecuencias              { get { return (Int32) CalcularTotalDeRuedas(); } }

        public Int32 MaximoTramasRecibidas          { get { return( markersPosiciones.Length - 2 ); } }

        public tiposDeSecuencia TipoDeSecuenciaActual
        {
            get { return secuenciaActual; }
            set
            {
                switch (value)
                {
                    case tiposDeSecuencia.SECUENCIAL:
                        secuenciaActual = tiposDeSecuencia.SECUENCIAL;
                        textoSecuenciaElegida = "Secuencial";
                        break;
                    case tiposDeSecuencia.CAOS:
                        secuenciaActual = tiposDeSecuencia.CAOS;
                        textoSecuenciaElegida = "Caos";
                        break;
                    case tiposDeSecuencia.ALEATORIA:
                    default:
                        secuenciaActual = tiposDeSecuencia.ALEATORIA;
                        textoSecuenciaElegida = "Aleatoria";
                        break;
                }
            }
        }

        public string TextoTipoDeSecuenciaActual
        {
            get { return (textoSecuenciaElegida); }
            set
            {
                switch (value)
                {
                    case "Secuencial":
                        secuenciaActual = tiposDeSecuencia.SECUENCIAL;
                        textoSecuenciaElegida = "Secuencial";
                        break;
                    case "Caos":
                        secuenciaActual = tiposDeSecuencia.CAOS;
                        textoSecuenciaElegida = "Caos";
                        break;
                    case "Aleatoria":
                    default:
                        secuenciaActual = tiposDeSecuencia.ALEATORIA;
                        textoSecuenciaElegida = "Aleatoria";
                        break;
                }
            }
        }
        


        /********************************************************************************************************************************************/
        /*                                                                 PRIVADOS                                                                 */
        /********************************************************************************************************************************************/

        private double CalcularTotalDeRuedas() { return (( (double) tiempoEstudioMiliSegundos) / MiliSegundosPorRueda); }

        private Int32 MiliSegundosPorSimbolo { get { return (tiempoDeDescanso + tiempoDeExcitacion); } }

        private Int32 MiliSegundosPorRueda { get { return (MiliSegundosPorSimbolo * cantidadOpcionesActuales); } }

        private void VerificarCambiosEnParametros()
        {
            if ( cambioTemporalUOpciones == false )
                return;         // No hubo ningun cambio temporal o en la cantidad de opciones, asi que las variables no se tienen que recalcular

            // Si alguno se modifico, hay que recalcular la dimension de las variables y borrar el flag indicador
            cambioTemporalUOpciones = false;

            // Primero se calcula el nuevo tiempo del estudio, para que sea multiplo del total de segundos por rueda
            double totalDeRuedas;
            totalDeRuedas = Math.Ceiling( CalcularTotalDeRuedas() );
            tiempoEstudioMiliSegundos = ((int)totalDeRuedas) * MiliSegundosPorRueda;
            tiempoEstudioSegundos = tiempoEstudioMiliSegundos / 1000;

            contadorRepeticiones = new Int32[ CantidadOpciones ];
            longitudDeSecuencia = tiempoEstudioMiliSegundos / MiliSegundosPorSimbolo;
            repeticionesMaximasPorSimbolo = longitudDeSecuencia / cantidadOpcionesActuales;
            lista = new List<Int32>();
            secuenciaFinal = new List<Int32>();
            markersPosiciones = new byte[ ( tiempoEstudioMiliSegundos / DatosCasco.TiempoEntreTramas ) + 2 ];

            return;
        }


    }
}
