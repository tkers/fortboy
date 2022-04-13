require struct.fs
require stack.fs
require ./bag.fs

\ utils
:m square dup * ;

\ number of rooms to create
14 constant #rooms-max
#rooms-max square constant #grid-max

: #rooms #rooms-max ;
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
: is-occupied? c@ 0 <> ;

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
    1 of room>lock-east endof
    2 of room>lock-south endof
    3 of room>lock-west endof
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
      dup room>aux @ 0 = if
        \ set depth
        dup room>aux curr-depth @ swap !

        \ add next rooms (only if not locked)
        dup room>lock-north c@ 0 = if
          dup room>north c@ ?dup 0 <> if next-visit push then
        then
        dup room>lock-east c@ 0 = if
          dup room>east  c@ ?dup 0 <> if next-visit push then
        then
        dup room>lock-south c@ 0 = if
          dup room>south c@ ?dup 0 <> if next-visit push then
        then
        dup room>lock-west c@ 0 = if
          dup room>west  c@ ?dup 0 <> if next-visit push then
        then
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
  c@ ?dup 0 <> if
    ix>room
    dup room>aux @
    ?dup 0= if drop else
      rot > if drop else nip then
      dup room>aux @
    then
  then ;

\ find path from start to finish
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

\ : debug:show-main-path
\   roompath-length @ 0 do
\     roompath I cells + @ room>name 2@ type cr
\   loop ;

: in-main-path? ( x -- f )
  false
  #rooms 0 do
    over
    roompath I cells + @
    = or
  loop nip ;

variable openrooms-length
create openrooms #rooms-max cells allot

: store-open-rooms
  0 openrooms-length !
  openrooms #rooms cells erase

  #rooms 1+ 1 do
    I ix>room room>aux @ 0 <> \ reachable (depth <>0 )
    I ix>room room>item c@ 0= \ empty ( item 0= )
    and if
      I ix>room
      openrooms openrooms-length @ cells + !
      1 openrooms-length +!
    then
  loop ;

\ : debug:show-open-rooms
\   openrooms-length @ 0 do
\     openrooms I cells + @ room>name 2@ type cr
\   loop ;

: random-open-room
  openrooms-length @ 1- random 1+ \ exclude start
  cells openrooms + @ ;

: random-room \ ix starts at 1
  #rooms random 1+ ix>room ;

: place-lock
  \ get random room in path
  roompath-length @ 1- random 1+
  dup     cells roompath + @
  swap 1- cells roompath + @ \ room room+1
  over room>coord @
  swap room>coord @ coords>nesw \ room dir
  room>lock-nesw c! ;

: place-item
  \ add item to any reachable room
  random-open-room
  tuck room>item c!
  store-main-path ;

: place-lock-and-item
  \ no space to add more locks, skip
  roompath-length @ 2 < if exit then
  item-bag draw-bag 1+ dup
  place-lock
  erase-depths annotate-depths store-open-rooms
  ( n ) place-item ;

: place-gold
  \ add gold to any non-final and non-item room
  0 begin
    drop random-room
    dup room>final c@ 0=
    over room>item c@ 0= and
  until
  room>gold dup c@ rot +
  swap c! ;

: gen-maze
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
  place-lock-and-item
  place-lock-and-item
  place-lock-and-item
  place-lock-and-item

  \ sprinkle some gold coins around
  \ 10 total, spread over 1-7 rooms
  1 place-gold
  1 place-gold
  1 place-gold
  1 place-gold
  1 place-gold
  2 place-gold
  3 place-gold ;
