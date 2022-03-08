\ wrap text

20 constant max-line
: .wrapped ( buf len -- )
  begin
    dup max-line >
  while
    over max-line
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    begin 1- 2dup + c@ bl <> until
    1+ type cr
    r> /string
  repeat type cr ;

: strip-nl ( buf len -- )
  bounds do
    I c@ 10 = if bl I c! then
  loop ;

: main
  s\" WEST OF HOUSE\nYou are standing in an open field west of a white house, with a boarded front door.\nThere is a small mailbox here."
  2dup strip-nl
  .wrapped
  bye ;

main
