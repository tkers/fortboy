RAM

\ bag to draw random item ids from
12 bag: item-bag
: fill-item-bag 12 item-bag fill-bag ;

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
  -" A pair of garden shears were left behind."
  -" You spot a bucket of water on the floor."
  -" A piece of parchment hangs on the wall."
  -" You notice an old locket near the door."
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
  -" You take the garden shears with you. Time for some landscaping!"
  -" You lift up the bucket. You're coming with me, water!"
  -" You take a closer look at the piece of parchment. It's covered in strange symbols and arcane writing, and you sense the powerful magic emanating from it. Thinking it could prove helpful later on, you carefully tuck it away in your pocket."
  -" You pick up the locket and open it, revealing an old family portrait inside. Despite its aged and worn state, you can sense that the locket was once very important to someone, and you take it with you."
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
  -" You leave the garden shears behind."
  -" You put the bucket back down."
  -" You carefully take the parchment out of your pocket and leave it behind."
  -" You place the locket back on the floor."
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
  -" Armed with your trusty garden shears, you begin to prune the intertwined vines. Snip, snip! A path starts to clear up..."
  -" With the water bucket by your side, you manage to tame the flames and clear up the smouldering debris."
  -" You pull out the parchment and study the spell written on it. The writing is strange and difficult to read, but you give it a go. For a moment, nothing seems to happen... Then the mist slowly starts to disperse, creating a safe passage. Onwards!"
  -" You return the locket to the ghostly figure. Her face softens and she looks at you with gratitude. With a quiet nod, she steps aside, allowing you to pass. You can't help but feel a sense of relief as the figure vanishes behind you."
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
  -" You find the path ahead utterly overgrown with vines. You can't get through and decide to turn back."
  -" You walk onwards but discover a pile of burning rubble obstructing your passage. Is someone trying to keep you here?"
  -" You are suddenly confronted by a thick mist seeping from the walls, filling the area with an otherworldly, cold presence. You watch in horror as the mist envelops a nearby rat, consuming it in an instant. Better find a way around!"
  -" A ghostly figure appears out of thin air, blocking your path. She glows with a faint light and looks around frantically, as if searching for something... Noticing the melancholy and anger in her eyes, you do not feel brave enough to pass her."
end-strings: item-need[]

: itemid>need 1- item-need[] ;
