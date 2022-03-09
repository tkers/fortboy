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
  2 cells field room-name
  2 cells field room-description
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

: push ( xy -- )
  roomlist roomlist-length @ rooms + >r
  next-room-index r@ room-index !
  next-room-index ix>name r@ room-name 2!
  next-room-index ix>desc r@ room-description 2!
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
    dup room-description 2@ .wrapped
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
