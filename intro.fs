require ./hero.fs

: state-of-being
  6 random case
    0 of s" bounding headache" endof
    1 of s" deep frown" endof
    2 of s" soft gasp" endof
    3 of s" big jawn" endof
    4 of s" small shiver" endof
    5 of s" flinch" endof
  endcase ;

: atmosphere
  7 random case
    0 of s" sunny and bright" endof
    1 of s" warm and cozy" endof
    2 of s" small and windowless" endof
    3 of s" beautiful, richly-furnished" endof
    4 of s" bright and colorful" endof
    5 of s" sinister and cobwebbed" endof
    6 of s" dusty and bleak" endof
endcase ;

: state-of-mind
  6 random case
    0 of s" eye lids feel heavy" endof
    1 of s" vision is blurred" endof
    2 of s" thoughts are clouded" endof
    3 of s" head feels empty" endof
    4 of s" stomach rumbles" endof
    5 of s" head is spinning" endof
  endcase ;

: intro
  s" You wake up with a " pad place
  state-of-being pad append
  s"  from a " pad append
  atmosphere pad append
  s"  room. Your " pad append
  state-of-mind pad append
  s"  and the only thing you can remember is your name, " pad append
  first-name pad append
  bl pad cappend
  last-name-1 pad append
  last-name-2 pad append
  [char] . pad cappend

  page pad count .wrapped
  key drop

  s" Yet for a reason you can't explain, none of this makes you feel scared. For ahead of you an adventure awaits, and aspiring adventurers (or professional " pad place
  profession pad append
  s" ) have no time to waste!" pad append

  page pad count .wrapped
  key drop ;
