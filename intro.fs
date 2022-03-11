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

: name
  16 random case
  0 of s" Merry" endof
  1 of s" Perry" endof
  2 of s" Ally" endof
  3 of s" Sally" endof
  4 of s" Garry" endof
  5 of s" Gilly" endof
  6 of s" Filly" endof
  7 of s" Elly" endof
  8 of s" Polly" endof
  9 of s" Addy" endof
  10 of s" Dally" endof
  11 of s" Helly" endof
  12 of s" Jolly" endof
  13 of s" Vally" endof
  14 of s" Villy" endof
  15 of s" Dilly" endof
  endcase ;

: profession
  14 random case
    0 of s" common thieves" endof
    1 of s" reindeer walkers" endof
    2 of s" ghost hunters" endof
    3 of s" rollercoaster testers" endof
    4 of s" chocolate tasters" endof
    5 of s" space travel agents" endof
    6 of s" cat behavior consultants" endof
    7 of s" namers of clouds" endof
    8 of s" ranch dressing experts" endof
    9 of s" space lawyers" endof
    10 of s" penguinologists" endof
    11 of s" parkour specialists" endof
    12 of s" bread scientists" endof
    13 of s" gold searcher" endof
   endcase ;

: .intro
  s" You wake up with a " pad place
  state-of-being pad append
  s"  from a " pad append
  atmosphere pad append
  s"  room. Your " pad append
  state-of-mind pad append
  s"  and the only thing you can remember is your name, " pad append
  name pad append
  [char] . pad cappend

  pad count .wrapped
  key drop page

  s" Yet for a reason you can't explain, none of this makes you feel scared. For ahead of you an adventure awaits, and aspiring adventurers (or professional " pad place
  profession pad append
  s" ) have no time to waste!" pad append

  pad count .wrapped
  key drop page ;
