\ Helpers to define a compact list of strings with minimal overhead
\
\ Define the list like this:
\
\   start-strings
\     -" First"
\     -" Second"
\     -" Third"
\   end-strings: my-strings
\
\ And use the list like this:
\
\   0 my-strings type
\   1 my-strings type
\   2 my-strings type

:m start-strings ( -- 0 ) 0 ;

:m -" ( u 'ccc"' -- u+1 )
  1+ here swap
  [char] " [host] parse [target]
  tuck mem, swap ;

\ this adds a bit of overhead in cycles
\ but reduces duplicated CALLs in the ROM
\ saving some 170 bytes of space
: (end-strings:does>) swap 2* cells + 2@ ;

:m end-strings: ( u "name" -- )
  create 0 [host] do [target] , , [host] loop [target]
  does> (end-strings:does>) ;
