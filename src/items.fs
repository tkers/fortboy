RAM

\ bag to draw random item ids from
8 bag: item-bag
: fill-item-bag 8 item-bag fill-bag ;

ROM

start-strings
  -" You step on something hard: a rusty key."
  -" A sword is leaning against the wall."
  -" An unlit torch lays long forgotten."
  -" You nearly trip over a heavy crowbar."
  -" A black cat is napping in the corner."
  -" Maah! A bratty goat startles you."
  -" A gemstone sparkles in the corner."
  -" A box of dynamite awaits its finder."
end-strings: item-look[]

: itemid>look 1- item-look[] ;

start-strings
  -" You pick up the rusty key, almost regretting it as you see the traces of red on your hands. Hopefully it'll be useful."
  -" You take the sword. Oof, it's a lot heavier than you thought."
  -" You wrap your fingers around the torch, relieved to find out it's still dry and ready for lighting up."
  -" You pick up the heavy crowbar. Fighting crime one crowbar at a time!"
  -" Despite it never having worked in the past, you crouch down and 'pspsps' from the bottom of your heart. To your surprise, the cat happily runs towards you, your new friend purring loudly."
  -" You pull out a half-eaten apple from your pocket, and offer it to the goat. With a hungry 'maaah', the animal decides to follow you."
  -" You pick up the gemstone and find yourself hoping you'd get to keep it for yourself. In its purple sparkle it'd surely look beautiful on your bookshelf."
  -" You pick up the box of dynamite. Better stay far away from torches for now."
end-strings: item-take[]

: itemid>take 1- item-take[] ;

start-strings
  -" You drop the rusty key on the floor again."
  -" You carefully place the sword against the wall. You might still need it later."
  -" You place down the torch next to the wall. You prefer the darkness, anyway."
  -" You leave the crowbar behind. Until we meet again, trusty tool!"
  -" The cat suddenly sprints away. I guess it was time for dinner?"
  -" You toss your half-eaten apple away, and the goat immediately loses interest in you."
  -" With a heavy heart, you part ways with the sparkling gemstone."
  -" You gently put down the box of dynamite. Easy does it..."
end-strings: item-drop[]

: itemid>drop 1- item-drop[] ;

start-strings
  -" You slide the rusty key into the lock. With a click and a creak the door opens."
  -" With a swift strike of your sword, you defeat the rock troll."
  -" With your torch lit up, you're ready to conquer the darkness. Watch out, lurking monsters! I'm not afraid anymore."
  -" The flimsy door offers no match to your crowbar, and the wood gives in and shatters underneath the tool's force. Watch and learn, burglars!"
  -" You gently slide your cat in front of the demonic mouse. The rodent is confident, but all it takes for your cat to defeat it is a gentle push with its paw. The way is cleared and your furry friend falls back asleep in the corner of the room, purring loudly."
  -" The dragon wakes up with a huff, and you toss the goat at the yawning creature. You'll be able to slip past it now that the dragon is having its breakfast served in bed. Sorry, goat! Its offended 'maaah' is the last of it you'll ever hear."
  -" You take a curious step closer to the door and pull out the heavy gemstone. The sparkling gem slides seamlessly to the spot reserved for it, and in its purple sheen it completes the elegant look. The door silently slides open."
  -" You set down the sticks of dynamite, light the fuse and take cover. Fire in the hole! The rock gets blasted into the finest sand."
end-strings: item-use[]

: itemid>use 1- item-use[] ;

start-strings
  -" You try to open the door, but it won't budge. The rusty lock looks too difficult for you to pick."
  -" You approach the door, but a rock troll is blocking the way. Better not wake it up..."
  -" The path before you is pitch black. You can barely see your own hand. Maybe I should come back here later."
  -" The flimsy door shakes on its hinges and you can nearly see through the holes and scratches on it. The lock seems broken, yet despite its flimsiness, the door is stuck."
  -" Sudden squeaking and screeching brings a frown to your features. For such a small being, the demonic mouse in front of the door sure does a good job at freaking you out. Maybe I'll come back tomorrow!"
  -" A dragon, thrice your size, is blocking your path. Its breathing is warm and heavy, and your knees shake as you fight your urge to run away."
  -" The path is blocked by a door crafted from pure gold. In the middle of it two gemstones sparkle elegantly. A space for a third one stands out in its emptiness and the door won't budge."
  -" A heavy boulder blocks the doorway and despite your desperate attempts to push it aside, the boulder wins the battle."
end-strings: item-need[]

: itemid>need 1- item-need[] ;
