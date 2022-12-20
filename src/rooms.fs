RAM

\ bag to draw random room ids from
24 bag: room-bag
: fill-room-bag 24 room-bag fill-bag ;

ROM

start-strings
  -" Cellar"
  -" Icey Room"
  -" Library"
  -" Observatory"
  -" Treasury"
  -" Dimly Lit Room"
  -" Tower"
  -" Boiler Room"
  -" Art Gallery"
  -" Catacombs"
  -" Attic"
  -" Corridor"
  -" Tunnel"
  -" Hall"
  -" Ballroom"
  -" Dining Room"
  -" Dressing Room"
  -" Chapel"
  -" Pantry"
  -" Lavatory"
  -" Bed Chambers"
  -" Dungeon"
  -" Courtyard"
  -" Nursery"
end-strings: roomid>name

start-strings
  -" The sound of bubbling reaches your ears. You see a cauldron in the middle of the room, surrounded by herbs and bones of creatures you've never seen before."
  -" A shiver runs along your spine as the cold wind sweeps through a room completely covered in ice. You nearly slip on the surface, but manage to regain your balance."
  -" Piles and piles of books cover every last corner of the library. If only you had time to sit in the armchair and dive into the adventures hidden in the dusty pages..."
  -" The sun, the moon, and every last planet of our solar system greet you as the observatory opens before your eyes. Even the ceiling sparkles with a thousand stars."
  -" You squint your eyes as you get blinded by the sudden brightness of the golden walls around you, adorned by thousands of gemstones."
  -" The room before you is dark as the night, the only light source in it a chandelier in which a single candle flickers faintly. Something about this feels sinister."
  -" Soft rustling of something leathery reaches your ears, and you notice a hundred bats hanging right above your head. Please be fruit bats, please be fruit bats..."
  -" A loud roar and a puff stop you in your tracks. Is it a ghoul? Is it a demon? No, just the boiler room of the castle. Phew."
  -" A room full of life-sized portraits piques your curiosity, each painting incredible in its detail. Wait... Did those eyes just move?"
  -" You stagger as something crunches beneath your feet. The floor is uneven: piles of bones cover every inch, not offering a stable ground to walk on."
  -" A small climb later, you find yourself in a dusty attic: the home of badly taxidermied animals with their bead eyes and matted fur."
  -" A long corridor stretches before your eyes, its narrow hallway dark and gloomy. The torches on each side of it offer little light."
  -" The tunnel's ceiling is low, and you can barely stand up straight. Please stay in your webs, spiders! I'm not a fly."
  -" There is not much to see in this small hall: only dried flower arrangements placed on small tables."
  -" This magnificent hall was surely once used to host the most extravagant balls, and you can nearly hear the music from the past."
  -" A table set for a hundred guests sits empty in the middle of the room. The food remains untouched, and the smell of rot makes your stomach churn. I'll stick with crackers for now."
  -" Pearl necklaces, golden crowns, and silk dresses lay abandoned around the room, and you wonder who had once lived here."
  -" Rows after rows of wooden benches sit facing the altar, on which softly flickering candles illuminate a wooden cross."
  -" Bags of wheat and barrels of ale cover every last inch of the pantry. You hear something squeak out of sight."
  -" You discover a hole in the wooden floor. Just as you are about to look down to see where it leads, the smell hits you. Blech!"
  -" A king-sized bed stands in the middle of the room, the satin sheets inviting in their soft sheen. If it wasn't for the skeleton resting in their midst, that is."
  -" Clattering chains hang from the ceiling, and the floor beneath feels sticky with something red. People should be more careful when carrying jars of jam."
  -" Deep green vines climb along an alluring sculpture, its surface tainted grey from the years spent outside. All around her, roses of red and pink bloom in their full glory."
  -" Sudden goosebumps run along your skin, and you quickly uncover the cause: the doll in the middle of the room is keeping an eye on each of your steps. No, thank you!"
end-strings: roomid>desc
