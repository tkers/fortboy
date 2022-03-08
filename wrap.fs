\ wrap text

20 constant max-line
18 constant max-height
variable lineno

: .wrapped ( buf len -- )
  0 lineno !
  begin
    dup max-line >
  while
    over max-line
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    begin 1- 2dup + c@ bl <> until
    lineno @ max-height = if
      0 lineno ! key drop page
    then
    1 lineno +!
    1+ type cr
    r> /string
  repeat type cr ;

: strip-nl ( buf len -- )
  bounds do
    I c@ 10 = if bl I c! then
  loop ;

: main
  s\" You are in the kitchen of the white house. A table seems to have been used recently for the preparation of food. A passage leads to the west and a dark staircase can be seen leading upward. A dark chimney leads down and to the east is a small window which is open.\nOn the table is an elongated brown sack, smelling of hot peppers.\nA bottle is sitting on the table.\nThe glass bottle contains:\nA quantity of water"
  2dup strip-nl
  .wrapped
  bye ;

main
