[host]
: emit-bytes(
  begin
  parse-name
  \ dup 0<> while
  2dup s" )" str= invert while
    dup 0= abort" Unexpected end of line"
    s>number? invert abort" Not a valid number"
    d>s dup $00 < over $ff > or abort" Not a valid byte value"
    [target] c, [host]
  repeat 2drop ;
[target]
