/**
 * @param {{
*  graph: Record<string, string[]>,
*  startVertex: string,
*  endVertex: string,
* }}
* @returns {string[]}
*/

function solution({ graph, startVertex, endVertex }) {
   if (startVertex === endVertex) {
       return [startVertex];
   }

   const whatched = new Set();
   const line = [[startVertex]];

   while (line.length > 0) {
       const path = line.shift();
       const vertex = path[path.length - 1];

       if (!whatched.has(vertex)) {
          whatched.add(vertex);

           for (const neighbor of graph[vertex] || []) {
               const newPath = [...path, neighbor];

               if (neighbor === endVertex) {
                   return newPath;
               }

               line.push(newPath);
           }
       }
   }

   return [];
};

const firstGraff = {
  graph: {
    Александра: ["Борис"],
    Борис: ["Александра", "Светлана"],
    Светлана: ["Борис"],
  },
  startVertex: "Александра",
  endVertex: "Светлана",
};

const secondGraff = {
  graph: {
    Артемий: ["Бронислав", "Дементий"],
    Бронислав: ["Артемий", "Софья", "Дементий"],
    Софья: ["Бронислав"],
    Дементий: ["Артемий","Бронислав"],
    Фаина: ["Гаврила"],
    Гаврила: ["Фаина"],
  },
  startVertex: "Артемий",
  endVertex: "Фаина",
};

console.log(solution(firstGraff));
console.log(solution(secondGraff));
