require struct.fs

\ utils
:m square dup * ;

\ number of rooms to create
10 constant #rooms
#rooms square constant #grid

ROM
struct
  cell field room>grid
  cell field room>index
  2 cells field room>name
  2 cells field room>description
  cell field room>north
  cell field room>east
  cell field room>south
  cell field room>west
end-struct room%
RAM

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

: ix>desc
  case
     1 of s" You are facing the south side of a white house. There is no door here, and all the windows are boarded."   endof
     2 of s" You are behind the white house. A path leads into the forest to the east. In one corner of the house there is a small window which is slightly ajar."   endof
     3 of s" You are in the kitchen of the white house. A table seems to have been used recently for the preparation of food." endof
     4 of s" You are in the living room. There is a doorway to the east, a wooden door with strange gothic lettering to the west, which appears to be nailed shut, a trophy case, and a large oriental rug in the center of the room."  endof
     5 of s" You are in a dark and damp cellar with a narrow passageway leading north, and a crawlway to the south. On the west is the bottom of a steep metal ramp which is unclimbable."  endof
     6 of s" You are on the east edge of a chasm, the bottom of which cannot be seen. "   endof
     7 of s" This is an art gallery. Most of the paintings have been stolen by vandals with exceptional taste. The vandals left through either the north or west exits." endof
     8 of s" This is the attic. The only exit is a stairway leading down." endof
     9 of s" This is a small room with passages to the east and south and a forbidding hole leading west."  endof
    10 of s" You have entered a low cave with passages leading northwest and east. There are old engravings on the walls here."   endof
  endcase ;


: rooms room% * ;
:m rooms room% * ;

\ list with push and peek
variable roomlist-length
create roomlist #rooms rooms allot
: next-room-index roomlist-length @ 1+ ;
: next-room-addr roomlist-length @ rooms roomlist + ;
: last-room-index roomlist-length @ ;

: push-room ( xy -- )
  next-room-addr >r
  next-room-index         r@ room>index !
  next-room-index ix>name r@ room>name 2!
  next-room-index ix>desc r@ room>description 2!
                   ( xy ) r@ room>grid !
  rdrop
  1 roomlist-length +! ;

: ix>room ( u -- x )
  1- rooms roomlist + ;

: rand-room-ix ( -- u ) roomlist-length @ random 1+ ;

\ occupancy grid to check free space
CREATE grid #grid allot

: occupy-grid next-room-index swap c! ;
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
: rand-grid ( -- coord )
  #grid random grid + ;

: nesw ( coord1 dir -- coord2 )
  case
    0 of #rooms - endof
    1 of 1+ endof
    2 of #rooms + endof
    3 of 1- endof
  endcase ;


: flip-nesw
  case
    0 of 2 endof
    1 of 3 endof
    2 of 0 endof
    3 of 1 endof
  endcase ;

: room>nesw
  case
    0 of room>north endof
    1 of room>east endof
    2 of room>south endof
    3 of room>west endof
  endcase ;

: dig-tunnel  ( ix1 dir ix2 -- )
  swap >r \ ix1 ix2
  2dup \ ix1 ix2 ix1 ix2
  swap ix>room \ ix1 ix2 ix2 a1
  r@ room>nesw ! \ ix1 ix2
  ix>room \ ix1 a2
  r> flip-nesw room>nesw ! ;


\ variable first-room
variable current-room
: make-and-show
  0 roomlist-length !
  grid #grid 0 fill

  \ first room
  rand-grid
  dup occupy-grid
  push-room

  \ grow
  #rooms 1 do
    rand-room-ix dup \ ix ix
    ix>room room>grid @ \ ix coord
    4 random tuck nesw  \ ix dir coord2
    dup is-free? if \ ix dir coord2
      dup occupy-grid
      push-room \ ix dir
      last-room-index dig-tunnel
      1
    else
      drop drop drop
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

  1 current-room !

  begin
    page

    current-room @ ix>room

    ." ~~ in room " dup room>name 2@ type space ." ~~" cr
    \ dup room>description 2@ .wrapped

    dup room>north @ . cr
    dup room>east @ . cr
    dup room>south @ . cr
    dup room>west @ . cr

    cr
    ." Doors:" cr
    dup @ 0 nesw is-occupied? if ." - North" cr then
    dup @ 1 nesw is-occupied? if ." - East" cr then
    dup @ 2 nesw is-occupied? if ." - South" cr then
    dup @ 3 nesw is-occupied? if ." - West" cr then
    cr
    ."  Where to next?" cr
    ." (use arrow keys)" cr

    room>grid @
    key case
      k-up    of 0 endof
      k-right of 1 endof
      k-down  of 2 endof
      k-left  of 3 endof
                 4
    endcase nesw

    dup is-occupied? if c@ current-room ! page else drop then
  again
 ;
