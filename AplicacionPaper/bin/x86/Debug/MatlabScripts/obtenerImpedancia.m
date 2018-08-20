function [ z1 ] = obtenerImpedancia( vector1 )

    % Cerrar todo lo previo
    close all;
    
    % Defines de los filtros
    % Filtro notch de 50 Hz con Fs = 250Hz
    numNotch50 = [ 0.5792   -0.3580    0.5792 ];
    denNotch50 = [ 1.0000   -0.3580    0.1584 ];
    
    vector1 = filter( numNotch50, denNotch50, vector1 );       % Notch de 50 Hz
    
    % Parametros del ensayo
    Fs = 250;       % 250 Hz
    Ts = 1/Fs;      % Tiempo de sampleo, la inversa de la frecuencia de muestreo
    L = length(vector1);       % Largo del vector de muestras
    t = ( 0 : L-1 ) * Ts;   % Vector temporal

    % % La variable "vectorX" representa las muestras de cada canal
    % % Ploteo del resultado temporal
    % figure();
    % subplot(2,1,1);
    % plot( t, vector1 );
    % xlabel( 'S' );
    % ylabel( 'uV' );


    % Obtencion del espectro
    Y = fft( vector1 );                               % Espectro completo
    Pcompleto = abs( Y / L );                   % Acomodada la amplitud
    Putil = Pcompleto( 1 : floor(L/2) + 1 );           % Solo hace falta la mitad de la señal
    Putil( 2 : end-1 ) = 2 * Putil( 2 : end-1 );

    % % Ploteo del resultado frecuencial
    % ejeF = Fs * ( 0 : L/2 ) / L;
    % subplot(2,1,2);
    % plot( ejeF(5:end), Putil(5:end) );
    % xlabel( 'f [Hz]' );
    % ylabel( 'Amplitud' );

    z1 = Putil( floor(( length(Putil) - 1 ) / 4) ) / 6e-3

end