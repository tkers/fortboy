require gbhw.fs

SCRN_X_B constant max-width
SCRN_Y_B constant max-height

: .wrapped ( buf len -- )
  begin
    dup max-width >
  while
    over max-width 1+
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    type cr
    r> /string
    cursor-y @ max-height = if
      key drop partial-page
    then
  repeat type cr ;

:m w" postpone s" postpone .wrapped ; immediate
