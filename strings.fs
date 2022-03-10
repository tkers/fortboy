\ screen fits 20x18 = 360 chars
\ 1 kb should fill ~2.8 screens
1024 chars constant pad-size
create pad pad-size allot

: place ( c-addr1 u1 c-addr2 -- )
  over >r rot over char+ r> move c! ;

: count ( c-addr1 -- c-addr2 u )
  dup char+ swap c@ ;

: append ( c-addr1 u1 c-addr2 -- )
  over over >r >r
  count chars +
  swap chars move
  r> r@ c@ + r> c! ;

: cappend ( c c-addr )
  dup c@ char+ over c!
  count 1- chars + c! ;
