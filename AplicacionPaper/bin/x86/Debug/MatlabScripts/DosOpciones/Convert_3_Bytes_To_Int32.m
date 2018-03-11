function [outValue] = Convert_3_Bytes_To_Int32(a,b,c)

if(a >= 0)
    b3 = '00000000';
    b2 = dec2bin(int8(a),8);
else
    b3 = '11111111';
    b2 = dec2bin(typecast(int8(a),'uint8'),8);
end;

if(b >= 0)
    b1 = dec2bin(int8(b),8);
else
    b1 = dec2bin(typecast(int8(b),'uint8'),8);
end;

if(c >= 0)
    b0 = dec2bin(int8(c),8);
else
    b0 = dec2bin(typecast(int8(c),'uint8'),8);
end;




valBin = [b3 b2 b1 b0];

outValue = typecast(uint32(bin2dec(valBin)),'int32');