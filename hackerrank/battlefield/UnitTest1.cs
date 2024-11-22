namespace battlefield;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var ships = new Dictionary<string, List<Tuple<int, int>>>();
        ships.Add("First", new List<Tuple<int, int>>() { new Tuple<int, int>(1, 1), new Tuple<int, int>(2, 1)});
        ships.Add("Second", new List<Tuple<int, int>>() { new Tuple<int, int>(2, 2), new Tuple<int, int>(3, 2)});
        
        var battleship = new SolutionBattleship(ships);
        
        Xunit.Assert.Equal("missed", battleship.Shoot(0, 0));
        Xunit.Assert.Equal("hit", battleship.Shoot(1, 1));
        Xunit.Assert.Equal("hit again", battleship.Shoot(1, 1));
        Xunit.Assert.Equal("sink First", battleship.Shoot(2, 1));
    }

    public class SolutionBattleship
    {
        private Dictionary<string, List<Tuple<int, int>>> _ships;
        private List<List<string>> _battlefield;
        
        public SolutionBattleship(Dictionary<string, List<Tuple<int, int>>> ships)
        {
            _ships = ships;
            _battlefield = new List<List<string>>(){
                new List<string>() { "", "", "", "", "" },
                new List<string>() { "", "First", "First", "", "" },
                new List<string>() { "", "", "Second", "Second", "" },
                new List<string>() { "", "", "", "", "" },
                new List<string>() { "", "", "", "", "" }
            };
        }
        
        public string Shoot(int x, int y)
        {
            // 0 - missed
            // 1 - hit
            //   - sink ship_name
            var currentValue = GetCoordinatesValue(x, y);           
            var shipName = currentValue;
            var splitted = currentValue.Split("-");
            if (splitted.Length == 2)
            {
                return "hit again";
            }

            if (_ships.ContainsKey(shipName) && _ships[shipName].Any(t => t.Item1 == x && t.Item2 == y)) {
                var coordinatesLeft = _ships[shipName].Count;
                var shipCoordinate = _ships[shipName].First(t => t.Item1 == x && t.Item2 == y);
                _ships[shipName].Remove(shipCoordinate);
                SetCoordinatesValue(x, y, $"{shipName}-hit");
                if (coordinatesLeft == 1) {
                    return $"sink {shipName}";
                }

                return "hit";
            }

            return "missed";
        }
        
        private string GetCoordinatesValue(int x, int y)
        {
            return _battlefield[y][x];
        }
        
        private void SetCoordinatesValue(int x, int y, string value)
        {
            _battlefield[y][x] = value;
        }
    }
}