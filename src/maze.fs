require struct.fs
require stack.fs
require ./bag.fs

\ the current world size
RAM value #rooms

\ number of rooms to create
8  1+ constant #rooms-min
16 1+ constant #rooms-med
28 1+ constant #rooms-max

: #locks ( -- )
  #rooms case
    #rooms-min of 2 endof
    #rooms-med of 4 endof
    #rooms-max of 8 endof
  endcase ;

10 constant #coins

\ utils
:m square dup * ;

#rooms-max square constant #grid-max
: #grid #rooms dup * ;

ROM
struct
  cell field room>coord
  2 cells field room>name
  2 cells field room>description
  1 chars field room>north
  1 chars field room>east
  1 chars field room>south
  1 chars field room>west
  1 chars field room>lock-north
  1 chars field room>lock-east
  1 chars field room>lock-south
  1 chars field room>lock-west
  1 chars field room>item
  1 chars field room>gold
  1 chars field room>final
  cell field room>aux
end-struct room%
RAM

include rooms.fs
include items.fs

: rooms room% * ;
:m rooms room% * ;

RAM

\ list with push and peek
variable roomlist-length
create roomlist #rooms-max rooms allot
: next-room-addr roomlist-length @ rooms roomlist + ;
: next-room-ix roomlist-length @ 1+ ;
: last-room-ix roomlist-length @ ;

: push-room ( xy -- )
  next-room-addr room>coord !
  1 roomlist-length +! ;

: ix>room ( u -- x )
  1- rooms roomlist + ;

: rand-room-ix ( -- u ) roomlist-length @ random 1+ ;

\ occupancy grid to check free space
CREATE grid #grid-max allot

: occupy-grid next-room-ix swap c! ;
: is-occupied? c@ 0<> ;

: in-range? ( addr -- f )
  dup [ grid 1- ]L >
  swap grid #grid + <
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

: coords>nesw ( coord1 coord2 -- dir )
  case
    over 0 nesw of 0 endof
    over 1 nesw of 1 endof
    over 2 nesw of 2 endof
    over 3 nesw of 3 endof
  endcase nip ;

: room>lock-nesw ( room dir -- lock-addr )
  case
    0 of room>lock-north endof
    1 of room>lock-east  endof
    2 of room>lock-south endof
    3 of room>lock-west  endof
  endcase ;

: flip-nesw ( u -- u )
  case
    0 of 2 endof
    1 of 3 endof
    2 of 0 endof
    3 of 1 endof
  endcase ;

: room>nesw ( room dir -- nesw-addr )
  case
    0 of room>north endof
    1 of room>east  endof
    2 of room>south endof
    3 of room>west  endof
  endcase ;

: dig-tunnel  ( ix1 dir ix2 -- )
  swap >r \ ix1 ix2
  2dup \ ix1 ix2 ix1 ix2
  swap ix>room \ ix1 ix2 ix2 a1
  r@ room>nesw c! \ ix1 ix2
  ix>room \ ix1 a2
  r> flip-nesw room>nesw c! ;

: shape-maze
  \ add first room
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
      last-room-ix dig-tunnel
      1
    else
      drop drop drop
      0
    then
  +loop ;

\ used for floodfill
#rooms-max STACK visit-stack-1
#rooms-max STACK visit-stack-2
value curr-visit
value next-visit
variable curr-depth

: erase-depths
  #rooms 1+ 1 DO
    0 I ix>room room>aux !
  LOOP ;

\ floodfill for depth calculation
: annotate-depths
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
      dup room>aux @ 0= if
        \ set depth
        dup room>aux curr-depth @ swap !

        \ add connected rooms (only if not locked)
        4 0 DO
          dup I
          2dup room>lock-nesw c@ 0= if
            room>nesw c@ ?dup 0<> if next-visit push then
          else 2drop then
        LOOP
      then
      drop
    repeat
    curr-visit
    next-visit to curr-visit
               to next-visit
  curr-visit empty? until ;

: max-room-depth ( -- u )
  0
  #rooms 1+ 2 DO
    I ix>room room>aux @ \ a b
    2dup < if nip else drop then
  LOOP ;

