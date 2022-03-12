ROM
create bl-bl-3 bl c, bl c, 3 c,
here create popup-frame
bl c, bl c,  5 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  1 c,  6 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  3 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,  4 c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c, bl c,
bl c, bl c,  7 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  2 c,  8 c,
here swap - constant popup-frame-size
RAM

: .tiny ( buf len -- )
  begin
    dup 14 >
  while
    over 14
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    begin 1- 2dup + c@ bl <> until
    1+ type cr bl-bl-3 3 type
    r> /string
  repeat type ;

: popup
  popup-frame popup-frame-size
  0 3 at-xy type
  3 4 at-xy .tiny ;
