using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AplicacionPaper
{
    public class EstimulacionFrecuencial
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                    VARIABLES Y CONSTANTES                                                                */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        
        // Indicador de la cantidad de opciones en uso
        private Int32 cantidadOpcionesActuales;

        // Variable para almacenar el tiempo total del estudio
        private Int32 tiempoDelEstudioEnSegundos;
        private Int32 tiempoDelEstudioEnMiliSegundos;


        private List<Thread> listaDeSubThreads;
        private List<Int32>  listaDePeriodos;
        private List<string> listaDeNombresDeBotones;



        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        // Constructor por default, sin parametros
        public EstimulacionFrecuencial()
        {
            BorrarDatos();
            TiempoDelEstudioEnSegundos = 10;
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
        /* BorrarDatos                                                  */
        /*   Limpia todos los datos previos                             */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: Nada                                               */
        /****************************************************************/
        public void BorrarDatos()
        {
            listaDeSubThreads = new List<Thread>();
            listaDeNombresDeBotones = new List<string>();
        }



        /****************************************************************/
        /* LanzarTodosLosSubThreads                                     */
        /*   Pone en ejecucion todos los subthreads agregados           */
        /*   previamente en la lista. Si no hay ninguno, se indica un   */
        /*   error                                                      */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: False, si no hay subthreads agregados              */
        /****************************************************************/
        public bool LanzarTodosLosSubThreads()
        {
            // Si la lista esta vacia, se indica un error
            if ( listaDeSubThreads.Count == 0 ) { return (false); }

            // Caso contrario, se lanzan a la ejecucion a todos los subthreads que se hayan agregado
            for (Int32 indice = 0; indice < listaDeSubThreads.Count; indice++)
                { listaDeSubThreads[indice].Start(indice); }
            return (true);
        }



        /****************************************************************/
        /* TerminarTodosLosSubThreads                                   */
        /*   Cancela todos los subthreads en ejecucion. Si no hay       */
        /*   ninguno, se indica un error                                */
        /*                                                              */
        /* Recibe: Nada                                                 */
        /* Devuelve: False, si no hay subthreads en ejecucion           */
        /****************************************************************/
        public bool TerminarTodosLosSubThreads()
        {
            // Si la lista esta vacia, se indica un error
            if ( listaDeSubThreads.Count == 0 ) { return (false); }

            // Caso contrario, se abortan todos los threads activos capturando la excepcion generada
            for (Int32 indice = 0; indice < listaDeSubThreads.Count; indice++)
            {
                try { listaDeSubThreads[indice].Abort(); }
                catch (ThreadAbortException) { }
            }

            // Al cancelar todos los threads, se limpia la lista. Se la debe regenerar para hacer otro ensayo
            listaDeSubThreads.Clear();
            return (true);
        }



        /****************************************************************/
        /* NombreDelBoton                                               */
        /*   Devuelve el nombre del boton solicitado en la lista        */
        /*                                                              */
        /* Recibe: Posicion en la lista del dato buscado                */
        /* Devuelve: El nombre en forma de string                       */
        /****************************************************************/
        public string NombreDelBoton(Int32 numeroDelBoton)
        {
            return ( listaDeNombresDeBotones[numeroDelBoton] );
        }



        /****************************************************************/
        /* PeriodoDelBoton                                              */
        /*   Devuelve el periodo del boton solicitado en la lista       */
        /*                                                              */
        /* Recibe: Posicion en la lista del dato buscado                */
        /* Devuelve: El periodo de dicho boton                          */
        /****************************************************************/
        public Int32 PeriodoDelBoton(Int32 numeroDelBoton)
        {
            return ( listaDePeriodos[numeroDelBoton] );
        }



        /****************************************************************/
        /* AgregarSubThread                                             */
        /*   Inserta el subThread suministrado en la lista de control   */
        /*                                                              */
        /* Recibe: El subThread para agregar                            */
        /* Devuelve: False, si el subThread pasado es nulo              */
        /****************************************************************/
        public bool AgregarSubThread( System.Threading.Thread subThread )
        {
            // Si el argumento suministrado es nulo, se indica un error
            if( subThread == null )
                return(false);

            // De lo contrario, se lo agrega a la lista de subThreads
            listaDeSubThreads.Add(subThread);
            return(true);
        }



        /****************************************************************/
        /* AgregarNombre                                                */
        /*   Inserta el nombre suministrado en la lista de control      */
        /*                                                              */
        /* Recibe: El nombre para agregar                               */
        /* Devuelve: False, si el nombre pasado es nulo                 */
        /****************************************************************/
        public bool AgregarNombre( string nombre )
        {
            // Si el argumento suministrado es nulo, se indica un error
            if( nombre.Length == 0 )
                return(false);

            // De lo contrario, se lo agrega a la lista de nombres
            listaDeNombresDeBotones.Add( nombre );
            return(true);
        }



        /****************************************************************/
        /* AgregarPeriodo                                               */
        /*   Inserta el periodo suministrado en la lista de control     */
        /*                                                              */
        /* Recibe: El valor del periodo para agregar                    */
        /* Devuelve: False, si el periodo pasado es nulo  negativo      */
        /****************************************************************/
        public bool AgregarPeriodo( Int32 periodo )
        {
            // Si el argumento suministrado es nulo o negativo, se indica un error
            if(periodo <= 0)
                return(false);

            // De lo contrario, se lo agrega a la lista de periodos
            listaDePeriodos.Add(periodo);
            return(true);
        }





        /***    METODOS "SET" Y "GET" PARA UTILIZAR LAS VARIABLES INTERNAS DEL OBJETO    ***/

        public Int32 TiempoDelEstudioEnSegundos
        {
            get { return tiempoDelEstudioEnSegundos; }
            set { tiempoDelEstudioEnSegundos = value; tiempoDelEstudioEnMiliSegundos = value * 1000; }
        }

        public Int32 TiempoDelEstudioEnMiliSegundos
        {
            get { return tiempoDelEstudioEnMiliSegundos; }
        }

        public Int32 CantidadDeOpciones
        {
            get { return cantidadOpcionesActuales; }
            set { cantidadOpcionesActuales = value; }
        }
        public List<Int32> ListaDePeriodos
        {
            get { return listaDePeriodos; }
            set { listaDePeriodos = value; }
        }




        /********************************************************************************************************************************************/
        /*                                                                 PRIVADOS                                                                 */
        /********************************************************************************************************************************************/




    }
}
