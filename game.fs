title: FortBoy
makercode: TJ

require ./src/textmachine-font.fs
require ./src/tiles.fs

require term.fs
require random.fs
require input.fs
require gbhw.fs

: reseed ( -- )
  seed @ 8 lshift
  rDIV c@ or
  seed ! ;

require ./src/emit-bytes.fs
require ./src/string-list.fs
require ./src/palette-fade.fs
require ./src/stars.fs
require ./src/partial-page.fs
require ./src/wrap.fs
require ./src/divider.fs
require ./src/maze.fs
require ./src/play.fs
require ./src/screens.fs
require ./src/startup.fs
require ./src/intro.fs
require ./src/outro.fs
require ./src/menu.fs

: init
  install-font
  install-2bit-tiles
  init-term
  init-input
  $7DFB seed ! ;

: main
  to-white 200 ms
  init
  scroll-down-animation
  0 menu-selection !
  begin show-menu again ;
