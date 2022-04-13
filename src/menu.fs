RAM variable menu-selection

: play-game
  splash-screen
  5 13 at-xy ." Assembling"
  6 14 at-xy ." Fortress"
  menu-selection @ gen-maze
  show-intro run-maze show-outro show-gold ;

: menu-move ( n -- )
  menu-selection @ +
  0 max 2 min
  menu-selection ! ;

: .caret ( u -- c )
  menu-selection @ = if 9 else bl then emit ;

: show-size
  begin
    splash-screen
    6 13 at-xy 0 .caret ." Humble"
    6 14 at-xy 1 .caret ." Common"
    6 15 at-xy 2 .caret ." Mighty"
    key reseed case
      k-up   of -1 menu-move endof
      k-down of  1 menu-move endof
      k-a    of play-game exit endof
      k-b    of           exit endof
    endcase
  again ;

: menu-confirm
  menu-selection @ case
    0 of show-size endof
    1 of show-help endof
    2 of show-info endof
  endcase ;

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
