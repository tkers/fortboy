: /string ( c-addr1 u1 n -- c-addr2 u2 )
  tuck - >r + r> ;

: bounds ( addr u – addr+u addr )
  over + swap ;
