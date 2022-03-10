require textmachine-font.fs

require term.fs
require random.fs
require input.fs
require time.fs

require ./wrap.fs
require ./maze.fs

: wait-for-key key drop ;

: init
  install-font
  init-term
  init-input
  \ w" Press any key to start!"
  \ wait-for-key page
  \ utime seed ! ;
  1234 seed ! ;

: main
  init

  gen-maze
  show-maze wait-for-key
  (
     ..........
     x.........
     xxxxs.....
     .xxxx.....
     ..........
  )

  play-maze
  bye ;

\ debugging stats
cr cr
rom here          [host] ." ROM Used: " 5 .r space ." bytes"    [target]
rom here          100 * 32768 / [host] ."  (" 1 .r ." %)"    cr [target]
rom unused        [host] ." ROM Left: " 5 .r space ." bytes" cr [target]
cr
ram 4096 unused - [host] ." RAM Used: " 5 .r space ." bytes"    [target]
ram 4096 unused - 100 * 4096 / [host] ."  (" 1 .r ." %)"     cr [target]
ram unused        [host] ." RAM Left: " 5 .r space ." bytes" cr [target]
