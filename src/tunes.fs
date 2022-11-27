require music.fs
require time.fs

: snd-block
  B4 note 180 ms
  A4 note 180 ms
  C4 note ;

: snd-hooray
  E5  note 120 ms
  A5  note 120 ms
  G5  note 120 ms
  A5  note 60 ms
  E6  note ;

: snd-take
  A5 note 80 ms
  E6 note ;

: snd-drop
  E6 note 80 ms
  A5 note ;

: snd-unlock
  C4 note 60 ms
  A4 note 60 ms
  G5 note ;

: snd-confirm
  $53 rNR10 c!
  $91 rNR11 c!
  $83 rNR12 c!
  $aa rNR13 c!
  $85 rNR14 c! ;

: snd-thud
  $71 rNR10 c!
  $8f rNR11 c!
  $83 rNR12 c!
  $20 rNR13 c!
  $85 rNR14 c! ;

: snd-launch
  $75 rNR10 c!
  $cc rNR11 c!
  $38 rNR12 c!
  $e8 rNR13 c!
  $83 rNR14 c! ;
