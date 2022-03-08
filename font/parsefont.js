const fs = require('fs')
const PNG = require('pngjs').PNG

const header = `require tileset-mono.fs

\\ Textmachine Handwriting by Ben Jones (@Polyducks)
ROM CREATE tmh-font-data

`
const footer = `

: install-font ( -- )
  tmh-font-data 256 install-tileset ;

RAM
`

fs.createReadStream('textmachine_handwriting.png')
  .pipe(new PNG())
  .on('parsed', function () {
    let data = ''
    for (let c = 0; c < Math.floor(this.width / 8); c++) {
      data += '\n'
      for (let y = 0; y < 8; y++) {
        data += '\nl: '
        for (let x = 0; x < 8; x++) {
          const idx = (this.width * y + x + c * 8) << 2
          const col = this.data[idx] + this.data[idx + 1] + this.data[idx + 2]
          if (col === 0) data += 'X'
          else if (col === 765) data += '.'
          else console.log('ERROR', c, col)
        }
      }
    }
    fs.writeFileSync('textmachine.fs', `${header}${data}${footer}`)
  })
