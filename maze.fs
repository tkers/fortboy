require struct.fs
require stack.fs
require ./strings.fs
require ./bag.fs
require ./tunes.fs
require ./popup.fs

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

\ list with push and peek
variable roomlist-length
create roomlist #rooms rooms allot
: next-room-index roomlist-length @ 1+ ;
: next-room-addr roomlist-length @ rooms roomlist + ;
: last-room-index roomlist-length @ ;

: push-room ( xy -- )
  next-room-addr >r
       ( xy ) r@ room>coord !
  room-bag draw-bag
  dup roomid>name r@ room>name 2!
      roomid>desc r@ room>description 2!
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
      last-room-index dig-tunnel
      1
    else
      drop drop drop
      0
    then
  +loop ;

\ used for floodfill
#rooms STACK visit-stack-1
#rooms STACK visit-stack-2
value curr-visit
value next-visit
variable curr-depth

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

: erase-depths
  #rooms 1+ 1 DO
    0 I ix>room room>aux !
  LOOP ;

\ used for path finding
variable roompath-length
create roompath #rooms cells allot

: check-path-step ( room depth nesw-addr -- room' depth' )
  c@ ?dup 0 <> if
    ix>room
    dup room>aux @
    rot > if drop else nip then
    dup room>aux @
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

: debug:show-main-path
  roompath-length @ 0 do
    roompath I cells + @ room>name 2@ type cr
  loop ;

: in-main-path? ( x -- f )
  false
  #rooms 0 do
    over
    roompath I cells + @
    = or
  loop nip ;

variable openrooms-length
create openrooms #rooms cells allot

: store-open-rooms
  \ list rooms that are reachable (depth <> 0)
  0 openrooms-length !
  openrooms #rooms cells erase

  #rooms 1+ 1 do
    I ix>room room>aux @ 0 <> if
      I ix>room
      openrooms openrooms-length @ cells + !
      1 openrooms-length +!
    then
  loop ;

: debug:show-open-rooms
  openrooms-length @ 0 do
    openrooms I cells + @ room>name 2@ type cr
  loop ;

: random-open-room
  openrooms-length @ 1- random 1+ \ exclude start
  cells openrooms + @ ;

: random-room
  #rooms random ix>room ;

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
  random-open-room room>item c! ;

: place-gold
  \ add gold to any room
  random-room room>gold c! ;

: gen-maze
  \ clear the grid & room data
  grid #grid erase
  0 roomlist-length !
  roomlist #rooms rooms erase
  #rooms room-bag fill-bag

  \ create rooms and add depth info
  shape-maze
  annotate-depths

  find-deepest-room
    dup room>final 1 swap c! \ mark as final
    dup store-main-path
  drop

  \ TODO: consider placing this always at the end?
  1 place-lock

  \ find reachable rooms and place key
  erase-depths annotate-depths store-open-rooms
  1 place-item

  \ sprinkle some gold coins around
  2 random 1+ place-gold
  2 random 1+ place-gold ;

(
  Gameplay & Interacting with rooms
)

variable current-room
variable gold-coins
variable inventory

: show-map
  page
  #rooms 0 do
    #rooms 0 do
      I J xy>grid c@ case
        0 of ." ." endof
        current-room @ of ." i" endof
        ." X"
      endcase
    loop
    cr
  loop
  cr ." Seed: " initial-seed @ .
  key drop ;

: .alert ( c-addr u -- )
  page popup key drop ;

: center ( c-addr u -- c-addr u )
  SCRN_X_B over - 2/ spaces ;

