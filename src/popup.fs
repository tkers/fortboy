ROM
create popup-frame
emit-bytes( 31 31  5  1  1  1  1  1  1  1  1  1  1  1  1  1  1  6 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
create popup-indent
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 31 31 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 31 31  7  2  2  2  2  2  2  2  2  2  2  2  2  2  2  8 )
here popup-frame - constant popup-frame-size
RAM

: .frame
  popup-frame popup-frame-size 0 3 at-xy type
  0 4 at-xy ;

: .tiny ( buf len -- )
  begin
    dup 14 >
  while
    over 15
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    begin 1- 2dup + c@ bl <> until
    popup-indent 3 type 1+ type cr
    r> /string
    cursor-y @ 14 = if
      ?from-white key drop .frame
    then
  repeat popup-indent 3 type type ;

: popup ( c-addr u -- )
  to-white
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L 31 fill
  .frame .tiny ?from-white
  key drop to-white ;
