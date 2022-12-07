title: FortBoy
makercode: TJ

require ./src/textmachine-font.fs
require ./src/tiles.fs

require term.fs
require random.fs
require input.fs
require gbhw.fs

RAM variable initial-seed

: reseed ( -- )
  seed @ 8 lshift
  rDIV c@ or
  dup initial-seed !
  seed ! ;

require ./src/emit-bytes.fs
require ./src/string-list.fs
require ./src/stars.fs
require ./src/partial-page.fs
require ./src/wrap.fs
require ./src/divider.fs
require ./src/maze.fs
require ./src/play.fs
require ./src/screens.fs
require ./src/intro.fs
require ./src/outro.fs
require ./src/menu.fs

: init
  install-font
  install-2bit-tiles
  init-term
  init-input
  $7DFB dup seed ! initial-seed ! ;

: main
  init
  0 menu-selection !
  begin show-menu again ;