: look-room ( -- )
  current-room @ ix>room

  dup room>name        2@ center type cr cr
  dup room>description 2@ pad place

  dup room>item c@
  ?dup 0 <> if
    bl pad cappend
    itemid>look pad append
  then

  dup room>gold c@
  ?dup 0 <> if
    bl pad cappend
    dup 1 = if
      drop
      s" A gold coin is laying on the floor." pad append
    else
      s" You see " pad append
      pad #append
      bl pad cappend
      s" gold coins on the floor." pad append
    then
  then

  bl pad cappend
  s" You can see " pad append

  0 swap
  dup room>north c@ 0 <> if swap 1+ swap then
  dup room>east  c@ 0 <> if swap 1+ swap then
  dup room>south c@ 0 <> if swap 1+ swap then
  dup room>west  c@ 0 <> if swap 1+ swap then

  over 1 = if s" a door " else s" doors " then pad append
  s" leading to the " pad append

  dup room>north c@ 0 <> if
    s" North" pad append
    swap 1- swap
    over 1 = if s"  and " pad append then
    over 1 > if s" , " pad append then
  then
  dup room>east  c@ 0 <> if
    s" East"  pad append
    swap 1- swap
    over 1 = if s"  and " pad append then
    over 1 > if s" , " pad append then
  then
  dup room>south c@ 0 <> if
    s" South" pad append
    swap 1- swap
    over 1 = if s"  and " pad append then
    over 1 > if s" , " pad append then
  then
  dup room>west  c@ 0 <> if
    s" West"  pad append
    swap 1- swap
    over 1 = if s"  and " pad append then
    over 1 > if s" , " pad append then
  then
  nip
  [char] . pad cappend

  \ dup room>lock-north c@ 0 <> if
  \   bl pad cappend
  \   s" The path to the North is blocked." pad append
  \ then

  \ dup room>lock-east c@ 0 <> if
  \   bl pad cappend
  \   s" The path to the East is blocked." pad append
  \ then

  \ dup room>lock-south c@ 0 <> if
  \   bl pad cappend
  \   s" The path to the South is blocked." pad append
  \ then

  \ dup room>lock-west c@ 0 <> if
  \   bl pad cappend
  \   s" The path to the West is blocked." pad append
  \ then

  pad count .wrapped
  drop ;

: go-room ( dir -- )
  current-room @ ix>room swap
  2dup room>lock-nesw c@
  ?dup 0 <> if
    itemid>need .alert
    2drop exit
  then
  room>nesw c@
  ?dup 0 <> if
    current-room ! page
  then ;

: room>any-lock@ ( room -- u )
  0
  over room>lock-north c@ or
  over room>lock-east  c@ or
  over room>lock-south c@ or
  over room>lock-west  c@ or
  nip ;

: take-item ( -- )
  current-room @ ix>room room>item
  dup c@ ?dup 0 <> if
    snd-take
    dup itemid>take .alert
    inventory c!
    0 swap c!
  else
    drop current-room @ ix>room room>gold
    dup c@ ?dup 0 <> if
      s" You take the gold coin" pad place
      dup 1 > if [char] s pad cappend then [char] . pad cappend
      snd-take
      pad count .alert
      ( n ) gold-coins +!
      0 swap c!
    else
      drop
      s" There is nothing you can take from this room." .alert
    then
  then ;

: drop-item
  inventory c@ ?dup 0 <> if
    snd-drop
    dup itemid>drop .alert
    current-room @ ix>room room>item c!
    0 inventory c!
  else
    s" You are not carrying any items right now." .alert
  then ;

: use-item
  inventory c@ ?dup 0 = if
    s" You don't have any items in your inventory." .alert
    exit
  then

  current-room @ ix>room room>any-lock@ ?dup 0= if
    drop \ todo use item name somehow?
    s" You can't use your item anywhere." .alert exit
  then

  over <> if
    drop \ todo use item name somehow?
    s" That item doesn't help you here." .alert exit
  then

  current-room @ ix>room
    dup room>lock-north 0 swap c!
    dup room>lock-east  0 swap c!
    dup room>lock-south 0 swap c!
    dup room>lock-west  0 swap c!
  drop

  0 inventory c!

  snd-unlock
  itemid>use .alert ;

: key>action
  case
    k-up     of 0 go-room endof
    k-right  of 1 go-room endof
    k-down   of 2 go-room endof
    k-left   of 3 go-room endof
    k-a      of take-item endof
    k-b      of drop-item endof
    k-start  of  use-item endof
    k-select of  show-map endof
  endcase ;

: play-maze
  1 current-room !
  0 gold-coins !
  0 inventory c!
  begin
    page
    look-room
    current-room @ ix>room room>final c@ 0 <> if
      cr cr
      ."   Hooray, you win!" cr
      ."    Gold found: " gold-coins ?
      snd-hooray
      exit \ back to menu
    then
    key key>action
  again ;
