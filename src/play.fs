require ./strings.fs
require ./popup.fs
require ./tunes.fs

variable current-room
variable gold-coins
variable inventory

: center ( c-addr u -- c-addr u )
  SCRN_X_B over - 2/ spaces ;

: look-room ( -- )
  current-room @
  dup ix>room

  dup room>name        2@ center type cr
  =====
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
      s" A gold coin lays on the floor." pad append
    else
      pad #append
      bl pad cappend
      s" gold coins lay on the floor." pad append
    then
  then

  0 swap
  dup room>north c@ 0 <> if swap 1+ swap then
  dup room>east  c@ 0 <> if swap 1+ swap then
  dup room>south c@ 0 <> if swap 1+ swap then
  dup room>west  c@ 0 <> if swap 1+ swap then

  bl pad cappend
  over 1 = if
    rot dup 1 = if
      s" A path is leading to the " pad append
    else
      s" You can only go back " pad append
    then -rot
  else
    s" Paths lead to the " pad append
  then

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

  pad count .wrapped
  drop drop ;

: go-room ( dir -- )
  current-room @ ix>room swap
  2dup room>lock-nesw c@
  ?dup 0 <> if
    snd-block
    itemid>need popup
    2drop exit
  then
  room>nesw c@
  ?dup 0 <> if
    current-room ! page
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
  over room>lock-east c@ over = >r
  over room>lock-south c@ over = >r
  over room>lock-west c@ over = >r
  2drop
  r> r> r> r> or or or ;

: take-item ( -- )
  current-room @ ix>room room>item
  dup c@ ?dup 0 <> if
    inventory c@ tuck
    ?dup 0 <> if
      snd-drop
      itemid>drop popup
    then
    snd-take
    dup itemid>take popup
    inventory c!
    swap c! \ switch inventory<>room
  else
    drop current-room @ ix>room room>gold
    dup c@ ?dup 0 <> if
      s" You take the gold coin" pad place
      dup 1 > if [char] s pad cappend then [char] . pad cappend
      snd-take
      pad count popup
      ( n ) gold-coins +!
      0 swap c!
    else
      drop
      s" There is nothing worth taking in this room." popup
    then
  then ;

: drop-item
  inventory c@ ?dup 0 <> if
    snd-drop
    dup itemid>drop popup
    current-room @ ix>room room>item c!
    0 inventory c!
  else
    s" You do not have any items right now." popup
  then ;

: use-item
  inventory c@ ?dup 0 = if
    s" You do not have any items right now." popup
    exit
  then

  current-room @ ix>room has-no-locks? if
    drop \ todo use item name somehow?
    s" You scratch your head. Your item is of no use here." popup exit
  then

  current-room @ ix>room over matches-any-lock? invert if
    drop \ todo use item name somehow?
    s" Your item does not help you here. Let's keep looking!" popup exit
  then

  current-room @ ix>room swap \ room item

  over room>lock-north c@
  over = if
    over room>lock-north 0 swap c!
  then

  over room>lock-east c@
  over = if
    over room>lock-east 0 swap c!
  then

  over room>lock-south c@
  over = if
    over room>lock-south 0 swap c!
  then

  over room>lock-west c@
  over = if
    over room>lock-west 0 swap c!
  then

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
    k-select of drop-item endof
    k-start  of show-help endof
  endcase ;

: win?
  current-room @ ix>room room>final c@ 0<> ;

: run-maze
  1 current-room !
  0 gold-coins !
  0 inventory c!
  begin
    page
    win? if exit then
    look-room
    key key>action
  again ;
