require gbhw.fs
require time.fs

: to-white
  %10100100 rBGP v! 60 ms
  %01010100 rBGP v! 60 ms
  %00000000 rBGP v! ;

: from-white
  %01010100 rBGP v! 60 ms
  %10100100 rBGP v! 60 ms
  %11100100 rBGP v! ;

: ?from-white
  rBGP c@ 0= if from-white then ;

: to-black
  %01010101 rBGP v! 60 ms
  %10101010 rBGP v! 60 ms
  %11111111 rBGP v! ;

: from-black
  %11101010 rBGP v! 60 ms
  %11100101 rBGP v! 60 ms
  %11100100 rBGP v! ;
