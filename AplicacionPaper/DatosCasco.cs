using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionPaper
{
    class DatosCasco
    {
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/
        /*                                                          VARIABLES                                                                       */
        /********************************************************************************************************************************************/
        /********************************************************************************************************************************************/

        /*  Descripcion de la trama: Consta de 33 bytes, que se ordenan de la siguiente forma
         *      Byte 0:         Header inicial  =>  0xA0
         *      Byte 1:         Contador de trama
         *      Bytes 2-4:      Canal 0
         *      Bytes 5-7:      Canal 1
         *      Bytes 8-10:     Canal 2
         *      Bytes 11-13:    Canal 3
         *      Bytes 14-16:    Canal 4
         *      Bytes 17-19:    Canal 5
         *      Bytes 20-22:    Canal 6
         *      Bytes 23-25:    Canal 7
         *      Bytes 26-27:    Acelecrómetro eje X
         *      Bytes 28-29:    Acelecrómetro eje Y
         *      Bytes 30-31:    Acelecrómetro eje Z
         *      Byte 0:         Header final  =>  0xC0
        */

        // Constantes para indicar la posicion de cada byte dentro de la trama recepcionada
        // Header inicial
        private const int HeaderInicialPosicion = 0;
        private const byte HeaderInicial = 0xA0;
        // Contador de tramas
        private const int ContadorDeTramasPosicion = 1;
        // Canal 0
        private const int CanalCero_ByteAlto = 2;
        private const int CanalCero_ByteMedio = 3;
        private const int CanalCero_ByteBajo = 4;
        // Canal 1
        private const int CanalUno_ByteAlto = 5;
        private const int CanalUno_ByteMedio = 6;
        private const int CanalUno_ByteBajo = 7;
        // Canal 2
        private const int CanalDos_ByteAlto = 8;
        private const int CanalDos_ByteMedio = 9;
        private const int CanalDos_ByteBajo = 10;
        // Canal 3
        private const int CanalTres_ByteAlto = 11;
        private const int CanalTres_ByteMedio = 12;
        private const int CanalTres_ByteBajo = 13;
        // Canal 4
        private const int CanalCuatro_ByteAlto = 14;
        private const int CanalCuatro_ByteMedio = 15;
        private const int CanalCuatro_ByteBajo = 16;
        // Canal 5
        private const int CanalCinco_ByteAlto = 17;
        private const int CanalCinco_ByteMedio = 18;
        private const int CanalCinco_ByteBajo = 19;
        // Canal 6
        private const int CanalSeis_ByteAlto = 20;
        private const int CanalSeis_ByteMedio = 21;
        private const int CanalSeis_ByteBajo = 22;
        // Canal 7
        private const int CanalSiete_ByteAlto = 23;
        private const int CanalSiete_ByteMedio = 24;
        private const int CanalSiete_ByteBajo = 25;
        // Header final
        private const int HeaderFinalPosicion = 32;
        private const byte HeaderFinal = 0xC0;


        // Lista para almacenar los datos
        private List<Int32> CanalCero, CanalUno, CanalDos, CanalTres, CanalCuatro, CanalCinco, CanalSeis, CanalSiete;
        // Lista auxiliar para almacenar el contador de tramas
        private List<Int32> NumerosDeTramas;

        // Contador auxiliares para agilizar los cálculos
        private int CantidadDeTramasFalladas;
        private int CantidadDeTramasValidas;



        /********************************************************************************************************************************************/
        /*                                                         CONSTRUCTOR DE LA CLASE                                                          */
        /********************************************************************************************************************************************/

        public DatosCasco()
        {
            // Inicialización de las listas
            CanalCero = new List<Int32>();
            CanalUno = new List<Int32>();
            CanalDos = new List<Int32>();
            CanalTres = new List<Int32>();
            CanalCuatro = new List<Int32>();
            CanalCinco = new List<Int32>();
            CanalSeis = new List<Int32>();
            CanalSiete = new List<Int32>();
            NumerosDeTramas = new List<Int32>();
            CantidadDeTramasValidas = 0;
            CantidadDeTramasFalladas = 0;
            AgregarMuestraNula();
        }



        /********************************************************************************************************************************************/
        /*                                                           MÉTODOS Y PROPIEDADES                                                          */
        /********************************************************************************************************************************************/

        public bool AlmacenarTrama(byte[] trama)
        {
            if (trama[HeaderInicialPosicion] == HeaderInicial && trama[HeaderFinalPosicion] == HeaderFinal)
            {
                CanalCero.Add(   ConvertirBytesEnInt( trama[ CanalCero_ByteAlto ],   trama[ CanalCero_ByteMedio ],   trama[ CanalCero_ByteBajo ] ) );
                CanalUno.Add(    ConvertirBytesEnInt( trama[ CanalUno_ByteAlto ],    trama[ CanalUno_ByteMedio ],    trama[ CanalUno_ByteBajo ] ) );
                CanalDos.Add(    ConvertirBytesEnInt( trama[ CanalDos_ByteAlto ],    trama[ CanalDos_ByteMedio ],    trama[ CanalDos_ByteBajo ] ) );
                CanalTres.Add(   ConvertirBytesEnInt( trama[ CanalTres_ByteAlto],    trama[ CanalTres_ByteMedio],    trama[ CanalTres_ByteBajo ] ) );
                CanalCuatro.Add( ConvertirBytesEnInt( trama[ CanalCuatro_ByteAlto ], trama[ CanalCuatro_ByteMedio ], trama[ CanalCuatro_ByteBajo ] ) );
                CanalCinco.Add(  ConvertirBytesEnInt( trama[ CanalCinco_ByteAlto ],  trama[ CanalCinco_ByteMedio ],  trama[ CanalCinco_ByteBajo ] ) );
                CanalSeis.Add(   ConvertirBytesEnInt( trama[ CanalSeis_ByteAlto ],   trama[ CanalSeis_ByteMedio ],   trama[ CanalSeis_ByteBajo ] ) );
                CanalSiete.Add(  ConvertirBytesEnInt( trama[ CanalSiete_ByteAlto ],  trama[ CanalSiete_ByteMedio ],  trama[ CanalSiete_ByteBajo ] ) );
                NumerosDeTramas.Add( trama[ ContadorDeTramasPosicion ] );
                CantidadDeTramasValidas++;
                return( true );
            }
            else
            {
                CantidadDeTramasFalladas++;
                return (false);
            }
            
        }

        public Int32[] LeerCanal(int canal)
        {
            switch (canal)
            {
                case 0:
                    return CanalCero.ToArray();
                case 1:
                    return CanalUno.ToArray();
                case 2:
                    return CanalDos.ToArray();
                case 3:
                    return CanalTres.ToArray();
                case 4:
                    return CanalCuatro.ToArray();
                case 5:
                    return CanalCinco.ToArray();
                case 6:
                    return CanalSeis.ToArray();
                default:
                    return CanalSiete.ToArray();
            }
        }

        public Int32 LeerDatosDelCanal( int canal, int muestra )
        {
            switch (canal)
            {
                case 0:
                    return CanalCero[ muestra ];
                case 1:
                    return CanalUno[ muestra ];
                case 2:
                    return CanalDos[ muestra ];
                case 3:
                    return CanalTres[ muestra ];
                case 4:
                    return CanalCuatro[ muestra ];
                case 5:
                    return CanalCinco[ muestra ];
                case 6:
                    return CanalSeis[ muestra ];
                default:
                    return CanalSiete[ muestra ];
            }
        }

        public void AgregarMuestraNula()
        {
            // Agrega una muestra nula para equilibrar los tamaños de los vectores
            CanalCero.Add(0);
            CanalUno.Add(0);
            CanalDos.Add(0);
            CanalTres.Add(0);
            CanalCuatro.Add(0);
            CanalCinco.Add(0);
            CanalSeis.Add(0);
            CanalSiete.Add(0);
            NumerosDeTramas.Add(0);
        }

        public Int32 LongitudDeLaLista()
        {
            return (NumerosDeTramas.Count);
        }

        private Int32 ConvertirBytesEnInt(byte ParteAlta, byte ParteMedia, byte ParteBaja)
        {
            Int32 Dato, Auxiliar;

            Dato = ParteAlta << 16;
            Auxiliar = ParteMedia << 8;
            Dato += Auxiliar;
            Auxiliar = ParteBaja;
            Dato += Auxiliar;

            return( Dato );
        }

    }
}
