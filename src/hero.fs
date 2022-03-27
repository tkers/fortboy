ROM
create vowels
emit-bytes( 'a 'e 'i 'o 'u )

create consonants
emit-bytes( 'b 'd 'f 'g 'k 'l 'm 'n 'p 'r 's 't 'v 'z )
\ only for first consonant
emit-bytes( 'c 'h 'j 'q 'w 'x 'y )
RAM

: random-vowel 5 random vowels + c@ ;
: random-consonant 21 random consonants + c@ ;
: random-consonant-snd 14 random consonants + c@ ;
: to-upper 32 - ;

create fname 6 chars allot
: first-name
  0 fname !
  random-consonant to-upper fname cappend
  random-vowel              fname cappend
  random-consonant-snd dup  fname cappend fname cappend
  random-vowel              fname cappend
  fname count ;

ROM

start-strings
  -" Dragon"
  -" Truth"
  -" Light"
  -" Oath"
  -" Shield"
  -" Sword"
  -" Fire"
  -" Cloud"
  -" Death"
  -" Grave"
  -" Fear"
  -" Fury"
end-strings: last-name-1[]

: last-name-1 12 random last-name-1[] ;

start-strings
  -" bringer"
  -" smasher"
  -" breaker"
  -" seer"
  -" wielder"
  -" breather"
  -" speaker"
  -" teller"
  -" whisperer"
  -" gazer"
end-strings: last-name-2[]

: last-name-2 10 random last-name-2[] ;

start-strings
  -" shepard"
  -" blacksmith"
  -" alchemist"
  -" druid"
  -" soothsayer"
  -" herbologist"
  -" goldminer"
  -" sharpshooter"
  -" warlock"
  -" cleric"
  -" paladin"
  -" ranger"
end-strings: profession[]

: profession 12 random profession[] ;
