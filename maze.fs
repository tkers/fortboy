require struct.fs
require stack.fs
require ./strings.fs
require ./bag.fs
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

\ bag to draw random ids from
#rooms bag: room-bag

: id>name
  case
     0 of s" Treasury"       endof
     1 of s" Icey Room"      endof
     2 of s" Library"        endof
     3 of s" Observatory"    endof
     4 of s" Cellar"         endof
     5 of s" Boiler Room"    endof
     6 of s" Dimly Lit Room" endof
     7 of s" Bat Cave"       endof
     8 of s" Art Gallery"    endof
     9 of s" Catacombs"      endof
  endcase ;

: id>desc
  case
     0 of s" You squint your eyes, as you get blinded by the sudden brightness of the golden walls around you, adorned by thousands of gemstones."   endof
     1 of s" A shiver runs along your spine as the cold wind sweeps through the room covered in ice and snow. You nearly slip on the surface, but manage to regain your balance."   endof
     2 of s" Piles of books after piles of books cover every last corner of the library. If only you had the time to sit in the armchair and dive into the adventures hidden between the dusty covers." endof
     3 of s" The sun, the moon and every last planet of our solar system greet you, as the view to the observatory opens before your eyes. Even the ceiling sparkles with thousands of stars." endof
     4 of s" The sound of bubbling reaches your ears and you see a cauldron in the middle of the room, surrounded by herbs and bones of creatures you've never seen before."  endof
     5 of s" A loud roar and a puff stops you in your tracks. Is it a zombie? A demon? No, just the boiler room of the castle. Phew."   endof
     6 of s" The room before you is dark as the night, the only light source in it a chandelier in which a single candle flickers faintly. Something about this feels sinister." endof
     7 of s" Soft squeaks and the rustling of something leathery reaches your ears. Yet the room seems empty... Apart from the hundred bats sleeping right above your head, that is. Please be fruit bats, please be fruit bats..." endof
     8 of s" A room full of life-sized portraits piques your curiosity. The detail in each painting is incredible and you feel tempted to reach forward to feel the dried paint beneath your fingers."  endof
     9 of s" Something crunches beneath your feet and you stagger. The floor is uneven-the pile of bones covering every inch not offering a stable ground to walk on."   endof
    \ 10 of s" Music dances across the room, its sweet melody reminding you of something that you can’t quite place your finger on." endof
    \ 11 of s" Something colorful flutters past your eye and you follow the sight. A soft smile rises on your lips as you see the dozens of butteflies flying around the room, otherwise covered in flowers unlike any you have ever seen."  endof
    \ 12 of s" The feeling of someone watching you sends goosebumps to run along your skin. It doesn’t take long for you to spot the pair of eyes: the doll in the middle of the room following each of your steps. No thank you!" endof
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
  room-bag draw-bag
  dup id>name r@ room>name 2!
      id>desc r@ room>description 2!
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

: place-key-item
  \ add item to any reachable room
  random-open-room room>item 2! ;

: place-ext-item
  \ add item to any room
  random-room room>item 2! ;

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
  123 place-lock

  \ find reachable rooms and place key
  erase-depths annotate-depths store-open-rooms
  s" a rusty key" place-key-item
  s" a gold coin" place-ext-item ;

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
  cr ." Seed: " initial-seed @ .
  key drop ;

: .alert ( c-addr u -- )
  page .wrapped key drop ;

: center ( c-addr u -- c-addr u )
  SCRN_X_B over - 2/ spaces ;

: look-room ( -- )
  current-room @ ix>room

  dup room>name        2@ center type cr cr
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

  dup room>lock-north c@ 0 <> if
    bl pad cappend
    s" The path to the North is blocked." pad append
    \ s" You need item #" pad append
    \ dup room>lock-north c@ pad #append
    \ bl pad cappend
    \ s" to continue." pad append
  then

  dup room>lock-east c@ 0 <> if
    bl pad cappend
    s" The path to the East is blocked." pad append
    \ s" You need item #" pad append
    \ dup room>lock-east c@ pad #append
    \ bl pad cappend
    \ s" to continue." pad append
  then

  dup room>lock-south c@ 0 <> if
    bl pad cappend
    s" The path to the South is blocked." pad append
    \ s" You need item #" pad append
    \ dup room>lock-south c@ pad #append
    \ bl pad cappend
    \ s" to continue." pad append
  then

  dup room>lock-west c@ 0 <> if
    bl pad cappend
    s" The path to the West is blocked." pad append
    \ s" You need item #" pad append
    \ dup room>lock-west c@ pad #append
    \ bl pad cappend
    \ s" to continue." pad append
  then

  pad count .wrapped
  drop ;

: go-room ( dir -- )
  current-room @ ix>room swap
  2dup room>lock-nesw c@
  ?dup 0 <> if
    s" This path is blocked. You can use item #" pad place
    pad #append
    bl pad cappend
    s" to unlock it." pad append
    pad count .alert
    2drop exit
  then
  room>nesw c@
  ?dup 0 <> if
    current-room ! page
  then ;

: has-locks? ( room -- f )
  false
    over room>lock-north c@ 0 <> or
    over room>lock-east c@ 0 <> or
    over room>lock-south c@ 0 <> or
    over room>lock-west c@ 0 <> or
  nip ;

: take-item ( -- )
  current-room @ ix>room room>item
  dup 2@ ?dup 0 <> if
    s" You take " pad place
    pad append
    bl pad cappend
    s" from the room." pad append

    dup 2@ inventory place
    0 0 rot 2! \ clear room item

    pad count .alert
  else
    2drop
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

: use-item
  inventory @ 0 = if
    s" You don't have any item you can use here." .alert
    exit
  then

  current-room @ ix>room has-locks? invert if
    s" There is nothing you can use " pad place
    inventory count pad append
    bl pad cappend
    s" on in this room." pad append
    pad count .alert exit
  then

  current-room @ ix>room
    dup room>lock-north 0 swap c!
    dup room>lock-east  0 swap c!
    dup room>lock-south 0 swap c!
    dup room>lock-west  0 swap c!
  drop

  s" You use " pad place
  inventory count pad append
  bl pad cappend
  s" to unlock all doors in this room." pad append

  pad count .alert ;

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
  inventory 20 chars erase
  begin
    page
    look-room
    current-room @ ix>room room>final c@ 0 <> if
      cr cr
      ."   Hooray, you win!"
      snd-hooray
      exit \ back to menu
    then
    key key>action
  again ;
