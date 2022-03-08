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

\ occupancy grid to check free space
CREATE grid #rooms square allot

: xy>grid ( x y -- addr )
  #rooms * + grid + ;
: grid>xy ( addr -- x y )
  grid - #rooms /mod ;

: in-range? ( x y -- f )
  2dup
      -1 >     swap
      -1 > and swap
  #rooms < and swap
  #rooms < and ;

: occupy xy>grid 1 swap c! ;
: is-occupied? xy>grid c@ 1 = ;
: is-free?
  2dup in-range? if
    is-occupied? invert
  else
    2drop false
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
: rand-xy ( -- x y )
  #rooms random #rooms random ;

: nesw ( x y d -- x2 y2 )
  case
    0 of 1- endof
    1 of swap 1+ swap endof
    2 of 1+ endof
    3 of swap 1- swap endof
  endcase ;

\ demo

: make-and-show
  0 length !
  grid [ #rooms square ]L erase

  \ first room
  rand-xy
  2dup occupy
  xy>grid push

  #rooms 1 do
    length @ random peek  \ addr
    grid>xy  \ x y
    4 random nesw  \ x2 y2
    2dup is-free? if
      2dup occupy
      xy>grid push
      1
    else
      2drop
      0
    then
  +loop

  #rooms 0 do
    #rooms 0 do
      I J is-occupied? if ." X" else ." ." then
    loop
    cr
  loop ;

: main
  utime drop seed !
  make-and-show
  bye ;

main
