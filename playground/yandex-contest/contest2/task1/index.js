const fs = require('fs')
let fileContent = fs.readFileSync("input.json", "utf8");

const map = JSON.parse(fileContent);
const res = [];

for (let i = 0; i < map.participants.length; i++) {
  const temp = [];
  temp.push(map.sports[i]);
  temp.push(map.participants[map.participants.length - 1 - i]);
  res.push(temp);
}

// console.log(JSON.stringify(res));
fs.writeFileSync("output.json", JSON.stringify(res));

process.exit();
