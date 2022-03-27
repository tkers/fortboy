: splash-screen
  0 0 at-xy
  stars stars-len type
  [ _SCRN0 SCRN_VY_B 11 * + ]L [ SCRN_VX_B SCRN_VY_B 11 - * ]L blank
  0 11 at-xy ;

: show-title
  splash-screen
  7 13 at-xy ." Press"
  6 14 at-xy ." any key"
  key drop ;

: show-help
  splash-screen
  1 12 at-xy ." Collect gold and"
  3 13 at-xy ." escape the fort!"
  6 15 at-xy ." [+]Walk"
  2 16 at-xy ." [A]Take [B]Use"
  3 16 at-xy 29 emit
  11 16 at-xy 28 emit
  key drop ;
