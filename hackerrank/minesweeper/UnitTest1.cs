namespace minesweeper;

public class UnitTest1
{
    // Task:
    // You are given a Minesweeper board of size MxN, represented as a 2D array where:
    // 0 represents an empty tile.
    // 1 represents a tile with a mine.
    // Write a function that takes the board and a coordinate (x, y). Return:
    // "safe" if the tile is empty (0).
    // "mine" if the tile has a mine (1).
    // "invalid" if the coordinate is outside the board.
    // Additionally, if the tile is "safe", return the count of adjacent mines (tiles with 1 in the 8 surrounding cells).

    [Fact]
    public void Test1()
    {
        int[,] board = {
            { 0, 0, 0, 0},
            { 0, 1, 0, 0},
            { 0, 0, 0, 0}
        };
        Xunit.Assert.Equal("safe", GetTileInfo(board, 0, 0).Status);
        Xunit.Assert.Equal(1, GetTileInfo(board, 0, 0).AdajcentMinesCount);
        Xunit.Assert.Equal("mine", GetTileInfo(board, 1, 1).Status);
        Xunit.Assert.Equal("invalid", GetTileInfo(board, 4, 4).Status);
    }

    private TileInfo GetTileInfo(int[,] board, int x, int y)
    {
        if (x > board.GetLength(0) || y > board.GetLength(1))
        {
            return new TileInfo { Status = "invalid", AdajcentMinesCount = -1 };
        }

        if (board[y, x] == 1)
        {
            return new TileInfo { Status = "mine", AdajcentMinesCount = -1 };
        }

        var adajcentMinesCount = 0;
        if (x - 1 > 0)
        {
            if (y - 1 > 0 && board[y - 1, x - 1] == 1)
            {
                adajcentMinesCount++;
            }
            if (board[y, x - 1] == 1)
            {
                adajcentMinesCount++;
            }
            if (y + 1 < board.GetLength(1) && board[y + 1, x - 1] == 1)
            {
                adajcentMinesCount++;
            }
        }

        if (y - 1 > 0 && board[y - 1, x] == 1)
        {
            adajcentMinesCount++;
        }
        if (y + 1 < board.GetLength(1) && board[y + 1, x] == 1)
        {
            adajcentMinesCount++;
        }

        if (x + 1 < board.GetLength(0))
        {
            if (y - 1 > 0 && board[y - 1, x + 1] == 1)
            {
                adajcentMinesCount++;
            }
            if (board[y, x + 1] == 1)
            {
                adajcentMinesCount++;
            }
            if (y + 1 < board.GetLength(1) && board[y + 1, x + 1] == 1)
            {
                adajcentMinesCount++;
            }
        }

        return new TileInfo { Status = "safe", AdajcentMinesCount = adajcentMinesCount };
    }
}

public class TileInfo
{
    public string Status { get; set; }
    public int AdajcentMinesCount { get; set; }
}
