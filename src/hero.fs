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

: last-name-1
  12 random case
    0 of s" Dragon" endof
    1 of s" Truth" endof
    2 of s" Light" endof
    3 of s" Oath" endof
    4 of s" Shield" endof
    5 of s" Sword" endof
    6 of s" Fire" endof
    7 of s" Cloud" endof
    8 of s" Death" endof
    9 of s" Grave" endof
   10 of s" Fear" endof
   11 of s" Fury" endof
  endcase ;

: last-name-2
  10 random case
    0 of s" bringer" endof
    1 of s" smasher" endof
    2 of s" breaker" endof
    3 of s" seer" endof
    4 of s" wielder" endof
    5 of s" breather" endof
    6 of s" speaker" endof
    7 of s" teller" endof
    8 of s" whisperer" endof
    9 of s" gazer" endof
  endcase ;

: profession
  12 random case
    0 of s" shepard" endof
    1 of s" blacksmith" endof
    2 of s" alchemist" endof
    3 of s" druid" endof
    4 of s" soothsayer" endof
    5 of s" herbologist" endof
    6 of s" goldminer" endof
    7 of s" sharpshooter" endof
    8 of s" warlock" endof
    9 of s" cleric" endof
   10 of s" paladin" endof
   11 of s" ranger" endof
 endcase ;
