\ bag to draw random item ids from
2 bag: item-bag
: fill-item-bag 2 item-bag fill-bag ;

: itemid>look
  1- case
    0 of s" You spot a rusty key laying on the floor." endof
    1 of s" A sword is leaning against the wall." endof
  endcase ;

: itemid>take
  1- case
    0 of s" You pick up the rusty key, and place it in your pocket." endof
    1 of s" You take the sword. Oof, it's a lot heavier than you thought." endof
  endcase ;

: itemid>drop
  1- case
    0 of s" You drop the key on the floor." endof
    1 of s" You carefully place the sword against the wall. You might need it later still." endof
  endcase ;

: itemid>use
  1- case
    0 of s" You use the key to unlock the heavy door." endof
    1 of s" With a swift strike of your sword, you kill the rock troll." endof
  endcase ;

: itemid>need
  1- case
    0 of s" You try to open the door, but it won't open. The rusty lock looks too difficult for you to pick." endof
    1 of s" You approach the door, but a rock troll is blocking the way. Better not wake it up..." endof
  endcase ;
