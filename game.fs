require textmachine-font.fs

require term.fs
require random.fs
require input.fs

require ./maze.fs
require ./wrap.fs

: main
  install-font
  init-term
  init-input
  1234 seed !
  make-and-show
  key drop page
  w" You are in the kitchen of the white house. A table seems to have been used recently for the preparation of food. A passage leads to the west and a dark staircase can be seen leading upward. A dark chimney leads down and to the east is a small window which is open. On the table is an elongated brown sack, smelling of hot peppers. A bottle is sitting on the table. The glass bottle contains: A quantity of water"
  bye ;
