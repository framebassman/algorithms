const fs = require('fs')
let fileContent = fs.readFileSync("input.txt", "utf8");

const [first, second] = fileContent.toString().split('\n');
if (first.length !== second.length) {
  console.log('0');
  return;
}

const map1 = converToMap(first);
const map2 = converToMap(second);

if (map1.size !== map2.size) {
  console.log('0');
  return;
}

for (let [key, value] of map1) {
  if (!map2.has(key) || value !== map2.get(key)) {
    console.log('0');
    return;
  }
}

console.log('1');
return;

function converToMap(origin) {
  const map = new Map();
  for (var i = 0; i < origin.length; i++) {
    if (map.has(origin[i])) {
      map.set(origin[i], map.get(origin[i] + 1));
    }
    map.set(origin[i], 1);
  }
  return map;
}

