namespace battleship;

public class UnitTest1
{
    private SolutionBattleship battleship;
    public UnitTest1()
    {
        battleship = new SolutionBattleship();
    }

    [Fact]
    public void Test1()
    {
        Xunit.Assert.Equal("missed", this.battleship.Shoot(0, 0));
        Xunit.Assert.Equal("hit", this.battleship.Shoot(1, 1));
        Xunit.Assert.Equal("hit again", this.battleship.Shoot(1, 1));
        Xunit.Assert.Equal("sink First", this.battleship.Shoot(2, 1));
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public string ShipName { get; set; }
    public bool IsDamaged {get;set;}

    public Point(int X, int Y, string ShipName, bool IsDamaged)
    {
        this.X = X;
        this.Y = Y;
        this.ShipName = ShipName;
        this.IsDamaged = IsDamaged;
    }
    public Point(int X, int Y, string ShipName) : this(X, Y, ShipName, false) {}
    public Point(int X, int Y) : this(X, Y, string.Empty, false) {}
}

public class SolutionBattleship
{
    private List<List<Point>> field;
    private Dictionary<string, List<Point>> ships;

    public SolutionBattleship()
    {
        field =
        [
            new List<Point> {new Point(0, 0), new Point(1, 0), new Point(2, 0)},
            new List<Point> {new Point(0, 1), new Point(1, 1, "First"), new Point(2, 1, "First")},
            new List<Point> {new Point(0, 2), new Point(1, 2, "Second"), new Point(2, 2, "Second")}
        ];
        ships = new Dictionary<string, List<Point>>
        {
            { "First", [new Point(1, 1), new Point(2, 1)] },
            { "Second", [new Point(1, 2), new Point(2, 2)] }
        };
    }

    public string Shoot(int x, int y)
    {
        var fieldPoint = GetPoint(x, y);
        if (fieldPoint.ShipName != string.Empty)
        {
            if (ships[fieldPoint.ShipName] != null && fieldPoint.IsDamaged)
            {
                return "hit again";
            }

            this.HitPoint(fieldPoint);
            ships[fieldPoint.ShipName].RemoveAll(c => c.X == fieldPoint.X && c.Y == fieldPoint.Y);
            
            if (ships[fieldPoint.ShipName].Count == 0)
            {
                return $"sink {fieldPoint.ShipName}";
            }
            else
            {
                return "hit";
            }
        }

        return "missed";
    }

    private Point GetPoint(int x, int y)
    {
        return field[y][x];
    }

    private void HitPoint(Point point)
    {
        field[point.Y][point.X].IsDamaged = true;
    }
}

