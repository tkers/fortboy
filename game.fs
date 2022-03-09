require textmachine-font.fs

require term.fs
require random.fs
require input.fs
require time.fs

require ./wrap.fs
require ./maze.fs

: main
  install-font
  init-term
  init-input
  \ w" Press any key to start!"
  \ key drop page
  \ utime seed !
  1234 seed !
  make-and-show
  key drop page
  w" You are in the kitchen of the white house. A table seems to have been used recently for the preparation of food. A passage leads to the west and a dark staircase can be seen leading upward. A dark chimney leads down and to the east is a small window which is open. On the table is an elongated brown sack, smelling of hot peppers. A bottle is sitting on the table. The glass bottle contains: A quantity of water"
  bye ;

\ debugging stats
cr cr
rom here          [host] ." ROM Used:      " 5 .r space ." bytes" cr    [target]
rom unused        [host] ." ROM Available: " 5 .r space ." bytes" cr cr [target]
ram 4096 unused - [host] ." Memory Used:      " 5 .r space ." bytes" cr [target]
ram unused        [host] ." Memory Available: " 5 .r space ." bytes" cr [target]
