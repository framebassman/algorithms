'use strict';

process.stdin.resume();
process.stdin.setEncoding('utf-8');

let inputString = '';
let currentLine = 0;

process.stdin.on('data', inputStdin => {
    inputString += inputStdin;
});

process.stdin.on('end', _ => {
    inputString = inputString.trim().split('\n').map(string => {
        return string.trim();
    });

    main();
});

function readline() {
    return inputString[currentLine++];
}


function main() {
    const x = readline();
    var num = x.split(" ").map(x => parseInt(x));

    sum(num);
}
function sum(num) {
  const i = num[0] + num[1];
  console.log(i);
}
