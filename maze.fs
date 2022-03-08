\ gbforth random
: xorshift ( n -- n )
  dup 7 lshift xor
  dup 9 rshift xor
  dup 8 lshift xor ;

variable seed

: rnd ( -- n )
  seed @
  xorshift
  dup seed ! ;

: random ( n -- n )
  rnd swap mod ;

\ utils
: square dup * ;

\ number of rooms to create
10 constant #rooms
#rooms square constant #grid

\ occupancy grid to check free space
CREATE grid #grid allot

: occupy 1 swap c! ;
: is-occupied? c@ 1 = ;

: xy>grid ( x y -- addr )
  #rooms * + grid + ;

: in-range? ( addr -- f )
  dup [ grid 1- ]L >
  swap [ grid #grid + ]L <
  and ;

: is-free? ( addr -- f )
  dup in-range? if
    is-occupied? invert
  else
    drop false
  then ;

\ list with push and peek
variable length
create roomlist #rooms cells allot

: push ( x -- )
  roomlist length @ cells + !
  1 length +! ;

: peek ( u -- x )
  cells roomlist + @ ;

\ room utils
: rand-grid ( -- addr )
  #grid random grid + ;

: nesw ( addr1 dir - addr2 )
  case
    0 of #rooms - endof
    1 of 1+ endof
    2 of #rooms + endof
    3 of 1- endof
  endcase ;

\ demo

: make-and-show
  0 length !
  grid #grid erase

  \ first room
  rand-grid
  dup occupy
  push

  #rooms 1 do
    length @ random peek \ addr
    4 random nesw  \ addr2
    dup is-free? if
      dup occupy
      push
      1
    else
      drop
      0
    then
  +loop

  #rooms 0 do
    #rooms 0 do
      I J xy>grid is-occupied? if ." X" else ." ." then
    loop
    cr
  loop ;

: main
  utime drop seed !
  make-and-show
  bye ;

main