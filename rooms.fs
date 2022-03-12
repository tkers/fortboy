\ bag to draw random room ids from
10 bag: room-bag

: roomid>name
  case
     0 of s" Cellar"         endof
     1 of s" Icey Room"      endof
     2 of s" Library"        endof
     3 of s" Observatory"    endof
     4 of s" Treasury"       endof
     5 of s" Dimly Lit Room" endof
     6 of s" Bat Cave"       endof
     7 of s" Boiler Room"    endof
     8 of s" Art Gallery"    endof
     9 of s" Catacombs"      endof
  endcase ;

: roomid>desc
  case
     0 of s" The sound of bubbling reaches your ears and you see a cauldron in the middle of the room, surrounded by herbs and bones of creatures you've never seen before."  endof
     1 of s" A shiver runs along your spine as the cold wind sweeps through the room covered in ice and snow. You nearly slip on the surface, but manage to regain your balance."   endof
     2 of s" Piles of books after piles of books cover every last corner of the library. If only you had the time to sit in the armchair and dive into the adventures hidden between the dusty covers." endof
     3 of s" The sun, the moon and every last planet of our solar system greet you, as the view to the observatory opens before your eyes. Even the ceiling sparkles with thousands of stars." endof
     4 of s" You squint your eyes, as you get blinded by the sudden brightness of the golden walls around you, adorned by thousands of gemstones."   endof
     5 of s" The room before you is dark as the night, the only light source in it a chandelier in which a single candle flickers faintly. Something about this feels sinister." endof
     6 of s" Soft squeaks and the rustling of something leathery reaches your ears. Yet the room seems empty... Apart from the hundred bats sleeping right above your head, that is. Please be fruit bats, please be fruit bats..." endof
     7 of s" A loud roar and a puff stops you in your tracks. Is it a zombie? A demon? No, just the boiler room of the castle. Phew."   endof
     8 of s" A room full of life-sized portraits piques your curiosity. The detail in each painting is incredible and you feel tempted to reach forward to feel the dried paint beneath your fingers."  endof
     9 of s" Something crunches beneath your feet and you stagger. The floor is uneven-the pile of bones covering every inch not offering a stable ground to walk on."   endof
    \ 10 of s" Music dances across the room, its sweet melody reminding you of something that you can’t quite place your finger on." endof
    \ 11 of s" Something colorful flutters past your eye and you follow the sight. A soft smile rises on your lips as you see the dozens of butteflies flying around the room, otherwise covered in flowers unlike any you have ever seen."  endof
    \ 12 of s" The feeling of someone watching you sends goosebumps to run along your skin. It doesn’t take long for you to spot the pair of eyes: the doll in the middle of the room following each of your steps. No thank you!" endof
  endcase ;
