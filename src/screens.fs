: splash-screen
  0 0 at-xy
  stars stars-len type
  [ _SCRN0 SCRN_VY_B 11 * + ]L [ SCRN_VX_B SCRN_VY_B 11 - * ]L blank
  0 11 at-xy ;

: show-title
  splash-screen
  4 14 at-xy ." Press Start!"
  key drop ;

: show-help
  splash-screen
  1 12 at-xy ." Collect gold and"
  3 13 at-xy ." escape the fort!"
  6 15 at-xy ." [+]Walk"
  3 16 at-xy ." [A]Take [B]Use"
  4 16 at-xy 29 emit
  12 16 at-xy 28 emit
  key drop ;

: show-info
  splash-screen
  2 12 at-xy ." Code and stories"
  2 13 at-xy ." by Tijn & Jasmin"
  1 15 at-xy ." Font by @Polyducks"
  1 16 at-xy ." Running on GBForth"
  key drop ;
