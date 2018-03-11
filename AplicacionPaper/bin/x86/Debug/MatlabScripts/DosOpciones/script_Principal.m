% Caracteres utilizados
% 
% @ comienza experimento       = 0x40 = 64
% $ finaliza el experimento    = 0x24 = 36
% % comienza estimulo 1        = 0x25 = 
% & finaliza estimulo 1        = 0x26 = 
% # comienza estimulo 2        = 0x23 = 35
% ! finaliza estimulo 2        = 0x21 = 33

function [opcion] = script_Principal( CH_01, MARKER )

CH_01 = CH_01';

INICIO_ELECCION = uint8('@');
FIN_ELECCION    = uint8('$');
MARCA_SI        = uint8('%');
MARCA_NO        = uint8('#');
CANT_MUESTRAS   = 125;
fs              = 250;

nEleccion = 1;

% Armo un cellarray con los buffers que van a ser analizados luego. 
% La idea es que, puede que se use solo el Oz, o varios, pero que la
% funcion solo corte por MARKER

temp = {CH_01,MARKER'};

%% Ploteo de la señal obtenida en Oz


%     figure('NumberTitle','off','name', 'Figura: Registro de electroencefalograma' ); 
%     ejeX        = 'Tiempo[seg]';
%     ejeY        = 'Amplitud';
%     gridEstado  = 0;
%     nMuestras = length(CH_01);
%     xTemp = 0:1/fs:(nMuestras-1)*1/fs;
%     grid on;
%     plot(CH_01,'LineWidth',2); 
%     xlabel(ejeX); ylabel(ejeY);
%     title('Registro EEG','FontSize',20);

    
    
%% Segmentado de EEG

%Obtengo elecciones
eleccion = CortarEleccion(temp,INICIO_ELECCION,FIN_ELECCION);

siCortados = {zeros(length(eleccion))};
noCortados = {zeros(length(eleccion))};
siPromedio = {zeros(CANT_MUESTRAS)};
noPromedio = {zeros(CANT_MUESTRAS)};
siNormalizado = {zeros(CANT_MUESTRAS)};
noNormalizado = {zeros(CANT_MUESTRAS)};
siEnergiaPost={};
noEnergiaPost={};

for elec = 1 : length(eleccion)
    
    %Como no estÃ¡n divididos, corto todos los SI y los NO
    siCortados{elec} = CortarNMuestras(eleccion{elec},MARCA_SI,CANT_MUESTRAS);
    noCortados{elec} = CortarNMuestras(eleccion{elec},MARCA_NO,CANT_MUESTRAS);

    %Ahora tengo que promediarlas!!
    % http://stackoverflow.com/questions/5197597/how-to-average-over-a-cell-array-of-arrays
    
    siPromedio{elec}= Promediar(siCortados{elec},1);
    noPromedio{elec}= Promediar(noCortados{elec},1);
    
    siNormalizado{elec}=Normalizar(siPromedio{elec},1);
    noNormalizado{elec}=Normalizar(noPromedio{elec},1);
 
 %% Procesamiento (wavelet) y informacion de energia
    
    %fprintf('\n\n Eleccion numero: %d',nEleccion); nEleccion = nEleccion + 1;
    
    %% Forma (1)
    % verificacionEnergia(siNormalizado{elec},noNormalizado{elec},0);
    % resultado = Procesar(siNormalizado{elec},noNormalizado{elec},5,'noplot');

%% Ploteo de resultados obtenidos en tiempo
    
%     ejeX        = 'Tiempo';
%     ejeY        = 'Amplitud[V]';
%     gridEstado  = 0;
%     nMuestras   = length(siNormalizado{elec});
%     xTemp       = 0:1/fs:(nMuestras-1)*1/fs;
%     titulo      = 'Resultados superpuestos antes de aplicar wavelet';
%     plotTiempo(siNormalizado{elec},noNormalizado{elec},elec,xTemp,titulo,ejeX,ejeY,gridEstado);
    
    %% Forma (2)
    % De la forma (1) está normalizada la energía y da el mismo valor de
    % energía para ambas señales. Acá le saqué la normalización.
    
    verificacionEnergia(siPromedio{elec},noPromedio{elec},0);
    resultado = Procesar(siPromedio{elec},noPromedio{elec},5,'noplot');
     
    %% Continua procesamiento (Wavelet) y información de energía
    energiaTemp=verificacionEnergia(resultado{1},resultado{2},1);

%% Ploteo de resultados obtenidos en tiempo
    
%     ejeX        = 'Tiempo';
%     ejeY        = 'Amplitud[V]';
%     gridEstado  = 0;
%     nMuestras = length(siNormalizado{elec}); xTemp = 0:1/fs:(nMuestras-1)*1/fs;
%     titulo     = 'Resultados superpuestos';
%     plotTiempo(resultado{1},resultado{2},elec,xTemp,titulo,ejeX,ejeY,gridEstado);
    
%% Evaluacion de eleccion por energias (250mS a 350mSeg estandar)

    inicioP300=round(fs*0.250);
    finP300=round(fs*0.350);
    opcion = EvaluarEleccion(resultado{1},resultado{2},elec, inicioP300,finP300);
    
    if( opcion == 1 )
        opcion = 'SI';
    else
        opcion = 'NO';
    end
    
end

end