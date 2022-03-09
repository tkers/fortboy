\ utils
:m square dup * ;

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
variable roomlist-length
create roomlist #rooms cells allot

: push ( x -- )
  roomlist roomlist-length @ cells + !
  1 roomlist-length +! ;

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

\ variable first-room
variable current-room
: make-and-show
  0 roomlist-length !
  grid #grid erase

  \ first room
  rand-grid
  dup occupy
  dup push
  current-room !

  #rooms 1 do
    roomlist-length @ random peek \ addr
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

  begin
    page

    current-room @

    ." ~~ in room " dup . ." ~~" cr
    cr
    ." Doors:" cr
    dup 0 nesw is-occupied? if ." - North" cr then
    dup 1 nesw is-occupied? if ." - East" cr then
    dup 2 nesw is-occupied? if ." - South" cr then
    dup 3 nesw is-occupied? if ." - West" cr then
    cr
    ."  Where to next?" cr
    ." (use arrow keys)" cr

    key case
      k-up    of 0 endof
      k-right of 1 endof
      k-down  of 2 endof
      k-left  of 3 endof
                 4
    endcase nesw

    dup is-occupied? if current-room ! page else drop then
  again
 ;
