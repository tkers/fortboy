: .tiny ( buf len -- )
  begin
    dup 14 >
  while
    over 14
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    begin 1- 2dup + c@ bl <> until
    1+ type cr space space 3 emit
    r> /string
  repeat type ;

create popup-pad 1024 chars allot

: popup
  0 popup-pad !
  bl popup-pad cappend   bl popup-pad cappend 5 popup-pad cappend 14 0 do 1 popup-pad cappend loop 6 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend 3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend  3 popup-pad cappend s"               " popup-pad append      4 popup-pad cappend   14 0 do bl popup-pad cappend loop
  bl popup-pad cappend   bl popup-pad cappend 7 popup-pad cappend 14 0 do 2 popup-pad cappend loop 8 popup-pad cappend
  popup-pad count
  0 3 at-xy type
  3 4 at-xy .tiny ;
