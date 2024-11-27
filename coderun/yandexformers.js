const findSolution = (N, staff, K) => {
  let firstIndex = 0;
  let secondIndex = N - 1;
  let copyStaff = staff;
  let window = copyStaff.slice(firstIndex, secondIndex + 1);
  while (secondIndex < copyStaff.length) {
    window = copyStaff.slice(firstIndex, secondIndex + 1);
    window.sort((a, b) => b - a);
    
    copyStaff = copyStaff.slice(0, firstIndex)
      .concat(window)
      .concat(copyStaff.slice(secondIndex + window.length - 1, copyStaff.length));
    firstIndex++;
    secondIndex++;
  }
}

const result = findSolution(2, [10, 1, 23, 0, 1], 2)
