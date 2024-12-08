namespace construction_game;

public class ConstructionGame
{
    private int[,] field;
    private int maxHeight;
    private int minHeight;

    public ConstructionGame(int length, int width) 
    {
        field = new int[length, width];
        maxHeight = 0;
        minHeight = 0;
    }

    public void AddCubes(bool[,] cubes) 
    {
        minHeight = maxHeight;
        for (int i = 0; i < field.GetLength(0); i++) {
            for (int j = 0; j < field.GetLength(1); j++) {
                if (cubes[i, j]) {
                    field[i, j]++;
                    maxHeight = Math.Max(maxHeight, field[i, j]);
                }
                minHeight = Math.Min(minHeight, field[i, j]);
            }
        }
    }

    public int GetHeight() 
    {
        return maxHeight - minHeight;
    }
}

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        ConstructionGame game = new ConstructionGame(2, 2);

        game.AddCubes(new bool[,] 
        {
            { true, true },
            { false, false } 
        });
        game.AddCubes(new bool[,] 
        {
            { true, true },
            { false, true } 
        });
        Xunit.Assert.Equal(2, game.GetHeight());
        

        game.AddCubes(new bool[,] 
        {
            { false, false },
            { true, true } 
        });
        Xunit.Assert.Equal(1, game.GetHeight());
    }
}
