require compat.fs

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

:m w" postpone s" postpone .wrapped ; immediate
