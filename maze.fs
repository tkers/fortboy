require struct.fs

\ utils
:m square dup * ;

\ number of rooms to create
10 constant #rooms
#rooms square constant #grid

ROM
struct
  cell field room-grid
  cell field room-index
  cell field room-name
  cell field room-description
  cell field room-north
  cell field room-east
  cell field room-south
  cell field room-west
end-struct room%
RAM

: rooms room% * ;
:m rooms room% * ;

\ list with push and peek
variable roomlist-length
create roomlist #rooms rooms allot
: next-room-index roomlist-length @ 1+ ;

: ix>name
  case
     1 of s" one"   endof
     2 of s" two"   endof
     3 of s" three" endof
     4 of s" four"  endof
     5 of s" five"  endof
     6 of s" six"   endof
     7 of s" seven" endof
     8 of s" eight" endof
     9 of s" nine"  endof
    10 of s" ten"   endof
  endcase ;

: push ( xy -- )
  roomlist roomlist-length @ rooms + >r
  next-room-index r@ room-index !
  next-room-index ix>name r@ room-name 2!
  r@ room-grid !
  rdrop
  1 roomlist-length +! ;

: seek ( u -- x )
  rooms roomlist + ;

\ occupancy grid to check free space
CREATE grid #grid allot

: occupy next-room-index swap c! ;
: is-occupied? c@ 0 <> ;

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

\ variable first-room
variable current-room
: make-and-show
  0 roomlist-length !
  grid #grid 0 fill

  \ first room
  rand-grid
  dup occupy
      push

  \ grow
  #rooms 1 do
    roomlist-length @ random \ ix
    seek room-grid @ \ addr
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
  loop

  (
     ..........
     x.........
     xxxxs.....
     .xxxx.....
     ..........
  )

  key drop

  0 current-room !

  begin
    page

    current-room @ seek

    ." ~~ in room " dup room-name 2@ type space ." ~~" cr
    cr
    ." Doors:" cr
    dup @ 0 nesw is-occupied? if ." - North" cr then
    dup @ 1 nesw is-occupied? if ." - East" cr then
    dup @ 2 nesw is-occupied? if ." - South" cr then
    dup @ 3 nesw is-occupied? if ." - West" cr then
    cr
    ."  Where to next?" cr
    ." (use arrow keys)" cr

    room-grid @
    key case
      k-up    of 0 endof
      k-right of 1 endof
      k-down  of 2 endof
      k-left  of 3 endof
                 4
    endcase nesw

    dup is-occupied? if c@ 1- current-room ! page else drop then
  again
 ;
