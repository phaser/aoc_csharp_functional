
namespace adventofcode.adventofcode.com._2015;

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
        if (AreCoordinatesOk(rx, ry))
            return this;
        _board[GetIndex(x, y)] = value;
        return this;
    }
    
    public int Get(int x, int y)
    {
        GetNormalizedCoords(x, y, out var rx, out var ry);
        if (AreCoordinatesOk(rx, ry))
            throw new ArgumentException($"{x}, {y} are not good coordinates.");
        return _board[GetIndex(x, y)];
    }

    private int GetIndex(int x, int y) => (x + 1) * _realHeight + y + 1;
    
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

    private bool AreCoordinatesOk(int _rx, int _ry) => !(_rx > 0 && _rx < _realWidth && _ry > 0 && _ry < _realHeight);
}

public record BoardCache
{
    private readonly List<Board> _boards = new();
    private readonly int _width;
    private readonly int _height;

    public BoardCache(int width, int height)
    {
        _width = width;
        _height = height;
    }
    
    public Board Get()
    {
        if (_boards.Count == 0)
            return new Board(_width, _height);
        var board = _boards.Last();
        _boards.RemoveAt(_boards.Count - 1);
        return board;
    }

    public BoardCache Return(Board board)
    {
        _boards.Add(board.Reset());
        return this;
    }
}

public static class Solution2015day0018
{
    public static int SolvePart1(string input, int steps)
        => input
            .Parse()
            .And(board => SolvePart1Internal(board, steps, new BoardCache(board.Width, board.Height)));

    private static Board Parse(this string input)
        => input
            .Split('\n')
            .Select(l => l.Trim())
            .ToList()
            .And(lines => new Board(lines.Count, lines.Count)
                .And(board => lines.Select((l, x) => l.Select((c, y) => board.Set(x, y, c == '#' ? 1 : 0)).ToList())
                    .ToList()
                    .And(_ => board.Modify(b => b.Print()))));

    private static int SolvePart1Internal(Board currentBoard, int steps, BoardCache cache)
        => Enumerable.Range(0, steps)
            .Select(step => ComputeNewStep(currentBoard, cache, GetNewCellValue))
            .Aggregate((a, b) => b)
            .And(_ => CountOnLights(currentBoard));

    private static int CountOnLights(Board currentBoard) 
        => (currentBoard.Width, currentBoard.Height).CrossRange().Count(e => currentBoard.Get(e.x, e.y) != 0);

    private static int ComputeNewStep(Board currentBoard, BoardCache cache, Func<Board, (int x, int y), int> getNewCellValue) 
        => cache.Get()
            .And(newBoard => (currentBoard.Width, currentBoard.Height)
                .CrossRange()
                .Select(v => newBoard.Set(v.x, v.y, getNewCellValue(currentBoard, v)))
                .Aggregate((a, b) => b)
                .Modify(latestBoard =>
                {
                    currentBoard.Copy(latestBoard);
                    cache.Return(latestBoard);
                }))
            .And(_ => 0);

    private static int GetNewCellValue(Board currentBoard, (int x, int y) v) 
        => currentBoard.Get(v.x, v.y) != 0 ? GetLightOnValue(currentBoard, v) : GetLightOffValue(currentBoard, v);

    private static int GetLightOffValue(Board currentBoard, (int x, int y) v)
        => currentBoard.GetNeighbors(v.x, v.y).Count(n => n != 0) == 3 ? 1 : 0;

    private static int GetLightOnValue(Board currentBoard, (int x, int y) v)
        => currentBoard.GetNeighbors(v.x, v.y).Count(n => n != 0) is 2 or 3 ? 1 : 0;
}
