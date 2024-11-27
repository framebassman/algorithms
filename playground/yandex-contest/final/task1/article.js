/**
 * @param {{
*  graph: Record<string, string[]>,
*  startVertex: string,
*  endVertex: string,
* }}
* @returns {string[]}
*/
function solution({ graph, startVertex, endVertex }) {
  class Graph {
    constructor() {
      this.vertices = {}; // список смежности графа
    }
    
    addVertex(value) {
      if (!this.vertices[value]) {
        this.vertices[value] = [];
      }
    }
    
    addEdge(vertex1, vertex2) {
      if (!(vertex1 in this.vertices) || !(vertex2 in this.vertices)) {
        throw new Error('В графе нет таких вершин');
      }
  
      if (!this.vertices[vertex1].includes(vertex2)) {
        this.vertices[vertex1].push(vertex2);
      }
      if (!this.vertices[vertex2].includes(vertex1)) {
        this.vertices[vertex2].push(vertex1);
      }
    }

    bfs2(startVertex) {
      let list = this.vertices; 
      let queue = [startVertex];
      let visited = { [startVertex]: 1 }; 
      
      // кратчайшее расстояние от стартовой вершины
      let distance = { [startVertex]: 0 }; 
      // предыдущая вершина в цепочке
      let previous = { [startVertex]: null };
  
      function handleVertex(vertex) {
        let neighboursList = list[vertex];
  
        neighboursList.forEach(neighbour => {
          if (!visited[neighbour]) {
            visited[neighbour] = 1;
            queue.push(neighbour);
            // сохраняем предыдущую вершину
            previous[neighbour] = vertex;
            // сохраняем расстояние 
            distance[neighbour] = distance[vertex] + 1;
          }
        });
      }
  
      // перебираем вершины из очереди, пока она не опустеет
      while(queue.length) {
        let activeVertex = queue.shift();
        handleVertex(activeVertex);
      }
      
      return { distance, previous }
    }

    findShortestPath(startVertex, finishVertex) {
      let result = this.bfs2(startVertex);
  
      if (!(finishVertex in result.previous)) 
          return []
          
      let path = [];
      
      let currentVertex = finishVertex;
      
      while(currentVertex !== startVertex) {
        path.unshift(currentVertex);
        currentVertex = result.previous[currentVertex];
      }
      
      path.unshift(startVertex);
      
      return path;
    }
  }

  const graphModel = new Graph();
  for (let i = 0; i < Object.keys(graph).length; i++) {
    graphModel.addVertex(Object.keys(graph)[i]);
  }
  for (let i = 0; i < Object.keys(graph).length; i++) {
    const vertex = Object.keys(graph)[i];
    for (let j = 0; j < graph[vertex].length; j++) {
      graphModel.addEdge(vertex, graph[vertex][j]);
    }
  }

  return graphModel.findShortestPath(startVertex, endVertex);
}

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
