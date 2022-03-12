\ bag to draw random item ids from
\ 1 bag: room-bag

: itemid>look
  1- case
    0 of s" You spot a rusty key laying on the floor." endof
  endcase ;

: itemid>take
  1- case
    0 of s" You pick up the rusty key, and place it in your pocket." endof
  endcase ;

: itemid>drop
  1- case
    0 of s" You drop the key on the floor." endof
  endcase ;

: itemid>use
  1- case
    0 of s" You use the key to unlock the heavy door." endof
  endcase ;

: itemid>need
  1- case
    0 of s" You try to open the door, but it won't open. The rusty lock looks too difficult for you to pick." endof
  endcase ;
