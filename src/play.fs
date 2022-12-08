require ./strings.fs
require ./popup.fs
require ./tunes.fs

variable current-room
variable gold-coins
variable inventory
variable moves

: center ( c-addr u -- c-addr u )
  SCRN_X_B over - 2/ spaces ;

: and-or-comma ( num -- num' )
  1-
  dup 1 = if s"  and " pad append then
  dup 1 > if s" , "    pad append then ;

: inc-if-door ( u nesw-addr -- u' )
  c@ 0<> if 1+ then ;

: look-room ( -- )
  current-room @
  dup ix>room

  \ room title and description
  dup room>name        2@ center type cr
  =====
  dup room>description 2@ pad place

  \ append item description
  dup room>item c@
  ?dup 0<> if
    bl pad cappend
    itemid>look pad append
  then

  \ append gold description
  dup room>gold c@
  ?dup 0<> if
    bl pad cappend
    dup 1 = if
      drop
      s" A gold coin lays on the floor." pad append
    else
      pad #append
      bl pad cappend
      s" gold coins lay on the floor." pad append
    then
  then

  \ append doors descriptions
  0
  over room>north inc-if-door
  over room>east  inc-if-door
  over room>south inc-if-door
  over room>west  inc-if-door

  bl pad cappend
  dup 1 = if
    rot dup 1 = if
      s" A path is leading to the " pad append
    else
      s" You can only go back " pad append
    then -rot
  else
    s" Paths lead to the " pad append
  then

  over room>north c@ 0<> if
    s" North" pad append
    and-or-comma
  then
  over room>east  c@ 0<> if
    s" East"  pad append
    and-or-comma
  then
  over room>south c@ 0<> if
    s" South" pad append
    and-or-comma
  then
  over room>west  c@ 0<> if
    s" West"  pad append
    \ and-or-comma
  then
  drop
  [char] . pad cappend

  pad count .wrapped
  drop drop ;

: go-room ( dir -- )
  current-room @ ix>room swap
  2dup room>lock-nesw c@
  ?dup 0<> if
    snd-block
    itemid>need popup
    2drop exit
  then
  room>nesw c@
  ?dup 0<> if
    current-room !
    snd-confirm
  else
    snd-thud
  then ;

: has-no-locks? ( room -- f )
  0
  over room>lock-north c@ or
  over room>lock-east  c@ or
  over room>lock-south c@ or
  over room>lock-west  c@ or
  nip 0= ;

: matches-any-lock? ( room item -- f )
  over room>lock-north c@ over = >r
  over room>lock-east  c@ over = >r
  over room>lock-south c@ over = >r
  over room>lock-west  c@ over = >r
  2drop
  r> r> r> r> or or or ;

: try-unlock ( room item lock-addr -- room item )
  over over c@ = if
    0 swap c!
  else drop then ;

: take-item ( -- )
  current-room @ ix>room room>item
  dup c@ ?dup 0<> if
    inventory c@ tuck
    ?dup 0<> if
      snd-drop
      itemid>drop popup
    then
    snd-take
    dup itemid>take popup
    inventory c!
    swap c! \ switch inventory<>room
  else
    drop current-room @ ix>room room>gold
    dup c@ ?dup 0<> if
      s" You take the gold coin" pad place
      dup 1 > if [char] s pad cappend then [char] . pad cappend
      snd-take
      pad count popup
      ( n ) gold-coins +!
      0 swap c!
    else
      drop snd-thud
      s" There is nothing worth taking in this room." popup
    then
  then ;

: use-item
  inventory c@ ?dup 0= if
    snd-thud
    s" You do not have any items right now." popup
    exit
  then

  current-room @ ix>room has-no-locks? if
    drop \ todo use item name somehow?
    snd-thud
    s" You scratch your head. Your item is of no use here." popup exit
    \ s" You scratch your head. " pad place
    \ itemid>name pad append
    \ s"  is of no use here." pad append
    \ pad count popup exit
  then

  current-room @ ix>room over matches-any-lock? invert if
    drop \ todo use item name somehow?
    snd-thud
    s" Your item does not help you here. Let's keep looking!" popup exit
    \ itemid>name pad place
    \ s"  does not help you here. Let's keep looking!" pad append
    \ pad count popup exit
  then

  current-room @ ix>room swap \ room item

  over room>lock-north try-unlock
  over room>lock-east  try-unlock
  over room>lock-south try-unlock
  over room>lock-west  try-unlock

  nip

  0 inventory c!

  snd-unlock
  itemid>use popup ;

: key>action
  case
    k-up     of 0 go-room endof
    k-right  of 1 go-room endof
    k-down   of 2 go-room endof
    k-left   of 3 go-room endof
    k-a      of take-item endof
    k-b      of  use-item endof
  endcase ;

: win?
  current-room @ ix>room room>final c@ 0<> ;

: run-maze
  1 current-room !
  0 gold-coins !
  0 moves !
  0 inventory c!
  begin
    page look-room
    key key>action
    1 moves +!
  win? until ;
