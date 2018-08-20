using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPaper
{
    class ConfiguracionEstimulacion
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                    VARIABLES Y CONSTANTES                                                                */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        /***    VARIABLES TEMPORALES    ***/

        private Int32 tiempoEstudioSegundos;
        private Int32 tiempoEstudioMiliSegundos;



        /***    VARIABLES DE CONTROL    ***/

        private Int32 cantidadOpcionesActuales;
        private string simboloElegido;
        private List<string> textosBotones;
        private string textoOpcionElegida;

        public enum tiposDeEstudios { PARPADEO_SECUENCIAL, PARPADEO_FRECUENCIAL };
        private tiposDeEstudios tipoDeEstudioActual;


        /***    VARIABLES VISUALES    ***/

        public enum estadosPantalla : byte { Minimizada, Normal, Maximizada };

        private byte filasTotales;
        private string colorDeFondo;
        private string colorExcitacion;
        private string colorDescanso;
        private string tamanioLetra;
        private string[] tamaniosDeLetra = { "Muy grande", "Grande", "Media", "Chica", "Muy chica" };
        private Int32 escalaLetra;
        private estadosPantalla pantallaActual;
        private estadosPantalla pantallaAnterior;
        private string tamanioPantalla;



        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        // Constructor por default, sin parametros
        public ConfiguracionEstimulacion()
        {
            // Inicializacion de las variables
            tiempoEstudioSegundos = 10;
            tiempoEstudioMiliSegundos = tiempoEstudioSegundos * 1000;
            cantidadOpcionesActuales = 2;
            filasTotales = 1;
            colorDeFondo = "Black";
            colorExcitacion = "White";
            colorDescanso = "Gray";
            tamanioLetra = "Grande";
            ActualizarEscalaDeLetra();
            pantallaActual = estadosPantalla.Normal;
            pantallaAnterior = estadosPantalla.Normal;
            tamanioPantalla = "Normal";
            tipoDeEstudioActual = tiposDeEstudios.PARPADEO_SECUENCIAL;
            BorrarTextosBotones();
            AgregarTextoBoton( "SI" );
            AgregarTextoBoton( "NO" );
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
        /* ActualizarEscalaDeLetra                                      */
        /*   Calcula el nuevo tamanio de las letras                     */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void ActualizarEscalaDeLetra()
        {
            escalaLetra = (Array.IndexOf(tamaniosDeLetra, tamanioLetra) + 1) * 3;
        }



        /****************************************************************/
        /* TextoBoton                                                   */
        /*   Devuelve el texto a colocar en el boton pedido             */
        /*                                                              */
        /* Recibe: La ubicacion del texto en la lista                   */
        /* Devuelve: El texto correspondiente                           */
        /****************************************************************/
        public string TextoBoton(int Numero)
        {
            if( Numero < 0 )
                return ("");
            else
                return textosBotones[Numero];
        }


        public void BorrarTextosBotones()
        {
            textosBotones = new List<string>();
        }

        public void AgregarTextoBoton(string nuevoTexto)
        {
            textosBotones.Add(nuevoTexto);
        }





        /***    METODOS "SET" Y "GET" PARA UTILIZAR LAS VARIABLES INTERNAS DEL OBJETO    ***/

        public string TamanioLetra
        {
            get { return tamanioLetra; }
            set { tamanioLetra = value; }
        }

        public string ColorDeFondo
        {
            get { return colorDeFondo; }
            set { colorDeFondo = value; }
        }

        public string ColorExcitacion
        {
            get { return colorExcitacion; }
            set { colorExcitacion = value; }
        }

        public string ColorDescanso
        {
            get { return colorDescanso; }
            set { colorDescanso = value; }
        }

        public byte Filas
        {
            get { return filasTotales; }
            set { filasTotales = value; }
        }

        public Int32 CantidadOpciones
        {
            get { return cantidadOpcionesActuales; }
            set { cantidadOpcionesActuales = value; }
        }

        public Int32 EscalaDeLetra
        {
            get { return escalaLetra; }
            set { escalaLetra = value; }
        }

        public estadosPantalla PantallaActual
        {
            get { return pantallaActual; }
            set { pantallaActual = value; }
        }

        public estadosPantalla PantallaAnterior
        {
            get { return pantallaAnterior; }
            set { pantallaAnterior = value; }
        }

        public string TamanioPantalla
        {
            get { return tamanioPantalla; }
            set { tamanioPantalla = value; }
        }

        public Int32 TiempoEstudioSegundos
        {
            get { return tiempoEstudioSegundos; }
            set { tiempoEstudioSegundos = value; tiempoEstudioMiliSegundos = tiempoEstudioSegundos * 1000; }
        }

        public Int32 TiempoEstudioMiliSegundos
        {
            get { return tiempoEstudioMiliSegundos; }
            set { tiempoEstudioMiliSegundos = value; }
        }

        public string SimboloElegido
        {
            get { return simboloElegido; }
            set { simboloElegido = value; }
        }

        public string TextoOpcionElegida
        {
            get { return textoOpcionElegida; }
            set { textoOpcionElegida = value; }
        }

        public tiposDeEstudios TipoDeEstudio
        {
            get { return tipoDeEstudioActual; }
            set { tipoDeEstudioActual = value; }
        }

        public List<string> TextosBotones
        {
            get { return textosBotones; }
            set { textosBotones = value; }
        }

        public string TipoDeEstudioTexto
        {
            get
            {
                if(tipoDeEstudioActual == tiposDeEstudios.PARPADEO_SECUENCIAL)
                    return ("Secuencial");
                return ("Frecuencial");
            }
        }



        /********************************************************************************************************************************************/
        /*                                                                 PRIVADOS                                                                 */
        /********************************************************************************************************************************************/


    }
}
