RAM variable menu-selection

: play-game
  splash-screen
  5 13 at-xy ." Assembling"
  6 14 at-xy ." Fortress"
  gen-maze
  show-intro run-maze show-outro ;

: menu-move ( n -- )
  menu-selection @ +
  0 max 2 min
  menu-selection ! ;

: menu-confirm
  menu-selection @ case
    0 of play-game endof
    1 of show-help endof
    2 of show-info endof
  endcase ;

: .caret ( u -- c )
  menu-selection @ = if 9 else bl then emit ;

: show-menu
  splash-screen
  7 13 at-xy 0 .caret ." Play"
  7 14 at-xy 1 .caret ." Help"
  7 15 at-xy 2 .caret ." Info"
  key reseed case
    k-up   of -1 menu-move endof
    k-down of  1 menu-move endof
    k-a    of menu-confirm endof
  endcase ;
