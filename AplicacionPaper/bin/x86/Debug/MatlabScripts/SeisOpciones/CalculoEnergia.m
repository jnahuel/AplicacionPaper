% function [Energia] = CalculoEnergia (signal)
% parámetro signal     - Señal a la cual se le desea calcular la energía
% retorno   energia    - Energía de la señal

function [Energia] = CalculoEnergia (signal)
        %TODO: plot(signal,'LineWidth',4);
     signalT = abs(fft(signal));
     signalT_aux = signalT.^2; 
     Energia = sum(signalT_aux);
    
end
