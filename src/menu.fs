: splash-screen
  0 0 at-xy
  stars stars-len type
  [ _SCRN0 SCRN_VY_B 11 * + ]L [ SCRN_VX_B SCRN_VY_B 11 - * ]L blank
  0 11 at-xy ;

: show-help
  splash-screen
  1 12 at-xy ." Collect gold and"
  3 13 at-xy ." escape the fort!"
  0 15 at-xy ."      [+] Walk"
  0 16 at-xy ."  [A] Take [B] Use"
  key drop ;
