require ./hero.fs

ROM

start-strings
  -" January"
  -" February"
  -" March"
  -" April"
  -" May"
  -" June"
  -" July"
  -" August"
  -" September"
  -" October"
  -" November"
  -" December"
end-strings: month[]

: month 12 random month[] ;

start-strings
  -" bounding headache"
  -" deep frown"
  -" soft gasp"
  -" big yawn"
  -" small shiver"
  -" flinch"
end-strings: state-of-being[]

: state-of-being 6 random state-of-being[] ;

start-strings
  -" large and frightening"
  -" vast and daunting"
  -" dark and petrifying"
  -" gloomy and menacing"
  -" ill-lit and cold"
  -" sinister and cobwebbed"
  -" dusty and bleak"
end-strings: atmosphere[]

: atmosphere 7 random atmosphere[] ;

start-strings
  -" eye lids feel heavy"
  -" vision is blurred"
  -" thoughts are clouded"
  -" head feels empty"
  -" stomach rumbles"
  -" head is spinning"
end-strings: state-of-mind[]

: state-of-mind 6 random state-of-mind[] ;

: show-intro
  page
  \ date header
  0 pad !
  30 random 1+ pad #append
  bl pad cappend
  month pad append
  bl pad cappend
  667 random 1203 + pad #append
  pad count center type cr
  =====

  s" You wake up with a " pad place
  state-of-being pad append
  s"  in a " pad append
  atmosphere pad append
  s"  fortress. Your " pad append
  state-of-mind pad append
  s" , and the only thing you can remember is your name: " pad append
  first-name pad append
  bl pad cappend
  last-name-1 pad append
  last-name-2 pad append
  [char] . pad cappend
  bl pad cappend

  pad count .wrapped
  key drop partial-page

  s" Being a simple " pad place
  profession pad append
  s" , you have no reason to be in this strange place. Better take a look around and unravel this mystery. Be brave, unlikely hero. Adventure awaits!" pad append
  pad count .wrapped
  key drop ;
