title: FortBoy
makercode: TJ

require ./src/textmachine-font.fs

require term.fs
require random.fs
require input.fs
require time.fs

variable initial-seed

require ./src/emit-bytes.fs
require ./src/string-list.fs
require ./src/stars.fs
require ./src/partial-page.fs
require ./src/menu.fs
require ./src/wrap.fs
require ./src/divider.fs
require ./src/maze.fs
require ./src/intro.fs
require ./src/outro.fs

: reseed ( u -- )
  8 lshift
  rDIV c@ or
  dup seed !
  initial-seed ! ;

: init
  install-font
  init-term
  init-input
  1234 dup seed ! initial-seed ! ;

: title-screen
  splash-screen
  7 13 at-xy ." Press"
  6 14 at-xy ." any key"
  key reseed ;

: play-game
  splash-screen
  5 13 at-xy ." Assembling"
  6 14 at-xy ." Fortress"
  gen-maze
  show-intro run-maze show-outro ;

: main
  init
  begin
    title-screen
    play-game
  again ;
