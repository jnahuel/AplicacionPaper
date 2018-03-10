function [opcion] = script_Principal( datos, MARKER )

%% InformaciÃ³n del experimento y datos de interfaz con usuario

% clc; clear; close all;
% feature('DefaultCharacterSet','UTF8');
% collation=feature('DefaultCharacterSet');
% if(strfind(collation,'UTF-8'))
%     disp(strcat('charset: ',collation));
% else
%     disp(strcat('charset ERROR: ',collation));
% end

% Caracteres utilizados

% Inicio y fin de elecciÃ³n
INICIO_ELECCION = uint8('@');
FIN_ELECCION    = uint8('$');

% Columnas de la matriz (6 columnas)
MARCA_COLUMNA1  = uint8('A');
MARCA_COLUMNA2  = uint8('B');
MARCA_COLUMNA3  = uint8('C');
MARCA_COLUMNA4  = uint8('D');
MARCA_COLUMNA5  = uint8('E');
MARCA_COLUMNA6  = uint8('F');

CANT_MUESTRAS   = 125;
fs              = 250;

% Otros parÃ¡metros del sistema
%CANT_MUESTRAS   = 127; %TODO: Modificar a 64 en las grabaciones originales
%FIXME: Creo que en este caso podrÃ­a ser 127. Nuestro casco es un poco mÃ¡s
%moderno que el de YaSabemosQuiÃ©nâ™¥

%fs              = 128;


ganancia = 24;  % 0.02235 [uVolts/cuenta] *según la bibliofrafía.
factorEscala = (4.5 / ganancia) / (2^23 - 1);


CH_01 = zeros( length(datos) / 3, 1 );
for i = 1 : length( CH_01 ) - 1
    CH_01(i) = double( Convert_3_Bytes_To_Int32( datos(i*3), datos(i*3+1), datos(i*3+2) ) ) * factorEscala;
end

%% Carga de datos del estudio EEG (23 junio)
%path = './Estudios/Software1x6/Registro1-1-23.06.17-18.00.26.csv';   % BIEN
%path = './Estudios/Software1x6/Registro3-3-23.06.17-18.11.31.csv';  % BIEN
%path = './Estudios/Software1x6/Registro4-4-23.06.17-18.19.34.csv';  % BIEN
%path = './Estudios/Software1x6/Registro5-5-23.06.17-18.27.49.csv';  % MAL tiene que dar D y da A
%path = './Estudios/Software1x6/Registro6-6-23.06.17-18.34.28.csv';  % MAL tiene que dar f da C
%path = './Estudios/Software1x6/Registro7-7-23.06.17-18.39.52.csv';  % BIEN

nEleccion = 1;

%[CH_AF3,CH_F7,CH_F3,CH_FC5,CH_T7,CH_P7,CH_01,CH_02,CH_P8,CH_T8,CH_FC6,CH_F4,CH_F8,CH_AF4,CH_CMS,CH_DRL,MARKER]  = CargarWorkspace(path);

temp = {CH_01,MARKER'};

%% Segmentado de EEG

eleccion = CortarEleccion(temp,INICIO_ELECCION,FIN_ELECCION);

disp('Cantidad de Elecciones');
disp(length(eleccion));

MARCAS_COLUMNA= {MARCA_COLUMNA1 MARCA_COLUMNA2 MARCA_COLUMNA3 MARCA_COLUMNA4 MARCA_COLUMNA5 MARCA_COLUMNA6};

columnaCortados = {{zeros(length(eleccion))} ...
    {zeros(length(eleccion))} ...
    {zeros(length(eleccion))} ...
    {zeros(length(eleccion))} ...
    {zeros(length(eleccion))} ...
    {zeros(length(eleccion))}};

columnaPromedio = {{zeros(CANT_MUESTRAS)}};
columnaNormalizado = {{zeros(CANT_MUESTRAS)}};

siEnergiaPost={};
noEnergiaPost={};
Palabra={};

for elec = 1 : length(eleccion)
    
    for columna = 1 : length(MARCAS_COLUMNA)
        columnaCortados{elec}{columna} = CortarNMuestras(eleccion{elec},MARCAS_COLUMNA{columna},CANT_MUESTRAS);
        columnaPromedio{elec}{columna}= Promediar(columnaCortados{elec}{columna},1);
        % Saqué la normalización en energía del algoritmo.
        %columnaNormalizado{elec}{columna}=Normalizar(columnaPromedio{elec}{columna},1);
    end

 %% Procesamiento (wavelet) y informacion de energia

    fprintf('\n\n Eleccion numero: %d',nEleccion); nEleccion = nEleccion + 1;

    for columna = 1:length(MARCAS_COLUMNA)
        nombreColumna=sprintf('Columna %d (Identificador %s)',columna,char(MARCAS_COLUMNA{columna}));
        verificacionEnergia(columnaPromedio{elec}{columna},nombreColumna,0);
    end

    for columna = 1:length(MARCAS_COLUMNA)
        resultadoColumna{elec}{columna} = Procesar(columnaPromedio{elec}{columna},5);
        %verificacionEnergia(columnaPromedio{elec}{columna},nombreColumna,1);
        %nombreColumna=sprintf('Columna %d (Identificador %s)',columna,char(MARCAS_COLUMNA{columna}));
    end
%% Ploteo de resultados obtenidos en tiempo

%     gridEstado  = 0;
%     nMuestras = 0;
%     
%     for columna = 1:length(MARCAS_COLUMNA)
%         if(nMuestras< length(columnaPromedio{elec}{columna}))
%             nMuestras=length(columnaPromedio{elec}{columna});
%         end
%     end

%     xTemp = 0:1/fs:(nMuestras-1)*1/fs;
      resColumna=[];
% 
      for columna = 1:length(MARCAS_COLUMNA)
          resColumna=[resColumna;(resultadoColumna{elec}{columna}')];
      end
 
%% Evaluacion de eleccion por energias (250mS a 350mSeg estandar)

    inicioP300=round(fs*0.250);
    finP300=round(fs*0.350);
    % Acá arregle lo que llega a evaluarelección (ver función adentro)
    opcion = EvaluarEleccion(resColumna,elec, inicioP300,finP300,path);

end

end