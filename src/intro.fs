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

start-strings
  -" remote"
  -" distant"
  -" dark"
  -" strange"
  -" dreadful"
  -" spooky"
end-strings: place-of-being[]

: place-of-being 6 random place-of-being[] ;

start-strings
  -" trusty goldfish"
  -" dying succulents"
  -" various cats"
  -" cosy bookshelves"
  -" worrying betrothed"
  -" butterfly collection"
end-strings: back-home[]

: back-home 6 random back-home[] ;

: show-intro
  to-white page

  \ date header
  0 pad !
  30 random 1+ pad #append
  bl pad cappend
  month pad append
  bl pad cappend
  512 random 1102 + pad #append
  pad count center type cr
  =====

  s" You wake up with a " pad place
  state-of-being pad append
  s"  in a " pad append
  atmosphere pad append
  s"  fortress. What is this place? And how did you end up here?" pad append
  pad count .wrapped cr

  s" Your " pad place
  state-of-mind pad append
  s" , and the only thing you can remember is your name: " pad append
  pad count .wrapped

  cursor-y @ 15 < if cr then

  first-name center type cr
  last-name-1 pad place
  last-name-2 pad append
  pad count center type cr

  from-white
  wait-a/b to-white partial-page

  s" Being a simple " pad place
  profession pad append
  s" , you have no reason to be in a " pad append
  place-of-being pad append
  s"  place like this. You need to return home to tend to your " pad append
  back-home pad append s"  at once! Better take a look around and unravel this mystery." pad append
  pad count .wrapped cr
  s" Be brave, unlikely hero. Adventure awaits!" .wrapped
  from-white wait-a/b to-white ;
