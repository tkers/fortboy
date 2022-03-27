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
  -" The sound of bubbling reaches your ears and you see a cauldron in the middle of the room, surrounded by herbs and bones of creatures you've never seen before."
  -" A shiver runs along your spine as the cold wind sweeps through a room completely covered in ice. You nearly slip on the surface, but manage to regain your balance."
  -" Piles of books cover every last corner of the library. If only you had the time to sit in the armchair and dive into the adventures hidden between the dusty covers."
  -" The sun, the moon and every last planet of our solar system greet you, as the view to the observatory opens before your eyes. Even the ceiling sparkles with thousands of stars."
  -" You squint your eyes, as you get blinded by the sudden brightness of the golden walls around you, adorned by thousands of gemstones."
  -" The room before you is dark as the night, the only light source in it a chandelier in which a single candle flickers faintly. Something about this feels sinister."
  -" Soft rustling of something leathery reaches your ears. You notice a hundred bats sleeping right above your head. Please be fruit bats, please be fruit bats..."
  -" A loud roar and a puff stops you in your tracks. Is it a zombie? A demon? No, just the boiler room of the castle. Phew."
  -" A room full of life-sized portraits piques your curiosity. The detail in each painting is incredible. You nearly expect to see them move."
  -" Something crunches beneath your feet and you stagger. The floor is uneven-the pile of bones covering every inch not offering a stable ground to walk on."
  -" A small climb later, you find yourself in a dusty attic: the home of badly taxidermied animals with their bead-eyes and matted fur."
  -" A long corridor opens before your eyes, its narrow hallway dark and gloomy. The torches on each side of it offer little light."
  -" The ceiling of the tunnel is low, and you can barely stand to your full length. Please stay in your webs, spiders! I'm not a fly."
  -" It's nothing but a simple hall with dried flower arrangements placed on small tables."
  -" The beautiful hall was surely once used to host the most extravagant balls, and you can nearly hear the whisper of the music from the past."
  -" A table set for a hundred guests sits empty in the middle of the room. The food remains untouched and the smell of its rot... I'll stick with crackers for now."
  -" Pearl necklaces, crowns and lacey dresses lay scattered around the room, and you wonder if once a Queen lived here."
  -" Rows after rows of wooden benches sit facing the altar, on which softly flickering candles illuminate a wooden cross."
  -" Bags of wheat and barrels of ale cover every last inch of the pantry. Somewhere a small mouse squeaks."
  -" The splashing of an overflowing toilet fills the otherwise silent room, and suddenly you wish you had listened to your mother. She always did tell you to become a plumber."
  -" A King sized bed stands in the middle of the room, the satin sheets inviting. If it wasn't for the skeleton resting in the midst of them, that is."
  -" Clattering chains hang from the ceiling, and the floor beneath feels sticky with something red. People should be more careful when carrying jars of jam."
  -" Deep green vines climb along the beautiful statue, its surface tainted grey during the years spent outside. All around it roses of red and pink bloom in their full glory."
  -" Sudden goosebumps run along your skin, and soon you spot the reason: the doll in the middle of the room following each of your steps. No thank you!"
end-strings: roomid>desc
