require time.fs

: splash-screen
  0 0 at-xy
  stars stars-len type
  [ _SCRN0 SCRN_VY_B 11 * + ]L [ SCRN_VX_B SCRN_VY_B 11 - * ]L blank
  0 11 at-xy ;

\ Prepare A and B with inverted colours
rom create A/B
s" [A]Take [B]Use" mem,
here A/B - constant A/B-len
29 A/B 1 + c!
28 A/B 9 + c!

: show-help
  splash-screen
  1 12 at-xy ." Collect gold and"
  3 13 at-xy ." escape the fort!"
  6 15 at-xy ." [+]Walk"
  3 16 at-xy A/B A/B-len type
  wait-a/b ;

: show-info
  splash-screen
  2 12 at-xy ." Code and stories"
  2 13 at-xy ." by Tijn & Jasmin"
  1 15 at-xy ." Font by @Polyducks"
  1 16 at-xy ." Running on GBForth"
  wait-a/b ;

: show-gold
  to-black splash-screen 1200 ms from-black
  2 13 at-xy ." You made it out!" 1200 ms
  3 14 at-xy ." Gold coins: " gold-coins ?
  snd-hooray
  800 ms
  2 16 at-xy ." (used " moves ? ." moves)"
  begin key k-start = until ;
