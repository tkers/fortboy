require struct.fs
require stack.fs
require ./strings.fs

\ utils
:m square dup * ;

\ number of rooms to create
10 constant #rooms
#rooms square constant #grid

ROM
struct
  cell field room>coord
  2 cells field room>name
  2 cells field room>description
  1 chars field room>north
  1 chars field room>east
  1 chars field room>south
  1 chars field room>west
  2 cells field room>item
  cell field room>aux
end-struct room%
RAM

: ix>name
  case
     1 of s" 1. South of House"     endof
     2 of s" 2. Behind House"       endof
     3 of s" 3. Kitchen"            endof
     4 of s" 4. Living Room"        endof
     5 of s" 5. Dark Cellar"        endof
     6 of s" 6. Chasm Edge"         endof
     7 of s" 7. Art Gallery"        endof
     8 of s" 8. Attic"              endof
     9 of s" 9. Small Room"         endof
    10 of s" 10. Engravings Cave"   endof
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
                   ( xy ) r@ room>coord !
  next-room-index ix>name r@ room>name 2!
  next-room-index ix>desc r@ room>description 2!
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
  r@ room>nesw c! \ ix1 ix2
  ix>room \ ix1 a2
  r> flip-nesw room>nesw c! ;


#rooms STACK visit-stack-1
#rooms STACK visit-stack-2
value curr-visit
value next-visit
variable curr-depth

: gen-maze
  \ clear the grid & room data
  grid #grid erase
  0 roomlist-length !
  roomlist #rooms rooms erase

  \ first room
  rand-grid
  dup occupy-grid
  push-room

  \ grow
  #rooms 1 do
    rand-room-ix dup \ ix ix
    ix>room room>coord @ \ ix coord
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

  \ floodfill for depth calculation
  visit-stack-1 clear
  visit-stack-2 clear
  visit-stack-1 to curr-visit
  visit-stack-2 to next-visit
  0 curr-depth !
  1 curr-visit push

  begin
    1 curr-depth +!
    begin
      curr-visit empty? invert
    while
      curr-visit pop
      ix>room
      dup room>aux @ 0 = if
        \ set depth
        dup room>aux curr-depth @ swap !

        \ add next rooms
        dup room>north c@ dup 0 <> if next-visit push else drop then
        dup room>east  c@ dup 0 <> if next-visit push else drop then
        dup room>south c@ dup 0 <> if next-visit push else drop then
        dup room>west  c@ dup 0 <> if next-visit push else drop then
      then
      drop
    repeat
    curr-visit
    next-visit to curr-visit
               to next-visit
  curr-visit empty? until

  \ get deepest level
  0
  #rooms 1+ 2 DO
    I ix>room room>aux @ \ a b
    2dup < if nip else drop then
  LOOP

  \ get deepest room
  #rooms 1+ 2 DO
    I ix>room room>aux @
    over = if I swap drop leave then
  LOOP
  ix>room room>description s" You Won!" rot 2!

  \ add item to random room (except the starting room)
  s" a rusty key"
  #rooms 1- random 2 +
  ix>room room>item 2!

  ;

: show-maze
  #rooms 0 do
    #rooms 0 do
      I J xy>grid is-occupied? if ." X" else ." ." then
    loop
    cr
  loop ;

variable current-room

: look-room ( -- )
  current-room @ ix>room

  dup room>name        2@ type cr cr
  dup room>description 2@ pad place

  dup room>item 2@
  dup 0 <> if
    bl pad cappend
    s" You spot " pad append
    ( item-addr item-u ) pad append
    bl pad cappend
    s" laying on the floor." pad append
  else 2drop then

  bl pad cappend
  s" You can see " pad append

  0 swap
  dup room>north c@ 0 <> if swap 1+ swap then
  dup room>east  c@ 0 <> if swap 1+ swap then
  dup room>south c@ 0 <> if swap 1+ swap then
  dup room>west  c@ 0 <> if swap 1+ swap then

  swap 1 = if s" a door " else s" doors " then pad append
  s" leading to the" pad append

  dup room>north c@ 0 <> if s"  North," pad append then
  dup room>east  c@ 0 <> if s"  East,"  pad append then
  dup room>south c@ 0 <> if s"  South," pad append then
  dup room>west  c@ 0 <> if s"  West,"  pad append then
  [char] . pad count 1- + c! \ replace last space with dot

  pad count .wrapped
  drop ;

: go-room ( dir -- )
  current-room @ ix>room
  swap room>nesw c@
  ?dup 0 <> if
    current-room ! page
  then ;

: key>action
  case
    k-up     of 0 go-room endof
    k-right  of 1 go-room endof
    k-down   of 2 go-room endof
    k-left   of 3 go-room endof
    k-select of page show-maze key drop endof
  endcase ;

: play-maze
  1 current-room !
  begin
    page
    look-room
    key key>action
  again ;
