rom
create tile-key
%00000000 c, %00000000 c,
%00000000 c, %00000000 c,
%00000000 c, %11100000 c,
%00000000 c, %10111110 c,
%00010100 c, %11101010 c,
%11101010 c, %00000000 c,
%00000000 c, %00000000 c,
%00000000 c, %00000000 c,

create tile-chain
%00000000 c, %00000000 c,
%00000000 c, %11000011 c,
%00000000 c, %00100100 c,
%00000000 c, %01111110 c,
%00100100 c, %00000000 c,
%00000000 c, %11000011 c,
%00000000 c, %00000000 c,
%00000000 c, %00000000 c,
ram

: install-tile ( addr char -- )
  16 * _VRAM +
  16 cmovevideo ;

: install-2bit-tiles
  tile-key    9 install-tile
  tile-chain 15 install-tile ;
