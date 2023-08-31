namespace adventofcode;

public record Board
{
    public int Width { get; init; }
    public int Height { get; init; }
    
    private readonly int[] _board;
    private int _realWidth => Width + 2;
    private int _realHeight => Height + 2;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        _board = new int[_realWidth * _realHeight];
    }

    public Board Set(int x, int y, int value)
    {
        GetNormalizedCoords(x, y, out var rx, out var ry);
        if (CoordinatesAreNotOk(rx, ry))
            return this;
        _board[GetIndex(x, y)] = value;
        return this;
    }
    
    public int Get(int x, int y)
    {
        GetNormalizedCoords(x, y, out var rx, out var ry);
        if (CoordinatesAreNotOk(rx, ry))
            throw new ArgumentException($"{x}, {y} are not good coordinates.");
        return _board[GetIndex(x, y)];
    }

    private int GetIndex(int x, int y) => (x + 1) * _realWidth + y + 1;
    
    public IEnumerable<int> GetNeighbors(int x, int y)
    {
        yield return _board[GetIndex(x - 1, y - 1)];
        yield return _board[GetIndex(x, y - 1)];
        yield return _board[GetIndex(x + 1, y - 1)];
        yield return _board[GetIndex(x + 1, y)];
        yield return _board[GetIndex(x + 1, y + 1)];
        yield return _board[GetIndex(x, y + 1)];
        yield return _board[GetIndex(x - 1, y + 1)];
        yield return _board[GetIndex(x - 1, y)];
    }

    public Board Print()
    {
        Console.WriteLine();
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                Console.Write(_board[GetIndex(i, j)] == 0 ? "." : "#");
            }
            Console.WriteLine();
        }

        return this;
    }

    public Board Reset()
    {
        Array.Fill(_board, 0, 0, _board.Length);
        return this;
    }

    public Board Copy(Board board)
    {
        Reset();
        for (var i = 0; i < Width; i++)
        for (var j = 0; j < Height; j++)
            _board[GetIndex(i, j)] = board.Get(i, j);
        return this;
    }
    
    private static void GetNormalizedCoords(int x, int y, out int rx, out int ry)
    {
        rx = x + 1;
        ry = y + 1;
    }

    private bool CoordinatesAreNotOk(int _rx, int _ry) => !(_rx > 0 && _rx < _realWidth && _ry > 0 && _ry < _realHeight);
}