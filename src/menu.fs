RAM variable menu-selection

: play-game
  splash-screen
  5 13 at-xy ." Assembling"
  6 14 at-xy ." Fortress"
  snd-launch
  menu-selection @ gen-maze
  show-intro run-maze show-outro show-gold ;

: menu-move ( n -- )
  snd-thud
  menu-selection @ +
  dup 0 < if 3 + then
  dup 2 > if 3 - then
  menu-selection ! ;

: .caret ( u -- c )
  menu-selection @ = if 9 else bl then emit ;

: show-size
  1 menu-selection !
  begin
    splash-screen
    6 13 at-xy 0 .caret ." Humble"
    6 14 at-xy 1 .caret ." Common"
    6 15 at-xy 2 .caret ." Mighty"
    key reseed case
      k-up   of -1 menu-move endof
      k-down of  1 menu-move endof
      k-a    of play-game exit endof
      k-b    of snd-thud  exit endof
    endcase
  again ;

: menu-confirm
  snd-confirm
  menu-selection @ case
    0 of show-size 0 menu-selection ! endof
    1 of show-help snd-thud endof
    2 of show-info snd-thud endof
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
