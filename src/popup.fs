ROM
create bl-bl-3 emit-bytes( 32 32 3 )
here create popup-frame
emit-bytes( 32 32  5  1  1  1  1  1  1  1  1  1  1  1  1  1  1  6 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  3 32 32 32 32 32 32 32 32 32 32 32 32 32 32  4 32 32 32 32 32 32 32 32 32 32 32 32 32 32 )
emit-bytes( 32 32  7  2  2  2  2  2  2  2  2  2  2  2  2  2  2  8 )
here swap - constant popup-frame-size
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
    bl-bl-3 3 type 1+ type cr
    r> /string
    cursor-y @ 14 = if
      key drop .frame
    then
  repeat bl-bl-3 3 type type ;

: popup ( c-addr u -- ) page .frame .tiny key drop ;
