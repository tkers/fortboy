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
  -" A black cat wakes up in the corner."
  -" Maah! A bratty goat startles you."
  -" A gemstone catches your attention."
  -" A box of dynamite awaits its finder."
end-strings: item-look[]

: itemid>look 1- item-look[] ;

start-strings
  -" You pick up the key, almost regretting it, as you see the traces of rust on your hands. It better be helpful."
  -" You take the sword. Oof! It's a lot heavier than you thought."
  -" You wrap your fingers around the torch, relieved to find it still dry and ready to be lit."
  -" You pick up the heavy crowbar. Fighting crime one crowbar at a time!"
  -" Despite it never having worked in the past, you crouch down and 'pspsps' confidently. To your surprise, the cat happily runs towards you, purring loudly."
  -" You pull out a half-eaten apple from your pocket and offer it to the goat. With a hungry 'maaah', the animal decides to follow you."
  -" You pick up the gemstone and find yourself hoping you'd get to take it back home. With its purple sparkle, it'd surely look beautiful on your bookshelf."
  -" You pick up the box of dynamite. Better stay away from torches for now."
end-strings: item-take[]

: itemid>take 1- item-take[] ;

start-strings
  -" The rusty key clatters as you drop it again."
  -" You carefully place the sword against the wall. You might still need it later."
  -" You place the torch next to the wall. You prefer the darkness, anyway."
  -" You leave the crowbar behind. Until we meet again, trusty tool!"
  -" The cat suddenly sprints away. I guess it was time for dinner?"
  -" The goat loses interest in you and wanders off."
  -" With a heavy heart, you part ways with the sparkling gemstone."
  -" You gently place down the dynamite. Easy does it..."
end-strings: item-drop[]

: itemid>drop 1- item-drop[] ;

start-strings
  -" You slide the rusty key into the lock. With a click and a creak, the door opens."
  -" You brandish your sword in front of you, ready to face the creature. It snarls and lunges towards you, but you deftly dodge its claws and teeth, strike back, and trap it into a corner. With one final blow, the creature falls to the ground, vanquished by your sword."
  -" With your torch lit up, you're ready to conquer the darkness. Watch out, lurking monsters! I'm not afraid anymore."
  -" The flimsy door is no match for your crowbar, and the wood shatters under its force. Watch and learn, thieves!"
  -" You gently place the cat in front of the demonic mouse. The rodent is fierce but is ultimately defeated in battle. The way is cleared, and your fuzzy friend continues his nap, purring contently."
  -" The dragon wakes up with a huff, and you toss the goat at the yawning creature. Now that the green serpent has breakfast served in bed, you can slip past. Sorry, goat! Its offended 'maaah' is the last you'll ever hear of it."
  -" You take a curious step closer to the door and pull out the heavy gemstone. The sparkling gem slides seamlessly into the spot reserved for it, its purple sheen completing the elegant look. The door silently slides open."
  -" You position the sticks of dynamite, light the fuse and take cover. Fire in the hole! The boulder is blasted into dust."
end-strings: item-use[]

: itemid>use 1- item-use[] ;

start-strings
  -" You try to open the door, but it won't budge. The rusty lock looks too difficult for you to pick."
  -" The air grows colder, and you come face to face with a monstrous creature. Its glowing eyes seem to pierce the darkness, and its sharp teeth glint wickedly. You hear its low growl as it prepares to attack, and you know that this will be a fight to the death."
  -" The path before you is pitch black, and you can't see what lies ahead... Maybe you should come back here later."
  -" The flimsy door shakes on its hinges, and you can almost see through its holes and scratches. The lock seems broken, yet despite its flimsiness, the door is stuck."
  -" Loud squealing and screeching alerts you to the creature ahead. For such a tiny being, the demonic mouse in front of the door sure seems determined to keep you out."
  -" A dragon, thrice your size, is blocking your path. Its breathing is warm and heavy, and your knees shake as you fight the urge to run away."
  -" The path is blocked by a door crafted from pure gold. In the middle of it, two elegant gemstones sparkle. A space for a third one stands out in its emptiness, and the door won't budge."
  -" A heavy boulder blocks the pathway, and despite your desperate attempts to push it aside, the rock wins the battle."
end-strings: item-need[]

: itemid>need 1- item-need[] ;