: find-deepest-room ( -- addr )
  max-room-depth
  #rooms 1+ 1 DO
    I ix>room room>aux @
    over = if I swap drop leave then
  LOOP
  ix>room ;

\ used for path finding
variable roompath-length
create roompath #rooms-max cells allot

: check-path-step ( room depth nesw-addr -- room' depth' )
  c@ ?dup 0<> if
    ix>room
    dup room>aux @
    ?dup 0= if drop else
      rot > if drop else nip then
      dup room>aux @
    then
  then ;

\ find path from start(1) to target
: store-main-path ( tgt-room -- )
  roompath #rooms cells erase
  1 roompath-length !
  dup roompath !

  dup room>aux @ \ room depth
  begin
    over >r

    \ get next room
    r@ room>north check-path-step
    r@ room>east  check-path-step
    r@ room>south check-path-step
    r> room>west  check-path-step

    over roompath roompath-length @ cells + !
    1 roompath-length +!

    over 1 ix>room =
  until
  -1 roompath-length +! \ remove starting room
  drop drop ;

variable openrooms-length
create openrooms #rooms-max cells allot

: store-open-rooms
  0 openrooms-length !
  openrooms #rooms cells erase

  #rooms 1+ 2 do \ skip starting room
    I ix>room room>aux @ 0<> \ reachable (depth <>0 )
    I ix>room room>item c@ 0= \ empty ( item 0= )
    and if
      I ix>room
      openrooms openrooms-length @ cells + !
      1 openrooms-length +!
    then
  loop ;

: comp-room-depth ( room1 room2 -- f )
  room>aux @ swap room>aux @ < ;

: sort-open-rooms ( -- )
  openrooms openrooms-length @ ( addr len )
  dup 1 ?do
    2dup I - cells bounds do
      I 2@ comp-room-depth if I 2@ swap I 2! then
    cell +loop
  loop 2drop ;

: random-open-room
  openrooms-length @ 2/ 1+ random
  cells openrooms + @ ;

: random-room \ ix starts at 1, non-start at 2
  #rooms 1- random 2 + ix>room ;

: place-lock ( u -- )
  \ get random room in path
  roompath-length @ 2/ random 1+
  dup     cells roompath + @
  swap 1- cells roompath + @ \ room room+1
  over room>coord @
  swap room>coord @ coords>nesw \ room dir
  room>lock-nesw c! ;

: place-item ( u -- )
  \ add item to any reachable room
  random-open-room
  tuck room>item c!
  store-main-path ;

: place-lock-and-item ( -- )
  \ no space to add more locks, skip
  roompath-length @ 2 < if exit then
  item-bag draw-bag 1+ dup
  place-lock
  erase-depths annotate-depths store-open-rooms sort-open-rooms
  ( n ) place-item ;

: place-gold ( u -- )
  \ add gold to any non-final and non-item room
  0 begin
    drop random-room
    dup room>final c@ 0=
    over room>item c@ 0= and
  until
  room>gold dup c@ rot +
  swap c! ;

: set-fort-size ( u -- )
  case
    0 of #rooms-min endof
    1 of #rooms-med endof
    2 of #rooms-max endof
  endcase to #rooms ;

: gen-maze ( u -- )
  \ update the world size and difficulty
  set-fort-size

  \ clear the grid & room data
  grid #grid erase
  roomlist #rooms rooms erase
  0 roomlist-length !

  \ create room layout on grid
  shape-maze

  \ calculate depths and mark deepest as final
  annotate-depths
  find-deepest-room
  dup room>final 1 swap c!
  store-main-path

  \ add flavour text to rooms
  fill-room-bag
  #rooms 1+ 1 do
    I ix>room room>final c@ 0= if
      room-bag draw-bag dup
      roomid>name I ix>room room>name 2!
      roomid>desc I ix>room room>description 2!
    then
  loop

  \ add obstacles and items
  fill-item-bag
  #locks 0 do
    place-lock-and-item
  loop

  \ sprinkle some gold coins around
  #coins 0 do
    1 place-gold
  loop ;
