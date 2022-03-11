require struct.fs
require stack.fs
require ./strings.fs
require ./tunes.fs

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
  2 cells field room>item
  1 chars field room>final
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

variable leafrooms-length
create leafrooms #rooms cells allot

: store-leaf-rooms
  \ list rooms that are not part of the victory path
  0 leafrooms-length !
  leafrooms #rooms cells erase

  #rooms 1+ 2 do
    I ix>room in-main-path? invert if
      I ix>room
      leafrooms leafrooms-length @ cells + !
      1 leafrooms-length +!
    then
  loop ;

: debug:show-leaf-rooms
  leafrooms-length @ 0 do
    leafrooms I cells + @ room>name 2@ type cr
  loop ;

: has-leaf-rooms?
  leafrooms-length @ 0 <> ;

: random-leaf-room
  leafrooms-length @ random
  cells leafrooms + @ ;

: random-room
  has-leaf-rooms? if \ prefer leaf
    random-leaf-room
  else \ exclude start
    #rooms 1- random 2 + ix>room
  then ;

: place-items
  \ add item to random room
   s" a rusty key" random-room room>item 2! ;

: gen-maze
  \ clear the grid & room data
  grid #grid erase
  0 roomlist-length !
  roomlist #rooms rooms erase

  \ create rooms and add depth info
  shape-maze
  annotate-depths

  find-deepest-room
    dup room>final 1 swap c! \ mark as finial
    dup store-main-path
  drop

  store-leaf-rooms

  \ get random room in path
  roompath-length @ 1- random 1+
  dup     cells roompath + @
  swap 1- cells roompath + @ \ room room+1
  over room>coord @
  swap room>coord @ coords>nesw \ room dir
  room>lock-nesw \ lock-addr
  123 swap c! \ add the lock

  place-items ;

(
  Gameplay & Interacting with rooms
)

variable current-room
create inventory 20 chars allot

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
  key drop ;

: .alert ( c-addr u -- )
  page .wrapped key drop ;

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

  bl pad cappend
  dup room>lock-north c@ 0 <> if s" The path to the North is blocked." pad append then
  dup room>lock-east  c@ 0 <> if s" The path to the East is blocked."  pad append then
  dup room>lock-south c@ 0 <> if s" The path to the South is blocked." pad append then
  dup room>lock-west  c@ 0 <> if s" The path to the West is blocked."  pad append then

  pad count .wrapped
  drop ;

: go-room ( dir -- )
  current-room @ ix>room
  swap room>nesw c@
  ?dup 0 <> if
    current-room ! page
  then ;

: take-item ( -- )
  current-room @ ix>room room>item
  dup 2@ dup 0 <> if
    s" You take " pad place
    pad append
    bl pad cappend
    s" from the room." pad append

    dup 2@ inventory place
    0 0 rot 2! \ clear room item

    pad count .alert
  else
    2drop drop
    s" There is nothing you can take from this room." .alert
  then ;

: drop-item
  inventory @ 0 <> if
    s" You drop " pad place
    inventory count 2dup pad append
    bl pad cappend
    s" on the floor." pad append

    current-room @ ix>room room>item 2!
    0 inventory !

    pad count .alert
  else
    s" You are not carrying any items right now." .alert
  then ;

: key>action
  case
    k-up     of 0 go-room endof
    k-right  of 1 go-room endof
    k-down   of 2 go-room endof
    k-left   of 3 go-room endof
    k-a      of take-item endof
    k-b      of drop-item endof
    k-select of  show-map endof
  endcase ;

: play-maze
  1 current-room !
  inventory 20 chars erase
  begin
    page
    look-room
    current-room @ ix>room room>final c@ 0 <> if
      cr cr cr
      ."   Hooray, you win!"
      snd-hooray
    then
    key key>action
  again ;
