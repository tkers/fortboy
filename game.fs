title: GrueBoy
makercode: TK

require textmachine-font.fs

require term.fs
require random.fs
require input.fs
require time.fs

variable initial-seed

require ./stars.fs
require ./wrap.fs
require ./maze.fs
require ./intro.fs
require ./outro.fs

: wait-for-key key drop ;
: reseed
  utime 1 max
  dup seed !
  initial-seed ! ;

: init
  install-font
  init-term
  init-input
  1234 dup seed ! initial-seed ! ;

: main
  init
  stars stars-len type
  4 14 at-xy ." Press Start"
  wait-for-key reseed
  2 14 at-xy ." Building Castle"
  gen-maze
  intro
  run-maze
  outro ;

\ force emit all code to get correct ROM size
' main drop

\ debugging stats
cr cr
rom here          [host] ." ROM Used: " 5 .r space ." bytes"    [target]
rom here          100 * 32768 / [host] ."  (" 1 .r ." %)"    cr [target]
rom unused        [host] ." ROM Left: " 5 .r space ." bytes" cr [target]
cr
ram 4096 unused - [host] ." RAM Used: " 5 .r space ." bytes"    [target]
ram 4096 unused - 100 * 4096 / [host] ."  (" 1 .r ." %)"     cr [target]
ram unused        [host] ." RAM Left: " 5 .r space ." bytes" cr [target]
